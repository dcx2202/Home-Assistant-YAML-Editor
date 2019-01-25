using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using YAMLEditor.Properties;

namespace YAMLEditor
{
    class OptionsWindow : Form
    {
		public OptionsWindow()
		{
			InitializeComponent();
		}

		#region ...
		private Label ha_address;
        private TextBox ha_address_field;
        private Label access_token_field;
        private Panel panel1;
        private Label label1;
        private Label WindowTitle;
        private Label rh_address;
        private TextBox rh_address_field;
        private Label username_label;
        private TextBox username_field;
        private TextBox password_field;
        private Label password_label;
        private Label label3;
        private Label label4;
        private TextBox files_directory_field;
        private Label files_directory_label;
        private Label gitrepo_label;
        private TextBox git_password_field;
        private Label gitpassword_label;
        private TextBox git_email_field;
        private Label gitemail_label;
        private TextBox gitrepo_link_field;
        private Label gitrepolink_label;
        private TextBox token_field;

        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(OptionsWindow));
            this.ha_address = new System.Windows.Forms.Label();
            this.ha_address_field = new System.Windows.Forms.TextBox();
            this.access_token_field = new System.Windows.Forms.Label();
            this.token_field = new System.Windows.Forms.TextBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.WindowTitle = new System.Windows.Forms.Label();
            this.rh_address = new System.Windows.Forms.Label();
            this.rh_address_field = new System.Windows.Forms.TextBox();
            this.username_label = new System.Windows.Forms.Label();
            this.username_field = new System.Windows.Forms.TextBox();
            this.password_field = new System.Windows.Forms.TextBox();
            this.password_label = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.files_directory_field = new System.Windows.Forms.TextBox();
            this.files_directory_label = new System.Windows.Forms.Label();
            this.gitrepo_label = new System.Windows.Forms.Label();
            this.git_password_field = new System.Windows.Forms.TextBox();
            this.gitpassword_label = new System.Windows.Forms.Label();
            this.git_email_field = new System.Windows.Forms.TextBox();
            this.gitemail_label = new System.Windows.Forms.Label();
            this.gitrepo_link_field = new System.Windows.Forms.TextBox();
            this.gitrepolink_label = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // ha_address
            // 
            this.ha_address.AutoSize = true;
            this.ha_address.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ha_address.Location = new System.Drawing.Point(11, 70);
            this.ha_address.Name = "ha_address";
            this.ha_address.Size = new System.Drawing.Size(181, 20);
            this.ha_address.TabIndex = 0;
            this.ha_address.Text = "Home assistant address";
            // 
            // ha_address_field
            // 
            this.ha_address_field.BackColor = System.Drawing.SystemColors.ControlDark;
            this.ha_address_field.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.ha_address_field.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ha_address_field.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(120)))), ((int)(((byte)(120)))), ((int)(((byte)(120)))));
            this.ha_address_field.Location = new System.Drawing.Point(198, 71);
            this.ha_address_field.Name = "ha_address_field";
            this.ha_address_field.Size = new System.Drawing.Size(180, 22);
            this.ha_address_field.TabIndex = 1;
            this.ha_address_field.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.ha_address_field.TextChanged += new System.EventHandler(this.ha_address_field_TextChanged);
            this.ha_address_field.Enter += new System.EventHandler(this.ha_address_field_Enter);
            this.ha_address_field.Leave += new System.EventHandler(this.ha_address_field_Leave);
            if (Settings.Default["ha_address"] as string == "")
                this.ha_address_field.Text = "ipaddress:port";
            else
            {
                this.ha_address_field.Text = Settings.Default["ha_address"] as string;
                this.ha_address_field.ForeColor = Color.Black;
            }
            // 
            // access_token_field
            // 
            this.access_token_field.AutoSize = true;
            this.access_token_field.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.access_token_field.Location = new System.Drawing.Point(11, 90);
            this.access_token_field.Name = "access_token_field";
            this.access_token_field.Size = new System.Drawing.Size(109, 20);
            this.access_token_field.TabIndex = 2;
            this.access_token_field.Text = "Access Token";
            // 
            // token_field
            // 
            this.token_field.BackColor = System.Drawing.SystemColors.ControlDark;
            this.token_field.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.token_field.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.token_field.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(120)))), ((int)(((byte)(120)))), ((int)(((byte)(120)))));
            this.token_field.Location = new System.Drawing.Point(15, 113);
            this.token_field.Name = "token_field";
            this.token_field.Size = new System.Drawing.Size(363, 22);
            this.token_field.TabIndex = 3;
            this.token_field.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.token_field.TextChanged += new System.EventHandler(this.token_field_TextChanged);
            this.token_field.Enter += new System.EventHandler(this.token_field_Enter);
            this.token_field.Leave += new System.EventHandler(this.token_field_Leave);
            if (Settings.Default["access_token"] as string == "")
                this.token_field.Text = "access token";
            else
            {
                this.token_field.Text = Settings.Default["access_token"] as string;
                this.token_field.ForeColor = Color.Black;
            }
            // 
            // panel1
            // 
            this.panel1.AutoScroll = true;
            this.panel1.Controls.Add(this.label1);
            this.panel1.Location = new System.Drawing.Point(12, 450);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(367, 110);
            this.panel1.TabIndex = 5;
            // 
            // label1
            // 
            this.label1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(0, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(367, 110);
            this.label1.TabIndex = 0;
            this.label1.Text = "You can get your access token at: \r\n\r\nhttp://ha_address:port/profile\r\n\r\nTokens ca" +
    "n be valid for a session only or for 10 years (long-lived token). \r\nYou can opt " +
    "for either of them.";
            // 
            // WindowTitle
            // 
            this.WindowTitle.AutoSize = true;
            this.WindowTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.WindowTitle.Location = new System.Drawing.Point(12, 9);
            this.WindowTitle.Name = "WindowTitle";
            this.WindowTitle.Size = new System.Drawing.Size(75, 24);
            this.WindowTitle.TabIndex = 6;
            this.WindowTitle.Text = "Options";
            // 
            // rh_address
            // 
            this.rh_address.AutoSize = true;
            this.rh_address.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rh_address.Location = new System.Drawing.Point(11, 193);
            this.rh_address.Name = "rh_address";
            this.rh_address.Size = new System.Drawing.Size(167, 20);
            this.rh_address.TabIndex = 7;
            this.rh_address.Text = "Remote Host Address";
            // 
            // rh_address_field
            // 
            this.rh_address_field.BackColor = System.Drawing.SystemColors.ControlDark;
            this.rh_address_field.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.rh_address_field.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rh_address_field.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(120)))), ((int)(((byte)(120)))), ((int)(((byte)(120)))));
            this.rh_address_field.Location = new System.Drawing.Point(198, 194);
            this.rh_address_field.Name = "rh_address_field";
            this.rh_address_field.Size = new System.Drawing.Size(180, 22);
            this.rh_address_field.TabIndex = 8;
            this.rh_address_field.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.rh_address_field.TextChanged += new System.EventHandler(this.rh_address_field_TextChanged);
            this.rh_address_field.Enter += new System.EventHandler(this.rh_address_field_Enter);
            this.rh_address_field.Leave += new System.EventHandler(this.rh_address_field_Leave);
            if (Settings.Default["rh_address"] as string == "")
                this.rh_address_field.Text = "ipaddress:port";
            else
            {
                this.rh_address_field.Text = Settings.Default["rh_address"] as string;
                this.rh_address_field.ForeColor = Color.Black;
            }
            // 
            // username_label
            // 
            this.username_label.AutoSize = true;
            this.username_label.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.username_label.Location = new System.Drawing.Point(11, 221);
            this.username_label.Name = "username_label";
            this.username_label.Size = new System.Drawing.Size(83, 20);
            this.username_label.TabIndex = 9;
            this.username_label.Text = "Username";
            // 
            // username_field
            // 
            this.username_field.BackColor = System.Drawing.SystemColors.ControlDark;
            this.username_field.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.username_field.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.username_field.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(120)))), ((int)(((byte)(120)))), ((int)(((byte)(120)))));
            this.username_field.Location = new System.Drawing.Point(198, 222);
            this.username_field.Name = "username_field";
            this.username_field.Size = new System.Drawing.Size(180, 22);
            this.username_field.TabIndex = 10;
            this.username_field.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.username_field.TextChanged += new System.EventHandler(this.username_field_TextChanged);
            this.username_field.Enter += new System.EventHandler(this.username_field_Enter);
            this.username_field.Leave += new System.EventHandler(this.username_field_Leave);
            if (Settings.Default["username"] as string == "")
                this.username_field.Text = "username";
            else
            {
                this.username_field.Text = Settings.Default["username"] as string;
                this.username_field.ForeColor = Color.Black;
            }
            // 
            // password_field
            // 
            this.password_field.BackColor = System.Drawing.SystemColors.ControlDark;
            this.password_field.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.password_field.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.password_field.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(120)))), ((int)(((byte)(120)))), ((int)(((byte)(120)))));
            this.password_field.Location = new System.Drawing.Point(198, 252);
            this.password_field.Name = "password_field";
            this.password_field.PasswordChar = '*';
            this.password_field.Size = new System.Drawing.Size(180, 22);
            this.password_field.TabIndex = 12;
            this.password_field.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.password_field.TextChanged += new System.EventHandler(this.password_field_TextChanged);
            this.password_field.Enter += new System.EventHandler(this.password_field_Enter);
            this.password_field.Leave += new System.EventHandler(this.password_field_Leave);
            if (Settings.Default["password"] as string == "")
                this.password_field.Text = "password";
            else
            {
                this.password_field.Text = Settings.Default["password"] as string;
                this.password_field.ForeColor = Color.Black;
            }
            // 
            // password_label
            // 
            this.password_label.AutoSize = true;
            this.password_label.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.password_label.Location = new System.Drawing.Point(11, 251);
            this.password_label.Name = "password_label";
            this.password_label.Size = new System.Drawing.Size(78, 20);
            this.password_label.TabIndex = 11;
            this.password_label.Text = "Password";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(9, 47);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(117, 13);
            this.label3.TabIndex = 13;
            this.label3.Text = "Home Assistant Restart";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(9, 166);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(123, 13);
            this.label4.TabIndex = 14;
            this.label4.Text = "Remote Host File Editing";
            // 
            // files_directory_field
            // 
            this.files_directory_field.BackColor = System.Drawing.SystemColors.ControlDark;
            this.files_directory_field.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.files_directory_field.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.files_directory_field.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(120)))), ((int)(((byte)(120)))), ((int)(((byte)(120)))));
            this.files_directory_field.Location = new System.Drawing.Point(198, 281);
            this.files_directory_field.Name = "files_directory_field";
            this.files_directory_field.Size = new System.Drawing.Size(180, 22);
            this.files_directory_field.TabIndex = 16;
            this.files_directory_field.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.files_directory_field.TextChanged += new System.EventHandler(this.files_directory_field_TextChanged);
            this.files_directory_field.Enter += new System.EventHandler(this.files_directory_field_Enter);
            this.files_directory_field.Leave += new System.EventHandler(this.files_directory_field_Leave);
            if (Settings.Default["remote_directory"] as string == "")
                this.files_directory_field.Text = "/C:/path/to/files/";
            else
            {
                this.files_directory_field.Text = Settings.Default["remote_directory"] as string;
                this.files_directory_field.ForeColor = Color.Black;
            }
            // 
            // files_directory_label
            // 
            this.files_directory_label.AutoSize = true;
            this.files_directory_label.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.files_directory_label.Location = new System.Drawing.Point(11, 280);
            this.files_directory_label.Name = "files_directory_label";
            this.files_directory_label.Size = new System.Drawing.Size(109, 20);
            this.files_directory_label.TabIndex = 15;
            this.files_directory_label.Text = "Files Directory";
            // 
            // gitrepo_label
            // 
            this.gitrepo_label.AutoSize = true;
            this.gitrepo_label.Location = new System.Drawing.Point(10, 329);
            this.gitrepo_label.Name = "gitrepo_label";
            this.gitrepo_label.Size = new System.Drawing.Size(73, 13);
            this.gitrepo_label.TabIndex = 23;
            this.gitrepo_label.Text = "Git Repository";
            // 
            // git_password_field
            // 
            this.git_password_field.BackColor = System.Drawing.SystemColors.ControlDark;
            this.git_password_field.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.git_password_field.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.git_password_field.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(120)))), ((int)(((byte)(120)))), ((int)(((byte)(120)))));
            this.git_password_field.Location = new System.Drawing.Point(199, 415);
            this.git_password_field.Name = "git_password_field";
            this.git_password_field.PasswordChar = '*';
            this.git_password_field.Size = new System.Drawing.Size(180, 22);
            this.git_password_field.TabIndex = 22;
            this.git_password_field.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.git_password_field.TextChanged += new System.EventHandler(this.git_password_field_TextChanged);
            this.git_password_field.Enter += new System.EventHandler(this.git_password_field_Enter);
            this.git_password_field.Leave += new System.EventHandler(this.git_password_field_Leave);
            if (Settings.Default["gitrepo_password"] as string == "")
                this.git_password_field.Text = "git password";
            else
            {
                this.git_password_field.Text = Settings.Default["gitrepo_password"] as string;
                this.git_password_field.ForeColor = Color.Black;
            }
            // 
            // gitpassword_label
            // 
            this.gitpassword_label.AutoSize = true;
            this.gitpassword_label.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gitpassword_label.Location = new System.Drawing.Point(12, 414);
            this.gitpassword_label.Name = "gitpassword_label";
            this.gitpassword_label.Size = new System.Drawing.Size(103, 20);
            this.gitpassword_label.TabIndex = 21;
            this.gitpassword_label.Text = "Git Password";
            // 
            // git_email_field
            // 
            this.git_email_field.BackColor = System.Drawing.SystemColors.ControlDark;
            this.git_email_field.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.git_email_field.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.git_email_field.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(120)))), ((int)(((byte)(120)))), ((int)(((byte)(120)))));
            this.git_email_field.Location = new System.Drawing.Point(199, 385);
            this.git_email_field.Name = "git_email_field";
            this.git_email_field.Size = new System.Drawing.Size(180, 22);
            this.git_email_field.TabIndex = 20;
            this.git_email_field.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.git_email_field.TextChanged += new System.EventHandler(this.git_email_field_TextChanged);
            this.git_email_field.Enter += new System.EventHandler(this.git_email_field_Enter);
            this.git_email_field.Leave += new System.EventHandler(this.git_email_field_Leave);
            if (Settings.Default["gitrepo_email"] as string == "")
                this.git_email_field.Text = "git email";
            else
            {
                this.git_email_field.Text = Settings.Default["gitrepo_email"] as string;
                this.git_email_field.ForeColor = Color.Black;
            }
            // 
            // gitemail_label
            // 
            this.gitemail_label.AutoSize = true;
            this.gitemail_label.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gitemail_label.Location = new System.Drawing.Point(12, 384);
            this.gitemail_label.Name = "gitemail_label";
            this.gitemail_label.Size = new System.Drawing.Size(73, 20);
            this.gitemail_label.TabIndex = 19;
            this.gitemail_label.Text = "Git Email";
            // 
            // gitrepo_link_field
            // 
            this.gitrepo_link_field.BackColor = System.Drawing.SystemColors.ControlDark;
            this.gitrepo_link_field.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.gitrepo_link_field.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gitrepo_link_field.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(120)))), ((int)(((byte)(120)))), ((int)(((byte)(120)))));
            this.gitrepo_link_field.Location = new System.Drawing.Point(199, 357);
            this.gitrepo_link_field.Name = "gitrepo_link_field";
            this.gitrepo_link_field.Size = new System.Drawing.Size(180, 22);
            this.gitrepo_link_field.TabIndex = 18;
            this.gitrepo_link_field.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.gitrepo_link_field.TextChanged += new System.EventHandler(this.gitrepo_link_field_TextChanged);
            this.gitrepo_link_field.Enter += new System.EventHandler(this.gitrepo_link_field_Enter);
            this.gitrepo_link_field.Leave += new System.EventHandler(this.gitrepo_link_field_Leave);
            if (Settings.Default["gitrepo_link"] as string == "")
                this.gitrepo_link_field.Text = "github.com/user/repo.git";
            else
            {
                this.gitrepo_link_field.Text = Settings.Default["gitrepo_link"] as string;
                this.gitrepo_link_field.ForeColor = Color.Black;
            }
            // 
            // gitrepolink_label
            // 
            this.gitrepolink_label.AutoSize = true;
            this.gitrepolink_label.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gitrepolink_label.Location = new System.Drawing.Point(12, 356);
            this.gitrepolink_label.Name = "gitrepolink_label";
            this.gitrepolink_label.Size = new System.Drawing.Size(100, 20);
            this.gitrepolink_label.TabIndex = 17;
            this.gitrepolink_label.Text = "Git Repo link";
            // 
            // OptionsWindow
            // 
            this.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.ClientSize = new System.Drawing.Size(397, 562);
            this.Controls.Add(this.gitrepo_label);
            this.Controls.Add(this.git_password_field);
            this.Controls.Add(this.gitpassword_label);
            this.Controls.Add(this.git_email_field);
            this.Controls.Add(this.gitemail_label);
            this.Controls.Add(this.gitrepo_link_field);
            this.Controls.Add(this.gitrepolink_label);
            this.Controls.Add(this.files_directory_field);
            this.Controls.Add(this.files_directory_label);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.password_field);
            this.Controls.Add(this.password_label);
            this.Controls.Add(this.username_field);
            this.Controls.Add(this.username_label);
            this.Controls.Add(this.rh_address_field);
            this.Controls.Add(this.rh_address);
            this.Controls.Add(this.WindowTitle);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.token_field);
            this.Controls.Add(this.access_token_field);
            this.Controls.Add(this.ha_address_field);
            this.Controls.Add(this.ha_address);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "OptionsWindow";
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }
		#endregion


		#region HomeAssistant Restart

		/// <summary>
		/// When clicking the text field, the example given will disappear
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void ha_address_field_Enter(object sender, EventArgs e)
        {
            if (ha_address_field.Text == "ipaddress:port")
                ha_address_field.Text = "";

            ha_address_field.ForeColor = Color.Black;
        }

		/// <summary>
		/// When leaving this text field, if the field is empty we restore the example given
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
        private void ha_address_field_Leave(object sender, EventArgs e)
        {
            if (ha_address_field.Text == "")
            {
                ha_address_field.Text = "ipaddress:port";
                ha_address_field.ForeColor = Color.FromArgb(120, 120, 120);
            }
        }

		/// <summary>
		/// Saves the input of the user in this text field
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
        private void ha_address_field_TextChanged(object sender, EventArgs e)
        {
			// User input validation
            if (ha_address_field.Text.All(c => Char.IsLetterOrDigit(c) || c.Equals(':') || c.Equals('.') || c.Equals("")) || ha_address_field.Text == "" || ha_address_field.Text == "ipaddress:port")
            {
                if (ha_address_field.Text == "ipaddress:port") return;
                if (ha_address_field.Text == Settings.Default["ha_address"] as string) return;

                // Update 
                Settings.Default["ha_address"] = ha_address_field.Text;

                // Save
                Properties.Settings.Default.Save();
            }
            else
            {
                MessageBox.Show("Addresses can only consist of letters, numbers, colon, dot or nothing (to delete)", "Error");
                ha_address_field.Text = ha_address_field.Text.Substring(0, ha_address_field.Text.Length - 1);
            }
        }


		/// <summary>
		/// When clicking the text field, the example given will disappear
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void token_field_Enter(object sender, EventArgs e)
        {
            if (token_field.Text == "access token")
                token_field.Text = "";

            token_field.ForeColor = Color.Black;
        }

		/// <summary>
		/// When leaving this text field, if the field is empty we restore the example given
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void token_field_Leave(object sender, EventArgs e)
        {
            if (token_field.Text == "")
            {
                token_field.Text = "access token";
                token_field.ForeColor = Color.FromArgb(120, 120, 120);
            }
        }

		/// <summary>
		/// Saves the input of the user in this text field
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void token_field_TextChanged(object sender, EventArgs e)
        {
			// User input validation
			if (token_field.Text.All(c => Char.IsLetterOrDigit(c) || c.Equals('-') || c.Equals('_') || c.Equals('.') || c.Equals("")) || token_field.Text == "" || token_field.Text == "access token")
            {
                if (token_field.Text == "access token") return;
                if (token_field.Text == Settings.Default["access_token"] as string) return;

                // Update
                Settings.Default["access_token"] = token_field.Text;

                // Save
                Properties.Settings.Default.Save();
            }
            else
            {
                MessageBox.Show("Access tokens can only consist of letters, numbers, dash, underscore, dot or nothing (to delete)", "Error");
                token_field.Text = token_field.Text.Substring(0, token_field.Text.Length - 1);
            }
        }
		#endregion


		#region Remote Host File Editing

		/// <summary>
		/// When clicking the text field, the example given will disappear
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void rh_address_field_Enter(object sender, EventArgs e)
        {
            if (rh_address_field.Text == "ipaddress:port")
                rh_address_field.Text = "";

            rh_address_field.ForeColor = Color.Black;
        }

		/// <summary>
		/// When leaving this text field, if the field is empty we restore the example given
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void rh_address_field_Leave(object sender, EventArgs e)
        {
            if (rh_address_field.Text == "")
            {
                rh_address_field.Text = "ipaddress:port";
                rh_address_field.ForeColor = Color.FromArgb(120, 120, 120);
            }
        }

		/// <summary>
		/// Saves the input of the user in this text field
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void rh_address_field_TextChanged(object sender, EventArgs e)
        {
			// User input validation
			if (rh_address_field.Text.All(c => Char.IsLetterOrDigit(c) || c.Equals(':') || c.Equals('.') || c.Equals("")) || rh_address_field.Text == "" || rh_address_field.Text == "ipaddress:port")
            {
                if (rh_address_field.Text == "ipaddress:port") return;
                if (rh_address_field.Text == Settings.Default["rh_address"] as string) return;

                // Update
                Settings.Default["rh_address"] = rh_address_field.Text;

                // Save
                Properties.Settings.Default.Save();
            }
            else
            {
                MessageBox.Show("Addresses can only consist of letters, numbers, colon, dot or nothing (to delete)", "Error");
                rh_address_field.Text = rh_address_field.Text.Substring(0, rh_address_field.Text.Length - 1);
            }
        }



		/// <summary>
		/// When clicking the text field, the example given will disappear
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void username_field_Enter(object sender, EventArgs e)
        {
            if (username_field.Text == "username")
                username_field.Text = "";

            username_field.ForeColor = Color.Black;
        }

		/// <summary>
		/// When leaving this text field, if the field is empty we restore the example given
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void username_field_Leave(object sender, EventArgs e)
        {
            if (username_field.Text == "")
            {
                username_field.Text = "username";
                username_field.ForeColor = Color.FromArgb(120, 120, 120);
            }
        }

		/// <summary>
		/// Saves the input of the user in this text field
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void username_field_TextChanged(object sender, EventArgs e)
        {
			// User input validation
			if (username_field.Text.All(c => Char.IsLetterOrDigit(c) || c.Equals("") || c.Equals(' ')) || username_field.Text == "" || username_field.Text == "username")
            {
                if (username_field.Text == "username") return;
                if (username_field.Text == Settings.Default["username"] as string) return;

                // Update
                Settings.Default["username"] = username_field.Text;

                // Save
                Properties.Settings.Default.Save();
            }
            else
            {
                MessageBox.Show("Usernames can only consist of letters, numbers, spaces or nothing (to delete)", "Error");
                username_field.Text = username_field.Text.Substring(0, username_field.Text.Length - 1);
            }
        }



		/// <summary>
		/// When clicking the text field, the example given will disappear
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void password_field_Enter(object sender, EventArgs e)
        {
            if (password_field.Text == "password")
                password_field.Text = "";

            password_field.ForeColor = Color.Black;
        }

		/// <summary>
		/// When leaving this text field, if the field is empty we restore the example given
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void password_field_Leave(object sender, EventArgs e)
        {
            if (password_field.Text == "")
            {
                password_field.Text = "password";
                password_field.ForeColor = Color.FromArgb(120, 120, 120);
            }
        }

		/// <summary>
		/// Saves the input of the user in this text field
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void password_field_TextChanged(object sender, EventArgs e)
        {
			// User input validation
			if (password_field.Text.All(c => Char.IsLetterOrDigit(c) || c.Equals("") || c.Equals('.') || c.Equals('-') || c.Equals('_')) || password_field.Text == "" || password_field.Text == "password")
            {
                if (password_field.Text == "password") return;
                if (password_field.Text == Settings.Default["password"] as string) return;

                // Update
                Settings.Default["password"] = password_field.Text;

                // Save
                Properties.Settings.Default.Save();
            }
            else
            {
                MessageBox.Show("Passwords can only consist of letters, numbers, dots, dashes, underscores or nothing (to delete)", "Error");
                password_field.Text = password_field.Text.Substring(0, password_field.Text.Length - 1);
            }
        }



		/// <summary>
		/// When clicking the text field, the example given will disappear
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void files_directory_field_Enter(object sender, EventArgs e)
        {
            if (files_directory_field.Text == "/C:/path/to/files/")
                files_directory_field.Text = "";

            files_directory_field.ForeColor = Color.Black;
        }

		/// <summary>
		/// When leaving this text field, if the field is empty we restore the example given
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void files_directory_field_Leave(object sender, EventArgs e)
        {
            if (files_directory_field.Text == "")
            {
                files_directory_field.Text = "/C:/path/to/files/";
                files_directory_field.ForeColor = Color.FromArgb(120, 120, 120);
            }
        }

		/// <summary>
		/// Saves the input of the user in this text field
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void files_directory_field_TextChanged(object sender, EventArgs e)
        {
			// User input validation
			if (files_directory_field.Text.All(c => Char.IsLetterOrDigit(c) || c.Equals("") || c.Equals('/') || c.Equals('\\') || c.Equals('_') || c.Equals(':')) || files_directory_field.Text == "" || files_directory_field.Text == "/C:/path/to/files/")
            {
                if (files_directory_field.Text == "/C:/path/to/files/") return;
                if (files_directory_field.Text == Settings.Default["remote_directory"] as string) return;

                if (!files_directory_field.Text.StartsWith("/") && files_directory_field.Text != "")
                {
                    MessageBox.Show("Remote directories must start and end with forward slashes.", "Error");
                    files_directory_field.Text = "";
                }

                // Update
                Settings.Default["remote_directory"] = files_directory_field.Text;

                // Save
                Properties.Settings.Default.Save();
            }
            else
            {
                MessageBox.Show("Remote directories can only consist of letters, numbers, forward slashes, back slashes, underscores, colon or nothing (to delete). Must start and end with forward slashes.", "Error");
                files_directory_field.Text = "";
            }
        }
		#endregion


		#region Github Configuration

		/// <summary>
		/// When clicking the text field, the example given will disappear
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void gitrepo_link_field_Enter(object sender, EventArgs e)
        {
            if (gitrepo_link_field.Text == "github.com/user/repo.git")
                gitrepo_link_field.Text = "";

            gitrepo_link_field.ForeColor = Color.Black;
        }

		/// <summary>
		/// When leaving this text field, if the field is empty we restore the example given
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void gitrepo_link_field_Leave(object sender, EventArgs e)
        {
            if (gitrepo_link_field.Text == "")
            {
                gitrepo_link_field.Text = "github.com/user/repo.git";
                gitrepo_link_field.ForeColor = Color.FromArgb(120, 120, 120);
            }
        }

		/// <summary>
		/// Saves the input of the user in this text field
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void gitrepo_link_field_TextChanged(object sender, EventArgs e)
        {
			// User input validation
			if (gitrepo_link_field.Text.All(c => Char.IsLetterOrDigit(c) || c.Equals("") || c.Equals('/') || c.Equals('_') || c.Equals(':') || c.Equals('.')) || gitrepo_link_field.Text == "" || gitrepo_link_field.Text == "github.com/user/repo.git")
            {
                if (gitrepo_link_field.Text == "github.com/user/repo.git") return;
                if (gitrepo_link_field.Text == Settings.Default["gitrepo_link"] as string) return;

                // Update
                Settings.Default["gitrepo_link"] = gitrepo_link_field.Text;

                // Save
                Properties.Settings.Default.Save();
            }
            else
            {
                MessageBox.Show("Links can only consist of letters, numbers, forward slashes, underscores, colon, dot or nothing (to delete).", "Error");
                gitrepo_link_field.Text = "";
            }
        }



		/// <summary>
		/// When clicking the text field, the example given will disappear
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void git_email_field_Enter(object sender, EventArgs e)
        {
            if (git_email_field.Text == "git email")
                git_email_field.Text = "";

            git_email_field.ForeColor = Color.Black;
        }

		/// <summary>
		/// When leaving this text field, if the field is empty we restore the example given
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void git_email_field_Leave(object sender, EventArgs e)
        {
            if (git_email_field.Text == "")
            {
                git_email_field.Text = "git email";
                git_email_field.ForeColor = Color.FromArgb(120, 120, 120);
            }
        }

		/// <summary>
		/// Saves the input of the user in this text field
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void git_email_field_TextChanged(object sender, EventArgs e)
        {
			// User input validation
			if (git_email_field.Text.All(c => Char.IsLetterOrDigit(c) || c.Equals("") || c.Equals('_') || c.Equals('@') || c.Equals('.')) || git_email_field.Text == "" || git_email_field.Text == "git email")
            {
                if (git_email_field.Text == "git email") return;
                if (git_email_field.Text == Settings.Default["gitrepo_email"] as string) return;

                // Update
                Settings.Default["gitrepo_email"] = git_email_field.Text;

                // Save
                Properties.Settings.Default.Save();
            }
            else
            {
                MessageBox.Show("Emails can only consist of letters, numbers, underscores, @, dots or nothing (to delete).", "Error");
                git_email_field.Text = "";
            }
        }



		/// <summary>
		/// When clicking the text field, the example given will disappear
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void git_password_field_Enter(object sender, EventArgs e)
        {
            if (git_password_field.Text == "git password")
                git_password_field.Text = "";

            git_password_field.ForeColor = Color.Black;
        }

		/// <summary>
		/// When leaving this text field, if the field is empty we restore the example given
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void git_password_field_Leave(object sender, EventArgs e)
        {
            if (git_password_field.Text == "")
            {
                git_password_field.Text = "git password";
                git_password_field.ForeColor = Color.FromArgb(120, 120, 120);
            }
        }

		/// <summary>
		/// Saves the input of the user in this text field
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void git_password_field_TextChanged(object sender, EventArgs e)
        {
			// User input validation
			if (git_password_field.Text.All(c => Char.IsLetterOrDigit(c) || c.Equals("") || c.Equals('.') || c.Equals('-') || c.Equals('_')) || git_password_field.Text == "" || git_password_field.Text == "git password")
            {
                if (git_password_field.Text == "git password") return;
                if (git_password_field.Text == Settings.Default["gitrepo_password"] as string) return;

                // Update
                Settings.Default["gitrepo_password"] = git_password_field.Text;

                // Save
                Properties.Settings.Default.Save();
            }
            else
            {
                MessageBox.Show("Passwords can only consist of letters, numbers, dots, dashes, underscores or nothing (to delete)", "Error");
                git_password_field.Text = "";
            }
        }
		#endregion
	}
}
