namespace FirstDesktopApp
{
    partial class LevelCompleteForm
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            this.lblCongrats = new System.Windows.Forms.Label();
            this.lblLevel = new System.Windows.Forms.Label();
            this.lblScore = new System.Windows.Forms.Label();
            this.btnNextLevel = new System.Windows.Forms.Button();
            this.btnReplay = new System.Windows.Forms.Button();
            this.btnMainMenu = new System.Windows.Forms.Button();
            this.panelMain = new System.Windows.Forms.Panel();
            this.panelMain.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblCongrats
            // 
            this.lblCongrats.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.lblCongrats.Font = new System.Drawing.Font("Segoe UI", 42F, System.Drawing.FontStyle.Bold);
            this.lblCongrats.ForeColor = System.Drawing.Color.FromArgb(100, 255, 150);
            this.lblCongrats.Location = new System.Drawing.Point(0, 40);
            this.lblCongrats.Name = "lblCongrats";
            this.lblCongrats.Size = new System.Drawing.Size(600, 70);
            this.lblCongrats.TabIndex = 0;
            this.lblCongrats.Text = "🎉 CONGRATULATIONS!";
            this.lblCongrats.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblLevel
            // 
            this.lblLevel.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.lblLevel.Font = new System.Drawing.Font("Segoe UI", 24F, System.Drawing.FontStyle.Bold);
            this.lblLevel.ForeColor = System.Drawing.Color.White;
            this.lblLevel.Location = new System.Drawing.Point(0, 120);
            this.lblLevel.Name = "lblLevel";
            this.lblLevel.Size = new System.Drawing.Size(600, 45);
            this.lblLevel.TabIndex = 1;
            this.lblLevel.Text = "Level 1 Complete!";
            this.lblLevel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblScore
            // 
            this.lblScore.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.lblScore.Font = new System.Drawing.Font("Segoe UI", 18F);
            this.lblScore.ForeColor = System.Drawing.Color.FromArgb(255, 215, 0);
            this.lblScore.Location = new System.Drawing.Point(0, 175);
            this.lblScore.Name = "lblScore";
            this.lblScore.Size = new System.Drawing.Size(600, 35);
            this.lblScore.TabIndex = 2;
            this.lblScore.Text = "Score: 0";
            this.lblScore.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;

            // 
            // btnNextLevel
            // 
            this.btnNextLevel.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btnNextLevel.BackColor = System.Drawing.Color.FromArgb(50, 150, 80);
            this.btnNextLevel.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnNextLevel.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(80, 200, 120);
            this.btnNextLevel.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(70, 180, 100);
            this.btnNextLevel.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(60, 170, 90);
            this.btnNextLevel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnNextLevel.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Bold);
            this.btnNextLevel.ForeColor = System.Drawing.Color.White;
            this.btnNextLevel.Location = new System.Drawing.Point(160, 240);
            this.btnNextLevel.Name = "btnNextLevel";
            this.btnNextLevel.Size = new System.Drawing.Size(280, 50);
            this.btnNextLevel.TabIndex = 3;
            this.btnNextLevel.Text = "Next Level →";
            this.btnNextLevel.UseVisualStyleBackColor = false;
            this.btnNextLevel.Click += new System.EventHandler(this.btnNextLevel_Click);
            // 
            // btnReplay
            // 
            this.btnReplay.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btnReplay.BackColor = System.Drawing.Color.FromArgb(60, 60, 100);
            this.btnReplay.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnReplay.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(100, 100, 140);
            this.btnReplay.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(100, 100, 160);
            this.btnReplay.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(80, 80, 140);
            this.btnReplay.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnReplay.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.btnReplay.ForeColor = System.Drawing.Color.White;
            this.btnReplay.Location = new System.Drawing.Point(160, 310);
            this.btnReplay.Name = "btnReplay";
            this.btnReplay.Size = new System.Drawing.Size(280, 45);
            this.btnReplay.TabIndex = 4;
            this.btnReplay.Text = "Replay Level";
            this.btnReplay.UseVisualStyleBackColor = false;
            this.btnReplay.Click += new System.EventHandler(this.btnReplay_Click);
            // 
            // btnMainMenu
            // 
            this.btnMainMenu.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btnMainMenu.BackColor = System.Drawing.Color.FromArgb(60, 60, 100);
            this.btnMainMenu.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnMainMenu.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(100, 100, 140);
            this.btnMainMenu.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(100, 100, 160);
            this.btnMainMenu.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(80, 80, 140);
            this.btnMainMenu.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnMainMenu.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.btnMainMenu.ForeColor = System.Drawing.Color.White;
            this.btnMainMenu.Location = new System.Drawing.Point(160, 370);
            this.btnMainMenu.Name = "btnMainMenu";
            this.btnMainMenu.Size = new System.Drawing.Size(280, 45);
            this.btnMainMenu.TabIndex = 5;
            this.btnMainMenu.Text = "Main Menu";
            this.btnMainMenu.UseVisualStyleBackColor = false;
            this.btnMainMenu.Click += new System.EventHandler(this.btnMainMenu_Click);
            // 
            // panelMain
            // 
            this.panelMain.BackColor = System.Drawing.Color.Transparent;
            this.panelMain.Controls.Add(this.lblCongrats);
            this.panelMain.Controls.Add(this.lblLevel);
            this.panelMain.Controls.Add(this.lblScore);
            this.panelMain.Controls.Add(this.btnNextLevel);
            this.panelMain.Controls.Add(this.btnReplay);
            this.panelMain.Controls.Add(this.btnMainMenu);
            this.panelMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelMain.Location = new System.Drawing.Point(0, 0);
            this.panelMain.Name = "panelMain";
            this.panelMain.Size = new System.Drawing.Size(600, 450);
            this.panelMain.TabIndex = 6;
            // 
            // LevelCompleteForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(30, 80, 50);
            this.ClientSize = new System.Drawing.Size(600, 450);
            this.Controls.Add(this.panelMain);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "LevelCompleteForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Level Complete!";
            this.panelMain.ResumeLayout(false);
            this.ResumeLayout(false);
        }

        #endregion

        private System.Windows.Forms.Label lblCongrats;
        private System.Windows.Forms.Label lblLevel;
        private System.Windows.Forms.Label lblScore;
        private System.Windows.Forms.Button btnNextLevel;
        private System.Windows.Forms.Button btnReplay;
        private System.Windows.Forms.Button btnMainMenu;
        private System.Windows.Forms.Panel panelMain;
    }
}
