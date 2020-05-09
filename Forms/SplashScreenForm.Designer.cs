namespace TODORoutine {
    partial class SplashScreen {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing) {
            if (disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SplashScreen));
            this.imgSplashLogo = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.imgSplashLogo)).BeginInit();
            this.SuspendLayout();
            // 
            // imgSplashLogo
            // 
            this.imgSplashLogo.Image = ((System.Drawing.Image)(resources.GetObject("imgSplashLogo.Image")));
            this.imgSplashLogo.Location = new System.Drawing.Point(0, 0);
            this.imgSplashLogo.Name = "imgSplashLogo";
            this.imgSplashLogo.Size = new System.Drawing.Size(495, 469);
            this.imgSplashLogo.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.imgSplashLogo.TabIndex = 0;
            this.imgSplashLogo.TabStop = false;
            // 
            // SplashScreen
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(494, 463);
            this.Controls.Add(this.imgSplashLogo);
            this.Name = "SplashScreen";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.SplashScreen_Load);
            ((System.ComponentModel.ISupportInitialize)(this.imgSplashLogo)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox imgSplashLogo;
    }
}