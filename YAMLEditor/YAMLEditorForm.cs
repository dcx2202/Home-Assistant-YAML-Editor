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
using WebSocketSharp;
using Newtonsoft.Json;
using YAMLEditor.Properties;
using Renci.SshNet;

namespace YAMLEditor
{
    public partial class YAMLEditorForm : Form
    {
        public static CommandManager Manager = new CommandManager();
        public static TreeNode parentNode = new TreeNode();

        //use mLogger.Write(string message) to log to the textbox
        private static ILogger mLogger = Logging.Logger.Instance;
        public static IComponent composite { get; set; }
        public static IComponent currentParent;
        public static string filename;
        public static string openedfilename;
        public static string workingdir;
        public static bool componentExists { get; set; } = false;
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

            workingdir = Environment.CurrentDirectory;

            Application.ApplicationExit += new EventHandler(this.OnApplicationExit);
        }

        private void OnApplicationExit(object sender, EventArgs e)
        {
            Directory.SetCurrentDirectory(workingdir);

            if (Directory.Exists("./RemoteFiles/"))
            {
                DirectoryInfo d = new DirectoryInfo("./RemoteFiles/");

                foreach (FileInfo file in d.GetFiles())
                {
                    file.Delete();
                }
                foreach (DirectoryInfo dir in d.GetDirectories())
                {
                    dir.Delete(true);
                }

                d.Delete();
            }

            if (Directory.Exists(workingdir + "/GitRepo/"))
            {
                DirectoryInfo d = new DirectoryInfo(workingdir + "/GitRepo/");

                foreach (FileInfo file in d.GetFiles())
                {
                    file.Delete();
                }
                foreach (DirectoryInfo dir in d.GetDirectories())
                {
                    setAttributesNormal(dir);
                    dir.Delete(true);
                }

                d.Delete();
            }
        }


        #region Button Actions
        private void OnNewComponent(object sender, EventArgs e)
        {
            IComponent component;

            if (mainTreeView.SelectedNode == null || mainTreeView.SelectedNode.Tag == null)
                component = composite;
            else
                component = mainTreeView.SelectedNode.Tag as Component;

            if (component.Name != "root")
            {
                List<IComponent> allchildren = new List<IComponent>();
                allchildren = GetAllChildren(allchildren, component);

                if ((allchildren.Count == 1 && allchildren.First().Name != "") || allchildren.Count == 0)
                {
                    if(component.Name == "")
                        MessageBox.Show("Can't add a new component to this node. Try adding to " + component.getParent().Name + ".");
                    else
                        MessageBox.Show("Can't add a new component to this node.");
                    return;
                }
            }

            NewComponent nc = new NewComponent(component);


            nc.ShowDialog();
        }

