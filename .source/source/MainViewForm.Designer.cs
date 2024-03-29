﻿/*
 * User: xo
 * Date: 3/27/2018
 * Time: 7:14 AM
 */
namespace YouTubeDownloadUI
{
  partial class MainViewForm
  {
    /// <summary>
    /// Designer variable used to keep track of non-visual components.
    /// </summary>
    private System.ComponentModel.IContainer components = null;
    private System.Windows.Forms.RichTextBox richTextBox1;
    private System.Windows.Forms.Panel panel1;
    private System.Windows.Forms.Panel panel2;
    private System.Windows.Forms.TextBox textBox1;
    private System.Windows.Forms.Button button1;
    private System.Windows.Forms.CheckBox ckEmbedSubs;
    private System.Windows.Forms.CheckBox ckVerbose;
    private System.Windows.Forms.CheckBox ckUsePlaylist;
    private System.Windows.Forms.CheckBox ckHasPlaylist;
    private System.Windows.Forms.LinkLabel linkLabel4;
    private System.Windows.Forms.LinkLabel linkLabel3;
    private System.Windows.Forms.LinkLabel linkLabel2;
    private System.Windows.Forms.LinkLabel linkLabel1;
    
    /// <summary>
    /// Disposes resources used by the form.
    /// </summary>
    /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
    protected override void Dispose(bool disposing)
    {
      if (disposing) {
        if (components != null) {
          components.Dispose();
        }
      }
      base.Dispose(disposing);
    }
    
