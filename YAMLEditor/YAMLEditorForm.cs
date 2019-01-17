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
using Microsoft.VisualBasic;

namespace YAMLEditor
{
    public partial class YAMLEditorForm : Form
    {
        public static CommandManager Manager = new CommandManager();

        //use mLogger.Write(string message) to log to the textbox
        private static ILogger mLogger = Logger.Instance;
        public static IComponent composite {get; set;}
        public static IComponent currentParent;
        public static string filename;
        public static string openedfilename;
        public static bool componentexists { get; set; } = false;
        public static TreeNode FileTreeRoot { get; set; }

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
                FileTreeRoot = mainTreeView.Nodes.Add(Path.GetFileName(dialog.FileName));
                FileTreeRoot.ImageIndex = FileTreeRoot.SelectedImageIndex = 3;

                openedfilename = dialog.FileName;
                filename = openedfilename;
                LoadFile(FileTreeRoot, dialog.FileName);
                FileTreeRoot.Expand();
            }
        }

        private static void LoadFile(TreeNode node, string filename)
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

        private static void LoadChildren(TreeNode root, YamlMappingNode mapping)
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

                    IComponent comp = new Component(key.Value, filename, currentParent);
                    currentParent.add(comp);
                    currentParent = comp;

                    var node = root.Nodes.Add($"{key.Value}");
                    node.Tag = comp;
                    node.ImageIndex = node.SelectedImageIndex = GetImageIndex(scalar);

                    //if (scalar.Value != "")
                    //{
                    comp = new Component(scalar.Value, filename, currentParent);
                    currentParent.add(comp);

                    node = root.Nodes[root.Nodes.Count - 1].Nodes.Add($"{scalar.Value}");
                    node.Tag = comp;
                    node.ImageIndex = node.SelectedImageIndex = GetImageIndex(scalar);
                    //}

                    if (scalar.Tag == "!include")
                    {
                        filename = scalar.Value;
                        LoadFile(node, scalar.Value);
                        filename = openedfilename;
                    }
                }
                else if(child.Value is YamlSequenceNode)
                {
                    IComponent comp = new Component(key.Value, filename, currentParent);
                    currentParent.add(comp);
                    currentParent = comp;

                    var node = root.Nodes.Add(key.Value);
                    node.Tag = comp;
                    node.ImageIndex = node.SelectedImageIndex = GetImageIndex(child.Value);

                    LoadChildren(node, child.Value as YamlSequenceNode);
                }
                else if(child.Value is YamlMappingNode)
                {
                    IComponent comp = new Component(key.Value, filename, currentParent);
                    currentParent.add(comp);
                    currentParent = comp;

                    var node = root.Nodes.Add(key.Value);
                    node.Tag = comp;
                    node.ImageIndex = node.SelectedImageIndex = GetImageIndex(child.Value);

                    LoadChildren(node, child.Value as YamlMappingNode);
                }

                if (currentParent.getParent() != null)
                    currentParent = currentParent.getParent();
            }
            
        }

        private static int GetImageIndex(YamlNode node)
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

        private static void LoadChildren(TreeNode root, YamlSequenceNode sequence)
        {
            foreach(var child in sequence.Children)
            {
                if(child is YamlSequenceNode)
                {
                    IComponent comp = new Component(root.Text, filename, currentParent);
                    currentParent.add(comp);
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

                    IComponent comp = new Component(root.Text, filename, currentParent);
                    currentParent.add(comp);
                    currentParent = comp;

                    var node = root.Nodes.Add(scalar.Value);
                    node.Tag = comp;
                    node.ImageIndex = node.SelectedImageIndex = GetImageIndex(child);
                }
            }

            if (currentParent.getParent() != null)
                currentParent = currentParent.getParent();
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
                using (var stream = new StreamReader(filename))
                {
                    yaml.Load(stream);
                }
            }
            catch (Exception exception)
            {
                mLogger.WriteLine(exception.Message);
            }

            if (yaml.Documents.Count == 0) return null;

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
			
			NewComponent nc = new NewComponent();
			nc.ShowDialog();
		}

		private void AboutButton(object sender, EventArgs e)
		{
			MessageBox.Show("Made by: " +
				"Diogo Cruz, " +
				"Diogo Nóbrega, " +
				"Francisco Teixeira, " +
				"Marco Lima", "About");
		}

        private void NewComponent2(object sender, EventArgs e)
        {
			NewComponent nc = new NewComponent();
			nc.ShowDialog();
		}

        public static void updateTree(IComponent component, TreeNode root)
        {
            if (root == null)
                root = FileTreeRoot;

            if(root.Tag == component)
            {
                root.Text = component.Name;
                return;
            }

            foreach(TreeNode node in root.Nodes)
            {
                updateTree(component, node);
            }
        }

        public static void updateComposite(IComponent node, IComponent component, string aValue)
        {
            if (node == null)
                node = composite;

            if (node == component)
            {
                node.setName(aValue);
                return;
            }

            foreach (IComponent n in node.getChildren())
            {
                updateComposite(n, component, aValue);
            }
        }

        public static void checkIfComponentExists(IComponent node, IComponent component)
        {
            if (node == null)
                node = composite;

            if (node.Name == component.Name)
            {
                componentexists = true;
                return;
            }

            foreach (IComponent n in node.getChildren())
            {
                checkIfComponentExists(n, component);
            }
        }

        public static Dictionary<IComponent, TreeNode> getComponentFromFile(string filename)
        {
            // Get composite and tree from the opened component file
            IComponent newcomponent = new Component("root", filename, null);
            TreeNode newtree = new TreeNode();

            currentParent = newcomponent;
            LoadFile(newtree, filename);
            currentParent = composite;

            return new Dictionary<IComponent, TreeNode> { { newcomponent, newtree } };
        }

        public static void addComponent(IComponent aComponent, TreeNode aTree)
        {
            aComponent.setParent(currentParent);
            composite.add(aComponent);
            FileTreeRoot.Nodes.Add(aTree);
        }

        private void pasteToolStripButton_Click(object sender, EventArgs e)
        {
            var a = composite;
            var b = FileTreeRoot;
        }
    }
}