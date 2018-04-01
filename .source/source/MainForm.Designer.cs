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
    private System.Windows.Forms.MenuStrip menuStrip1;
    
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
      this.components = new System.ComponentModel.Container();
      System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
      this.panel1 = new System.Windows.Forms.Panel();
      this.panel2 = new System.Windows.Forms.Panel();
      this.textBox1 = new System.Windows.Forms.TextBox();
      this.button1 = new System.Windows.Forms.Button();
      this.richTextBox1 = new System.Windows.Forms.RichTextBox();
      this.menuStrip1 = new System.Windows.Forms.MenuStrip();
      this.ckHasPlaylist = new System.Windows.Forms.ToolStripMenuItem();
      this.btnAbort = new System.Windows.Forms.ToolStripMenuItem();
      this.lbLast = new System.Windows.Forms.ToolStripSplitButton();
      this.lbBest = new System.Windows.Forms.ToolStripMenuItem();
      this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
      this.bestaudioToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.lbM4a = new System.Windows.Forms.ToolStripMenuItem();
      this.lbMp3 = new System.Windows.Forms.ToolStripMenuItem();
      this.oggToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripSeparator();
      this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripMenuItem();
      this.lbMp4 = new System.Windows.Forms.ToolStripMenuItem();
      this.webmToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.toolStripMenuItem4 = new System.Windows.Forms.ToolStripSeparator();
      this.gpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.flvToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.wavToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.toolStripMenuItem3 = new System.Windows.Forms.ToolStripMenuItem();
      this.worstaudioToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.worstvideoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.groupBox1 = new System.Windows.Forms.GroupBox();
      this.textBox2 = new System.Windows.Forms.TextBox();
      this.label3 = new System.Windows.Forms.Label();
      this.label2 = new System.Windows.Forms.Label();
      this.numericUpDown2 = new System.Windows.Forms.NumericUpDown();
      this.label1 = new System.Windows.Forms.Label();
      this.numericUpDown1 = new System.Windows.Forms.NumericUpDown();
      this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
      this.tabControl1 = new System.Windows.Forms.TabControl();
      this.tabPage1 = new System.Windows.Forms.TabPage();
      this.tabPage2 = new System.Windows.Forms.TabPage();
      this.statusStrip1 = new System.Windows.Forms.StatusStrip();
      this.panel1.SuspendLayout();
      this.panel2.SuspendLayout();
      this.menuStrip1.SuspendLayout();
      this.groupBox1.SuspendLayout();
      ((System.ComponentModel.ISupportInitialize)(this.numericUpDown2)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).BeginInit();
      this.tabControl1.SuspendLayout();
      this.tabPage1.SuspendLayout();
      this.tabPage2.SuspendLayout();
      this.SuspendLayout();
      // 
      // panel1
      // 
      this.panel1.BackColor = System.Drawing.Color.White;
      this.panel1.Controls.Add(this.panel2);
      this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
      this.panel1.Location = new System.Drawing.Point(0, 0);
      this.panel1.Name = "panel1";
      this.panel1.Size = new System.Drawing.Size(384, 29);
      this.panel1.TabIndex = 5;
      // 
      // panel2
      // 
      this.panel2.Controls.Add(this.textBox1);
      this.panel2.Controls.Add(this.button1);
      this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
      this.panel2.Location = new System.Drawing.Point(0, 0);
      this.panel2.Name = "panel2";
      this.panel2.Size = new System.Drawing.Size(384, 27);
      this.panel2.TabIndex = 7;
      // 
      // textBox1
      // 
      this.textBox1.Dock = System.Windows.Forms.DockStyle.Fill;
      this.textBox1.Font = new System.Drawing.Font("Roboto", 12F, System.Drawing.FontStyle.Bold);
      this.textBox1.Location = new System.Drawing.Point(0, 0);
      this.textBox1.Name = "textBox1";
      this.textBox1.Size = new System.Drawing.Size(337, 27);
      this.textBox1.TabIndex = 8;
      // 
      // button1
      // 
      this.button1.Dock = System.Windows.Forms.DockStyle.Right;
      this.button1.FlatStyle = System.Windows.Forms.FlatStyle.System;
      this.button1.Font = new System.Drawing.Font("Marlett", 14.25F);
      this.button1.Location = new System.Drawing.Point(337, 0);
      this.button1.Name = "button1";
      this.button1.Size = new System.Drawing.Size(47, 27);
      this.button1.TabIndex = 9;
      this.button1.Text = "6";
      this.button1.UseVisualStyleBackColor = true;
      this.button1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Button1MouseDown);
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
      this.richTextBox1.Location = new System.Drawing.Point(3, 3);
      this.richTextBox1.Name = "richTextBox1";
      this.richTextBox1.ReadOnly = true;
      this.richTextBox1.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.ForcedBoth;
      this.richTextBox1.Size = new System.Drawing.Size(370, 369);
      this.richTextBox1.TabIndex = 3;
      this.richTextBox1.Text = "";
      this.richTextBox1.WordWrap = false;
      // 
      // menuStrip1
      // 
      this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ckHasPlaylist,
            this.btnAbort,
            this.lbLast});
      this.menuStrip1.Location = new System.Drawing.Point(0, 29);
      this.menuStrip1.Name = "menuStrip1";
      this.menuStrip1.Size = new System.Drawing.Size(384, 24);
      this.menuStrip1.TabIndex = 11;
      this.menuStrip1.Text = "menuStrip1";
      // 
      // ckHasPlaylist
      // 
      this.ckHasPlaylist.Name = "ckHasPlaylist";
      this.ckHasPlaylist.Size = new System.Drawing.Size(37, 20);
      this.ckHasPlaylist.Text = "PL?";
      // 
      // btnAbort
      // 
      this.btnAbort.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
      this.btnAbort.Name = "btnAbort";
      this.btnAbort.Size = new System.Drawing.Size(92, 20);
      this.btnAbort.Text = "Abort Process";
      this.btnAbort.Visible = false;
      this.btnAbort.Click += new System.EventHandler(this.UI_WorkerProcess_Abort);
      // 
      // lbLast
      // 
      this.lbLast.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
      this.lbLast.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
      this.lbLast.DropDownButtonWidth = 14;
      this.lbLast.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.lbBest,
            this.toolStripSeparator2,
            this.bestaudioToolStripMenuItem,
            this.lbM4a,
            this.lbMp3,
            this.oggToolStripMenuItem,
            this.toolStripMenuItem1,
            this.toolStripMenuItem2,
            this.lbMp4,
            this.webmToolStripMenuItem,
            this.toolStripMenuItem4,
            this.gpToolStripMenuItem,
            this.flvToolStripMenuItem,
            this.wavToolStripMenuItem,
            this.toolStripMenuItem3,
            this.worstaudioToolStripMenuItem,
            this.worstvideoToolStripMenuItem});
      this.lbLast.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.lbLast.Margin = new System.Windows.Forms.Padding(0, 0, 6, 0);
      this.lbLast.Name = "lbLast";
      this.lbLast.Padding = new System.Windows.Forms.Padding(4, 0, 4, 0);
      this.lbLast.Size = new System.Drawing.Size(60, 20);
      this.lbLast.Text = "[last]";
      this.lbLast.ButtonClick += new System.EventHandler(this.Event_BeginDownload);
      // 
      // lbBest
      // 
      this.lbBest.Name = "lbBest";
      this.lbBest.Size = new System.Drawing.Size(133, 22);
      this.lbBest.Text = "best";
      this.lbBest.Click += new System.EventHandler(this.Event_BeginDownloadType);
      // 
      // toolStripSeparator2
      // 
      this.toolStripSeparator2.Name = "toolStripSeparator2";
      this.toolStripSeparator2.Size = new System.Drawing.Size(130, 6);
      // 
      // bestaudioToolStripMenuItem
      // 
      this.bestaudioToolStripMenuItem.Name = "bestaudioToolStripMenuItem";
      this.bestaudioToolStripMenuItem.Size = new System.Drawing.Size(133, 22);
      this.bestaudioToolStripMenuItem.Text = "bestaudio";
      this.bestaudioToolStripMenuItem.Click += new System.EventHandler(this.Event_BeginDownloadType);
      // 
      // lbM4a
      // 
      this.lbM4a.Name = "lbM4a";
      this.lbM4a.Size = new System.Drawing.Size(133, 22);
      this.lbM4a.Text = "m4a";
      this.lbM4a.Click += new System.EventHandler(this.Event_BeginDownloadType);
      // 
      // lbMp3
      // 
      this.lbMp3.Name = "lbMp3";
      this.lbMp3.Size = new System.Drawing.Size(133, 22);
      this.lbMp3.Text = "mp3";
      this.lbMp3.Click += new System.EventHandler(this.Event_BeginDownloadType);
      // 
      // oggToolStripMenuItem
      // 
      this.oggToolStripMenuItem.Name = "oggToolStripMenuItem";
      this.oggToolStripMenuItem.Size = new System.Drawing.Size(133, 22);
      this.oggToolStripMenuItem.Text = "ogg";
      this.oggToolStripMenuItem.Click += new System.EventHandler(this.Event_BeginDownloadType);
      // 
      // toolStripMenuItem1
      // 
      this.toolStripMenuItem1.Name = "toolStripMenuItem1";
      this.toolStripMenuItem1.Size = new System.Drawing.Size(130, 6);
      // 
      // toolStripMenuItem2
      // 
      this.toolStripMenuItem2.Name = "toolStripMenuItem2";
      this.toolStripMenuItem2.Size = new System.Drawing.Size(133, 22);
      this.toolStripMenuItem2.Text = "bestvideo";
      this.toolStripMenuItem2.Click += new System.EventHandler(this.Event_BeginDownloadType);
      // 
      // lbMp4
      // 
      this.lbMp4.Name = "lbMp4";
      this.lbMp4.Size = new System.Drawing.Size(133, 22);
      this.lbMp4.Text = "mp4";
      this.lbMp4.Click += new System.EventHandler(this.Event_BeginDownloadType);
      // 
      // webmToolStripMenuItem
      // 
      this.webmToolStripMenuItem.Name = "webmToolStripMenuItem";
      this.webmToolStripMenuItem.Size = new System.Drawing.Size(133, 22);
      this.webmToolStripMenuItem.Text = "webm";
      this.webmToolStripMenuItem.Click += new System.EventHandler(this.Event_BeginDownloadType);
      // 
      // toolStripMenuItem4
      // 
      this.toolStripMenuItem4.Name = "toolStripMenuItem4";
      this.toolStripMenuItem4.Size = new System.Drawing.Size(130, 6);
      // 
      // gpToolStripMenuItem
      // 
      this.gpToolStripMenuItem.Name = "gpToolStripMenuItem";
      this.gpToolStripMenuItem.Size = new System.Drawing.Size(133, 22);
      this.gpToolStripMenuItem.Text = "3gp";
      this.gpToolStripMenuItem.Click += new System.EventHandler(this.Event_BeginDownloadType);
      // 
      // flvToolStripMenuItem
      // 
      this.flvToolStripMenuItem.Name = "flvToolStripMenuItem";
      this.flvToolStripMenuItem.Size = new System.Drawing.Size(133, 22);
      this.flvToolStripMenuItem.Text = "flv";
      this.flvToolStripMenuItem.Click += new System.EventHandler(this.Event_BeginDownloadType);
      // 
      // wavToolStripMenuItem
      // 
      this.wavToolStripMenuItem.Name = "wavToolStripMenuItem";
      this.wavToolStripMenuItem.Size = new System.Drawing.Size(133, 22);
      this.wavToolStripMenuItem.Text = "wav";
      this.wavToolStripMenuItem.Click += new System.EventHandler(this.Event_BeginDownloadType);
      // 
      // toolStripMenuItem3
      // 
      this.toolStripMenuItem3.Name = "toolStripMenuItem3";
      this.toolStripMenuItem3.Size = new System.Drawing.Size(133, 22);
      this.toolStripMenuItem3.Text = "worst";
      this.toolStripMenuItem3.Click += new System.EventHandler(this.Event_BeginDownloadType);
      // 
      // worstaudioToolStripMenuItem
      // 
      this.worstaudioToolStripMenuItem.Name = "worstaudioToolStripMenuItem";
      this.worstaudioToolStripMenuItem.Size = new System.Drawing.Size(133, 22);
      this.worstaudioToolStripMenuItem.Text = "worstaudio";
      this.worstaudioToolStripMenuItem.Click += new System.EventHandler(this.Event_BeginDownloadType);
      // 
      // worstvideoToolStripMenuItem
      // 
      this.worstvideoToolStripMenuItem.Name = "worstvideoToolStripMenuItem";
      this.worstvideoToolStripMenuItem.Size = new System.Drawing.Size(133, 22);
      this.worstvideoToolStripMenuItem.Text = "worstvideo";
      this.worstvideoToolStripMenuItem.Click += new System.EventHandler(this.Event_BeginDownloadType);
      // 
      // groupBox1
      // 
      this.groupBox1.Controls.Add(this.textBox2);
      this.groupBox1.Controls.Add(this.label3);
      this.groupBox1.Controls.Add(this.label2);
      this.groupBox1.Controls.Add(this.numericUpDown2);
      this.groupBox1.Controls.Add(this.label1);
      this.groupBox1.Controls.Add(this.numericUpDown1);
      this.groupBox1.Enabled = false;
      this.groupBox1.Font = new System.Drawing.Font("Roboto", 12F, System.Drawing.FontStyle.Bold);
      this.groupBox1.Location = new System.Drawing.Point(15, 13);
      this.groupBox1.Name = "groupBox1";
      this.groupBox1.Size = new System.Drawing.Size(365, 106);
      this.groupBox1.TabIndex = 0;
      this.groupBox1.TabStop = false;
      this.groupBox1.Text = "Playlist Options";
      // 
      // textBox2
      // 
      this.textBox2.Location = new System.Drawing.Point(134, 20);
      this.textBox2.Name = "textBox2";
      this.textBox2.Size = new System.Drawing.Size(219, 27);
      this.textBox2.TabIndex = 4;
      // 
      // label3
      // 
      this.label3.AutoSize = true;
      this.label3.Location = new System.Drawing.Point(6, 73);
      this.label3.Name = "label3";
      this.label3.Size = new System.Drawing.Size(105, 19);
      this.label3.TabIndex = 3;
      this.label3.Text = "--playlist-end";
      this.toolTip1.SetToolTip(this.label3, "Playlist video to end at (default is last)");
      // 
      // label2
      // 
      this.label2.AutoSize = true;
      this.label2.Location = new System.Drawing.Point(6, 48);
      this.label2.Name = "label2";
      this.label2.Size = new System.Drawing.Size(111, 19);
      this.label2.TabIndex = 3;
      this.label2.Text = "--playlist-start";
      this.toolTip1.SetToolTip(this.label2, "Playlist video to start at (default is 1)");
      // 
      // numericUpDown2
      // 
      this.numericUpDown2.BorderStyle = System.Windows.Forms.BorderStyle.None;
      this.numericUpDown2.Location = new System.Drawing.Point(152, 74);
      this.numericUpDown2.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
      this.numericUpDown2.Name = "numericUpDown2";
      this.numericUpDown2.Size = new System.Drawing.Size(75, 23);
      this.numericUpDown2.TabIndex = 2;
      this.numericUpDown2.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
      this.numericUpDown2.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
      // 
      // label1
      // 
      this.label1.AutoSize = true;
      this.label1.Location = new System.Drawing.Point(6, 23);
      this.label1.Name = "label1";
      this.label1.Size = new System.Drawing.Size(122, 19);
      this.label1.TabIndex = 3;
      this.label1.Text = " --playlist-items";
      this.toolTip1.SetToolTip(this.label1, resources.GetString("label1.ToolTip"));
      // 
      // numericUpDown1
      // 
      this.numericUpDown1.BorderStyle = System.Windows.Forms.BorderStyle.None;
      this.numericUpDown1.Location = new System.Drawing.Point(152, 49);
      this.numericUpDown1.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
      this.numericUpDown1.Name = "numericUpDown1";
      this.numericUpDown1.Size = new System.Drawing.Size(75, 23);
      this.numericUpDown1.TabIndex = 2;
      this.numericUpDown1.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
      this.numericUpDown1.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
      // 
      // tabControl1
      // 
      this.tabControl1.Alignment = System.Windows.Forms.TabAlignment.Bottom;
      this.tabControl1.Controls.Add(this.tabPage1);
      this.tabControl1.Controls.Add(this.tabPage2);
      this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
      this.tabControl1.ItemSize = new System.Drawing.Size(90, 18);
      this.tabControl1.Location = new System.Drawing.Point(0, 53);
      this.tabControl1.Multiline = true;
      this.tabControl1.Name = "tabControl1";
      this.tabControl1.SelectedIndex = 0;
      this.tabControl1.Size = new System.Drawing.Size(384, 401);
      this.tabControl1.SizeMode = System.Windows.Forms.TabSizeMode.Fixed;
      this.tabControl1.TabIndex = 12;
      // 
      // tabPage1
      // 
      this.tabPage1.Controls.Add(this.richTextBox1);
      this.tabPage1.Location = new System.Drawing.Point(4, 4);
      this.tabPage1.Name = "tabPage1";
      this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
      this.tabPage1.Size = new System.Drawing.Size(376, 375);
      this.tabPage1.TabIndex = 0;
      this.tabPage1.Text = "Output";
      this.tabPage1.UseVisualStyleBackColor = true;
      // 
      // tabPage2
      // 
      this.tabPage2.AutoScroll = true;
      this.tabPage2.Controls.Add(this.groupBox1);
      this.tabPage2.Location = new System.Drawing.Point(4, 4);
      this.tabPage2.Name = "tabPage2";
      this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
      this.tabPage2.Size = new System.Drawing.Size(376, 375);
      this.tabPage2.TabIndex = 1;
      this.tabPage2.Text = "Advanced";
      this.tabPage2.UseVisualStyleBackColor = true;
      // 
      // statusStrip1
      // 
      this.statusStrip1.Location = new System.Drawing.Point(0, 454);
      this.statusStrip1.Name = "statusStrip1";
      this.statusStrip1.Size = new System.Drawing.Size(384, 22);
      this.statusStrip1.TabIndex = 13;
      this.statusStrip1.Text = "statusStrip1";
      // 
      // MainForm
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(384, 476);
      this.Controls.Add(this.tabControl1);
      this.Controls.Add(this.menuStrip1);
      this.Controls.Add(this.panel1);
      this.Controls.Add(this.statusStrip1);
      this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
      this.MinimumSize = new System.Drawing.Size(400, 200);
      this.Name = "MainForm";
      this.Text = "YouTubeDownloadUI";
      this.panel1.ResumeLayout(false);
      this.panel2.ResumeLayout(false);
      this.panel2.PerformLayout();
      this.menuStrip1.ResumeLayout(false);
      this.menuStrip1.PerformLayout();
      this.groupBox1.ResumeLayout(false);
      this.groupBox1.PerformLayout();
      ((System.ComponentModel.ISupportInitialize)(this.numericUpDown2)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).EndInit();
      this.tabControl1.ResumeLayout(false);
      this.tabPage1.ResumeLayout(false);
      this.tabPage2.ResumeLayout(false);
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    private System.Windows.Forms.GroupBox groupBox1;
    private System.Windows.Forms.Label label1;
    private System.Windows.Forms.Label label3;
    private System.Windows.Forms.Label label2;
    private System.Windows.Forms.NumericUpDown numericUpDown2;
    private System.Windows.Forms.NumericUpDown numericUpDown1;
    private System.Windows.Forms.ToolTip toolTip1;
    private System.Windows.Forms.TextBox textBox2;
    private System.Windows.Forms.TabControl tabControl1;
    private System.Windows.Forms.TabPage tabPage1;
    private System.Windows.Forms.TabPage tabPage2;
    private System.Windows.Forms.StatusStrip statusStrip1;
    private System.Windows.Forms.ToolStripSplitButton lbLast;
    private System.Windows.Forms.ToolStripMenuItem lbBest;
    private System.Windows.Forms.ToolStripMenuItem lbMp3;
    private System.Windows.Forms.ToolStripMenuItem lbMp4;
    private System.Windows.Forms.ToolStripMenuItem lbM4a;
    private System.Windows.Forms.ToolStripMenuItem btnAbort;
    private System.Windows.Forms.ToolStripSeparator toolStripMenuItem1;
    private System.Windows.Forms.ToolStripMenuItem gpToolStripMenuItem;
    private System.Windows.Forms.ToolStripMenuItem oggToolStripMenuItem;
    private System.Windows.Forms.ToolStripMenuItem webmToolStripMenuItem;
    private System.Windows.Forms.ToolStripMenuItem flvToolStripMenuItem;
    private System.Windows.Forms.ToolStripMenuItem wavToolStripMenuItem;
    private System.Windows.Forms.ToolStripMenuItem bestaudioToolStripMenuItem;
    private System.Windows.Forms.ToolStripMenuItem worstaudioToolStripMenuItem;
    private System.Windows.Forms.ToolStripMenuItem worstvideoToolStripMenuItem;
    private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
    private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem2;
    private System.Windows.Forms.ToolStripSeparator toolStripMenuItem4;
    private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem3;
  }
}
