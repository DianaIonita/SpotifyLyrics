namespace SpotifyLyricsViewer
{
    partial class SpotifyLyricsViewer
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
            this.nowPlayingLabel = new System.Windows.Forms.Label();
            this.spotifyStatusLabel = new System.Windows.Forms.Label();
            this.lyricsTextBox = new System.Windows.Forms.TextBox();
            this.MainTimer = new System.Windows.Forms.Timer(this.components);
            this.SuspendLayout();
            // 
            // nowPlayingLabel
            // 
            this.nowPlayingLabel.AutoSize = true;
            this.nowPlayingLabel.Dock = System.Windows.Forms.DockStyle.Left;
            this.nowPlayingLabel.Location = new System.Drawing.Point(0, 0);
            this.nowPlayingLabel.Name = "nowPlayingLabel";
            this.nowPlayingLabel.Size = new System.Drawing.Size(0, 13);
            this.nowPlayingLabel.TabIndex = 0;
            // 
            // spotifyStatusLabel
            // 
            this.spotifyStatusLabel.AutoSize = true;
            this.spotifyStatusLabel.Dock = System.Windows.Forms.DockStyle.Right;
            this.spotifyStatusLabel.Location = new System.Drawing.Point(527, 0);
            this.spotifyStatusLabel.Name = "spotifyStatusLabel";
            this.spotifyStatusLabel.Size = new System.Drawing.Size(0, 13);
            this.spotifyStatusLabel.TabIndex = 1;
            // 
            // lyricsTextBox
            // 
            this.lyricsTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lyricsTextBox.Location = new System.Drawing.Point(12, 44);
            this.lyricsTextBox.Multiline = true;
            this.lyricsTextBox.Name = "lyricsTextBox";
            this.lyricsTextBox.ReadOnly = true;
            this.lyricsTextBox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.lyricsTextBox.Size = new System.Drawing.Size(503, 596);
            this.lyricsTextBox.TabIndex = 2;
            this.lyricsTextBox.Text = "Loading...";
            // 
            // MainTimer
            // 
            this.MainTimer.Enabled = true;
            this.MainTimer.Interval = 500;
            this.MainTimer.Tick += new System.EventHandler(this.MainTimer_Tick);
            // 
            // SpotifyLyricsViewer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(527, 652);
            this.Controls.Add(this.lyricsTextBox);
            this.Controls.Add(this.spotifyStatusLabel);
            this.Controls.Add(this.nowPlayingLabel);
            this.Name = "SpotifyLyricsViewer";
            this.Text = "SpotifyLyricsViewer";
            this.Load += new System.EventHandler(this.SpotifyLyricsViewer_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label nowPlayingLabel;
        private System.Windows.Forms.Label spotifyStatusLabel;
        private System.Windows.Forms.TextBox lyricsTextBox;
        private System.Windows.Forms.Timer MainTimer;
    }
}

