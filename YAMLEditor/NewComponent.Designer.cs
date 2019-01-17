namespace YAMLEditor
{
	partial class NewComponent
	{
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Windows Form Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.components = new System.ComponentModel.Container();
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(NewComponent));
			this.pictureBox1 = new System.Windows.Forms.PictureBox();
			this.label1 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.button1 = new System.Windows.Forms.Button();
			this.button2 = new System.Windows.Forms.Button();
			this.menuStrip2 = new System.Windows.Forms.MenuStrip();
			this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
			this.contextMenuStrip2 = new System.Windows.Forms.ContextMenuStrip(this.components);
			this.contextMenuStrip3 = new System.Windows.Forms.ContextMenuStrip(this.components);
			this.label3 = new System.Windows.Forms.Label();
			this.File = new System.Windows.Forms.TextBox();
			this.button3 = new System.Windows.Forms.Button();
			this.label4 = new System.Windows.Forms.Label();
			this.linkLabel1 = new System.Windows.Forms.LinkLabel();
			((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
			this.SuspendLayout();
			// 
			// pictureBox1
			// 
			this.pictureBox1.Image = global::YAMLEditor.Properties.Resources.ha;
			this.pictureBox1.Location = new System.Drawing.Point(31, 27);
			this.pictureBox1.Name = "pictureBox1";
			this.pictureBox1.Size = new System.Drawing.Size(192, 192);
			this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
			this.pictureBox1.TabIndex = 1;
			this.pictureBox1.TabStop = false;
			this.pictureBox1.Click += new System.EventHandler(this.pictureBox1_Click);
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.label1.Font = new System.Drawing.Font("Calibri", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label1.ForeColor = System.Drawing.SystemColors.ControlText;
			this.label1.Location = new System.Drawing.Point(329, 27);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(409, 51);
			this.label1.TabIndex = 4;
			this.label1.Text = "Home Assistant Wizard";
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Font = new System.Drawing.Font("Calibri", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label2.Location = new System.Drawing.Point(288, 119);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(501, 29);
			this.label2.TabIndex = 5;
			this.label2.Text = "Choose one of the following existing components:";
			// 
			// button1
			// 
			this.button1.BackColor = System.Drawing.SystemColors.ActiveCaption;
			this.button1.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.button1.Location = new System.Drawing.Point(432, 316);
			this.button1.Name = "button1";
			this.button1.Size = new System.Drawing.Size(84, 43);
			this.button1.TabIndex = 6;
			this.button1.Text = "OK";
			this.button1.UseVisualStyleBackColor = false;
			this.button1.Click += new System.EventHandler(this.OKButton);
			// 
			// button2
			// 
			this.button2.BackColor = System.Drawing.SystemColors.ActiveCaption;
			this.button2.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.button2.Location = new System.Drawing.Point(641, 316);
			this.button2.Name = "button2";
			this.button2.Size = new System.Drawing.Size(91, 43);
			this.button2.TabIndex = 7;
			this.button2.Text = "Cancel";
			this.button2.UseVisualStyleBackColor = false;
			this.button2.Click += new System.EventHandler(this.CloseButton);
			// 
			// menuStrip2
			// 
			this.menuStrip2.ImageScalingSize = new System.Drawing.Size(20, 20);
			this.menuStrip2.Location = new System.Drawing.Point(0, 0);
			this.menuStrip2.Name = "menuStrip2";
			this.menuStrip2.Size = new System.Drawing.Size(822, 24);
			this.menuStrip2.TabIndex = 8;
			this.menuStrip2.Text = "menuStrip2";
			// 
			// contextMenuStrip1
			// 
			this.contextMenuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
			this.contextMenuStrip1.Name = "contextMenuStrip1";
			this.contextMenuStrip1.Size = new System.Drawing.Size(61, 4);
			// 
			// contextMenuStrip2
			// 
			this.contextMenuStrip2.ImageScalingSize = new System.Drawing.Size(20, 20);
			this.contextMenuStrip2.Name = "contextMenuStrip2";
			this.contextMenuStrip2.Size = new System.Drawing.Size(61, 4);
			// 
			// contextMenuStrip3
			// 
			this.contextMenuStrip3.ImageScalingSize = new System.Drawing.Size(20, 20);
			this.contextMenuStrip3.Name = "contextMenuStrip3";
			this.contextMenuStrip3.Size = new System.Drawing.Size(61, 4);
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Font = new System.Drawing.Font("Calibri", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label3.Location = new System.Drawing.Point(325, 215);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(43, 21);
			this.label3.TabIndex = 9;
			this.label3.Text = "File: ";
			// 
			// File
			// 
			this.File.Location = new System.Drawing.Point(363, 215);
			this.File.Name = "File";
			this.File.Size = new System.Drawing.Size(369, 22);
			this.File.TabIndex = 10;
			// 
			// button3
			// 
			this.button3.Image = ((System.Drawing.Image)(resources.GetObject("button3.Image")));
			this.button3.Location = new System.Drawing.Point(738, 205);
			this.button3.Name = "button3";
			this.button3.Size = new System.Drawing.Size(51, 42);
			this.button3.TabIndex = 11;
			this.button3.UseVisualStyleBackColor = true;
			this.button3.Click += new System.EventHandler(this.FileExplorer);
			// 
			// label4
			// 
			this.label4.AutoSize = true;
			this.label4.Font = new System.Drawing.Font("Calibri", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label4.Location = new System.Drawing.Point(90, 306);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(203, 21);
			this.label4.TabIndex = 12;
			this.label4.Text = "For more components visit:";
			// 
			// linkLabel1
			// 
			this.linkLabel1.AutoSize = true;
			this.linkLabel1.Cursor = System.Windows.Forms.Cursors.Hand;
			this.linkLabel1.Font = new System.Drawing.Font("Calibri", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.linkLabel1.Location = new System.Drawing.Point(27, 338);
			this.linkLabel1.Name = "linkLabel1";
			this.linkLabel1.Size = new System.Drawing.Size(329, 21);
			this.linkLabel1.TabIndex = 13;
			this.linkLabel1.TabStop = true;
			this.linkLabel1.Text = "https://www.home-assistant.io/components/";
			this.linkLabel1.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.LinkButton);
			// 
			// NewComponent
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(822, 399);
			this.Controls.Add(this.linkLabel1);
			this.Controls.Add(this.label4);
			this.Controls.Add(this.button3);
			this.Controls.Add(this.File);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.button2);
			this.Controls.Add(this.button1);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.pictureBox1);
			this.Controls.Add(this.menuStrip2);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.Name = "NewComponent";
			this.Text = "NewComponent";
			this.Load += new System.EventHandler(this.NewComponent_Load);
			((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion
		private System.Windows.Forms.PictureBox pictureBox1;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Button button1;
		private System.Windows.Forms.Button button2;
		private System.Windows.Forms.MenuStrip menuStrip2;
		private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
		private System.Windows.Forms.ContextMenuStrip contextMenuStrip2;
		private System.Windows.Forms.ContextMenuStrip contextMenuStrip3;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.TextBox File;
		private System.Windows.Forms.Button button3;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.LinkLabel linkLabel1;
	}
}