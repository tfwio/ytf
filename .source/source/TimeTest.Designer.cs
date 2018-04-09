/*
 * User: xo
 * Date: 4/8/2018
 * Time: 6:46 AM
 */
namespace YouTubeDownloadUtil
{
  partial class TimeTest
  {
    /// <summary>
    /// Designer variable used to keep track of non-visual components.
    /// </summary>
    private System.ComponentModel.IContainer components = null;
    private System.Windows.Forms.TextBox tbCommandText;
    private System.Windows.Forms.Button button1;
    private System.Windows.Forms.TextBox textBox1;
    
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
      this.tbCommandText = new System.Windows.Forms.TextBox();
      this.button1 = new System.Windows.Forms.Button();
      this.textBox1 = new System.Windows.Forms.TextBox();
      this.SuspendLayout();
      // 
      // tbCommandText
      // 
      this.tbCommandText.Location = new System.Drawing.Point(12, 12);
      this.tbCommandText.Name = "tbCommandText";
      this.tbCommandText.Size = new System.Drawing.Size(125, 20);
      this.tbCommandText.TabIndex = 0;
      this.tbCommandText.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
      // 
      // button1
      // 
      this.button1.Location = new System.Drawing.Point(12, 64);
      this.button1.Name = "button1";
      this.button1.Size = new System.Drawing.Size(125, 32);
      this.button1.TabIndex = 2;
      this.button1.Text = "Okay";
      this.button1.UseVisualStyleBackColor = true;
      this.button1.Click += new System.EventHandler(this.Button1Click);
      // 
      // textBox1
      // 
      this.textBox1.Location = new System.Drawing.Point(12, 38);
      this.textBox1.Name = "textBox1";
      this.textBox1.Size = new System.Drawing.Size(125, 20);
      this.textBox1.TabIndex = 0;
      this.textBox1.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
      // 
      // TimeTest
      // 
      this.AcceptButton = this.button1;
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(145, 102);
      this.ControlBox = false;
      this.Controls.Add(this.button1);
      this.Controls.Add(this.textBox1);
      this.Controls.Add(this.tbCommandText);
      this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
      this.MaximizeBox = false;
      this.MinimizeBox = false;
      this.Name = "TimeTest";
      this.ShowIcon = false;
      this.ShowInTaskbar = false;
      this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
      this.Text = "TimeTest";
      this.ResumeLayout(false);
      this.PerformLayout();

    }
  }
}
