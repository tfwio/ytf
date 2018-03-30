namespace YouTubeDownloadUtil
{
  partial class MainForm
  {
    /// <summary>
    /// Designer variable used to keep track of non-visual components.
    /// </summary>
    private System.ComponentModel.IContainer components = null;
    private System.Windows.Forms.TextBox textBox1;
    private System.Windows.Forms.RichTextBox richTextBox1;
    private System.Windows.Forms.Panel panel1;
    private System.Windows.Forms.ToolStripMenuItem ckHasPlaylist;
    private System.Windows.Forms.Button button1;
    private System.Windows.Forms.Panel panel2;
    private System.Windows.Forms.SplitContainer splitContainer1;
    private System.Windows.Forms.MenuStrip menuStrip1;
    private System.Windows.Forms.ToolStripMenuItem lbM4a;
    private System.Windows.Forms.ToolStripMenuItem lbMp3;
    private System.Windows.Forms.ToolStripMenuItem lbMp4;
    private System.Windows.Forms.ToolStripMenuItem lbLast;
    private System.Windows.Forms.ToolStripMenuItem lbBest;
    
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
      this.panel2 = new System.Windows.Forms.Panel();
      this.textBox1 = new System.Windows.Forms.TextBox();
      this.button1 = new System.Windows.Forms.Button();
      this.splitContainer1 = new System.Windows.Forms.SplitContainer();
      this.richTextBox1 = new System.Windows.Forms.RichTextBox();
      this.menuStrip1 = new System.Windows.Forms.MenuStrip();
      this.ckHasPlaylist = new System.Windows.Forms.ToolStripMenuItem();
      this.lbM4a = new System.Windows.Forms.ToolStripMenuItem();
      this.lbMp4 = new System.Windows.Forms.ToolStripMenuItem();
      this.lbMp3 = new System.Windows.Forms.ToolStripMenuItem();
      this.lbBest = new System.Windows.Forms.ToolStripMenuItem();
      this.lbLast = new System.Windows.Forms.ToolStripMenuItem();
      this.panel1.SuspendLayout();
      this.panel2.SuspendLayout();
      this.splitContainer1.Panel2.SuspendLayout();
      this.splitContainer1.SuspendLayout();
      this.menuStrip1.SuspendLayout();
      this.SuspendLayout();
      // 
      // panel1
      // 
      this.panel1.BackColor = System.Drawing.Color.White;
      this.panel1.Controls.Add(this.panel2);
      this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
      this.panel1.Location = new System.Drawing.Point(0, 24);
      this.panel1.Name = "panel1";
      this.panel1.Size = new System.Drawing.Size(521, 29);
      this.panel1.TabIndex = 5;
      // 
      // panel2
      // 
      this.panel2.Controls.Add(this.textBox1);
      this.panel2.Controls.Add(this.button1);
      this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
      this.panel2.Location = new System.Drawing.Point(0, 0);
      this.panel2.Name = "panel2";
      this.panel2.Size = new System.Drawing.Size(521, 27);
      this.panel2.TabIndex = 7;
      // 
      // textBox1
      // 
      this.textBox1.Dock = System.Windows.Forms.DockStyle.Fill;
      this.textBox1.Font = new System.Drawing.Font("Roboto", 12F, System.Drawing.FontStyle.Bold);
      this.textBox1.Location = new System.Drawing.Point(0, 0);
      this.textBox1.Name = "textBox1";
      this.textBox1.Size = new System.Drawing.Size(474, 27);
      this.textBox1.TabIndex = 8;
      // 
      // button1
      // 
      this.button1.Dock = System.Windows.Forms.DockStyle.Right;
      this.button1.FlatStyle = System.Windows.Forms.FlatStyle.System;
      this.button1.Font = new System.Drawing.Font("Marlett", 14.25F);
      this.button1.Location = new System.Drawing.Point(474, 0);
      this.button1.Name = "button1";
      this.button1.Size = new System.Drawing.Size(47, 27);
      this.button1.TabIndex = 9;
      this.button1.Text = "6";
      this.button1.UseVisualStyleBackColor = true;
      this.button1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Button1MouseDown);
      // 
      // splitContainer1
      // 
      this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
      this.splitContainer1.Location = new System.Drawing.Point(0, 53);
      this.splitContainer1.Name = "splitContainer1";
      this.splitContainer1.Panel1Collapsed = true;
      // 
      // splitContainer1.Panel2
      // 
      this.splitContainer1.Panel2.Controls.Add(this.richTextBox1);
      this.splitContainer1.Size = new System.Drawing.Size(521, 109);
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
      this.richTextBox1.Font = new System.Drawing.Font("Consolas", 12F);
      this.richTextBox1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
      this.richTextBox1.Location = new System.Drawing.Point(0, 0);
      this.richTextBox1.Name = "richTextBox1";
      this.richTextBox1.ReadOnly = true;
      this.richTextBox1.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.ForcedBoth;
      this.richTextBox1.Size = new System.Drawing.Size(521, 109);
      this.richTextBox1.TabIndex = 3;
      this.richTextBox1.Text = "";
      this.richTextBox1.WordWrap = false;
      // 
      // menuStrip1
      // 
      this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
      this.ckHasPlaylist,
      this.lbM4a,
      this.lbMp4,
      this.lbMp3,
      this.lbLast,
      this.lbBest});
      this.menuStrip1.Location = new System.Drawing.Point(0, 0);
      this.menuStrip1.Name = "menuStrip1";
      this.menuStrip1.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
      this.menuStrip1.Size = new System.Drawing.Size(521, 24);
      this.menuStrip1.TabIndex = 11;
      this.menuStrip1.Text = "menuStrip1";
      // 
      // ckHasPlaylist
      // 
      this.ckHasPlaylist.Name = "ckHasPlaylist";
      this.ckHasPlaylist.Size = new System.Drawing.Size(37, 20);
      this.ckHasPlaylist.Text = "PL?";
      // 
      // lbM4a
      // 
      this.lbM4a.Name = "lbM4a";
      this.lbM4a.Size = new System.Drawing.Size(42, 20);
      this.lbM4a.Text = "m4a";
      this.lbM4a.Click += new System.EventHandler(this.Event_BeginDownloadType);
      // 
      // lbMp4
      // 
      this.lbMp4.Name = "lbMp4";
      this.lbMp4.Size = new System.Drawing.Size(43, 20);
      this.lbMp4.Text = "mp4";
      this.lbMp4.Click += new System.EventHandler(this.Event_BeginDownloadType);
      // 
      // lbMp3
      // 
      this.lbMp3.Name = "lbMp3";
      this.lbMp3.Size = new System.Drawing.Size(43, 20);
      this.lbMp3.Text = "mp3";
      this.lbMp3.Click += new System.EventHandler(this.Event_BeginDownloadType);
      // 
      // lbBest
      // 
      this.lbBest.Name = "lbBest";
      this.lbBest.Size = new System.Drawing.Size(41, 20);
      this.lbBest.Text = "best";
      this.lbBest.Click += new System.EventHandler(this.Event_BeginDownloadType);
      // 
      // lbLast
      // 
      this.lbLast.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
      this.lbLast.Name = "lbLast";
      this.lbLast.Size = new System.Drawing.Size(45, 20);
      this.lbLast.Text = "[last]";
      this.lbLast.Click += new System.EventHandler(this.Event_BeginDownload);
      // 
      // MainForm
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(521, 162);
      this.Controls.Add(this.splitContainer1);
      this.Controls.Add(this.panel1);
      this.Controls.Add(this.menuStrip1);
      this.DoubleBuffered = true;
      this.MinimumSize = new System.Drawing.Size(400, 200);
      this.Name = "MainForm";
      this.Text = "YouTubeDownloadUI";
      this.panel1.ResumeLayout(false);
      this.panel2.ResumeLayout(false);
      this.panel2.PerformLayout();
      this.splitContainer1.Panel2.ResumeLayout(false);
      this.splitContainer1.ResumeLayout(false);
      this.menuStrip1.ResumeLayout(false);
      this.menuStrip1.PerformLayout();
      this.ResumeLayout(false);
      this.PerformLayout();

    }
  }
}
