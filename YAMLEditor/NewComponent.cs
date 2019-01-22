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

		public NewComponent()
		{
			InitializeComponent();
		}

		private void pictureBox1_Click(object sender, EventArgs e)
		{

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
            Dictionary<IComponent, TreeNode> component = YAMLEditorForm.getComponentFromFile(filename);
            var splits = filename.Split('\\');
            var name = splits[splits.Length - 1];

            component.Keys.First().setFileName(name);

            if (component != null)
            {
                YAMLEditorForm.CheckIfComponentExists(null, component.Keys.First().getChild(0));

                if (YAMLEditorForm.componentExists)
                {
                    // Show error popup
                    MessageBox.Show("This component already exists in the file currently open.", "Error");
                }
                else
                {
                    // Add component to main composite/tree
                    YAMLEditorForm.AddComponent(component.Keys.First().getChild(0), component.Values.First().Nodes[0]);
                    MessageBox.Show("Component added.", "Success");

                    YAMLEditorForm.componentExists = false;
                    this.Close();
                }
            }

            YAMLEditorForm.componentExists = false;
        }
    }
}
