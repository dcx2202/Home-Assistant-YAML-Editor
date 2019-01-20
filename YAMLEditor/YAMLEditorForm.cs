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

        /// <summary>
        /// {K, V}, K -> {oldvalue, parents at the time}, V -> component that changed
        /// </summary>
        public static Dictionary<Dictionary<string, List<IComponent>>, IComponent> changedComponents { get; set; }
        public static List<IComponent> addedComponents { get; set; }

        /// <summary>
        /// {K, V}, K -> component that got removed, V -> parents at the time
        /// </summary>
        public static Dictionary<IComponent, List<IComponent>> removedComponents { get; set; }

        public YAMLEditorForm()
        {
            InitializeComponent();
            mainTabControl.SelectedIndexChanged += new EventHandler(LoadHelpPageEvent);

            mLogger.Recorder = new TextBoxRecorder(mainTextBox);
            composite = new Component("root", "root", null);
            currentParent = composite;
            changedComponents = new Dictionary<Dictionary<string, List<IComponent>>, IComponent>();
            addedComponents = new List<IComponent>();
            removedComponents = new Dictionary<IComponent, List<IComponent>>();
        }


        public void LoadHelpPageEvent(object sender, EventArgs e)
        {
            try
            {
                LoadHelpPage();
            }
            catch (Exception exception)
            {
                mLogger.WriteLine(DateTime.Now.ToString("HH:mm:ss") + " - " + exception.StackTrace);
            }
        }

        public void LoadHelpPage()
        {
            if (mainTabControl.SelectedTab.Text == "Help")
            {
                if (mainTreeView.SelectedNode == null || filename == null)
                    return;

                else if (mainTreeView.SelectedNode.Tag == null)
                    return;

                var node = (Component)mainTreeView.SelectedNode.Tag;

                if (node.getParent().Name != "root")
                {
                    List<IComponent> parents = new List<IComponent>();
                    node = GetParents(parents, node).ElementAt(parents.Count - 2) as Component;
                }

                webBrowser.Navigate("https://www.home-assistant.io/components/" + node.Name);
            }
        }

        #region Button Actions
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
                changedComponents = new Dictionary<Dictionary<string, List<IComponent>>, IComponent>();
                addedComponents = new List<IComponent>();
                if(FileTreeRoot != null)
                    FileTreeRoot.Nodes.Clear();
                composite = new Component("root", "root", null);
                currentParent = composite;

                mLogger.WriteLine(DateTime.Now.ToString("HH:mm:ss") + " - Opened " + $"Filename: {dialog.FileName}");
                System.Diagnostics.Trace.WriteLine($"Filename: {dialog.FileName}");
                Directory.SetCurrentDirectory(Path.GetDirectoryName(dialog.FileName) ?? "");

                mainTreeView.Nodes.Clear();
                FileTreeRoot = mainTreeView.Nodes.Add(Path.GetFileName(dialog.FileName));
                FileTreeRoot.ImageIndex = FileTreeRoot.SelectedImageIndex = 3;

                openedfilename = dialog.FileName;
                var splits = openedfilename.Split('\\');
                openedfilename = splits[splits.Length - 1];
                filename = openedfilename;

                LoadFile(FileTreeRoot, filename);
                FileTreeRoot.Expand();

                // After opening a file we enable these buttons
                newToolStripButton.Enabled = true;
                newToolStripMenuItem.Enabled = true;
                cutToolStripButton.Enabled = true;
                cutToolStripMenuItem.Enabled = true;
                saveToolStripButton.Enabled = true;
                saveToolStripMenuItem.Enabled = true;
            }
        }

        private void OnPasteToolStripButton_Click(object sender, EventArgs e)
        {
            var a = composite;
            var b = FileTreeRoot;
            var c = mainTabControl.SelectedTab;
            var d = mainTabControl.SelectedTab;
        }

        private void OnSaveButton(object sender, EventArgs e)
        {
            Save();
        }

        private void OnUndo(object sender, EventArgs e)
        {
            Manager.Undo();
        }

        private void OnRedo(object sender, EventArgs e)
        {
            Manager.Redo();
        }

        private void OnAboutButton(object sender, EventArgs e)
        {
            MessageBox.Show("Made by: " +
                "Diogo Cruz, " +
                "Diogo Nóbrega, " +
                "Francisco Teixeira, " +
                "Marco Lima", "About");
        }
        #endregion

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
                mLogger.WriteLine(DateTime.Now.ToString("HH:mm:ss") + " - " + exception.Message);
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

                    comp = new Component(scalar.Value, filename, currentParent);
                    currentParent.add(comp);

                    node = root.Nodes[root.Nodes.Count - 1].Nodes.Add($"{scalar.Value}");
                    node.Tag = comp;
                    node.ImageIndex = node.SelectedImageIndex = GetImageIndex(scalar);

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
            LoadHelpPage();
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
                mLogger.WriteLine(DateTime.Now.ToString("HH:mm:ss") + " - " + exception.Message);
            }

            if (yaml.Documents.Count == 0) return null;

            YamlMappingNode mapping = yaml.Documents[0].RootNode as YamlMappingNode;
            return mapping?.Children;
        }

		private void NewComponent(object sender, EventArgs e)
		{
			NewComponent nc = new NewComponent();
			nc.ShowDialog();
		}

        private void NewComponent2(object sender, EventArgs e)
        {
            NewComponent nc = new NewComponent();
			nc.ShowDialog();
		}

        public static void UpdateTree(IComponent component, TreeNode root, string aValue)
        {
            if (root == null)
                root = FileTreeRoot;

            var parents1 = new List<IComponent>();
            var parents2 = new List<TreeNode>();

            parents1 = GetParents(parents1, component);
            parents2 = GetParents(parents2, root);

            if(parents1.Count > 1)
                parents1.Remove(parents1.Last());
            if (parents2.Count > 1)
                parents2.Remove(parents2.Last());

            if (parents1.Count == 0 && parents1.Count == parents2.Count)
            {
                if (component.Name.Equals(root.Name))
                {
                    //parents1.Remove(parents1.Last());
                    //YAMLEditorForm.changedComponents.Add(new Dictionary<string, List<IComponent>>() { { node.Name, parents1 } }, component);
                    root.Name = aValue;
                    root.Text = aValue;
                    return;
                }
            }
            else if (parents1.Count == parents2.Count)
            {
                for (var i = 0; i < parents1.Count; i++)
                {
                    if (parents1[i].Name != parents2[i].Text && parents2[i].Tag != null)
                        return;
                }

                var treecomp = root.Tag as Component;

                if (component.Name.Equals(root.Name) || component.Name.Equals(treecomp.Name))
                {
                    //parents1.Remove(parents1.Last());
                    //YAMLEditorForm.changedComponents.Add(new Dictionary<string, List<IComponent>>() { { node.Name, parents1 } }, component);
                    root.Name = aValue;
                    root.Text = aValue;
                    return;
                }
            }


            /*if (root == null)
                root = FileTreeRoot;

            if (root.Tag == component)
            {
                root.Text = component.Name;
                return;
            }*/

            foreach (TreeNode node in root.Nodes)
            {
                UpdateTree(component, node, aValue);
            }
        }

        public static void UpdateComposite(IComponent node, IComponent component, string aValue)
        {
            if (node == null)
                node = composite;

            var parents1 = new List<IComponent>();
            var parents2 = new List<IComponent>();

            parents1 = GetParents(parents1, node);
            parents2 = GetParents(parents2, component);

            if(parents1.Count > 0)
                parents1.Remove(parents1.Last());
            if (parents2.Count > 0)
                parents2.Remove(parents2.Last());

            if (parents1.Count == 0 && parents1.Count == parents2.Count)
            {
                if(node.Name.Equals(component.Name))
                {
                    //parents1.Remove(parents1.Last());
                    //YAMLEditorForm.changedComponents.Add(new Dictionary<string, List<IComponent>>() { { node.Name, parents1 } }, component);
                    node.setName(aValue);
                    return;
                }
            }
            else if(parents1.Count == parents2.Count)
            {
                for(var i = 0; i < parents1.Count; i++)
                {
                    if (!parents1[i].Name.Equals(parents2[i].Name))
                        return;
                }

                if (node.Name.Equals(component.Name))
                {
                    //parents1.Remove(parents1.Last());
                    //YAMLEditorForm.changedComponents.Add(new Dictionary<string, List<IComponent>>() { { node.Name, parents1 } }, component);
                    node.setName(aValue);
                    return;
                }
            }

            foreach (IComponent n in node.getChildren())
            {
                UpdateComposite(n, component, aValue);
            }
        }

        public static void CheckIfComponentExists(IComponent node, IComponent component)
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
                CheckIfComponentExists(n, component);
            }
        }

        public static Dictionary<IComponent, TreeNode> getComponentFromFile(string filename)
        {
            if (File.ReadAllText(filename).Trim() != "")
            {
                // Get composite and tree from the opened component file
                IComponent newcomponent = new Component("root", filename, null);
                TreeNode newtree = new TreeNode();

                currentParent = newcomponent;
                LoadFile(newtree, filename);
                currentParent = composite;

                return new Dictionary<IComponent, TreeNode> { { newcomponent, newtree } };
            }
            else
            {
                MessageBox.Show("The file opened is empty.", "Error");
                return null;
            }
        }

        public static void AddComponent(IComponent aComponent, TreeNode aTree)
        {
            aComponent.setParent(currentParent);
            composite.add(aComponent);
            addedComponents.Add(aComponent);
            FileTreeRoot.Nodes.Add(aTree);

            mLogger.WriteLine(DateTime.Now.ToString("HH:mm:ss") + " - Added " + aComponent.Name);
        }

        public static List<IComponent> GetParents(List<IComponent> parents, IComponent node)
        {
            if (node.getParent() == null)
            {
                return parents;
            }
            else
            {
                parents.Add(node.getParent());
                return GetParents(parents, node.getParent());
            }
        }

        public static List<TreeNode> GetParents(List<TreeNode> parents, TreeNode node)
        {
            if (node.Parent == null)
            {
                return parents;
            }
            else
            {
                parents.Add(node.Parent);
                return GetParents(parents, node.Parent);
            }
        }

        public static List<IComponent> GetAllChildren(List<IComponent> children, IComponent node)
        {
            if(node.getChildren().Count == 0)
            {
                return children;
            }
            else
            {
                foreach(IComponent child in node.getChildren())
                {
                    children.Add(child);
                    GetAllChildren(children, child);
                }
            }

            return children;
        }

        public static List<string> GetAllChildrenstring(List<string> lines, IComponent comp, string ident)
        {
            List<IComponent> allChildren = new List<IComponent>();
            allChildren = GetAllChildren(allChildren, comp);

            if (allChildren.Count == 0)
            {
                lines.Add(ident + comp.Name);
            }
            /*else if (allChildren.Count == 1)
            {
                lines.Add(ident + comp.Name + ":  " + allChildren.First().Name);
            }*/
            else
            {
                ident += "  ";
                foreach (IComponent child in comp.getChildren())
                {
                    List<IComponent> siblings = new List<IComponent>();
                    GetAllChildren(siblings, child.getParent());

                    if (siblings.Count == 1)
                        continue;

                    lines.Add(ident + child.Name + ":");

                    //ident += "  ";

                    GetAllChildrenstring(lines, child, ident);
                }
                return lines;
            }

            return lines;
        }

        public void Save()
        {
            // For each component that was added we write it to the opened file
            foreach (IComponent comp in addedComponents)
            {

                var lines = File.ReadAllLines(filename).ToList();
                lines.Add("");
                lines.Add(comp.Name + ": !include " + comp.getFileName());

                File.WriteAllLines(filename, lines);
            }

            // For each component that suffered changes we look for it in the files (opened and !included)
            foreach (KeyValuePair< Dictionary<string, List<IComponent>>, IComponent > comp in changedComponents)
            {
                // changed component
                var node = comp.Value;

                // its parents
                var nodeparents = comp.Key.Values.First();

                // its old value
                var oldvalue = comp.Key.Keys.First();

                string newvalue = node.Name;

                List<string> lines = new List<string>();

                try
                {
                    lines = File.ReadAllLines(node.getFileName()).ToList();
                }
                catch (IOException e)
                {
                    mLogger.WriteLine(DateTime.Now.ToString("HH:mm:ss") + " - " + e.ToString());
                }

                int ln = 0;

                // if it's a level 0 component, the parent will be null
                // and as such, the "parent's" line will 0 (as if the
                // hypothetical parent was the whole file)
                if (nodeparents.Count != 0)
                {
                    var aux = false;
                    for (var j = nodeparents.Count - 1; j >= 0; j--)// IComponent parent in nodeparents)
                    {
                        // For each line of this file we look for the component
                        for (var i = ln; i < lines.Count; i++)
                        {
                            if (lines[i].Trim().StartsWith(nodeparents[j].Name) || lines[i].Trim().StartsWith("- " + nodeparents[j].Name))//lines[i].Contains(nodeparents[j].Name))
                            {
                                aux = true;
                                ln = i;
                                nodeparents.RemoveAt(j);
                                break;
                            }
                        }

                        if (aux == false)
                            break; // didn't find the component - should never happen
                    }

                    if (aux == false)
                        continue; // didn't find the component - should never happen
                }

                // after we've got the parent's exact line
                for (var i = ln; i < lines.Count; i++)
                {
                    if (lines[i].Contains(oldvalue) && !lines[i].Trim().StartsWith("#"))
                    {
                        // "parent:" --> "parent: newvalue"
                        if (oldvalue == "" && newvalue != "")
                            lines[i] = lines[i] + " " + newvalue;

                        // "oldvalue:?" --> "newvalue:?"
                        else if (lines[i].Contains(oldvalue + ":") && oldvalue != "" && newvalue != "")
                        {
                            var splits = lines[i].Split(':');
                            if (splits.Length == 2)
                                lines[i] = splits[0].Replace(oldvalue, newvalue) + ":" + splits[1];
                            else
                                lines[i] = splits[0].Replace(oldvalue, newvalue) + ":";
                        }
                        else
                        {
                            // "parent: oldvalue" --> "parent:"
                            if (newvalue == "")
                                lines[i] = lines[i].Split(':')[0] + ":";

                            // "parent: oldvalue" --> "parent: newvalue"
                            else
                                lines[i] = lines[i].Split(':')[0] + ": " + newvalue;
                        }
                        break;
                    }
                }

                File.WriteAllLines(node.getFileName(), lines);
            }

            // For each component that was removed we remove it from the opened file
            foreach (KeyValuePair<IComponent, List<IComponent>> comp in removedComponents)
            {
                var nodeparents = comp.Value;
                var node = comp.Key;

                List<string> lines = File.ReadAllLines(node.getFileName()).ToList();

                int ln = 0;

                var aux = false;

                if (nodeparents.Count > 1)
                {
                    for (var j = nodeparents.Count - 1; j >= 0; j--)// IComponent parent in nodeparents)
                    {
                        // For each line of this file we look for the component
                        for (var i = ln; i < lines.Count; i++)
                        {
                            if (lines[i].Trim().StartsWith(nodeparents[j].Name) || lines[i].Trim().StartsWith("- " + nodeparents[j].Name))//lines[i].Contains(nodeparents[j].Name))
                            {
                                aux = true;
                                ln = i;
                                nodeparents.RemoveAt(j);
                                break;
                            }
                        }

                        if (aux == false)
                            break; // didn't find the component - should never happen
                    }
                }

                // temos a linha do pai direto do no
                aux = false;
                for (var i = ln; i < lines.Count; i++)
                {
                    if (lines[i].Contains(node.Name) && !lines[i].Trim().StartsWith("#") && !aux)
                    {
                        lines.RemoveAt(i);
                        if(i > 0)
                            i--;
                        aux = true;
                    }

                    if (aux && node.getChildren().Count > 1)
                    {
                        foreach (IComponent child in node.getChildren())
                        {
                            if (lines[i].Contains(child.Name) && !lines[i].Trim().StartsWith("#"))
                            {
                                lines.RemoveAt(i);
                                if (i > 0)
                                    i--;
                            }
                        }
                    }

                    if (aux && lines[i].Trim() == "")
                        break;
                }

                File.WriteAllLines(node.getFileName(), lines);
            }

            changedComponents = new Dictionary<Dictionary<string, List<IComponent>>, IComponent>();
            addedComponents = new List<IComponent>();
            removedComponents = new Dictionary<IComponent, List<IComponent>>();
            FileTreeRoot.Nodes.Clear();
            composite = new Component("root", "root", null);
            currentParent = composite;
            LoadFile(FileTreeRoot, filename);

            mLogger.WriteLine(DateTime.Now.ToString("HH:mm:ss") + " - Saved");
        }

        private void RemoveComponent(object sender, EventArgs e)
        {
            if(mainTreeView.SelectedNode == null)
            {
                MessageBox.Show("There is no node currently selected", "Error");
                return;
            }

            var component = mainTreeView.SelectedNode.Tag as Component;

            DialogResult result = MessageBox.Show("Do you really want to remove " + component.Name + "?", "Warning",
            MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

            if (result == DialogResult.No)
            {
                MessageBox.Show("Removal cancelled");
                mLogger.WriteLine(DateTime.Now.ToString("HH:mm:ss") + " - " + "Removal cancelled");
                return;
            }

            var treenode = mainTreeView.SelectedNode;
            var node = (Component) treenode.Tag;

            List<IComponent> nodeparents = new List<IComponent>();
            nodeparents = GetParents(nodeparents, node);
            nodeparents.Remove(nodeparents.Last());

            treenode.Parent.Nodes.Remove(treenode);
            node.getParent().getChildren().Remove(node);

            removedComponents.Add(node, nodeparents);

            mLogger.WriteLine(DateTime.Now.ToString("HH:mm:ss") + " - Removed " + component.Name);
        }

        public static void WriteToTextBox(string aMessage)
        {
            mLogger.WriteLine(aMessage);
        }
    }
}