        private void OnExit(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void OnOpen(object sender, EventArgs e)
        {
            Directory.SetCurrentDirectory(workingdir);
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
                removeToolStripMenuItem.Enabled = true;
                saveToolStripButton.Enabled = true;
                saveToolStripMenuItem.Enabled = true;
                uploadtourl.Enabled = true;
                push_toolStrip.Enabled = true;
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

        private void OnRemoveComponent(object sender, EventArgs e)
        {
            if (mainTreeView.SelectedNode == null)
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
            var node = (Component)treenode.Tag;

            List<IComponent> nodeparents = new List<IComponent>();
            nodeparents = GetParents(nodeparents, node);
            nodeparents.Remove(nodeparents.Last());

            treenode.Parent.Nodes.Remove(treenode);
            node.getParent().getChildren().Remove(node);

            removedComponents.Add(node, nodeparents);

            mLogger.WriteLine(DateTime.Now.ToString("HH:mm:ss") + " - Removed " + component.Name);
        }

        private void OnAboutButton(object sender, EventArgs e)
        {
            MessageBox.Show("Developed by: " +
                "Diogo Cruz, " +
                "Diogo Nóbrega, " +
                "Francisco Teixeira, " +
                "Marco Lima", "About");
        }

        private void OnRestartHomeassistant(object sender, EventArgs e)
        {
            string user_ha_address = (string)Settings.Default["ha_address"];
            string user_access_token = (string)Settings.Default["access_token"];

            if (user_ha_address == "" || user_access_token == "")
            {

                mLogger.WriteLine(DateTime.Now.ToString("HH:mm:ss") + " - You need to specify your details. Check settings.");
                return;
            }

            try
            {
                using (var ws = new WebSocket("ws://" + user_ha_address + "/api/websocket"))
                {
                    ws.Connect();

                    Dictionary<string, string> auth = new Dictionary<string, string>() { { "type", "auth" }, { "access_token", user_access_token } };
                    string json = JsonConvert.SerializeObject(auth);

                    ws.Send(json);

                    var service = new Dictionary<object, object>() { { "type", "call_service" }, { "domain", "homeassistant" }, { "service", "restart" }, { "service_data", new Dictionary<string, string>() { } }, { "id", "14" } };

                    json = JsonConvert.SerializeObject(service);

                    ws.Send(json);

                    mLogger.WriteLine(DateTime.Now.ToString("HH:mm:ss") + " - Restarting HomeAssistant on address " + user_ha_address);
                }
            }
            catch (Exception exc)
            {
                mLogger.WriteLine(DateTime.Now.ToString("HH:mm:ss") + " - Couldn't restart HomeAssistant. Check settings.");
            }
        }

        private void OnOptionsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OptionsWindow options = new OptionsWindow();
            options.ShowDialog();
        }

        private void OnAfterSelect(object sender, TreeViewEventArgs e)
        {
            mainPropertyGrid.SelectedObject = e.Node.Tag;
            LoadHelpPage();
        }

        private void OnOpenFromURL(object sender, EventArgs e)
        {
            OpenFromURL();
        }

        private void OnUploadToURL(object sender, EventArgs e)
        {
            UploadToURL();
        }

        private void OnToolStripOpenFromURL(object sender, EventArgs e)
        {
            OpenFromURL();
        }

        private void OnToolStripUploadToRemote(object sender, EventArgs e)
        {
            UploadToURL();
        }

        private void OnPullFromRemote(object sender, EventArgs e)
        {
            PullFromRemote();
        }

        private void OnPushToRemote(object sender, EventArgs e)
        {
            PushToRemote();
        }

        private void OnToolStripPullFromRemote(object sender, EventArgs e)
        {
            PullFromRemote();
        }

        private void OnToolStripPushToRemote(object sender, EventArgs e)
        {
            PushToRemote();
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
        #endregion

        public void OpenFromURL()
        {
            string rh_address = (string)Settings.Default["rh_address"];
            string username = (string)Settings.Default["username"];
            string password = (string)Settings.Default["password"];
            string remote_dir = (string)Settings.Default["remote_directory"];

            if (rh_address == "" || username == "" || remote_dir == "") // no password -> ""
            {
                MessageBox.Show("Please check your remote host file editing settings.", "Error");
                mLogger.WriteLine(DateTime.Now.ToString("HH:mm:ss") + " - Please check your remote host file editing settings before trying to open from URL.");
                return;
            }
            else
            {
                DialogResult result = MessageBox.Show("Starting download of files from remote host on " + rh_address, "Open from URL",
                MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
                mLogger.WriteLine(DateTime.Now.ToString("HH:mm:ss") + " - Starting download of files from remote host on " + rh_address);

                if (result == DialogResult.Cancel)
                    return;

                Directory.SetCurrentDirectory(workingdir);

                if (Directory.Exists("./RemoteFiles/"))
                {
                    DirectoryInfo d = new DirectoryInfo("./RemoteFiles/");

                    foreach (FileInfo file in d.GetFiles())
                    {
                        file.Delete();
                    }
                    foreach (DirectoryInfo dir in d.GetDirectories())
                    {
                        dir.Delete(true);
                    }
                }

                if (!remote_dir.StartsWith("/"))
                    remote_dir = "/" + remote_dir;
                if (!remote_dir.EndsWith("/"))
                    remote_dir = remote_dir + "/";

                try
                {
                    SFTPManager.DownloadRemoteFiles(rh_address, username, password, remote_dir, "./RemoteFiles/", "yaml");
                }
                catch (Exception exc)
                {
                    MessageBox.Show("Couldn't download files from remote host.", "Error");
                    mLogger.WriteLine(DateTime.Now.ToString("HH:mm:ss") + " - Couldn't download files from remote host.");
                    return;
                }

                result = MessageBox.Show("Download complete. Open a file...", "Success",
                MessageBoxButtons.OK, MessageBoxIcon.Information);
                mLogger.WriteLine(DateTime.Now.ToString("HH:mm:ss") + " - Download complete.");

                var dialog = new OpenFileDialog()
                { Filter = @"Yaml files (*.yaml)|*.yaml|All files (*.*)|*.*", DefaultExt = "yaml" };

                dialog.InitialDirectory = workingdir + "\\RemoteFiles\\";

                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    changedComponents = new Dictionary<Dictionary<string, List<IComponent>>, IComponent>();
                    addedComponents = new List<IComponent>();
                    if (FileTreeRoot != null)
                        FileTreeRoot.Nodes.Clear();
                    composite = new Component("root", "root", null);
                    currentParent = composite;

                    System.Diagnostics.Trace.WriteLine($"Filename: {dialog.FileName}");

                    Directory.SetCurrentDirectory(Path.GetDirectoryName(dialog.FileName) ?? "");

                    mainTreeView.Nodes.Clear();
                    FileTreeRoot = mainTreeView.Nodes.Add(Path.GetFileName(dialog.FileName));
                    FileTreeRoot.ImageIndex = FileTreeRoot.SelectedImageIndex = 3;

                    openedfilename = dialog.FileName;
                    var splits = openedfilename.Split('\\');
                    openedfilename = splits[splits.Length - 1];
                    filename = openedfilename;

                    mLogger.WriteLine(DateTime.Now.ToString("HH:mm:ss") + " - Opened " + $"Filename: " + filename + " from remote host on " + rh_address);

                    LoadFile(FileTreeRoot, filename);
                    FileTreeRoot.Expand();

                    // After opening a file we enable these buttons
                    newToolStripButton.Enabled = true;
                    newToolStripMenuItem.Enabled = true;
                    cutToolStripButton.Enabled = true;
                    removeToolStripMenuItem.Enabled = true;
                    saveToolStripButton.Enabled = true;
                    saveToolStripMenuItem.Enabled = true;
                    uploadtourl.Enabled = true;
                }
            }
        }

        public void UploadToURL()
        {
            string rh_address = (string)Settings.Default["rh_address"];
            string username = (string)Settings.Default["username"];
            string password = (string)Settings.Default["password"];
            string remote_dir = (string)Settings.Default["remote_directory"];

            if (rh_address == "" || username == "" || remote_dir == "") // no password -> ""
            {
                MessageBox.Show("Please check your remote host file editing settings.", "Error");
                mLogger.WriteLine(DateTime.Now.ToString("HH:mm:ss") + " - Please check your remote host file editing settings before trying to upload to remote host.");
                return;
            }
            else
            {
                DialogResult result = MessageBox.Show("Starting upload of files to remote host on " + rh_address, "Uploading to Remote",
                MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
                mLogger.WriteLine(DateTime.Now.ToString("HH:mm:ss") + " - Starting upload of files to remote host on " + rh_address);

                if (result == DialogResult.Cancel)
                    return;

                Directory.SetCurrentDirectory(workingdir);

                try
                {
                    SFTPManager.UploadRemoteFiles(rh_address, username, password, "./RemoteFiles/", remote_dir, "yaml");
                }
                catch (Exception exc)
                {
                    MessageBox.Show("Couldn't upload files to remote host.", "Error");
                    mLogger.WriteLine(DateTime.Now.ToString("HH:mm:ss") + " - Couldn't upload files to remote host.");
                    return;
                }

                result = MessageBox.Show("Upload complete.", "Success",
                MessageBoxButtons.OK, MessageBoxIcon.Information);
                mLogger.WriteLine(DateTime.Now.ToString("HH:mm:ss") + " - Upload complete.");
            }
        }

        private void PullFromRemote()
        {
            string gitrepo_link = (string)Settings.Default["gitrepo_link"];
            string gitrepo_email = (string)Settings.Default["gitrepo_email"];
            string gitrepo_password = (string)Settings.Default["gitrepo_password"];

            if (gitrepo_link == "" || gitrepo_email == "") // no password -> ""
            {
                MessageBox.Show("Please check your git settings.", "Error");
                mLogger.WriteLine(DateTime.Now.ToString("HH:mm:ss") + " - Please check your git settings before trying to pull from remote.");
                return;
            }
            else
            {
                DialogResult result = MessageBox.Show("Starting pull of files from remote host on " + gitrepo_link, "Pull from Remote",
                MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
                mLogger.WriteLine(DateTime.Now.ToString("HH:mm:ss") + " - Starting pull of files from remote host on " + gitrepo_link);

                if (result == DialogResult.Cancel)
                    return;

                Directory.SetCurrentDirectory(workingdir);

                if (Directory.Exists(workingdir + "/GitRepo/"))
                {
                    DirectoryInfo d = new DirectoryInfo(workingdir + "/GitRepo/");

                    foreach (FileInfo file in d.GetFiles())
                    {
                        file.Delete();
                    }
                    foreach (DirectoryInfo dir in d.GetDirectories())
                    {
                        setAttributesNormal(dir);
                        dir.Delete(true);
                    }
                }

                try
                {
                    GitRepoManager.clone(gitrepo_email, gitrepo_password, gitrepo_link, workingdir + "/GitRepo/");
                }
                catch (Exception exc)
                {
                    MessageBox.Show("Couldn't pull files from remote.", "Error");
                    mLogger.WriteLine(DateTime.Now.ToString("HH:mm:ss") + " - Couldn't pull files from remote.");
                    return;
                }

                result = MessageBox.Show("Pull complete. Open a file...", "Success",
                MessageBoxButtons.OK, MessageBoxIcon.Information);
                mLogger.WriteLine(DateTime.Now.ToString("HH:mm:ss") + " - Pull complete.");

                var dialog = new OpenFileDialog()
                { Filter = @"Yaml files (*.yaml)|*.yaml|All files (*.*)|*.*", DefaultExt = "yaml" };

                dialog.InitialDirectory = workingdir + "\\GitRepo\\";

                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    changedComponents = new Dictionary<Dictionary<string, List<IComponent>>, IComponent>();
                    addedComponents = new List<IComponent>();
                    if (FileTreeRoot != null)
                        FileTreeRoot.Nodes.Clear();
                    composite = new Component("root", "root", null);
                    currentParent = composite;

                    System.Diagnostics.Trace.WriteLine($"Filename: {dialog.FileName}");

                    Directory.SetCurrentDirectory(Path.GetDirectoryName(dialog.FileName) ?? "");

                    mainTreeView.Nodes.Clear();
                    FileTreeRoot = mainTreeView.Nodes.Add(Path.GetFileName(dialog.FileName));
                    FileTreeRoot.ImageIndex = FileTreeRoot.SelectedImageIndex = 3;

                    openedfilename = dialog.FileName;
                    var splits = openedfilename.Split('\\');
                    openedfilename = splits[splits.Length - 1];
                    filename = openedfilename;

                    mLogger.WriteLine(DateTime.Now.ToString("HH:mm:ss") + " - Opened " + $"Filename: " + filename + " from remote on " + gitrepo_link);

                    LoadFile(FileTreeRoot, filename);
                    FileTreeRoot.Expand();

                    // After opening a file we enable these buttons
                    newToolStripButton.Enabled = true;
                    newToolStripMenuItem.Enabled = true;
                    cutToolStripButton.Enabled = true;
                    removeToolStripMenuItem.Enabled = true;
                    saveToolStripButton.Enabled = true;
                    saveToolStripMenuItem.Enabled = true;
                    push_toolStrip.Enabled = true;
                }
            }
        }

        private void PushToRemote()
        {
            string gitrepo_link = (string)Settings.Default["gitrepo_link"];
            string gitrepo_email = (string)Settings.Default["gitrepo_email"];
            string gitrepo_password = (string)Settings.Default["gitrepo_password"];

            if (gitrepo_link == "" || gitrepo_email == "") // no password -> ""
            {
                MessageBox.Show("Please check your git settings.", "Error");
                mLogger.WriteLine(DateTime.Now.ToString("HH:mm:ss") + " - Please check your git settings before trying to push to remote.");
                return;
            }
            else
            {
                DialogResult result = MessageBox.Show("Starting push of files to remote on " + gitrepo_link, "Pushing to Remote",
                MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
                mLogger.WriteLine(DateTime.Now.ToString("HH:mm:ss") + " - Starting push of files to remote on " + gitrepo_link);

                if (result == DialogResult.Cancel)
                    return;

                Directory.SetCurrentDirectory(workingdir);

                try
                {
                    List<string> files_paths = Directory.EnumerateFiles(workingdir + "/GitRepo/").ToList();

                    GitRepoManager.commit(gitrepo_email, workingdir + "/GitRepo/", files_paths);
                    GitRepoManager.push(gitrepo_email, gitrepo_password, workingdir + "/GitRepo/");
                }
                catch (Exception exc)
                {
                    MessageBox.Show("Couldn't push files to remote.", "Error");
                    mLogger.WriteLine(DateTime.Now.ToString("HH:mm:ss") + " - Couldn't push files to remote.");
                    return;
                }

                result = MessageBox.Show("Push complete.", "Success",
                MessageBoxButtons.OK, MessageBoxIcon.Information);
                mLogger.WriteLine(DateTime.Now.ToString("HH:mm:ss") + " - Push complete.");
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

                    //if it's not another file, then add to tree
                    if (scalar.Tag != "!include")
                    {
                        node = root.Nodes[root.Nodes.Count - 1].Nodes.Add($"{scalar.Value}");
                        node.Tag = comp;
                        node.ImageIndex = node.SelectedImageIndex = GetImageIndex(scalar);
                    }

                    //if it's another file, then load that file
                    if (scalar.Tag == "!include")
                    {
                        var parentNode = node.Parent;
                        root.Nodes.Remove(node);
                        node = parentNode;
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

        private IDictionary<YamlNode, YamlNode> GetDataStructure(string filename)
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
                    //root.Name = aValue;
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

                /*if (component.Name.Equals(root.Name) || component.Name.Equals(treecomp.Name))
                {
                    //parents1.Remove(parents1.Last());
                    //YAMLEditorForm.changedComponents.Add(new Dictionary<string, List<IComponent>>() { { node.Name, parents1 } }, component);
                    //root.Name = aValue;
                    root.Text = aValue;
                    return;
                }*/

                if (component.Name.Equals(treecomp.Name))
                {
                    //parents1.Remove(parents1.Last());
                    //YAMLEditorForm.changedComponents.Add(new Dictionary<string, List<IComponent>>() { { node.Name, parents1 } }, component);
                    //root.Name = aValue;
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

        public static void FindTreeNode(IComponent component, TreeNode root)
        {

            if (root == null)
                root = FileTreeRoot;

            if (root.Tag == component || (root.Tag == null && component == composite))
            {
                parentNode = root;
                return;
            }

            foreach (TreeNode node in root.Nodes)
            {
                FindTreeNode(component, node);
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
                componentExists = true;
                return;
            }

            foreach (IComponent n in node.getChildren())
            {
                CheckIfComponentExists(n, component);
            }
        }

        public static Dictionary<IComponent, TreeNode> getComponentFromFile(string filename)
        {
            string content = File.ReadAllText(filename);

            if (content.Trim() != "" && !content.Trim().StartsWith("#"))
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

        public static void AddComponentToData(IComponent aComponent, TreeNode aTree, IComponent aParent)
        {
            aComponent.setParent(aParent);
            aParent.add(aComponent);
            addedComponents.Add(aComponent);
            //var parentNode = new TreeNode();
            FindTreeNode(aParent, null);
            parentNode.Nodes.Add(aTree);

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

        public void Save()
        {
            // For each component that was added we write it to the opened file
            foreach (IComponent comp in addedComponents)
            {
                List<string> lines = new List<string>();

                try
                {
                    lines = File.ReadAllLines(filename).ToList();
                }
                catch(IOException ioe)
                {
                    mLogger.WriteLine(DateTime.Now.ToString("HH:mm:ss") + " - Couldn't save added component " + comp.Name);
                    continue;
                }

                if (comp.getParent().Name == "root")
                {
                    lines.Add("");
                    lines.Add(comp.Name + ": !include " + comp.getFileName());
                }
                else
                {
                    List<IComponent> nodeparents = new List<IComponent>();
                    nodeparents = GetParents(nodeparents, comp);
                    nodeparents.Remove(nodeparents.Last());

                    string ident = "";

                    for(var i = 0; i < nodeparents.Count - 1; i++)
                    {
                        ident += "  ";
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


                    List<IComponent> siblings = new List<IComponent>();
                    siblings = GetAllChildren(siblings, comp.getParent());

                    if (siblings.Count == 1 && siblings.First().Name == "")
                    {
                        comp.getParent().getChildren().RemoveAt(0);
                        lines[ln] = lines[ln] + " !include " + comp.getFileName();
                        break;
                    }
                    else
                    {
                        // after we've got the parent's exact line
                        for (var i = ln + 1; i < lines.Count; i++)
                        {
                            string line_ident = "";

                            foreach(Char c in lines[i])
                            {
                                if (c.Equals(' '))
                                    line_ident += " ";
                                else if (c.Equals('#'))
                                {
                                    line_ident = "#";
                                    break;
                                }
                                else
                                    break;
                            }

                            if (line_ident == "#")
                                continue;

                            if (lines[i].Trim() == "")
                            {
                                lines.Insert(i, ident + "  " + comp.Name + ": !include " + comp.getFileName());
                                break;
                            }
                            else if (ident == line_ident && !lines[i].Trim().StartsWith("#"))
                            {
                                lines.Insert(i - 1, ident + "  " + comp.Name + ": !include " + comp.getFileName());
                                break;
                            }
                            else if(i == lines.Count)
                            {
                                lines.Add(ident + "  " + comp.Name + ": !include " + comp.getFileName());
                                break;
                            }
                        }
                    }
                }

                try
                {
                    File.WriteAllLines(filename, lines);
                }
                catch(IOException ioe)
                {
                    mLogger.WriteLine(DateTime.Now.ToString("HH:mm:ss") + " - Couldn't save added components");
                }
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
                    mLogger.WriteLine(DateTime.Now.ToString("HH:mm:ss") + " - Couldn't save changed component " + node.Name);
                    continue;
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

                try
                {
                    File.WriteAllLines(node.getFileName(), lines);
                }
                catch(IOException ioe)
                {
                    mLogger.WriteLine(DateTime.Now.ToString("HH:mm:ss") + " - Couldn't save changed components");
                }
            }

            // For each component that was removed we remove it from the opened file
            foreach (KeyValuePair<IComponent, List<IComponent>> comp in removedComponents)
            {
                var nodeparents = comp.Value;
                var node = comp.Key;

                List<string> lines = new List<string>();

                try
                {
                    lines = File.ReadAllLines(node.getFileName()).ToList();
                }
                catch(IOException ioe)
                {
                    mLogger.WriteLine(DateTime.Now.ToString("HH:mm:ss") + " - Couldn't save removed component " + node.Name);
                    continue;
                }

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

                try
                {
                    File.WriteAllLines(node.getFileName(), lines);
                }
                catch(IOException ioe)
                {
                    mLogger.WriteLine(DateTime.Now.ToString("HH:mm:ss") + " - Couldn't save removed components");
                }
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

        public static void WriteToTextBox(string aMessage)
        {
            mLogger.WriteLine(aMessage);
        }

        public static void AddComponent(IComponent aParent, string aFilename)
        {
            Dictionary<IComponent, TreeNode> fileRoot = YAMLEditorForm.getComponentFromFile(aFilename);
            var splits = aFilename.Split('\\');
            var name = splits[splits.Length - 1];

            if (fileRoot != null)
            {
                fileRoot.Keys.First().setFileName(name);

                //Check if this component already exists where we are trying to add
                YAMLEditorForm.componentExists = false;
                YAMLEditorForm.CheckIfComponentExists(aParent, fileRoot.Keys.First().getChild(0));

                if (YAMLEditorForm.componentExists)
                {
                    MessageBox.Show(fileRoot.Keys.First().getChild(0).Name + " already exists under " + aParent.Name + ".", "Error");
                }
                else
                {
                    MessageBox.Show(fileRoot.Keys.First().getChild(0).Name + " added successfully under " + aParent.Name + ".", "Success");
                    AddComponentToData(fileRoot.Keys.First().getChild(0), fileRoot.Values.First().Nodes[0], aParent);
                }
                YAMLEditorForm.componentExists = false;
            }
        }

        private void setAttributesNormal(DirectoryInfo dir)
        {
            foreach (var subDir in dir.GetDirectories())
            {
                setAttributesNormal(subDir);
                subDir.Attributes = FileAttributes.Normal;
            }
            foreach (var file in dir.GetFiles())
            {
                file.Attributes = FileAttributes.Normal;
            }
        }
    }
}