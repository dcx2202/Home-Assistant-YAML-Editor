﻿using System;
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
        private Label ha_address;
        private TextBox ha_address_field;
        private Label access_token_field;
        private Panel panel1;
        private Label label1;
        private Label WindowTitle;
        private TextBox token_field;

        public OptionsWindow()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            this.ha_address = new System.Windows.Forms.Label();
            this.ha_address_field = new System.Windows.Forms.TextBox();
            this.access_token_field = new System.Windows.Forms.Label();
            this.token_field = new System.Windows.Forms.TextBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.WindowTitle = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // ha_address
            // 
            this.ha_address.AutoSize = true;
            this.ha_address.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ha_address.Location = new System.Drawing.Point(12, 73);
            this.ha_address.Name = "ha_address";
            this.ha_address.Size = new System.Drawing.Size(181, 20);
            this.ha_address.TabIndex = 0;
            this.ha_address.Text = "Home assistant address";
            // 
            // ha_address_field
            // 
            this.ha_address_field.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.ha_address_field.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ha_address_field.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(120)))), ((int)(((byte)(120)))), ((int)(((byte)(120)))));
            this.ha_address_field.Location = new System.Drawing.Point(199, 74);
            this.ha_address_field.Name = "ha_address_field";
            this.ha_address_field.Size = new System.Drawing.Size(180, 22);
            this.ha_address_field.TabIndex = 1;

            if (Settings.Default["ha_address"] as string == "")
                this.ha_address_field.Text = "ipaddress:port";
            else
            {
                this.ha_address_field.Text = Settings.Default["ha_address"] as string;
                this.ha_address_field.ForeColor = Color.Black;
            }

            this.ha_address_field.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.ha_address_field.TextChanged += new System.EventHandler(this.ha_address_field_TextChanged);
            this.ha_address_field.Enter += new System.EventHandler(this.ha_address_field_Enter);
            this.ha_address_field.Leave += new System.EventHandler(this.ha_address_field_Leave);
            // 
            // access_token_field
            // 
            this.access_token_field.AutoSize = true;
            this.access_token_field.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.access_token_field.Location = new System.Drawing.Point(12, 120);
            this.access_token_field.Name = "access_token_field";
            this.access_token_field.Size = new System.Drawing.Size(109, 20);
            this.access_token_field.TabIndex = 2;
            this.access_token_field.Text = "Access Token";
            // 
            // token_field
            // 
            this.token_field.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.token_field.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.token_field.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(120)))), ((int)(((byte)(120)))), ((int)(((byte)(120)))));
            this.token_field.Location = new System.Drawing.Point(16, 154);
            this.token_field.Name = "token_field";
            this.token_field.Size = new System.Drawing.Size(363, 22);
            this.token_field.TabIndex = 3;

            if (Settings.Default["access_token"] as string == "")
                this.token_field.Text = "access token";
            else
            {
                this.token_field.Text = Settings.Default["access_token"] as string;
                this.token_field.ForeColor = Color.Black;
            }

            this.token_field.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.token_field.TextChanged += new System.EventHandler(this.token_field_TextChanged);
            this.token_field.Enter += new System.EventHandler(this.token_field_Enter);
            this.token_field.Leave += new System.EventHandler(this.token_field_Leave);
            // 
            // panel1
            // 
            this.panel1.AutoScroll = true;
            this.panel1.Controls.Add(this.label1);
            this.panel1.Location = new System.Drawing.Point(12, 252);
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
            this.WindowTitle.Location = new System.Drawing.Point(12, 20);
            this.WindowTitle.Name = "WindowTitle";
            this.WindowTitle.Size = new System.Drawing.Size(75, 24);
            this.WindowTitle.TabIndex = 6;
            this.WindowTitle.Text = "Options";
            // 
            // OptionsWindow
            // 
            this.ClientSize = new System.Drawing.Size(397, 374);
            this.Controls.Add(this.WindowTitle);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.token_field);
            this.Controls.Add(this.access_token_field);
            this.Controls.Add(this.ha_address_field);
            this.Controls.Add(this.ha_address);
            this.Name = "OptionsWindow";
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        private void ha_address_field_TextChanged(object sender, EventArgs e)
        {
            if (ha_address_field.Text.All(c => Char.IsLetterOrDigit(c) || c.Equals(':') || c.Equals('.') || c.Equals("")) || ha_address_field.Text == "" || ha_address_field.Text == "ipaddress:port")
            {
                if (ha_address_field.Text == "ipaddress:port") return;
                if (ha_address_field.Text == Settings.Default["ha_address"] as string) return;

                // update
                Settings.Default["ha_address"] = ha_address_field.Text;

                // save
                Properties.Settings.Default.Save();
            }
            else
            {
                MessageBox.Show("Addresses can only consist of letters, numbers, colon, dot or nothing (to delete)", "Error");
                ha_address_field.Text = "ipaddress:port";
                ha_address_field.ForeColor = Color.FromArgb(120, 120, 120);
            }
        }

        private void token_field_TextChanged(object sender, EventArgs e)
        {
            if (token_field.Text.All(c => Char.IsLetterOrDigit(c) || c.Equals('-') || c.Equals('_') || c.Equals('.') || c.Equals("")) || token_field.Text == "" || token_field.Text == "access token")
            {
                if (token_field.Text == "access token") return;
                if (token_field.Text == Settings.Default["access_token"] as string) return;

                // update
                Settings.Default["access_token"] = token_field.Text;

                // save
                Properties.Settings.Default.Save();
            }
            else
            {
                MessageBox.Show("Access tokens can only consist of letters, numbers, dash, underscore, dot or nothing (to delete)", "Error");
                token_field.Text = "access token";
                token_field.ForeColor = Color.FromArgb(120, 120, 120);
            }
        }

        private void ha_address_field_Enter(object sender, EventArgs e)
        {
            if (ha_address_field.Text == "ipaddress:port")
                ha_address_field.Text = "";

            ha_address_field.ForeColor = Color.Black;
        }

        private void ha_address_field_Leave(object sender, EventArgs e)
        {
            if (ha_address_field.Text == "")
            {
                ha_address_field.Text = "ipaddress:port";
                ha_address_field.ForeColor = Color.FromArgb(120, 120, 120);
            }
        }

        private void token_field_Enter(object sender, EventArgs e)
        {
            if (token_field.Text == "access token")
                token_field.Text = "";

            token_field.ForeColor = Color.Black;
        }

        private void token_field_Leave(object sender, EventArgs e)
        {
            if (token_field.Text == "")
            {
                token_field.Text = "access token";
                token_field.ForeColor = Color.FromArgb(120, 120, 120);
            }
        }
    }
}