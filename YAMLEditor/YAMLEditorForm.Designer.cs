namespace YAMLEditor
{
    partial class YAMLEditorForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose( bool disposing )
        {
            if ( disposing && ( components != null ) )
            {
                components.Dispose();
            }
            base.Dispose( disposing );
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(YAMLEditorForm));
            this.mainMenuStrip = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.newComponentMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripOpenFromURL = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripUploadToRemote = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripPullFromRemote = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripPushToRemote = new System.Windows.Forms.ToolStripMenuItem();
            this.saveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.editToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.undoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.redoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.removeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.optionsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.mainToolStrip = new System.Windows.Forms.ToolStrip();
            this.newComponentButton = new System.Windows.Forms.ToolStripButton();
            this.openToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.openfromurl = new System.Windows.Forms.ToolStripButton();
            this.uploadtourl = new System.Windows.Forms.ToolStripButton();
            this.pull_toolStrip = new System.Windows.Forms.ToolStripButton();
            this.push_toolStrip = new System.Windows.Forms.ToolStripButton();
            this.saveToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.removeToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton1 = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton2 = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton3 = new System.Windows.Forms.ToolStripButton();
            this.mainSplitContainer = new System.Windows.Forms.SplitContainer();
            this.mainTreeView = new System.Windows.Forms.TreeView();
            this.mainImageList = new System.Windows.Forms.ImageList(this.components);
            this.mainTabControl = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.mainPropertyGrid = new System.Windows.Forms.PropertyGrid();
            this.helpTabPage = new System.Windows.Forms.TabPage();
            this.webBrowser = new System.Windows.Forms.WebBrowser();
            this.mainTextBox = new System.Windows.Forms.TextBox();
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this.mainMenuStrip.SuspendLayout();
            this.mainToolStrip.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.mainSplitContainer)).BeginInit();
            this.mainSplitContainer.Panel1.SuspendLayout();
            this.mainSplitContainer.Panel2.SuspendLayout();
            this.mainSplitContainer.SuspendLayout();
            this.mainTabControl.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.helpTabPage.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).BeginInit();
            this.splitContainer2.Panel1.SuspendLayout();
            this.splitContainer2.Panel2.SuspendLayout();
            this.splitContainer2.SuspendLayout();
            this.SuspendLayout();
            // 
            // mainMenuStrip
            // 
            this.mainMenuStrip.BackColor = System.Drawing.SystemColors.ControlDark;
            this.mainMenuStrip.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.mainMenuStrip.ImageScalingSize = new System.Drawing.Size(28, 28);
            this.mainMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.editToolStripMenuItem,
            this.toolsToolStripMenuItem,
            this.helpToolStripMenuItem});
            this.mainMenuStrip.Location = new System.Drawing.Point(0, 0);
            this.mainMenuStrip.Name = "mainMenuStrip";
            this.mainMenuStrip.Padding = new System.Windows.Forms.Padding(2, 1, 0, 1);
            this.mainMenuStrip.Size = new System.Drawing.Size(754, 25);
            this.mainMenuStrip.TabIndex = 0;
            this.mainMenuStrip.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.newComponentMenuItem,
            this.openToolStripMenuItem,
            this.toolStripOpenFromURL,
            this.toolStripUploadToRemote,
            this.toolStripPullFromRemote,
            this.toolStripPushToRemote,
            this.saveToolStripMenuItem,
            this.exitToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(41, 23);
            this.fileToolStripMenuItem.Text = "&File";
            // 
            // newComponentMenuItem
            // 
            this.newComponentMenuItem.BackColor = System.Drawing.SystemColors.ControlDark;
            this.newComponentMenuItem.Enabled = false;
            this.newComponentMenuItem.Image = global::YAMLEditor.Properties.Resources.add_new_file;
            this.newComponentMenuItem.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.newComponentMenuItem.Name = "newComponentMenuItem";
            this.newComponentMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.N)));
            this.newComponentMenuItem.Size = new System.Drawing.Size(199, 34);
            this.newComponentMenuItem.Text = "&New";
            this.newComponentMenuItem.Click += new System.EventHandler(this.OnNewComponent);
            // 
            // openToolStripMenuItem
            // 
            this.openToolStripMenuItem.BackColor = System.Drawing.SystemColors.ControlDark;
            this.openToolStripMenuItem.Image = global::YAMLEditor.Properties.Resources.open_folder_outline;
            this.openToolStripMenuItem.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.openToolStripMenuItem.Name = "openToolStripMenuItem";
            this.openToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.O)));
            this.openToolStripMenuItem.Size = new System.Drawing.Size(199, 34);
            this.openToolStripMenuItem.Text = "&Open";
            this.openToolStripMenuItem.Click += new System.EventHandler(this.OnOpen);
            // 
            // toolStripOpenFromURL
            // 
            this.toolStripOpenFromURL.BackColor = System.Drawing.SystemColors.ControlDark;
            this.toolStripOpenFromURL.Image = global::YAMLEditor.Properties.Resources.download;
            this.toolStripOpenFromURL.Name = "toolStripOpenFromURL";
            this.toolStripOpenFromURL.Size = new System.Drawing.Size(199, 34);
            this.toolStripOpenFromURL.Text = "Open from URL";
            this.toolStripOpenFromURL.Click += new System.EventHandler(this.OnToolStripOpenFromURL);
            // 
            // toolStripUploadToRemote
            // 
            this.toolStripUploadToRemote.BackColor = System.Drawing.SystemColors.ControlDark;
            this.toolStripUploadToRemote.Image = global::YAMLEditor.Properties.Resources.upload;
            this.toolStripUploadToRemote.Name = "toolStripUploadToRemote";
            this.toolStripUploadToRemote.Size = new System.Drawing.Size(199, 34);
            this.toolStripUploadToRemote.Text = "Upload to remote";
            this.toolStripUploadToRemote.Click += new System.EventHandler(this.OnToolStripUploadToRemote);
            // 
            // toolStripPullFromRemote
            // 
            this.toolStripPullFromRemote.BackColor = System.Drawing.SystemColors.ControlDark;
            this.toolStripPullFromRemote.Image = global::YAMLEditor.Properties.Resources.pullremote;
            this.toolStripPullFromRemote.Name = "toolStripPullFromRemote";
            this.toolStripPullFromRemote.Size = new System.Drawing.Size(199, 34);
            this.toolStripPullFromRemote.Text = "Pull from remote";
            this.toolStripPullFromRemote.Click += new System.EventHandler(this.OnToolStripPullFromRemote);
            // 
            // toolStripPushToRemote
            // 
            this.toolStripPushToRemote.BackColor = System.Drawing.SystemColors.ControlDark;
            this.toolStripPushToRemote.Image = global::YAMLEditor.Properties.Resources.pushremote;
            this.toolStripPushToRemote.Name = "toolStripPushToRemote";
            this.toolStripPushToRemote.Size = new System.Drawing.Size(199, 34);
            this.toolStripPushToRemote.Text = "Push to remote";
            this.toolStripPushToRemote.Click += new System.EventHandler(this.OnToolStripPushToRemote);
            // 
            // saveToolStripMenuItem
            // 
            this.saveToolStripMenuItem.BackColor = System.Drawing.SystemColors.ControlDark;
            this.saveToolStripMenuItem.Enabled = false;
            this.saveToolStripMenuItem.Image = global::YAMLEditor.Properties.Resources.floppy_disk_save_button;
            this.saveToolStripMenuItem.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.saveToolStripMenuItem.Name = "saveToolStripMenuItem";
            this.saveToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.S)));
            this.saveToolStripMenuItem.Size = new System.Drawing.Size(199, 34);
            this.saveToolStripMenuItem.Text = "&Save";
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.BackColor = System.Drawing.SystemColors.ControlDark;
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(199, 34);
            this.exitToolStripMenuItem.Text = "E&xit";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.OnExit);
            // 
            // editToolStripMenuItem
            // 
            this.editToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.undoToolStripMenuItem,
            this.redoToolStripMenuItem,
            this.removeToolStripMenuItem});
            this.editToolStripMenuItem.Name = "editToolStripMenuItem";
            this.editToolStripMenuItem.Size = new System.Drawing.Size(44, 23);
            this.editToolStripMenuItem.Text = "&Edit";
            // 
            // undoToolStripMenuItem
            // 
            this.undoToolStripMenuItem.BackColor = System.Drawing.SystemColors.ControlDark;
            this.undoToolStripMenuItem.Image = global::YAMLEditor.Properties.Resources.undo;
            this.undoToolStripMenuItem.Name = "undoToolStripMenuItem";
            this.undoToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Z)));
            this.undoToolStripMenuItem.Size = new System.Drawing.Size(188, 34);
            this.undoToolStripMenuItem.Text = "&Undo";
            // 
            // redoToolStripMenuItem
            // 
            this.redoToolStripMenuItem.BackColor = System.Drawing.SystemColors.ControlDark;
            this.redoToolStripMenuItem.Image = global::YAMLEditor.Properties.Resources.redo;
            this.redoToolStripMenuItem.Name = "redoToolStripMenuItem";
            this.redoToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Y)));
            this.redoToolStripMenuItem.Size = new System.Drawing.Size(188, 34);
            this.redoToolStripMenuItem.Text = "&Redo";
            // 
            // removeToolStripMenuItem
            // 
            this.removeToolStripMenuItem.BackColor = System.Drawing.SystemColors.ControlDark;
            this.removeToolStripMenuItem.Enabled = false;
            this.removeToolStripMenuItem.Image = global::YAMLEditor.Properties.Resources.rubbish_bin;
            this.removeToolStripMenuItem.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.removeToolStripMenuItem.Name = "removeToolStripMenuItem";
            this.removeToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.X)));
            this.removeToolStripMenuItem.Size = new System.Drawing.Size(188, 34);
            this.removeToolStripMenuItem.Text = "Remove";
            // 
            // toolsToolStripMenuItem
            // 
            this.toolsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.optionsToolStripMenuItem});
            this.toolsToolStripMenuItem.Name = "toolsToolStripMenuItem";
            this.toolsToolStripMenuItem.Size = new System.Drawing.Size(52, 23);
            this.toolsToolStripMenuItem.Text = "&Tools";
            // 
            // optionsToolStripMenuItem
            // 
            this.optionsToolStripMenuItem.BackColor = System.Drawing.SystemColors.ControlDark;
            this.optionsToolStripMenuItem.Name = "optionsToolStripMenuItem";
            this.optionsToolStripMenuItem.Size = new System.Drawing.Size(127, 24);
            this.optionsToolStripMenuItem.Text = "&Options";
            this.optionsToolStripMenuItem.Click += new System.EventHandler(this.OnOptionsToolStripMenuItem_Click);
            // 
            // helpToolStripMenuItem
            // 
            this.helpToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.aboutToolStripMenuItem});
            this.helpToolStripMenuItem.Name = "helpToolStripMenuItem";
            this.helpToolStripMenuItem.Size = new System.Drawing.Size(49, 23);
            this.helpToolStripMenuItem.Text = "&Help";
            // 
            // aboutToolStripMenuItem
            // 
            this.aboutToolStripMenuItem.BackColor = System.Drawing.SystemColors.ControlDark;
            this.aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
            this.aboutToolStripMenuItem.Size = new System.Drawing.Size(125, 24);
            this.aboutToolStripMenuItem.Text = "&About...";
            this.aboutToolStripMenuItem.Click += new System.EventHandler(this.OnAboutButton);
            // 
            // mainToolStrip
            // 
            this.mainToolStrip.BackColor = System.Drawing.SystemColors.ButtonShadow;
            this.mainToolStrip.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.mainToolStrip.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.mainToolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.newComponentButton,
            this.openToolStripButton,
            this.openfromurl,
            this.uploadtourl,
            this.pull_toolStrip,
            this.push_toolStrip,
            this.saveToolStripButton,
            this.removeToolStripButton,
            this.toolStripButton1,
            this.toolStripButton2,
            this.toolStripButton3});
            this.mainToolStrip.Location = new System.Drawing.Point(0, 25);
            this.mainToolStrip.Name = "mainToolStrip";
            this.mainToolStrip.Size = new System.Drawing.Size(754, 27);
            this.mainToolStrip.TabIndex = 2;
            this.mainToolStrip.Text = "toolStrip";
            // 
            // newComponentButton
            // 
            this.newComponentButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.newComponentButton.Enabled = false;
            this.newComponentButton.Image = global::YAMLEditor.Properties.Resources.add_new_file;
            this.newComponentButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.newComponentButton.Name = "newComponentButton";
            this.newComponentButton.Size = new System.Drawing.Size(24, 24);
            this.newComponentButton.Text = "&New";
            this.newComponentButton.Click += new System.EventHandler(this.OnNewComponent);
            // 
            // openToolStripButton
            // 
            this.openToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.openToolStripButton.Image = global::YAMLEditor.Properties.Resources.open_folder_outline;
            this.openToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.openToolStripButton.Name = "openToolStripButton";
            this.openToolStripButton.Size = new System.Drawing.Size(24, 24);
            this.openToolStripButton.Text = "&Open";
            this.openToolStripButton.Click += new System.EventHandler(this.OnOpen);
            // 
            // openfromurl
            // 
            this.openfromurl.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.openfromurl.Image = global::YAMLEditor.Properties.Resources.download;
            this.openfromurl.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.openfromurl.Name = "openfromurl";
            this.openfromurl.Size = new System.Drawing.Size(24, 24);
            this.openfromurl.Text = "&Open From URL";
            this.openfromurl.ToolTipText = "Open From URL";
            this.openfromurl.Click += new System.EventHandler(this.OnOpenFromURL);
            // 
            // uploadtourl
            // 
            this.uploadtourl.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.uploadtourl.Enabled = false;
            this.uploadtourl.Image = global::YAMLEditor.Properties.Resources.upload;
            this.uploadtourl.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.uploadtourl.Name = "uploadtourl";
            this.uploadtourl.Size = new System.Drawing.Size(24, 24);
            this.uploadtourl.Text = "&Upload To URL";
            this.uploadtourl.Click += new System.EventHandler(this.OnUploadToURL);
            // 
            // pull_toolStrip
            // 
            this.pull_toolStrip.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.pull_toolStrip.Image = global::YAMLEditor.Properties.Resources.pullremote;
            this.pull_toolStrip.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.pull_toolStrip.Name = "pull_toolStrip";
            this.pull_toolStrip.Size = new System.Drawing.Size(24, 24);
            this.pull_toolStrip.Text = "Pull from Remote";
            this.pull_toolStrip.Click += new System.EventHandler(this.OnPullFromRemote);
            // 
            // push_toolStrip
            // 
            this.push_toolStrip.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.push_toolStrip.Enabled = false;
            this.push_toolStrip.Image = global::YAMLEditor.Properties.Resources.pushremote;
            this.push_toolStrip.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.push_toolStrip.Name = "push_toolStrip";
            this.push_toolStrip.Size = new System.Drawing.Size(24, 24);
            this.push_toolStrip.Text = "Push to Remote";
            this.push_toolStrip.Click += new System.EventHandler(this.OnPushToRemote);
            // 
            // saveToolStripButton
            // 
            this.saveToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.saveToolStripButton.Enabled = false;
            this.saveToolStripButton.Image = global::YAMLEditor.Properties.Resources.floppy_disk_save_button;
            this.saveToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.saveToolStripButton.Name = "saveToolStripButton";
            this.saveToolStripButton.Size = new System.Drawing.Size(24, 24);
            this.saveToolStripButton.Text = "&Save";
            this.saveToolStripButton.Click += new System.EventHandler(this.OnSaveButton);
            // 
            // removeToolStripButton
            // 
            this.removeToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.removeToolStripButton.Enabled = false;
            this.removeToolStripButton.Image = global::YAMLEditor.Properties.Resources.rubbish_bin;
            this.removeToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.removeToolStripButton.Name = "removeToolStripButton";
            this.removeToolStripButton.Size = new System.Drawing.Size(24, 24);
            this.removeToolStripButton.Text = "C&ut";
            this.removeToolStripButton.ToolTipText = "Remove";
            this.removeToolStripButton.Click += new System.EventHandler(this.OnRemoveComponent);
            // 
            // toolStripButton1
            // 
            this.toolStripButton1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton1.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton1.Image")));
            this.toolStripButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton1.Name = "toolStripButton1";
            this.toolStripButton1.Size = new System.Drawing.Size(24, 24);
            this.toolStripButton1.Text = "Undo";
            this.toolStripButton1.Click += new System.EventHandler(this.OnUndo);
            // 
            // toolStripButton2
            // 
            this.toolStripButton2.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton2.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton2.Image")));
            this.toolStripButton2.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton2.Name = "toolStripButton2";
            this.toolStripButton2.Size = new System.Drawing.Size(24, 24);
            this.toolStripButton2.Text = "Redo";
            this.toolStripButton2.Click += new System.EventHandler(this.OnRedo);
            // 
            // toolStripButton3
            // 
            this.toolStripButton3.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton3.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton3.Image")));
            this.toolStripButton3.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton3.Name = "toolStripButton3";
            this.toolStripButton3.Size = new System.Drawing.Size(24, 24);
            this.toolStripButton3.Text = "toolStripButton3";
            this.toolStripButton3.ToolTipText = "Restart Home Assistant";
            this.toolStripButton3.Click += new System.EventHandler(this.OnRestartHomeassistant);
            // 
            // mainSplitContainer
            // 
            this.mainSplitContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mainSplitContainer.Location = new System.Drawing.Point(0, 0);
            this.mainSplitContainer.Margin = new System.Windows.Forms.Padding(2);
            this.mainSplitContainer.Name = "mainSplitContainer";
            // 
            // mainSplitContainer.Panel1
            // 
            this.mainSplitContainer.Panel1.Controls.Add(this.mainTreeView);
            // 
            // mainSplitContainer.Panel2
            // 
            this.mainSplitContainer.Panel2.Controls.Add(this.mainTabControl);
            this.mainSplitContainer.Size = new System.Drawing.Size(754, 457);
            this.mainSplitContainer.SplitterDistance = 352;
            this.mainSplitContainer.SplitterWidth = 2;
            this.mainSplitContainer.TabIndex = 3;
            // 
            // mainTreeView
            // 
            this.mainTreeView.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.mainTreeView.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.mainTreeView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mainTreeView.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.mainTreeView.ForeColor = System.Drawing.SystemColors.ControlLight;
            this.mainTreeView.FullRowSelect = true;
            this.mainTreeView.ImageIndex = 0;
            this.mainTreeView.ImageList = this.mainImageList;
            this.mainTreeView.Location = new System.Drawing.Point(0, 0);
            this.mainTreeView.Margin = new System.Windows.Forms.Padding(2);
            this.mainTreeView.Name = "mainTreeView";
            this.mainTreeView.SelectedImageIndex = 0;
            this.mainTreeView.Size = new System.Drawing.Size(352, 457);
            this.mainTreeView.TabIndex = 0;
            this.mainTreeView.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.OnAfterSelect);
            // 
            // mainImageList
            // 
            this.mainImageList.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("mainImageList.ImageStream")));
            this.mainImageList.TransparentColor = System.Drawing.Color.Transparent;
            this.mainImageList.Images.SetKeyName(0, "tag_blue.png");
            this.mainImageList.Images.SetKeyName(1, "page_white_link.png");
            this.mainImageList.Images.SetKeyName(2, "lock.png");
            this.mainImageList.Images.SetKeyName(3, "package.png");
            this.mainImageList.Images.SetKeyName(4, "brick.png");
            this.mainImageList.Images.SetKeyName(5, "brick_go.png");
            this.mainImageList.Images.SetKeyName(6, "");
            // 
            // mainTabControl
            // 
            this.mainTabControl.Controls.Add(this.tabPage1);
            this.mainTabControl.Controls.Add(this.helpTabPage);
            this.mainTabControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mainTabControl.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.mainTabControl.Location = new System.Drawing.Point(0, 0);
            this.mainTabControl.Margin = new System.Windows.Forms.Padding(2);
            this.mainTabControl.Name = "mainTabControl";
            this.mainTabControl.SelectedIndex = 0;
            this.mainTabControl.Size = new System.Drawing.Size(400, 457);
            this.mainTabControl.TabIndex = 1;
            // 
            // tabPage1
            // 
            this.tabPage1.BackColor = System.Drawing.Color.DimGray;
            this.tabPage1.Controls.Add(this.mainPropertyGrid);
            this.tabPage1.Location = new System.Drawing.Point(4, 25);
            this.tabPage1.Margin = new System.Windows.Forms.Padding(2);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(2);
            this.tabPage1.Size = new System.Drawing.Size(392, 428);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Properties";
            // 
            // mainPropertyGrid
            // 
            this.mainPropertyGrid.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.mainPropertyGrid.CategorySplitterColor = System.Drawing.SystemColors.ControlDarkDark;
            this.mainPropertyGrid.DisabledItemForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.mainPropertyGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mainPropertyGrid.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.mainPropertyGrid.HelpBackColor = System.Drawing.SystemColors.ControlDark;
            this.mainPropertyGrid.LineColor = System.Drawing.SystemColors.ControlDark;
            this.mainPropertyGrid.Location = new System.Drawing.Point(2, 2);
            this.mainPropertyGrid.Margin = new System.Windows.Forms.Padding(2);
            this.mainPropertyGrid.Name = "mainPropertyGrid";
            this.mainPropertyGrid.SelectedItemWithFocusBackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.mainPropertyGrid.SelectedItemWithFocusForeColor = System.Drawing.SystemColors.ControlDark;
            this.mainPropertyGrid.Size = new System.Drawing.Size(388, 424);
            this.mainPropertyGrid.TabIndex = 0;
            this.mainPropertyGrid.ViewBackColor = System.Drawing.SystemColors.ControlDark;
            this.mainPropertyGrid.ViewBorderColor = System.Drawing.SystemColors.ControlDarkDark;
            // 
            // helpTabPage
            // 
            this.helpTabPage.Controls.Add(this.webBrowser);
            this.helpTabPage.Location = new System.Drawing.Point(4, 25);
            this.helpTabPage.Margin = new System.Windows.Forms.Padding(2);
            this.helpTabPage.Name = "helpTabPage";
            this.helpTabPage.Padding = new System.Windows.Forms.Padding(2);
            this.helpTabPage.Size = new System.Drawing.Size(310, 298);
            this.helpTabPage.TabIndex = 1;
            this.helpTabPage.Text = "Help";
            this.helpTabPage.UseVisualStyleBackColor = true;
            // 
            // webBrowser
            // 
            this.webBrowser.Dock = System.Windows.Forms.DockStyle.Fill;
            this.webBrowser.Location = new System.Drawing.Point(2, 2);
            this.webBrowser.Margin = new System.Windows.Forms.Padding(2);
            this.webBrowser.MinimumSize = new System.Drawing.Size(8, 8);
            this.webBrowser.Name = "webBrowser";
            this.webBrowser.Size = new System.Drawing.Size(306, 294);
            this.webBrowser.TabIndex = 0;
            // 
            // mainTextBox
            // 
            this.mainTextBox.BackColor = System.Drawing.SystemColors.ControlDark;
            this.mainTextBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.mainTextBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mainTextBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.mainTextBox.Location = new System.Drawing.Point(0, 0);
            this.mainTextBox.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.mainTextBox.Multiline = true;
            this.mainTextBox.Name = "mainTextBox";
            this.mainTextBox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.mainTextBox.Size = new System.Drawing.Size(754, 119);
            this.mainTextBox.TabIndex = 4;
            // 
            // splitContainer2
            // 
            this.splitContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer2.Location = new System.Drawing.Point(0, 52);
            this.splitContainer2.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.splitContainer2.Name = "splitContainer2";
            this.splitContainer2.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer2.Panel1
            // 
            this.splitContainer2.Panel1.Controls.Add(this.mainSplitContainer);
            // 
            // splitContainer2.Panel2
            // 
            this.splitContainer2.Panel2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.splitContainer2.Panel2.Controls.Add(this.mainTextBox);
            this.splitContainer2.Size = new System.Drawing.Size(754, 579);
            this.splitContainer2.SplitterDistance = 457;
            this.splitContainer2.SplitterWidth = 3;
            this.splitContainer2.TabIndex = 4;
            // 
            // YAMLEditorForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.ClientSize = new System.Drawing.Size(754, 631);
            this.Controls.Add(this.splitContainer2);
            this.Controls.Add(this.mainToolStrip);
            this.Controls.Add(this.mainMenuStrip);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.mainMenuStrip;
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "YAMLEditorForm";
            this.Text = "YAML Editor";
            this.mainMenuStrip.ResumeLayout(false);
            this.mainMenuStrip.PerformLayout();
            this.mainToolStrip.ResumeLayout(false);
            this.mainToolStrip.PerformLayout();
            this.mainSplitContainer.Panel1.ResumeLayout(false);
            this.mainSplitContainer.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.mainSplitContainer)).EndInit();
            this.mainSplitContainer.ResumeLayout(false);
            this.mainTabControl.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.helpTabPage.ResumeLayout(false);
            this.splitContainer2.Panel1.ResumeLayout(false);
            this.splitContainer2.Panel2.ResumeLayout(false);
            this.splitContainer2.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).EndInit();
            this.splitContainer2.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip mainMenuStrip;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem newComponentMenuItem;
        private System.Windows.Forms.ToolStripMenuItem openToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem editToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem undoToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem redoToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem removeToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem toolsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem optionsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem helpToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem aboutToolStripMenuItem;
        private System.Windows.Forms.ToolStrip mainToolStrip;
        private System.Windows.Forms.ToolStripButton newComponentButton;
        private System.Windows.Forms.ToolStripButton openToolStripButton;
        private System.Windows.Forms.ToolStripButton saveToolStripButton;
        private System.Windows.Forms.ToolStripButton removeToolStripButton;
        private System.Windows.Forms.SplitContainer mainSplitContainer;
        private System.Windows.Forms.PropertyGrid mainPropertyGrid;
        private System.Windows.Forms.ImageList mainImageList;
        private System.Windows.Forms.TabControl mainTabControl;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage helpTabPage;
        private System.Windows.Forms.WebBrowser webBrowser;
        private System.Windows.Forms.TextBox mainTextBox;
		private System.Windows.Forms.ToolStripButton toolStripButton1;
		private System.Windows.Forms.ToolStripButton toolStripButton2;
        private System.Windows.Forms.SplitContainer splitContainer2;
        private System.Windows.Forms.TreeView mainTreeView;
        private System.Windows.Forms.ToolStripButton toolStripButton3;
        private System.Windows.Forms.ToolStripButton openfromurl;
        private System.Windows.Forms.ToolStripButton uploadtourl;
        private System.Windows.Forms.ToolStripMenuItem toolStripOpenFromURL;
        private System.Windows.Forms.ToolStripMenuItem toolStripUploadToRemote;
        private System.Windows.Forms.ToolStripButton pull_toolStrip;
        private System.Windows.Forms.ToolStripButton push_toolStrip;
        private System.Windows.Forms.ToolStripMenuItem toolStripPullFromRemote;
        private System.Windows.Forms.ToolStripMenuItem toolStripPushToRemote;
    }
}

