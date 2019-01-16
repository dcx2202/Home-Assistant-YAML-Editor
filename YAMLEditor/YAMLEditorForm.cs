using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using YamlDotNet.RepresentationModel;
using YAMLEditor.Logging;
using YAMLEditor.Patterns;

namespace YAMLEditor
{
    public partial class YAMLEditorForm : Form
    {
        private CommandManager Manager = new CommandManager();

        //use mLogger.Write(string message) to log to the textbox
        private ILogger mLogger = Logger.Instance;
        public IComponent composite;
        public IComponent currentParent;

        public YAMLEditorForm()
        {
            InitializeComponent();
            mLogger.Recorder = new TextBoxRecorder(mainTextBox);
            composite = new Component("root", "root", null);
            currentParent = composite;
        }

        private void OnExit(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void OnOpen(object sender, EventArgs e)
        {
            var dialog = new OpenFileDialog()
            { Filter = @"Yaml files (*.yaml)|*.yaml|All files (*.*)|*.*", DefaultExt = "yaml" };
            if(dialog.ShowDialog() == DialogResult.OK)
            {
                mLogger.WriteLine($"Filename: {dialog.FileName}");
                System.Diagnostics.Trace.WriteLine($"Filename: {dialog.FileName}");
                Directory.SetCurrentDirectory(Path.GetDirectoryName(dialog.FileName) ?? "");

                mainTreeView.Nodes.Clear();
                var root = mainTreeView.Nodes.Add(Path.GetFileName(dialog.FileName));
                root.ImageIndex = root.SelectedImageIndex = 3;
                LoadFile(root, dialog.FileName);
                root.Expand();

                // Create the composite
                PopulateComposite(getDataStructure(dialog.FileName));

                // Update the components' filenames
                setFileNames(dialog.FileName);

                // Print the composite
                PrintComposite(composite, "", true);
            }
        }

        // Prints the composite
        public void PrintComposite(IComponent aRoot, String indent, bool last)
        {
            mLogger.WriteLine(indent + "+- " + aRoot.getName());
            indent += last ? "   " : "|    ";

            var nchildren = aRoot.getChildren().Count;

            for(int i = 0; i < nchildren; i++)
            {
                PrintComposite(aRoot.getChild(i), indent, i == nchildren - 1);
            }
        }

        // Update the components' filenames
        public void setFileNames(string FilePath)
        {
            // List that holds the components (level 1 only)
            Dictionary<string, IComponent> components = new Dictionary<string, IComponent>();

            // Add the components to the list
            foreach(Component node in composite.getChildren())
                components.Add(node.getName(), node);

            // List that holds the names of the files that are to be parsed for the components' names
            List<string> files = new List<string>() { FilePath };

            // Read all the lines of the opened file
            var lines = File.ReadAllLines(FilePath);

            // For each line in the file check if it includes another file
            // If so then add that file to the list of files
            foreach(string line in lines)
            {
                if(line.Contains("!include"))
                {
                    var a = line.Split(' ');
                    foreach(string s in a)
                    {
                        if(s.Contains(".yaml"))
                            files.Add(s);
                    }
                }
            }

            // For each file to be checked, read all of its lines and look for the components' names
            foreach(string file in files)
            {
                // Arranjar os paths. O FilePath (ficheiro que foi aberto) usa absoluto, os outros ficheiros em files so sao os nomes.
                lines = File.ReadAllLines(file);

                foreach(string line in lines)
                {
                    foreach(string component in components.Keys)
                    {
                        // If this component is in this file then set this component and its children's file names correctly
                        if(line.Contains(component + ":"))
                            components[component].setFileName(file);
                    }
                }
            }
        }

        // Populates the composite
        private void PopulateComposite(IDictionary<YamlNode, YamlNode> structure)
        {
            foreach(YamlNode key in structure.Keys)
            {
                // Create a new component for this node
                IComponent comp = new Component(key.ToString(), "", currentParent);

                // Add it to the current parent
                currentParent.add(comp);

                // Get the value
                var node = structure[key] as YamlMappingNode;

                // Holds the next level
                IDictionary<YamlNode, YamlNode> nextLevel;

                // Node is a leaf
                if(node == null)
                    nextLevel = null;

                // Node isn't a leaf
                else
                {
                    currentParent = comp; // update the current parent before we go into the next level down
                    nextLevel = node.Children; // get the next level
                }

                // if we are in a leaf then continue
                if(nextLevel == null)
                    continue;

                // else go one level deeper
                PopulateComposite(nextLevel);
            }

            // Go up one level
            if(currentParent.getParent() != null)
                currentParent = currentParent.getParent();
        }

        private void LoadFile(TreeNode node, string filename)
        {
            var yaml = new YamlStream();
            try
            {
                using(var stream = new StreamReader(filename))
                {
                    yaml.Load(stream);
                }
            }
            catch(Exception exception)
            {
                mLogger.WriteLine(exception.Message);
            }

            if(yaml.Documents.Count == 0) return;
            LoadChildren(node, yaml.Documents[0].RootNode as YamlMappingNode);
        }

        private void LoadChildren(TreeNode root, YamlMappingNode mapping)
        {
            var children = mapping?.Children;
            if(children == null) return;

            foreach(var child in children)
            {
                var key = child.Key as YamlScalarNode;
                System.Diagnostics.Trace.Assert(key != null);

                if(child.Value is YamlScalarNode)
                {
                    var scalar = child.Value as YamlScalarNode;

                    var node = root.Nodes.Add($"{key.Value}: {scalar.Value}");
                    node.Tag = child;
                    node.ImageIndex = node.SelectedImageIndex = GetImageIndex(scalar);

                    if(scalar.Tag == "!include")
                    {
                        LoadFile(node, scalar.Value);
                    }
                }
                else if(child.Value is YamlSequenceNode)
                {
                    var node = root.Nodes.Add(key.Value);
                    node.Tag = child.Value;
                    node.ImageIndex = node.SelectedImageIndex = GetImageIndex(child.Value);

                    LoadChildren(node, child.Value as YamlSequenceNode);
                }
                else if(child.Value is YamlMappingNode)
                {
                    var node = root.Nodes.Add(key.Value);
                    node.Tag = child.Value;
                    node.ImageIndex = node.SelectedImageIndex = GetImageIndex(child.Value);

                    LoadChildren(node, child.Value as YamlMappingNode);
                }
            }
        }

        private int GetImageIndex(YamlNode node)
        {
            switch(node.NodeType)
            {
                case YamlNodeType.Scalar:
                    if(node.Tag == "!secret") return 2;
                    if(node.Tag == "!include") return 1;
                    return 0;
                case YamlNodeType.Sequence: return 3;
                case YamlNodeType.Mapping:
                    if(node is YamlMappingNode mapping && mapping.Children.Any(pair => ((YamlScalarNode)pair.Key).Value == "platform")) return 5;
                    return 4;
            }
            return 0;
        }

        private void LoadChildren(TreeNode root, YamlSequenceNode sequence)
        {
            foreach(var child in sequence.Children)
            {
                if(child is YamlSequenceNode)
                {
                    var node = root.Nodes.Add(root.Text);
                    node.Tag = child;
                    node.ImageIndex = node.SelectedImageIndex = GetImageIndex(child);

                    LoadChildren(node, child as YamlSequenceNode);
                }
                else if(child is YamlMappingNode)
                {
                    var node = root.Nodes.Add(root.Text);
                    node.Tag = child;
                    node.ImageIndex = node.SelectedImageIndex = GetImageIndex(child);

                    LoadChildren(node, child as YamlMappingNode);
                }
                else if(child is YamlScalarNode)
                {
                    var scalar = child as YamlScalarNode;
                    var node = root.Nodes.Add(scalar.Value);
                    node.Tag = child;
                    node.ImageIndex = node.SelectedImageIndex = GetImageIndex(child);
                }
            }
        }

        private void OnAfterSelect(object sender, TreeViewEventArgs e)
        {
            mainPropertyGrid.SelectedObject = e.Node.Tag;
        }

        private void OnDoubleClick(object sender, EventArgs e)
        {
            if(mainTreeView.SelectedNode == null) return;
            var selected = mainTreeView.SelectedNode;

            if(selected.Tag is YamlMappingNode node)
            {
                if(node.Children.Any(p => ((YamlScalarNode)p.Key).Value == "platform"))
                {
                    var platform = node.Children.FirstOrDefault(p => ((YamlScalarNode)p.Key).Value == "platform");
                    mainWebBrowser.Url = new Uri($@"https://www.home-assistant.io/components/{ selected.Text }.{ platform.Value }");
                    mainTabControl.SelectTab(helpTabPage);
                }
            }
        }

        private IDictionary<YamlNode, YamlNode> getDataStructure(string filename)
        {
            // Read file
            var yaml = new YamlStream();
            try
            {
                using(var stream = new StreamReader(filename))
                {
                    yaml.Load(stream);
                }
            }
            catch(Exception exception)
            {
                mLogger.WriteLine(exception.Message);
            }

            if(yaml.Documents.Count == 0) return null;

            YamlMappingNode mapping = yaml.Documents[0].RootNode as YamlMappingNode;
            return mapping?.Children;
        }

        private void OnUndo(object sender, EventArgs e)
        {
            Manager.Undo();
        }

        private void OnRedo(object sender, EventArgs e)
        {
            Manager.Redo();
        }

        private void NewComponent(object sender, EventArgs e)
        {
            string input = Interaction.InputBox("New Component", "Name of the new component:", "Default", -1, -1);
        }

        private void AboutButton(object sender, EventArgs e)
        {
            MessageBox.Show("Made by: " +
                            "Diogo Cruz, " +
                            "Diogo Nóbrega, " +
                            "Francisco Teixeira, " +
                            "Marco Lima", "About");
        }
    }
}