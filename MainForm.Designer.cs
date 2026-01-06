namespace FirstDesktopApp
{
    partial class MainForm
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
            // Main Menu Panel
            this.panelMainMenu = new System.Windows.Forms.Panel();
            this.lblTitle = new System.Windows.Forms.Label();
            this.lblSubtitle = new System.Windows.Forms.Label();
            this.btnStartGame = new System.Windows.Forms.Button();
            this.btnLevelSelect = new System.Windows.Forms.Button();
            this.btnInstructions = new System.Windows.Forms.Button();
            this.btnExit = new System.Windows.Forms.Button();
            this.lblFooter = new System.Windows.Forms.Label();

            // Level Select Panel
            this.panelLevelSelect = new System.Windows.Forms.Panel();
            this.lblLevelSelectTitle = new System.Windows.Forms.Label();
            this.btnLevel1 = new System.Windows.Forms.Button();
            this.btnLevel2 = new System.Windows.Forms.Button();
            this.btnLevel3 = new System.Windows.Forms.Button();
            this.lblLevel1Name = new System.Windows.Forms.Label();
            this.lblLevel2Name = new System.Windows.Forms.Label();
            this.lblLevel3Name = new System.Windows.Forms.Label();
            this.lblLock2 = new System.Windows.Forms.Label();
            this.lblLock3 = new System.Windows.Forms.Label();
            this.btnBackFromLevelSelect = new System.Windows.Forms.Button();

            // Instructions Panel
            this.panelInstructions = new System.Windows.Forms.Panel();
            this.lblInstructionsTitle = new System.Windows.Forms.Label();
            this.panelInstructionsContent = new System.Windows.Forms.Panel();
            this.btnBackFromInstructions = new System.Windows.Forms.Button();

            // Game Panel
            this.panelGame = new System.Windows.Forms.Panel();

            // Level Complete Panel
            this.panelLevelComplete = new System.Windows.Forms.Panel();
            this.lblCongrats = new System.Windows.Forms.Label();
            this.lblCompleteLevel = new System.Windows.Forms.Label();
            this.lblCompleteScore = new System.Windows.Forms.Label();
            this.btnNextLevel = new System.Windows.Forms.Button();
            this.btnReplayLevel = new System.Windows.Forms.Button();
            this.btnCompleteToMenu = new System.Windows.Forms.Button();

            // Level Failed Panel
            this.panelLevelFailed = new System.Windows.Forms.Panel();
            this.lblGameOver = new System.Windows.Forms.Label();
            this.lblFailedLevel = new System.Windows.Forms.Label();
            this.lblFailedScore = new System.Windows.Forms.Label();
            this.lblFailedMessage = new System.Windows.Forms.Label();
            this.btnRetryLevel = new System.Windows.Forms.Button();
            this.btnFailedToMenu = new System.Windows.Forms.Button();

            this.panelMainMenu.SuspendLayout();
            this.panelLevelSelect.SuspendLayout();
            this.panelInstructions.SuspendLayout();
            this.panelLevelComplete.SuspendLayout();
            this.panelLevelFailed.SuspendLayout();
            this.SuspendLayout();

            // ========== MAIN MENU PANEL ==========
            // 
            // panelMainMenu
            // 
            this.panelMainMenu.BackColor = System.Drawing.Color.FromArgb(25, 25, 45);
            this.panelMainMenu.Controls.Add(this.lblTitle);
            this.panelMainMenu.Controls.Add(this.lblSubtitle);
            this.panelMainMenu.Controls.Add(this.btnStartGame);
            this.panelMainMenu.Controls.Add(this.btnLevelSelect);
            this.panelMainMenu.Controls.Add(this.btnInstructions);
            this.panelMainMenu.Controls.Add(this.btnExit);
            this.panelMainMenu.Controls.Add(this.lblFooter);
            this.panelMainMenu.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelMainMenu.Name = "panelMainMenu";
            // 
            // lblTitle
            // 
            this.lblTitle.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.lblTitle.Font = new System.Drawing.Font("Segoe UI", 48F, System.Drawing.FontStyle.Bold);
            this.lblTitle.ForeColor = System.Drawing.Color.White;
            this.lblTitle.Location = new System.Drawing.Point(0, 60);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(1024, 90);
            this.lblTitle.Text = "SHADOW HUNTER";
            this.lblTitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblSubtitle
            // 
            this.lblSubtitle.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.lblSubtitle.Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Italic);
            this.lblSubtitle.ForeColor = System.Drawing.Color.FromArgb(200, 200, 220);
            this.lblSubtitle.Location = new System.Drawing.Point(0, 150);
            this.lblSubtitle.Name = "lblSubtitle";
            this.lblSubtitle.Size = new System.Drawing.Size(1024, 35);
            this.lblSubtitle.Text = "A Platformer Adventure";
            this.lblSubtitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // btnStartGame
            // 
            this.btnStartGame.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btnStartGame.BackColor = System.Drawing.Color.FromArgb(60, 60, 100);
            this.btnStartGame.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnStartGame.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(100, 100, 140);
            this.btnStartGame.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(80, 80, 140);
            this.btnStartGame.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnStartGame.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Bold);
            this.btnStartGame.ForeColor = System.Drawing.Color.White;
            this.btnStartGame.Location = new System.Drawing.Point(372, 230);
            this.btnStartGame.Name = "btnStartGame";
            this.btnStartGame.Size = new System.Drawing.Size(280, 55);
            this.btnStartGame.Text = "Start Game";
            this.btnStartGame.Click += new System.EventHandler(this.btnStartGame_Click);
            // 
            // btnLevelSelect
            // 
            this.btnLevelSelect.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btnLevelSelect.BackColor = System.Drawing.Color.FromArgb(60, 60, 100);
            this.btnLevelSelect.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnLevelSelect.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(100, 100, 140);
            this.btnLevelSelect.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(80, 80, 140);
            this.btnLevelSelect.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnLevelSelect.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Bold);
            this.btnLevelSelect.ForeColor = System.Drawing.Color.White;
            this.btnLevelSelect.Location = new System.Drawing.Point(372, 305);
            this.btnLevelSelect.Name = "btnLevelSelect";
            this.btnLevelSelect.Size = new System.Drawing.Size(280, 55);
            this.btnLevelSelect.Text = "Level Select";
            this.btnLevelSelect.Click += new System.EventHandler(this.btnLevelSelect_Click);
            // 
            // btnInstructions
            // 
            this.btnInstructions.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btnInstructions.BackColor = System.Drawing.Color.FromArgb(60, 60, 100);
            this.btnInstructions.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnInstructions.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(100, 100, 140);
            this.btnInstructions.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(80, 80, 140);
            this.btnInstructions.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnInstructions.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Bold);
            this.btnInstructions.ForeColor = System.Drawing.Color.White;
            this.btnInstructions.Location = new System.Drawing.Point(372, 380);
            this.btnInstructions.Name = "btnInstructions";
            this.btnInstructions.Size = new System.Drawing.Size(280, 55);
            this.btnInstructions.Text = "Instructions";
            this.btnInstructions.Click += new System.EventHandler(this.btnInstructions_Click);
            // 
            // btnExit
            // 
            this.btnExit.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btnExit.BackColor = System.Drawing.Color.FromArgb(80, 40, 40);
            this.btnExit.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnExit.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(120, 60, 60);
            this.btnExit.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(100, 50, 50);
            this.btnExit.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnExit.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Bold);
            this.btnExit.ForeColor = System.Drawing.Color.White;
            this.btnExit.Location = new System.Drawing.Point(372, 455);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(280, 55);
            this.btnExit.Text = "Exit";
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // lblFooter
            // 
            this.lblFooter.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.lblFooter.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblFooter.ForeColor = System.Drawing.Color.FromArgb(150, 150, 170);
            this.lblFooter.Location = new System.Drawing.Point(0, 600);
            this.lblFooter.Name = "lblFooter";
            this.lblFooter.Size = new System.Drawing.Size(1024, 25);
            this.lblFooter.Text = "Press F11 for Fullscreen";
            this.lblFooter.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;

            // ========== LEVEL SELECT PANEL ==========
            // 
            // panelLevelSelect
            // 
            this.panelLevelSelect.BackColor = System.Drawing.Color.FromArgb(25, 25, 45);
            this.panelLevelSelect.Controls.Add(this.lblLevelSelectTitle);
            this.panelLevelSelect.Controls.Add(this.btnLevel1);
            this.panelLevelSelect.Controls.Add(this.btnLevel2);
            this.panelLevelSelect.Controls.Add(this.btnLevel3);
            this.panelLevelSelect.Controls.Add(this.lblLevel1Name);
            this.panelLevelSelect.Controls.Add(this.lblLevel2Name);
            this.panelLevelSelect.Controls.Add(this.lblLevel3Name);
            this.panelLevelSelect.Controls.Add(this.lblLock2);
            this.panelLevelSelect.Controls.Add(this.lblLock3);
            this.panelLevelSelect.Controls.Add(this.btnBackFromLevelSelect);
            this.panelLevelSelect.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelLevelSelect.Name = "panelLevelSelect";
            this.panelLevelSelect.Visible = false;
            // 
            // lblLevelSelectTitle
            // 
            this.lblLevelSelectTitle.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.lblLevelSelectTitle.Font = new System.Drawing.Font("Segoe UI", 36F, System.Drawing.FontStyle.Bold);
            this.lblLevelSelectTitle.ForeColor = System.Drawing.Color.White;
            this.lblLevelSelectTitle.Location = new System.Drawing.Point(0, 40);
            this.lblLevelSelectTitle.Name = "lblLevelSelectTitle";
            this.lblLevelSelectTitle.Size = new System.Drawing.Size(1024, 70);
            this.lblLevelSelectTitle.Text = "SELECT LEVEL";
            this.lblLevelSelectTitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // btnLevel1
            // 
            this.btnLevel1.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btnLevel1.BackColor = System.Drawing.Color.FromArgb(60, 60, 100);
            this.btnLevel1.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnLevel1.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(100, 100, 140);
            this.btnLevel1.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(80, 80, 140);
            this.btnLevel1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnLevel1.Font = new System.Drawing.Font("Segoe UI", 28F, System.Drawing.FontStyle.Bold);
            this.btnLevel1.ForeColor = System.Drawing.Color.White;
            this.btnLevel1.Location = new System.Drawing.Point(152, 180);
            this.btnLevel1.Name = "btnLevel1";
            this.btnLevel1.Size = new System.Drawing.Size(200, 150);
            this.btnLevel1.Text = "1";
            this.btnLevel1.Click += new System.EventHandler(this.btnLevel1_Click);
            // 
            // btnLevel2
            // 
            this.btnLevel2.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btnLevel2.BackColor = System.Drawing.Color.FromArgb(60, 60, 100);
            this.btnLevel2.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnLevel2.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(100, 100, 140);
            this.btnLevel2.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(80, 80, 140);
            this.btnLevel2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnLevel2.Font = new System.Drawing.Font("Segoe UI", 28F, System.Drawing.FontStyle.Bold);
            this.btnLevel2.ForeColor = System.Drawing.Color.White;
            this.btnLevel2.Location = new System.Drawing.Point(412, 180);
            this.btnLevel2.Name = "btnLevel2";
            this.btnLevel2.Size = new System.Drawing.Size(200, 150);
            this.btnLevel2.Text = "2";
            this.btnLevel2.Click += new System.EventHandler(this.btnLevel2_Click);
            // 
            // btnLevel3
            // 
            this.btnLevel3.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btnLevel3.BackColor = System.Drawing.Color.FromArgb(60, 60, 100);
            this.btnLevel3.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnLevel3.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(100, 100, 140);
            this.btnLevel3.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(80, 80, 140);
            this.btnLevel3.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnLevel3.Font = new System.Drawing.Font("Segoe UI", 28F, System.Drawing.FontStyle.Bold);
            this.btnLevel3.ForeColor = System.Drawing.Color.White;
            this.btnLevel3.Location = new System.Drawing.Point(672, 180);
            this.btnLevel3.Name = "btnLevel3";
            this.btnLevel3.Size = new System.Drawing.Size(200, 150);
            this.btnLevel3.Text = "3";
            this.btnLevel3.Click += new System.EventHandler(this.btnLevel3_Click);
            // 
            // lblLevel1Name
            // 
            this.lblLevel1Name.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.lblLevel1Name.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.lblLevel1Name.ForeColor = System.Drawing.Color.FromArgb(200, 200, 220);
            this.lblLevel1Name.Location = new System.Drawing.Point(152, 340);
            this.lblLevel1Name.Name = "lblLevel1Name";
            this.lblLevel1Name.Size = new System.Drawing.Size(200, 25);
            this.lblLevel1Name.Text = "Forest";
            this.lblLevel1Name.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblLevel2Name
            // 
            this.lblLevel2Name.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.lblLevel2Name.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.lblLevel2Name.ForeColor = System.Drawing.Color.FromArgb(200, 200, 220);
            this.lblLevel2Name.Location = new System.Drawing.Point(412, 340);
            this.lblLevel2Name.Name = "lblLevel2Name";
            this.lblLevel2Name.Size = new System.Drawing.Size(200, 25);
            this.lblLevel2Name.Text = "Cave";
            this.lblLevel2Name.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblLevel3Name
            // 
            this.lblLevel3Name.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.lblLevel3Name.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.lblLevel3Name.ForeColor = System.Drawing.Color.FromArgb(200, 200, 220);
            this.lblLevel3Name.Location = new System.Drawing.Point(672, 340);
            this.lblLevel3Name.Name = "lblLevel3Name";
            this.lblLevel3Name.Size = new System.Drawing.Size(200, 25);
            this.lblLevel3Name.Text = "Castle";
            this.lblLevel3Name.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblLock2
            // 
            this.lblLock2.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.lblLock2.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblLock2.ForeColor = System.Drawing.Color.FromArgb(255, 100, 100);
            this.lblLock2.Location = new System.Drawing.Point(412, 365);
            this.lblLock2.Name = "lblLock2";
            this.lblLock2.Size = new System.Drawing.Size(200, 20);
            this.lblLock2.Text = "🔒 Locked";
            this.lblLock2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblLock3
            // 
            this.lblLock3.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.lblLock3.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblLock3.ForeColor = System.Drawing.Color.FromArgb(255, 100, 100);
            this.lblLock3.Location = new System.Drawing.Point(672, 365);
            this.lblLock3.Name = "lblLock3";
            this.lblLock3.Size = new System.Drawing.Size(200, 20);
            this.lblLock3.Text = "🔒 Locked";
            this.lblLock3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // btnBackFromLevelSelect
            // 
            this.btnBackFromLevelSelect.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.btnBackFromLevelSelect.BackColor = System.Drawing.Color.FromArgb(60, 60, 100);
            this.btnBackFromLevelSelect.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnBackFromLevelSelect.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(100, 100, 140);
            this.btnBackFromLevelSelect.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(80, 80, 140);
            this.btnBackFromLevelSelect.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnBackFromLevelSelect.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.btnBackFromLevelSelect.ForeColor = System.Drawing.Color.White;
            this.btnBackFromLevelSelect.Location = new System.Drawing.Point(412, 450);
            this.btnBackFromLevelSelect.Name = "btnBackFromLevelSelect";
            this.btnBackFromLevelSelect.Size = new System.Drawing.Size(200, 45);
            this.btnBackFromLevelSelect.Text = "Back to Menu";
            this.btnBackFromLevelSelect.Click += new System.EventHandler(this.btnBackFromLevelSelect_Click);

            // ========== INSTRUCTIONS PANEL ==========
            // 
            // panelInstructions
            // 
            this.panelInstructions.BackColor = System.Drawing.Color.FromArgb(25, 25, 45);
            this.panelInstructions.Controls.Add(this.lblInstructionsTitle);
            this.panelInstructions.Controls.Add(this.panelInstructionsContent);
            this.panelInstructions.Controls.Add(this.btnBackFromInstructions);
            this.panelInstructions.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelInstructions.Name = "panelInstructions";
            this.panelInstructions.Visible = false;
            // 
            // lblInstructionsTitle
            // 
            this.lblInstructionsTitle.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.lblInstructionsTitle.Font = new System.Drawing.Font("Segoe UI", 36F, System.Drawing.FontStyle.Bold);
            this.lblInstructionsTitle.ForeColor = System.Drawing.Color.White;
            this.lblInstructionsTitle.Location = new System.Drawing.Point(0, 20);
            this.lblInstructionsTitle.Name = "lblInstructionsTitle";
            this.lblInstructionsTitle.Size = new System.Drawing.Size(1024, 60);
            this.lblInstructionsTitle.Text = "INSTRUCTIONS";
            this.lblInstructionsTitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // panelInstructionsContent
            // 
            this.panelInstructionsContent.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.panelInstructionsContent.BackColor = System.Drawing.Color.FromArgb(35, 35, 55);
            this.panelInstructionsContent.Location = new System.Drawing.Point(212, 90);
            this.panelInstructionsContent.Name = "panelInstructionsContent";
            this.panelInstructionsContent.Size = new System.Drawing.Size(600, 420);
            this.panelInstructionsContent.Paint += new System.Windows.Forms.PaintEventHandler(this.panelInstructionsContent_Paint);
            // 
            // btnBackFromInstructions
            // 
            this.btnBackFromInstructions.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.btnBackFromInstructions.BackColor = System.Drawing.Color.FromArgb(60, 60, 100);
            this.btnBackFromInstructions.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnBackFromInstructions.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(100, 100, 140);
            this.btnBackFromInstructions.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(80, 80, 140);
            this.btnBackFromInstructions.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnBackFromInstructions.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.btnBackFromInstructions.ForeColor = System.Drawing.Color.White;
            this.btnBackFromInstructions.Location = new System.Drawing.Point(412, 530);
            this.btnBackFromInstructions.Name = "btnBackFromInstructions";
            this.btnBackFromInstructions.Size = new System.Drawing.Size(200, 45);
            this.btnBackFromInstructions.Text = "Back to Menu";
            this.btnBackFromInstructions.Click += new System.EventHandler(this.btnBackFromInstructions_Click);

            // ========== GAME PANEL ==========
            // 
            // panelGame
            // 
            this.panelGame.BackColor = System.Drawing.Color.CornflowerBlue;
            this.panelGame.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelGame.Name = "panelGame";
            this.panelGame.Visible = false;

            // ========== LEVEL COMPLETE PANEL ==========
            // 
            // panelLevelComplete
            // 
            this.panelLevelComplete.BackColor = System.Drawing.Color.FromArgb(30, 80, 50);
            this.panelLevelComplete.Controls.Add(this.lblCongrats);
            this.panelLevelComplete.Controls.Add(this.lblCompleteLevel);
            this.panelLevelComplete.Controls.Add(this.lblCompleteScore);
            this.panelLevelComplete.Controls.Add(this.btnNextLevel);
            this.panelLevelComplete.Controls.Add(this.btnReplayLevel);
            this.panelLevelComplete.Controls.Add(this.btnCompleteToMenu);
            this.panelLevelComplete.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelLevelComplete.Name = "panelLevelComplete";
            this.panelLevelComplete.Visible = false;
            // 
            // lblCongrats
            // 
            this.lblCongrats.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.lblCongrats.Font = new System.Drawing.Font("Segoe UI", 42F, System.Drawing.FontStyle.Bold);
            this.lblCongrats.ForeColor = System.Drawing.Color.FromArgb(100, 255, 150);
            this.lblCongrats.Location = new System.Drawing.Point(0, 80);
            this.lblCongrats.Name = "lblCongrats";
            this.lblCongrats.Size = new System.Drawing.Size(1024, 70);
            this.lblCongrats.Text = "🎉 CONGRATULATIONS!";
            this.lblCongrats.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblCompleteLevel
            // 
            this.lblCompleteLevel.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.lblCompleteLevel.Font = new System.Drawing.Font("Segoe UI", 24F, System.Drawing.FontStyle.Bold);
            this.lblCompleteLevel.ForeColor = System.Drawing.Color.White;
            this.lblCompleteLevel.Location = new System.Drawing.Point(0, 160);
            this.lblCompleteLevel.Name = "lblCompleteLevel";
            this.lblCompleteLevel.Size = new System.Drawing.Size(1024, 45);
            this.lblCompleteLevel.Text = "Level 1 Complete!";
            this.lblCompleteLevel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblCompleteScore
            // 
            this.lblCompleteScore.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.lblCompleteScore.Font = new System.Drawing.Font("Segoe UI", 18F);
            this.lblCompleteScore.ForeColor = System.Drawing.Color.FromArgb(255, 215, 0);
            this.lblCompleteScore.Location = new System.Drawing.Point(0, 215);
            this.lblCompleteScore.Name = "lblCompleteScore";
            this.lblCompleteScore.Size = new System.Drawing.Size(1024, 35);
            this.lblCompleteScore.Text = "Score: 0";
            this.lblCompleteScore.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // btnNextLevel
            // 
            this.btnNextLevel.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btnNextLevel.BackColor = System.Drawing.Color.FromArgb(50, 150, 80);
            this.btnNextLevel.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnNextLevel.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(80, 200, 120);
            this.btnNextLevel.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(60, 170, 90);
            this.btnNextLevel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnNextLevel.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Bold);
            this.btnNextLevel.ForeColor = System.Drawing.Color.White;
            this.btnNextLevel.Location = new System.Drawing.Point(372, 290);
            this.btnNextLevel.Name = "btnNextLevel";
            this.btnNextLevel.Size = new System.Drawing.Size(280, 50);
            this.btnNextLevel.Text = "Next Level →";
            this.btnNextLevel.Click += new System.EventHandler(this.btnNextLevel_Click);
            // 
            // btnReplayLevel
            // 
            this.btnReplayLevel.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btnReplayLevel.BackColor = System.Drawing.Color.FromArgb(60, 60, 100);
            this.btnReplayLevel.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnReplayLevel.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(100, 100, 140);
            this.btnReplayLevel.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(80, 80, 140);
            this.btnReplayLevel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnReplayLevel.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.btnReplayLevel.ForeColor = System.Drawing.Color.White;
            this.btnReplayLevel.Location = new System.Drawing.Point(372, 360);
            this.btnReplayLevel.Name = "btnReplayLevel";
            this.btnReplayLevel.Size = new System.Drawing.Size(280, 45);
            this.btnReplayLevel.Text = "Replay Level";
            this.btnReplayLevel.Click += new System.EventHandler(this.btnReplayLevel_Click);
            // 
            // btnCompleteToMenu
            // 
            this.btnCompleteToMenu.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btnCompleteToMenu.BackColor = System.Drawing.Color.FromArgb(60, 60, 100);
            this.btnCompleteToMenu.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnCompleteToMenu.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(100, 100, 140);
            this.btnCompleteToMenu.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(80, 80, 140);
            this.btnCompleteToMenu.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCompleteToMenu.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.btnCompleteToMenu.ForeColor = System.Drawing.Color.White;
            this.btnCompleteToMenu.Location = new System.Drawing.Point(372, 420);
            this.btnCompleteToMenu.Name = "btnCompleteToMenu";
            this.btnCompleteToMenu.Size = new System.Drawing.Size(280, 45);
            this.btnCompleteToMenu.Text = "Main Menu";
            this.btnCompleteToMenu.Click += new System.EventHandler(this.btnCompleteToMenu_Click);

            // ========== LEVEL FAILED PANEL ==========
            // 
            // panelLevelFailed
            // 
            this.panelLevelFailed.BackColor = System.Drawing.Color.FromArgb(80, 30, 30);
            this.panelLevelFailed.Controls.Add(this.lblGameOver);
            this.panelLevelFailed.Controls.Add(this.lblFailedLevel);
            this.panelLevelFailed.Controls.Add(this.lblFailedScore);
            this.panelLevelFailed.Controls.Add(this.lblFailedMessage);
            this.panelLevelFailed.Controls.Add(this.btnRetryLevel);
            this.panelLevelFailed.Controls.Add(this.btnFailedToMenu);
            this.panelLevelFailed.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelLevelFailed.Name = "panelLevelFailed";
            this.panelLevelFailed.Visible = false;
            // 
            // lblGameOver
            // 
            this.lblGameOver.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.lblGameOver.Font = new System.Drawing.Font("Segoe UI", 42F, System.Drawing.FontStyle.Bold);
            this.lblGameOver.ForeColor = System.Drawing.Color.FromArgb(255, 80, 80);
            this.lblGameOver.Location = new System.Drawing.Point(0, 80);
            this.lblGameOver.Name = "lblGameOver";
            this.lblGameOver.Size = new System.Drawing.Size(1024, 70);
            this.lblGameOver.Text = "💀 GAME OVER";
            this.lblGameOver.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblFailedLevel
            // 
            this.lblFailedLevel.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.lblFailedLevel.Font = new System.Drawing.Font("Segoe UI", 20F, System.Drawing.FontStyle.Bold);
            this.lblFailedLevel.ForeColor = System.Drawing.Color.White;
            this.lblFailedLevel.Location = new System.Drawing.Point(0, 160);
            this.lblFailedLevel.Name = "lblFailedLevel";
            this.lblFailedLevel.Size = new System.Drawing.Size(1024, 40);
            this.lblFailedLevel.Text = "Level 1";
            this.lblFailedLevel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblFailedScore
            // 
            this.lblFailedScore.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.lblFailedScore.Font = new System.Drawing.Font("Segoe UI", 16F);
            this.lblFailedScore.ForeColor = System.Drawing.Color.FromArgb(255, 215, 0);
            this.lblFailedScore.Location = new System.Drawing.Point(0, 205);
            this.lblFailedScore.Name = "lblFailedScore";
            this.lblFailedScore.Size = new System.Drawing.Size(1024, 30);
            this.lblFailedScore.Text = "Score: 0";
            this.lblFailedScore.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblFailedMessage
            // 
            this.lblFailedMessage.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.lblFailedMessage.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Italic);
            this.lblFailedMessage.ForeColor = System.Drawing.Color.FromArgb(180, 180, 200);
            this.lblFailedMessage.Location = new System.Drawing.Point(0, 245);
            this.lblFailedMessage.Name = "lblFailedMessage";
            this.lblFailedMessage.Size = new System.Drawing.Size(1024, 25);
            this.lblFailedMessage.Text = "Don't give up! Try again!";
            this.lblFailedMessage.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // btnRetryLevel
            // 
            this.btnRetryLevel.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btnRetryLevel.BackColor = System.Drawing.Color.FromArgb(180, 60, 60);
            this.btnRetryLevel.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnRetryLevel.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(220, 80, 80);
            this.btnRetryLevel.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(200, 80, 80);
            this.btnRetryLevel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnRetryLevel.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Bold);
            this.btnRetryLevel.ForeColor = System.Drawing.Color.White;
            this.btnRetryLevel.Location = new System.Drawing.Point(372, 310);
            this.btnRetryLevel.Name = "btnRetryLevel";
            this.btnRetryLevel.Size = new System.Drawing.Size(280, 50);
            this.btnRetryLevel.Text = "🔄 Retry Level";
            this.btnRetryLevel.Click += new System.EventHandler(this.btnRetryLevel_Click);
            // 
            // btnFailedToMenu
            // 
            this.btnFailedToMenu.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btnFailedToMenu.BackColor = System.Drawing.Color.FromArgb(60, 60, 80);
            this.btnFailedToMenu.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnFailedToMenu.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(100, 100, 120);
            this.btnFailedToMenu.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(70, 70, 90);
            this.btnFailedToMenu.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnFailedToMenu.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.btnFailedToMenu.ForeColor = System.Drawing.Color.White;
            this.btnFailedToMenu.Location = new System.Drawing.Point(372, 375);
            this.btnFailedToMenu.Name = "btnFailedToMenu";
            this.btnFailedToMenu.Size = new System.Drawing.Size(280, 45);
            this.btnFailedToMenu.Text = "Main Menu";
            this.btnFailedToMenu.Click += new System.EventHandler(this.btnFailedToMenu_Click);

            // ========== MAIN FORM ==========
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(25, 25, 45);
            this.ClientSize = new System.Drawing.Size(1024, 640);
            this.Controls.Add(this.panelMainMenu);
            this.Controls.Add(this.panelLevelSelect);
            this.Controls.Add(this.panelInstructions);
            this.Controls.Add(this.panelGame);
            this.Controls.Add(this.panelLevelComplete);
            this.Controls.Add(this.panelLevelFailed);
            this.DoubleBuffered = true;
            this.MinimumSize = new System.Drawing.Size(800, 500);
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Shadow Hunter";
            this.panelMainMenu.ResumeLayout(false);
            this.panelLevelSelect.ResumeLayout(false);
            this.panelInstructions.ResumeLayout(false);
            this.panelLevelComplete.ResumeLayout(false);
            this.panelLevelFailed.ResumeLayout(false);
            this.ResumeLayout(false);
        }

        #endregion

        // Main Menu Panel
        private System.Windows.Forms.Panel panelMainMenu;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Label lblSubtitle;
        private System.Windows.Forms.Button btnStartGame;
        private System.Windows.Forms.Button btnLevelSelect;
        private System.Windows.Forms.Button btnInstructions;
        private System.Windows.Forms.Button btnExit;
        private System.Windows.Forms.Label lblFooter;

        // Level Select Panel
        private System.Windows.Forms.Panel panelLevelSelect;
        private System.Windows.Forms.Label lblLevelSelectTitle;
        private System.Windows.Forms.Button btnLevel1;
        private System.Windows.Forms.Button btnLevel2;
        private System.Windows.Forms.Button btnLevel3;
        private System.Windows.Forms.Label lblLevel1Name;
        private System.Windows.Forms.Label lblLevel2Name;
        private System.Windows.Forms.Label lblLevel3Name;
        private System.Windows.Forms.Label lblLock2;
        private System.Windows.Forms.Label lblLock3;
        private System.Windows.Forms.Button btnBackFromLevelSelect;

        // Instructions Panel
        private System.Windows.Forms.Panel panelInstructions;
        private System.Windows.Forms.Label lblInstructionsTitle;
        private System.Windows.Forms.Panel panelInstructionsContent;
        private System.Windows.Forms.Button btnBackFromInstructions;

        // Game Panel
        private System.Windows.Forms.Panel panelGame;

        // Level Complete Panel
        private System.Windows.Forms.Panel panelLevelComplete;
        private System.Windows.Forms.Label lblCongrats;
        private System.Windows.Forms.Label lblCompleteLevel;
        private System.Windows.Forms.Label lblCompleteScore;
        private System.Windows.Forms.Button btnNextLevel;
        private System.Windows.Forms.Button btnReplayLevel;
        private System.Windows.Forms.Button btnCompleteToMenu;

        // Level Failed Panel
        private System.Windows.Forms.Panel panelLevelFailed;
        private System.Windows.Forms.Label lblGameOver;
        private System.Windows.Forms.Label lblFailedLevel;
        private System.Windows.Forms.Label lblFailedScore;
        private System.Windows.Forms.Label lblFailedMessage;
        private System.Windows.Forms.Button btnRetryLevel;
        private System.Windows.Forms.Button btnFailedToMenu;
    }
}
