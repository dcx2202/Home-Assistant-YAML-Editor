using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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

		private void LinkButton(object sender, LinkLabelLinkClickedEventArgs e)
		{
			System.Diagnostics.Process.Start("https://www.home-assistant.io/components/");
		}

        private void OkButton(object sender, EventArgs e)
        {
            YAMLEditorForm.AddComponent(mParent, filename);
            this.Close();
        }
    }
}
