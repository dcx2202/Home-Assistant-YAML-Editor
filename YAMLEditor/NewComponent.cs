using System;
using System.Windows.Forms;

namespace YAMLEditor
{
    public partial class NewComponent : Form
	{
		public string filename;
		public string openedfilename;
        public IComponent mParent;

		public NewComponent(IComponent parent)
		{
			InitializeComponent();
            this.mParent = parent;
        }

		/// <summary>
		/// Allows the user to choose the desired yaml file to add it to the selected node
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void FileExplorer(object sender, EventArgs e)
		{
			var dialog = new OpenFileDialog()
			{ Filter = @"Yaml files (*.yaml)|*.yaml|All files (*.*)|*.*", DefaultExt = "yaml" };
			if (dialog.ShowDialog() == DialogResult.OK)
			{
				openedfilename = dialog.FileName;
				filename = openedfilename;
				File.Text = filename;
			}
		}

		private void CloseButton(object sender, EventArgs e)
		{
			this.Close();
		}

		/// <summary>
		/// Redirects the user to the HomeAssistant web page, where they can find new components
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void LinkButton(object sender, LinkLabelLinkClickedEventArgs e)
		{
			System.Diagnostics.Process.Start("https://www.home-assistant.io/components/");
		}

		/// <summary>
		/// Adds the chosen component to the previously selected node
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
        private void OkButton(object sender, EventArgs e)
        {
            YAMLEditorForm.AddComponent(mParent, filename);
            this.Close();
        }
    }
}
