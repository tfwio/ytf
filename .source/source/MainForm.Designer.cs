namespace YouTubeDownloadUtil
{
  partial class MainForm
  {
    /// <summary>
    /// Designer variable used to keep track of non-visual components.
    /// </summary>
    private System.ComponentModel.IContainer components = null;
    private System.Windows.Forms.TextBox textBox1;
    private System.Windows.Forms.LinkLabel lbM4a;
    private System.Windows.Forms.LinkLabel lbMp4;
    private System.Windows.Forms.LinkLabel lbBest;
    private System.Windows.Forms.RichTextBox richTextBox1;
    private System.Windows.Forms.Panel panel1;
    private System.Windows.Forms.CheckBox ckHasPlaylist;
    private System.Windows.Forms.LinkLabel lbMp3;
    private System.Windows.Forms.Button button1;
    private System.Windows.Forms.Panel panel2;
    private System.Windows.Forms.LinkLabel lbLast;
    private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
    private System.Windows.Forms.SplitContainer splitContainer1;
    
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
      this.panel1 = new System.Windows.Forms.Panel();
      this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
      this.lbM4a = new System.Windows.Forms.LinkLabel();
      this.lbMp4 = new System.Windows.Forms.LinkLabel();
      this.lbMp3 = new System.Windows.Forms.LinkLabel();
      this.lbBest = new System.Windows.Forms.LinkLabel();
      this.panel2 = new System.Windows.Forms.Panel();
      this.textBox1 = new System.Windows.Forms.TextBox();
      this.ckHasPlaylist = new System.Windows.Forms.CheckBox();
      this.button1 = new System.Windows.Forms.Button();
      this.lbLast = new System.Windows.Forms.LinkLabel();
      this.splitContainer1 = new System.Windows.Forms.SplitContainer();
      this.richTextBox1 = new System.Windows.Forms.RichTextBox();
      this.panel1.SuspendLayout();
      this.flowLayoutPanel1.SuspendLayout();
      this.panel2.SuspendLayout();
      this.splitContainer1.Panel2.SuspendLayout();
      this.splitContainer1.SuspendLayout();
      this.SuspendLayout();
      // 
      // panel1
      // 
      this.panel1.BackColor = System.Drawing.Color.White;
      this.panel1.Controls.Add(this.flowLayoutPanel1);
      this.panel1.Controls.Add(this.panel2);
      this.panel1.Controls.Add(this.lbLast);
      this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
      this.panel1.Location = new System.Drawing.Point(0, 0);
      this.panel1.Name = "panel1";
      this.panel1.Size = new System.Drawing.Size(579, 62);
      this.panel1.TabIndex = 5;
      // 
      // flowLayoutPanel1
      // 
      this.flowLayoutPanel1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
      this.flowLayoutPanel1.Controls.Add(this.lbM4a);
      this.flowLayoutPanel1.Controls.Add(this.lbMp4);
      this.flowLayoutPanel1.Controls.Add(this.lbMp3);
      this.flowLayoutPanel1.Controls.Add(this.lbBest);
      this.flowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Left;
      this.flowLayoutPanel1.Location = new System.Drawing.Point(0, 27);
      this.flowLayoutPanel1.Name = "flowLayoutPanel1";
      this.flowLayoutPanel1.Padding = new System.Windows.Forms.Padding(60, 4, 0, 0);
      this.flowLayoutPanel1.Size = new System.Drawing.Size(283, 35);
      this.flowLayoutPanel1.TabIndex = 8;
      // 
      // lbM4a
      // 
      this.lbM4a.AutoSize = true;
      this.lbM4a.Font = new System.Drawing.Font("Roboto", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lbM4a.LinkBehavior = System.Windows.Forms.LinkBehavior.HoverUnderline;
      this.lbM4a.LinkColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(119)))), ((int)(((byte)(255)))));
      this.lbM4a.Location = new System.Drawing.Point(63, 4);
      this.lbM4a.Name = "lbM4a";
      this.lbM4a.Size = new System.Drawing.Size(47, 23);
      this.lbM4a.TabIndex = 5;
      this.lbM4a.TabStop = true;
      this.lbM4a.Text = "m4a";
      this.lbM4a.VisitedLinkColor = System.Drawing.Color.Red;
      this.lbM4a.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.Event_BeginDownloadType);
      // 
      // lbMp4
      // 
      this.lbMp4.AutoSize = true;
      this.lbMp4.Font = new System.Drawing.Font("Roboto", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lbMp4.LinkBehavior = System.Windows.Forms.LinkBehavior.HoverUnderline;
      this.lbMp4.LinkColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(119)))), ((int)(((byte)(255)))));
      this.lbMp4.Location = new System.Drawing.Point(116, 4);
      this.lbMp4.Name = "lbMp4";
      this.lbMp4.Size = new System.Drawing.Size(48, 23);
      this.lbMp4.TabIndex = 4;
      this.lbMp4.TabStop = true;
      this.lbMp4.Text = "mp4";
      this.lbMp4.VisitedLinkColor = System.Drawing.Color.Red;
      this.lbMp4.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.Event_BeginDownloadType);
      // 
      // lbMp3
      // 
      this.lbMp3.AutoSize = true;
      this.lbMp3.Font = new System.Drawing.Font("Roboto", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lbMp3.LinkBehavior = System.Windows.Forms.LinkBehavior.HoverUnderline;
      this.lbMp3.LinkColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(119)))), ((int)(((byte)(255)))));
      this.lbMp3.Location = new System.Drawing.Point(170, 4);
      this.lbMp3.Name = "lbMp3";
      this.lbMp3.Size = new System.Drawing.Size(48, 23);
      this.lbMp3.TabIndex = 3;
      this.lbMp3.TabStop = true;
      this.lbMp3.Text = "mp3";
      this.lbMp3.VisitedLinkColor = System.Drawing.Color.Red;
      this.lbMp3.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.Event_BeginDownloadType);
      // 
      // lbBest
      // 
      this.lbBest.AutoSize = true;
      this.lbBest.Font = new System.Drawing.Font("Roboto", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lbBest.LinkBehavior = System.Windows.Forms.LinkBehavior.HoverUnderline;
      this.lbBest.LinkColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(119)))), ((int)(((byte)(255)))));
      this.lbBest.Location = new System.Drawing.Point(224, 4);
      this.lbBest.Name = "lbBest";
      this.lbBest.Size = new System.Drawing.Size(47, 23);
      this.lbBest.TabIndex = 3;
      this.lbBest.TabStop = true;
      this.lbBest.Text = "best";
      this.lbBest.VisitedLinkColor = System.Drawing.Color.Red;
      this.lbBest.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.Event_BeginDownloadType);
      // 
      // panel2
      // 
      this.panel2.Controls.Add(this.textBox1);
      this.panel2.Controls.Add(this.ckHasPlaylist);
      this.panel2.Controls.Add(this.button1);
      this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
      this.panel2.Location = new System.Drawing.Point(0, 0);
      this.panel2.Name = "panel2";
      this.panel2.Size = new System.Drawing.Size(579, 27);
      this.panel2.TabIndex = 7;
      // 
      // textBox1
      // 
      this.textBox1.Dock = System.Windows.Forms.DockStyle.Fill;
      this.textBox1.Font = new System.Drawing.Font("Roboto", 12F, System.Drawing.FontStyle.Bold);
      this.textBox1.Location = new System.Drawing.Point(59, 0);
      this.textBox1.Name = "textBox1";
      this.textBox1.Size = new System.Drawing.Size(473, 27);
      this.textBox1.TabIndex = 8;
      // 
      // ckHasPlaylist
      // 
      this.ckHasPlaylist.AutoCheck = false;
      this.ckHasPlaylist.AutoSize = true;
      this.ckHasPlaylist.Dock = System.Windows.Forms.DockStyle.Left;
      this.ckHasPlaylist.Font = new System.Drawing.Font("Roboto", 14.25F, System.Drawing.FontStyle.Bold);
      this.ckHasPlaylist.Location = new System.Drawing.Point(0, 0);
      this.ckHasPlaylist.Name = "ckHasPlaylist";
      this.ckHasPlaylist.Padding = new System.Windows.Forms.Padding(0, 0, 8, 0);
      this.ckHasPlaylist.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
      this.ckHasPlaylist.Size = new System.Drawing.Size(59, 27);
      this.ckHasPlaylist.TabIndex = 6;
      this.ckHasPlaylist.Text = "PL";
      this.ckHasPlaylist.UseVisualStyleBackColor = true;
      // 
      // button1
      // 
      this.button1.Dock = System.Windows.Forms.DockStyle.Right;
      this.button1.FlatStyle = System.Windows.Forms.FlatStyle.System;
      this.button1.Font = new System.Drawing.Font("Marlett", 14.25F);
      this.button1.Location = new System.Drawing.Point(532, 0);
      this.button1.Name = "button1";
      this.button1.Size = new System.Drawing.Size(47, 27);
      this.button1.TabIndex = 9;
      this.button1.Text = "6";
      this.button1.UseVisualStyleBackColor = true;
      this.button1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Button1MouseDown);
      // 
      // lbLast
      // 
      this.lbLast.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
      this.lbLast.AutoSize = true;
      this.lbLast.Font = new System.Drawing.Font("Roboto", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lbLast.LinkBehavior = System.Windows.Forms.LinkBehavior.HoverUnderline;
      this.lbLast.LinkColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(119)))), ((int)(((byte)(255)))));
      this.lbLast.Location = new System.Drawing.Point(516, 31);
      this.lbLast.Name = "lbLast";
      this.lbLast.Size = new System.Drawing.Size(51, 23);
      this.lbLast.TabIndex = 3;
      this.lbLast.TabStop = true;
      this.lbLast.Text = "[last]";
      this.lbLast.VisitedLinkColor = System.Drawing.Color.Red;
      this.lbLast.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.Event_BeginDownload);
      // 
      // splitContainer1
      // 
      this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
      this.splitContainer1.Location = new System.Drawing.Point(0, 62);
      this.splitContainer1.Name = "splitContainer1";
      this.splitContainer1.Panel1Collapsed = true;
      // 
      // splitContainer1.Panel2
      // 
      this.splitContainer1.Panel2.Controls.Add(this.richTextBox1);
      this.splitContainer1.Size = new System.Drawing.Size(579, 238);
      this.splitContainer1.SplitterDistance = 166;
      this.splitContainer1.TabIndex = 6;
      // 
      // richTextBox1
      // 
      this.richTextBox1.BackColor = System.Drawing.Color.White;
      this.richTextBox1.BorderStyle = System.Windows.Forms.BorderStyle.None;
      this.richTextBox1.CausesValidation = false;
      this.richTextBox1.DetectUrls = false;
      this.richTextBox1.Dock = System.Windows.Forms.DockStyle.Fill;
      this.richTextBox1.Font = new System.Drawing.Font("Roboto", 12F);
      this.richTextBox1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
      this.richTextBox1.Location = new System.Drawing.Point(0, 0);
      this.richTextBox1.Name = "richTextBox1";
      this.richTextBox1.ReadOnly = true;
      this.richTextBox1.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.ForcedBoth;
      this.richTextBox1.Size = new System.Drawing.Size(579, 238);
      this.richTextBox1.TabIndex = 3;
      this.richTextBox1.Text = "";
      this.richTextBox1.WordWrap = false;
      // 
      // MainForm
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(579, 300);
      this.Controls.Add(this.splitContainer1);
      this.Controls.Add(this.panel1);
      this.DoubleBuffered = true;
      this.MinimumSize = new System.Drawing.Size(400, 200);
      this.Name = "MainForm";
      this.Text = "YouTubeDownloadUI";
      this.panel1.ResumeLayout(false);
      this.panel1.PerformLayout();
      this.flowLayoutPanel1.ResumeLayout(false);
      this.flowLayoutPanel1.PerformLayout();
      this.panel2.ResumeLayout(false);
      this.panel2.PerformLayout();
      this.splitContainer1.Panel2.ResumeLayout(false);
      this.splitContainer1.ResumeLayout(false);
      this.ResumeLayout(false);

    }
  }
}
