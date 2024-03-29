﻿namespace YouTubeDownloadUtil
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
    private System.Windows.Forms.Button btnAbortProcess;
    private System.Windows.Forms.Panel panel2;
    
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
      this.btnAbortProcess = new System.Windows.Forms.Button();
      this.richTextBox1 = new System.Windows.Forms.RichTextBox();
      this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
      this.statusStrip1 = new System.Windows.Forms.StatusStrip();
      this.statText = new System.Windows.Forms.ToolStripStatusLabel();
      this.toolStrip1 = new System.Windows.Forms.ToolStrip();
      this.lbMaxDownloads = new System.Windows.Forms.ToolStripLabel();
      this.textMaxDownloads = new System.Windows.Forms.ToolStripTextBox();
      this.ckHasPlaylist = new System.Windows.Forms.ToolStripDropDownButton();
      this.toolStripLabel1 = new System.Windows.Forms.ToolStripLabel();
      this.toolStripTextBox1 = new System.Windows.Forms.ToolStripTextBox();
      this.toolStripLabel2 = new System.Windows.Forms.ToolStripLabel();
      this.toolStripTextBox2 = new System.Windows.Forms.ToolStripTextBox();
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
      this.worstvideoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.worstaudioToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.toolStripMenuItem3 = new System.Windows.Forms.ToolStripMenuItem();
      this.wavToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.flvToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.toolStripMenuItem5 = new System.Windows.Forms.ToolStripSeparator();
      this.singleJSONToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.dumpSingleJSONJToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.statusControls = new System.Windows.Forms.TableLayoutPanel();
      this.textFileName = new System.Windows.Forms.TextBox();
      this.panelFileName = new System.Windows.Forms.Panel();
      this.label1 = new System.Windows.Forms.Label();
      this.statItemCount = new wyDay.Controls.Windows7ProgressBar();
      this.statCurrent = new wyDay.Controls.Windows7ProgressBar();
      this.panel1.SuspendLayout();
      this.panel2.SuspendLayout();
      this.statusStrip1.SuspendLayout();
      this.toolStrip1.SuspendLayout();
      this.statusControls.SuspendLayout();
      this.panelFileName.SuspendLayout();
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
      this.panel2.Controls.Add(this.btnAbortProcess);
      this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
      this.panel2.Location = new System.Drawing.Point(0, 0);
      this.panel2.Name = "panel2";
      this.panel2.Size = new System.Drawing.Size(384, 27);
      this.panel2.TabIndex = 7;
      // 
      // textBox1
      // 
      this.textBox1.Dock = System.Windows.Forms.DockStyle.Fill;
      this.textBox1.Location = new System.Drawing.Point(0, 0);
      this.textBox1.Name = "textBox1";
      this.textBox1.Size = new System.Drawing.Size(353, 20);
      this.textBox1.TabIndex = 8;
      // 
      // btnAbortProcess
      // 
      this.btnAbortProcess.Dock = System.Windows.Forms.DockStyle.Right;
      this.btnAbortProcess.FlatStyle = System.Windows.Forms.FlatStyle.System;
      this.btnAbortProcess.Font = new System.Drawing.Font("Marlett", 14.25F);
      this.btnAbortProcess.Location = new System.Drawing.Point(353, 0);
      this.btnAbortProcess.Name = "btnAbortProcess";
      this.btnAbortProcess.Size = new System.Drawing.Size(31, 27);
      this.btnAbortProcess.TabIndex = 9;
      this.btnAbortProcess.Text = "6";
      this.btnAbortProcess.UseVisualStyleBackColor = true;
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
      this.richTextBox1.Location = new System.Drawing.Point(0, 54);
      this.richTextBox1.Name = "richTextBox1";
      this.richTextBox1.ReadOnly = true;
      this.richTextBox1.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.ForcedBoth;
      this.richTextBox1.Size = new System.Drawing.Size(384, 307);
      this.richTextBox1.TabIndex = 3;
      this.richTextBox1.Text = "";
      this.richTextBox1.WordWrap = false;
      // 
      // statusStrip1
      // 
      this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.statText});
      this.statusStrip1.Location = new System.Drawing.Point(0, 415);
      this.statusStrip1.Name = "statusStrip1";
      this.statusStrip1.Size = new System.Drawing.Size(384, 22);
      this.statusStrip1.TabIndex = 13;
      this.statusStrip1.Text = "statusStrip1";
      // 
      // statText
      // 
      this.statText.Name = "statText";
      this.statText.Size = new System.Drawing.Size(66, 17);
      this.statText.Text = "youtube-dl";
      // 
      // toolStrip1
      // 
      this.toolStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
      this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.lbMaxDownloads,
            this.textMaxDownloads,
            this.ckHasPlaylist,
            this.btnAbort,
            this.lbLast});
      this.toolStrip1.Location = new System.Drawing.Point(0, 29);
      this.toolStrip1.Name = "toolStrip1";
      this.toolStrip1.RenderMode = System.Windows.Forms.ToolStripRenderMode.Professional;
      this.toolStrip1.Size = new System.Drawing.Size(384, 25);
      this.toolStrip1.TabIndex = 15;
      this.toolStrip1.Text = "toolStrip1";
      // 
      // lbMaxDownloads
      // 
      this.lbMaxDownloads.Name = "lbMaxDownloads";
      this.lbMaxDownloads.Size = new System.Drawing.Size(94, 22);
      this.lbMaxDownloads.Text = "Max Downloads:";
      // 
      // textMaxDownloads
      // 
      this.textMaxDownloads.Name = "textMaxDownloads";
      this.textMaxDownloads.Size = new System.Drawing.Size(24, 25);
      // 
      // ckHasPlaylist
      // 
      this.ckHasPlaylist.AutoToolTip = false;
      this.ckHasPlaylist.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripLabel1,
            this.toolStripTextBox1,
            this.toolStripLabel2,
            this.toolStripTextBox2});
      this.ckHasPlaylist.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.ckHasPlaylist.Name = "ckHasPlaylist";
      this.ckHasPlaylist.Size = new System.Drawing.Size(102, 22);
      this.ckHasPlaylist.Text = "Playlist Options";
      this.ckHasPlaylist.Visible = false;
      // 
      // toolStripLabel1
      // 
      this.toolStripLabel1.Name = "toolStripLabel1";
      this.toolStripLabel1.Size = new System.Drawing.Size(77, 15);
      this.toolStripLabel1.Text = "Α: (start/first)";
      // 
      // toolStripTextBox1
      // 
      this.toolStripTextBox1.Name = "toolStripTextBox1";
      this.toolStripTextBox1.Size = new System.Drawing.Size(64, 23);
      this.toolStripTextBox1.Text = "1";
      // 
      // toolStripLabel2
      // 
      this.toolStripLabel2.Name = "toolStripLabel2";
      this.toolStripLabel2.Size = new System.Drawing.Size(73, 15);
      this.toolStripLabel2.Text = "Ω: (end/last)";
      // 
      // toolStripTextBox2
      // 
      this.toolStripTextBox2.Name = "toolStripTextBox2";
      this.toolStripTextBox2.Size = new System.Drawing.Size(64, 23);
      this.toolStripTextBox2.Text = "1";
      // 
      // btnAbort
      // 
      this.btnAbort.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
      this.btnAbort.Name = "btnAbort";
      this.btnAbort.Size = new System.Drawing.Size(92, 25);
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
            this.worstvideoToolStripMenuItem,
            this.worstaudioToolStripMenuItem,
            this.toolStripMenuItem3,
            this.wavToolStripMenuItem,
            this.flvToolStripMenuItem,
            this.toolStripMenuItem5,
            this.singleJSONToolStripMenuItem,
            this.dumpSingleJSONJToolStripMenuItem});
      this.lbLast.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.lbLast.Name = "lbLast";
      this.lbLast.Size = new System.Drawing.Size(52, 22);
      this.lbLast.Text = "[last]";
      this.lbLast.ButtonClick += new System.EventHandler(this.Event_BeginDownload);
      // 
      // lbBest
      // 
      this.lbBest.Name = "lbBest";
      this.lbBest.Size = new System.Drawing.Size(189, 22);
      this.lbBest.Text = "best";
      this.lbBest.Click += new System.EventHandler(this.Event_BeginDownloadType);
      // 
      // toolStripSeparator2
      // 
      this.toolStripSeparator2.Name = "toolStripSeparator2";
      this.toolStripSeparator2.Size = new System.Drawing.Size(186, 6);
      // 
      // bestaudioToolStripMenuItem
      // 
      this.bestaudioToolStripMenuItem.Name = "bestaudioToolStripMenuItem";
      this.bestaudioToolStripMenuItem.Size = new System.Drawing.Size(189, 22);
      this.bestaudioToolStripMenuItem.Text = "bestaudio";
      this.bestaudioToolStripMenuItem.Click += new System.EventHandler(this.Event_BeginDownloadType);
      // 
      // lbM4a
      // 
      this.lbM4a.Name = "lbM4a";
      this.lbM4a.Size = new System.Drawing.Size(189, 22);
      this.lbM4a.Text = "m4a";
      this.lbM4a.Click += new System.EventHandler(this.Event_BeginDownloadType);
      // 
      // lbMp3
      // 
      this.lbMp3.Name = "lbMp3";
      this.lbMp3.Size = new System.Drawing.Size(189, 22);
      this.lbMp3.Text = "mp3";
      this.lbMp3.Click += new System.EventHandler(this.Event_BeginDownloadType);
      // 
      // oggToolStripMenuItem
      // 
      this.oggToolStripMenuItem.Name = "oggToolStripMenuItem";
      this.oggToolStripMenuItem.Size = new System.Drawing.Size(189, 22);
      this.oggToolStripMenuItem.Text = "ogg";
      this.oggToolStripMenuItem.Click += new System.EventHandler(this.Event_BeginDownloadType);
      // 
      // toolStripMenuItem1
      // 
      this.toolStripMenuItem1.Name = "toolStripMenuItem1";
      this.toolStripMenuItem1.Size = new System.Drawing.Size(186, 6);
      // 
      // toolStripMenuItem2
      // 
      this.toolStripMenuItem2.Name = "toolStripMenuItem2";
      this.toolStripMenuItem2.Size = new System.Drawing.Size(189, 22);
      this.toolStripMenuItem2.Text = "bestvideo";
      this.toolStripMenuItem2.Click += new System.EventHandler(this.Event_BeginDownloadType);
      // 
      // lbMp4
      // 
      this.lbMp4.Name = "lbMp4";
      this.lbMp4.Size = new System.Drawing.Size(189, 22);
      this.lbMp4.Text = "mp4";
      this.lbMp4.Click += new System.EventHandler(this.Event_BeginDownloadType);
      // 
      // webmToolStripMenuItem
      // 
      this.webmToolStripMenuItem.Name = "webmToolStripMenuItem";
      this.webmToolStripMenuItem.Size = new System.Drawing.Size(189, 22);
      this.webmToolStripMenuItem.Text = "webm";
      this.webmToolStripMenuItem.Click += new System.EventHandler(this.Event_BeginDownloadType);
      // 
      // toolStripMenuItem4
      // 
      this.toolStripMenuItem4.Name = "toolStripMenuItem4";
      this.toolStripMenuItem4.Size = new System.Drawing.Size(186, 6);
      // 
      // gpToolStripMenuItem
      // 
      this.gpToolStripMenuItem.Name = "gpToolStripMenuItem";
      this.gpToolStripMenuItem.Size = new System.Drawing.Size(189, 22);
      this.gpToolStripMenuItem.Text = "3gp";
      this.gpToolStripMenuItem.Click += new System.EventHandler(this.Event_BeginDownloadType);
      // 
      // worstvideoToolStripMenuItem
      // 
      this.worstvideoToolStripMenuItem.Name = "worstvideoToolStripMenuItem";
      this.worstvideoToolStripMenuItem.Size = new System.Drawing.Size(189, 22);
      this.worstvideoToolStripMenuItem.Text = "worstvideo";
      this.worstvideoToolStripMenuItem.Click += new System.EventHandler(this.Event_BeginDownloadType);
      // 
      // worstaudioToolStripMenuItem
      // 
      this.worstaudioToolStripMenuItem.Name = "worstaudioToolStripMenuItem";
      this.worstaudioToolStripMenuItem.Size = new System.Drawing.Size(189, 22);
      this.worstaudioToolStripMenuItem.Text = "worstaudio";
      this.worstaudioToolStripMenuItem.Click += new System.EventHandler(this.Event_BeginDownloadType);
      // 
      // toolStripMenuItem3
      // 
      this.toolStripMenuItem3.Name = "toolStripMenuItem3";
      this.toolStripMenuItem3.Size = new System.Drawing.Size(189, 22);
      this.toolStripMenuItem3.Text = "worst";
      this.toolStripMenuItem3.Click += new System.EventHandler(this.Event_BeginDownloadType);
      // 
      // wavToolStripMenuItem
      // 
      this.wavToolStripMenuItem.Name = "wavToolStripMenuItem";
      this.wavToolStripMenuItem.Size = new System.Drawing.Size(189, 22);
      this.wavToolStripMenuItem.Text = "wav";
      this.wavToolStripMenuItem.Click += new System.EventHandler(this.Event_BeginDownloadType);
      // 
      // flvToolStripMenuItem
      // 
      this.flvToolStripMenuItem.Name = "flvToolStripMenuItem";
      this.flvToolStripMenuItem.Size = new System.Drawing.Size(189, 22);
      this.flvToolStripMenuItem.Text = "flv";
      this.flvToolStripMenuItem.Click += new System.EventHandler(this.Event_BeginDownloadType);
      // 
      // toolStripMenuItem5
      // 
      this.toolStripMenuItem5.Name = "toolStripMenuItem5";
      this.toolStripMenuItem5.Size = new System.Drawing.Size(186, 6);
      // 
      // singleJSONToolStripMenuItem
      // 
      this.singleJSONToolStripMenuItem.Name = "singleJSONToolStripMenuItem";
      this.singleJSONToolStripMenuItem.Size = new System.Drawing.Size(189, 22);
      this.singleJSONToolStripMenuItem.Text = "dump JSON \'-j\'";
      this.singleJSONToolStripMenuItem.Click += new System.EventHandler(this.Event_BeginDownloadType);
      // 
      // dumpSingleJSONJToolStripMenuItem
      // 
      this.dumpSingleJSONJToolStripMenuItem.Name = "dumpSingleJSONJToolStripMenuItem";
      this.dumpSingleJSONJToolStripMenuItem.Size = new System.Drawing.Size(189, 22);
      this.dumpSingleJSONJToolStripMenuItem.Text = "dump single JSON \'-J\'";
      this.dumpSingleJSONJToolStripMenuItem.Click += new System.EventHandler(this.Event_BeginDownloadType);
      // 
      // statusControls
      // 
      this.statusControls.AutoSize = true;
      this.statusControls.ColumnCount = 2;
      this.statusControls.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 80F));
      this.statusControls.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
      this.statusControls.Controls.Add(this.statItemCount, 0, 0);
      this.statusControls.Controls.Add(this.statCurrent, 0, 0);
      this.statusControls.Controls.Add(this.panelFileName, 0, 1);
      this.statusControls.Dock = System.Windows.Forms.DockStyle.Bottom;
      this.statusControls.Location = new System.Drawing.Point(0, 361);
      this.statusControls.Name = "statusControls";
      this.statusControls.RowCount = 2;
      this.statusControls.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
      this.statusControls.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 32F));
      this.statusControls.Size = new System.Drawing.Size(384, 54);
      this.statusControls.TabIndex = 16;
      // 
      // textFileName
      // 
      this.textFileName.Dock = System.Windows.Forms.DockStyle.Fill;
      this.textFileName.Location = new System.Drawing.Point(69, 0);
      this.textFileName.Name = "textFileName";
      this.textFileName.Size = new System.Drawing.Size(309, 20);
      this.textFileName.TabIndex = 17;
      // 
      // panelFileName
      // 
      this.statusControls.SetColumnSpan(this.panelFileName, 2);
      this.panelFileName.Controls.Add(this.textFileName);
      this.panelFileName.Controls.Add(this.label1);
      this.panelFileName.Dock = System.Windows.Forms.DockStyle.Fill;
      this.panelFileName.Location = new System.Drawing.Point(3, 25);
      this.panelFileName.Name = "panelFileName";
      this.panelFileName.Size = new System.Drawing.Size(378, 26);
      this.panelFileName.TabIndex = 2;
      // 
      // label1
      // 
      this.label1.Dock = System.Windows.Forms.DockStyle.Left;
      this.label1.Location = new System.Drawing.Point(0, 0);
      this.label1.Name = "label1";
      this.label1.Size = new System.Drawing.Size(69, 26);
      this.label1.TabIndex = 18;
      this.label1.Text = "File Name";
      this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
      // 
      // statItemCount
      // 
      this.statItemCount.ContainerControl = this;
      this.statItemCount.Dock = System.Windows.Forms.DockStyle.Fill;
      this.statItemCount.Location = new System.Drawing.Point(310, 3);
      this.statItemCount.Name = "statItemCount";
      this.statItemCount.Size = new System.Drawing.Size(71, 16);
      this.statItemCount.TabIndex = 1;
      // 
      // statCurrent
      // 
      this.statCurrent.ContainerControl = this;
      this.statCurrent.Dock = System.Windows.Forms.DockStyle.Fill;
      this.statCurrent.Location = new System.Drawing.Point(3, 3);
      this.statCurrent.Name = "statCurrent";
      this.statCurrent.Size = new System.Drawing.Size(301, 16);
      this.statCurrent.TabIndex = 0;
      // 
      // MainForm
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(384, 437);
      this.Controls.Add(this.richTextBox1);
      this.Controls.Add(this.statusControls);
      this.Controls.Add(this.toolStrip1);
      this.Controls.Add(this.panel1);
      this.Controls.Add(this.statusStrip1);
      this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
      this.MinimumSize = new System.Drawing.Size(400, 200);
      this.Name = "MainForm";
      this.Text = "YouTubeDownloadUI";
      this.panel1.ResumeLayout(false);
      this.panel2.ResumeLayout(false);
      this.panel2.PerformLayout();
      this.statusStrip1.ResumeLayout(false);
      this.statusStrip1.PerformLayout();
      this.toolStrip1.ResumeLayout(false);
      this.toolStrip1.PerformLayout();
      this.statusControls.ResumeLayout(false);
      this.panelFileName.ResumeLayout(false);
      this.panelFileName.PerformLayout();
      this.ResumeLayout(false);
      this.PerformLayout();

    }
    private System.Windows.Forms.ToolTip toolTip1;
    private System.Windows.Forms.StatusStrip statusStrip1;
    private System.Windows.Forms.ToolStrip toolStrip1;
    private System.Windows.Forms.ToolStripMenuItem btnAbort;
    private System.Windows.Forms.ToolStripSplitButton lbLast;
    private System.Windows.Forms.ToolStripMenuItem lbBest;
    private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
    private System.Windows.Forms.ToolStripMenuItem bestaudioToolStripMenuItem;
    private System.Windows.Forms.ToolStripMenuItem lbM4a;
    private System.Windows.Forms.ToolStripMenuItem lbMp3;
    private System.Windows.Forms.ToolStripMenuItem oggToolStripMenuItem;
    private System.Windows.Forms.ToolStripSeparator toolStripMenuItem1;
    private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem2;
    private System.Windows.Forms.ToolStripMenuItem lbMp4;
    private System.Windows.Forms.ToolStripMenuItem webmToolStripMenuItem;
    private System.Windows.Forms.ToolStripSeparator toolStripMenuItem4;
    private System.Windows.Forms.ToolStripMenuItem gpToolStripMenuItem;
    private System.Windows.Forms.ToolStripMenuItem worstvideoToolStripMenuItem;
    private System.Windows.Forms.ToolStripMenuItem worstaudioToolStripMenuItem;
    private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem3;
    private System.Windows.Forms.ToolStripMenuItem wavToolStripMenuItem;
    private System.Windows.Forms.ToolStripMenuItem flvToolStripMenuItem;
    private System.Windows.Forms.ToolStripDropDownButton ckHasPlaylist;
    private System.Windows.Forms.ToolStripLabel toolStripLabel1;
    private System.Windows.Forms.ToolStripTextBox toolStripTextBox1;
    private System.Windows.Forms.ToolStripLabel toolStripLabel2;
    private System.Windows.Forms.ToolStripTextBox toolStripTextBox2;
    private System.Windows.Forms.ToolStripLabel lbMaxDownloads;
    private System.Windows.Forms.ToolStripTextBox textMaxDownloads;
    private System.Windows.Forms.ToolStripStatusLabel statText;
    private System.Windows.Forms.TableLayoutPanel statusControls;
    private wyDay.Controls.Windows7ProgressBar statCurrent;
    private System.Windows.Forms.ToolStripSeparator toolStripMenuItem5;
    private System.Windows.Forms.ToolStripMenuItem singleJSONToolStripMenuItem;
    private System.Windows.Forms.ToolStripMenuItem dumpSingleJSONJToolStripMenuItem;
    private wyDay.Controls.Windows7ProgressBar statItemCount;
    private System.Windows.Forms.Panel panelFileName;
    private System.Windows.Forms.TextBox textFileName;
    private System.Windows.Forms.Label label1;
  }
}