    /// <summary>
    /// This method is required for Windows Forms designer support.
    /// Do not change the method contents inside the source code editor. The Forms designer might
    /// not be able to load this method if it was changed manually.
    /// </summary>
    private void InitializeComponent()
    {
      this.richTextBox1 = new System.Windows.Forms.RichTextBox();
      this.panel1 = new System.Windows.Forms.Panel();
      this.panel2 = new System.Windows.Forms.Panel();
      this.textBox1 = new System.Windows.Forms.TextBox();
      this.button1 = new System.Windows.Forms.Button();
      this.ckEmbedSubs = new System.Windows.Forms.CheckBox();
      this.ckVerbose = new System.Windows.Forms.CheckBox();
      this.ckUsePlaylist = new System.Windows.Forms.CheckBox();
      this.ckHasPlaylist = new System.Windows.Forms.CheckBox();
      this.linkLabel4 = new System.Windows.Forms.LinkLabel();
      this.linkLabel3 = new System.Windows.Forms.LinkLabel();
      this.linkLabel2 = new System.Windows.Forms.LinkLabel();
      this.linkLabel1 = new System.Windows.Forms.LinkLabel();
      this.panel1.SuspendLayout();
      this.panel2.SuspendLayout();
      this.SuspendLayout();
      // 
      // richTextBox1
      // 
      this.richTextBox1.BackColor = System.Drawing.SystemColors.ControlDark;
      this.richTextBox1.BorderStyle = System.Windows.Forms.BorderStyle.None;
      this.richTextBox1.Dock = System.Windows.Forms.DockStyle.Fill;
      this.richTextBox1.Location = new System.Drawing.Point(0, 108);
      this.richTextBox1.Name = "richTextBox1";
      this.richTextBox1.ReadOnly = true;
      this.richTextBox1.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.ForcedBoth;
      this.richTextBox1.Size = new System.Drawing.Size(578, 265);
      this.richTextBox1.TabIndex = 6;
      this.richTextBox1.Text = "";
      this.richTextBox1.WordWrap = false;
      // 
      // panel1
      // 
      this.panel1.Controls.Add(this.panel2);
      this.panel1.Controls.Add(this.ckEmbedSubs);
      this.panel1.Controls.Add(this.ckVerbose);
      this.panel1.Controls.Add(this.ckUsePlaylist);
      this.panel1.Controls.Add(this.ckHasPlaylist);
      this.panel1.Controls.Add(this.linkLabel4);
      this.panel1.Controls.Add(this.linkLabel3);
      this.panel1.Controls.Add(this.linkLabel2);
      this.panel1.Controls.Add(this.linkLabel1);
      this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
      this.panel1.Location = new System.Drawing.Point(0, 0);
      this.panel1.Name = "panel1";
      this.panel1.Size = new System.Drawing.Size(578, 108);
      this.panel1.TabIndex = 7;
      // 
      // panel2
      // 
      this.panel2.Controls.Add(this.textBox1);
      this.panel2.Controls.Add(this.button1);
      this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
      this.panel2.Location = new System.Drawing.Point(0, 0);
      this.panel2.Name = "panel2";
      this.panel2.Padding = new System.Windows.Forms.Padding(9);
      this.panel2.Size = new System.Drawing.Size(578, 42);
      this.panel2.TabIndex = 7;
      // 
      // textBox1
      // 
      this.textBox1.BorderStyle = System.Windows.Forms.BorderStyle.None;
      this.textBox1.Dock = System.Windows.Forms.DockStyle.Fill;
      this.textBox1.Font = new System.Drawing.Font("Roboto", 14.25F, System.Drawing.FontStyle.Bold);
      this.textBox1.Location = new System.Drawing.Point(9, 9);
      this.textBox1.Name = "textBox1";
      this.textBox1.Size = new System.Drawing.Size(536, 23);
      this.textBox1.TabIndex = 8;
      // 
      // button1
      // 
      this.button1.Dock = System.Windows.Forms.DockStyle.Right;
      this.button1.FlatStyle = System.Windows.Forms.FlatStyle.System;
      this.button1.Font = new System.Drawing.Font("Marlett", 14F);
      this.button1.Location = new System.Drawing.Point(545, 9);
      this.button1.Name = "button1";
      this.button1.Size = new System.Drawing.Size(24, 24);
      this.button1.TabIndex = 9;
      this.button1.Text = "6";
      this.button1.UseVisualStyleBackColor = true;
      // 
      // ckEmbedSubs
      // 
      this.ckEmbedSubs.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
      this.ckEmbedSubs.AutoSize = true;
      this.ckEmbedSubs.Checked = true;
      this.ckEmbedSubs.CheckState = System.Windows.Forms.CheckState.Checked;
      this.ckEmbedSubs.Location = new System.Drawing.Point(237, 76);
      this.ckEmbedSubs.Name = "ckEmbedSubs";
      this.ckEmbedSubs.Size = new System.Drawing.Size(86, 17);
      this.ckEmbedSubs.TabIndex = 6;
      this.ckEmbedSubs.Text = "Embed Subs";
      this.ckEmbedSubs.UseVisualStyleBackColor = true;
      // 
      // ckVerbose
      // 
      this.ckVerbose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
      this.ckVerbose.AutoSize = true;
      this.ckVerbose.Checked = true;
      this.ckVerbose.CheckState = System.Windows.Forms.CheckState.Checked;
      this.ckVerbose.Location = new System.Drawing.Point(329, 76);
      this.ckVerbose.Name = "ckVerbose";
      this.ckVerbose.Size = new System.Drawing.Size(65, 17);
      this.ckVerbose.TabIndex = 6;
      this.ckVerbose.Text = "Verbose";
      this.ckVerbose.UseVisualStyleBackColor = true;
      // 
      // ckUsePlaylist
      // 
      this.ckUsePlaylist.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
      this.ckUsePlaylist.AutoSize = true;
      this.ckUsePlaylist.Location = new System.Drawing.Point(486, 76);
      this.ckUsePlaylist.Name = "ckUsePlaylist";
      this.ckUsePlaylist.Size = new System.Drawing.Size(80, 17);
      this.ckUsePlaylist.TabIndex = 6;
      this.ckUsePlaylist.Text = "Use Playlist";
      this.ckUsePlaylist.UseVisualStyleBackColor = true;
      // 
      // ckHasPlaylist
      // 
      this.ckHasPlaylist.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
      this.ckHasPlaylist.AutoCheck = false;
      this.ckHasPlaylist.AutoSize = true;
      this.ckHasPlaylist.Location = new System.Drawing.Point(400, 76);
      this.ckHasPlaylist.Name = "ckHasPlaylist";
      this.ckHasPlaylist.Size = new System.Drawing.Size(80, 17);
      this.ckHasPlaylist.TabIndex = 6;
      this.ckHasPlaylist.Text = "Has Playlist";
      this.ckHasPlaylist.UseVisualStyleBackColor = true;
      // 
      // linkLabel4
      // 
      this.linkLabel4.AutoSize = true;
      this.linkLabel4.Cursor = System.Windows.Forms.Cursors.Hand;
      this.linkLabel4.Font = new System.Drawing.Font("Roboto", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.linkLabel4.Location = new System.Drawing.Point(118, 52);
      this.linkLabel4.Name = "linkLabel4";
      this.linkLabel4.Size = new System.Drawing.Size(48, 23);
      this.linkLabel4.TabIndex = 3;
      this.linkLabel4.TabStop = true;
      this.linkLabel4.Text = "mp3";
      // 
      // linkLabel3
      // 
      this.linkLabel3.AutoSize = true;
      this.linkLabel3.Cursor = System.Windows.Forms.Cursors.Hand;
      this.linkLabel3.Font = new System.Drawing.Font("Roboto", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.linkLabel3.Location = new System.Drawing.Point(172, 52);
      this.linkLabel3.Name = "linkLabel3";
      this.linkLabel3.Size = new System.Drawing.Size(47, 23);
      this.linkLabel3.TabIndex = 3;
      this.linkLabel3.TabStop = true;
      this.linkLabel3.Text = "best";
      // 
      // linkLabel2
      // 
      this.linkLabel2.AutoSize = true;
      this.linkLabel2.Cursor = System.Windows.Forms.Cursors.Hand;
      this.linkLabel2.Font = new System.Drawing.Font("Roboto", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.linkLabel2.Location = new System.Drawing.Point(64, 52);
      this.linkLabel2.Name = "linkLabel2";
      this.linkLabel2.Size = new System.Drawing.Size(48, 23);
      this.linkLabel2.TabIndex = 4;
      this.linkLabel2.TabStop = true;
      this.linkLabel2.Text = "mp4";
      // 
      // linkLabel1
      // 
      this.linkLabel1.AutoSize = true;
      this.linkLabel1.Cursor = System.Windows.Forms.Cursors.Hand;
      this.linkLabel1.Font = new System.Drawing.Font("Roboto", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.linkLabel1.Location = new System.Drawing.Point(11, 52);
      this.linkLabel1.Name = "linkLabel1";
      this.linkLabel1.Size = new System.Drawing.Size(47, 23);
      this.linkLabel1.TabIndex = 5;
      this.linkLabel1.TabStop = true;
      this.linkLabel1.Text = "m4a";
      // 
      // MainViewForm
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(578, 373);
      this.Controls.Add(this.richTextBox1);
      this.Controls.Add(this.panel1);
      this.Name = "MainViewForm";
      this.Text = "Form1";
      this.panel1.ResumeLayout(false);
      this.panel1.PerformLayout();
      this.panel2.ResumeLayout(false);
      this.panel2.PerformLayout();
      this.ResumeLayout(false);

    }
  }
}
