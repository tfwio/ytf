﻿namespace YouTubeDownloadUtil
{
  partial class FormFlagOptions
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
      this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
      this.toolTipControl = new TheArtOfDev.HtmlRenderer.WinForms.HtmlToolTip();
      this.SuspendLayout();
      // 
      // flowLayoutPanel1
      // 
      this.flowLayoutPanel1.AutoScroll = true;
      this.flowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
      this.flowLayoutPanel1.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
      this.flowLayoutPanel1.Location = new System.Drawing.Point(12, 12);
      this.flowLayoutPanel1.Margin = new System.Windows.Forms.Padding(16);
      this.flowLayoutPanel1.Name = "flowLayoutPanel1";
      this.flowLayoutPanel1.Padding = new System.Windows.Forms.Padding(16);
      this.flowLayoutPanel1.Size = new System.Drawing.Size(385, 417);
      this.flowLayoutPanel1.TabIndex = 0;
      // 
      // toolTipControl
      // 
      this.toolTipControl.AllowLinksHandling = true;
      this.toolTipControl.AutoPopDelay = 7000;
      this.toolTipControl.BaseStylesheet = null;
      this.toolTipControl.InitialDelay = 500;
      this.toolTipControl.MaximumSize = new System.Drawing.Size(0, 0);
      this.toolTipControl.OwnerDraw = true;
      this.toolTipControl.ReshowDelay = 100;
      this.toolTipControl.TooltipCssClass = "htmltooltip";
      this.toolTipControl.ToolTipIcon = System.Windows.Forms.ToolTipIcon.Info;
      this.toolTipControl.ToolTipTitle = "Flag Options";
      // 
      // FormFlagOptions
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.BackColor = System.Drawing.Color.White;
      this.ClientSize = new System.Drawing.Size(409, 441);
      this.Controls.Add(this.flowLayoutPanel1);
      this.Name = "FormFlagOptions";
      this.Padding = new System.Windows.Forms.Padding(12);
      this.ShowIcon = false;
      this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
      this.Text = "FormFlagOptions";
      this.ResumeLayout(false);

    }

    #endregion

    private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
    private TheArtOfDev.HtmlRenderer.WinForms.HtmlToolTip toolTipControl;
  }
}