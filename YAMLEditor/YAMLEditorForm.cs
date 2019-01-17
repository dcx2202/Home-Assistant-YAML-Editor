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
using Microsoft.VisualBasic;
using YAMLEditor.Commands;

namespace YAMLEditor
{
    public partial class YAMLEditorForm : Form
    {
        private CommandManager commandManager = new CommandManager();

        //use mLogger.Write(string message) to log to the textbox
        private ILogger mLogger = Logger.Instance;
        public IComponent composite;
        public IComponent currentParent;
        public string filename;
        public string openedfilename;
        static TreeNode FileTreeRoot;

        public YAMLEditorForm()
        {
            InitializeComponent();
            SetUndoRedoButtons();
            mLogger.Recorder = new TextBoxRecorder(mainTextBox);
            composite = new Component("root", "root", null, "root");
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
                FileTreeRoot = mainTreeView.Nodes.Add(Path.GetFileName(dialog.FileName));
                FileTreeRoot.ImageIndex = FileTreeRoot.SelectedImageIndex = 3;

                openedfilename = dialog.FileName;
                filename = openedfilename;
                LoadFile(FileTreeRoot, dialog.FileName);
                FileTreeRoot.Expand();
            }
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

                    IComponent comp = new Component(key.Value, filename, currentParent, "YamlScalarNode");
                    currentParent.Add(comp);
                    currentParent = comp;

                    var node = root.Nodes.Add($"{key.Value}");
                    node.Tag = comp;
                    node.ImageIndex = node.SelectedImageIndex = GetImageIndex(scalar);

                    //if (scalar.Value != "")
                    //{
                    comp = new Component(scalar.Value, filename, currentParent, "YamlScalarNode");
                    currentParent.Add(comp);

                    node = root.Nodes[root.Nodes.Count - 1].Nodes.Add($"{scalar.Value}");
                    node.Tag = comp;
                    node.ImageIndex = node.SelectedImageIndex = GetImageIndex(scalar);
                    //}

                    if(scalar.Tag == "!include")
                    {
                        filename = scalar.Value;
                        LoadFile(node, scalar.Value);
                        filename = openedfilename;
                    }
                }
                else if(child.Value is YamlSequenceNode)
                {
                    IComponent comp = new Component(key.Value, filename, currentParent, "YamlScalarNode");
                    currentParent.Add(comp);
                    currentParent = comp;

                    var node = root.Nodes.Add(key.Value);
                    node.Tag = comp;
                    node.ImageIndex = node.SelectedImageIndex = GetImageIndex(child.Value);

                    LoadChildren(node, child.Value as YamlSequenceNode);
                }
                else if(child.Value is YamlMappingNode)
                {
                    IComponent comp = new Component(key.Value, filename, currentParent, "YamlScalarNode");
                    currentParent.Add(comp);
                    currentParent = comp;

                    var node = root.Nodes.Add(key.Value);
                    node.Tag = comp;
                    node.ImageIndex = node.SelectedImageIndex = GetImageIndex(child.Value);

                    LoadChildren(node, child.Value as YamlMappingNode);
                }

                if(currentParent.GetParent() != null)
                    currentParent = currentParent.GetParent();
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
                    IComponent comp = new Component(root.Text, filename, currentParent, "YamlSequenceNode");
                    currentParent.Add(comp);
                    currentParent = comp;

                    var node = root.Nodes.Add(root.Text);
                    node.Tag = comp;
                    node.ImageIndex = node.SelectedImageIndex = GetImageIndex(child);

                    LoadChildren(node, child as YamlSequenceNode);
                }
                else if(child is YamlMappingNode)
                {
                    LoadChildren(root, child as YamlMappingNode);
                }
                else if(child is YamlScalarNode)
                {
                    var scalar = child as YamlScalarNode;

                    IComponent comp = new Component(root.Text, filename, currentParent, "YamlScalarNode");
                    currentParent.Add(comp);
                    currentParent = comp;

                    var node = root.Nodes.Add(scalar.Value);
                    node.Tag = comp;
                    node.ImageIndex = node.SelectedImageIndex = GetImageIndex(child);
                }
            }

            if(currentParent.GetParent() != null)
                currentParent = currentParent.GetParent();
        }

        private void OnAfterSelect(object sender, TreeViewEventArgs e)
        {
            mainPropertyGrid.SelectedObject = e.Node.Tag;
        }

        private void OnDoubleClick(object sender, EventArgs e)
        {
            if(mainTreeView.SelectedNode == null) return;
            var selected = (Component)mainTreeView.SelectedNode.Tag;

            if(selected.GetChildren().Any(p => ((Component)p).Name == "platform"))
            {
                var platform = selected.GetChildren().FirstOrDefault(p => ((Component)p).Name == "platform") as Component;
                mainWebBrowser.Url = new Uri($@"https://www.home-assistant.io/components/{ selected.Name }.{ ((Component)platform.GetChild(0)).Name }");
                mainTabControl.SelectTab(helpTabPage);
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
            commandManager.Undo();
        }

        private void OnRedo(object sender, EventArgs e)
        {
            commandManager.Redo();
        }

        private void NewComponent(object sender, EventArgs e)
        {

            NewComponent nc = new NewComponent();
            nc.ShowDialog();
            //string input = Interaction.InputBox("New Component", "Name of the new component:", "Default", -1, -1);
        }

        private void AboutButton(object sender, EventArgs e)
        {
            MessageBox.Show("Made by: " +
                "Diogo Cruz, " +
                "Diogo Nóbrega, " +
                "Francisco Teixeira, " +
                "Marco Lima", "About");
        }

        private void newToolStripButton_Click(object sender, EventArgs e)
        {
            // Using this for debugging
            var a = composite;
            var b = FileTreeRoot;
        }

        public static void UpdateTree(IComponent component, string aValue, TreeNode root)
        {
            if(root == null)
                root = FileTreeRoot;

            if(root.Tag == component)
            {
                root.Text = aValue;
                return;
            }

            foreach(TreeNode node in root.Nodes)
            {
                UpdateTree(component, aValue, node);
            }
        }

        private void OnUpdatePropertyGrid(object sender, PropertyValueChangedEventArgs e)
        {
            //adicionar comandos ao commandManager aqui
            mLogger.WriteLine("alteração");
        }

        private void SetUndoRedoButtons()
        {
            undoButton.Enabled = commandManager.HasUndo();
            redoButton.Enabled = commandManager.HasRedo();
        }
    }
}