namespace FirstDesktopApp
{
    partial class InstructionsForm
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
            this.lblTitle = new System.Windows.Forms.Label();
            this.panelInstructions = new System.Windows.Forms.Panel();
            this.lblMovementHeader = new System.Windows.Forms.Label();
            this.lblMovement1 = new System.Windows.Forms.Label();
            this.lblMovement2 = new System.Windows.Forms.Label();
            this.lblCombatHeader = new System.Windows.Forms.Label();
            this.lblCombat1 = new System.Windows.Forms.Label();
            this.lblObjectiveHeader = new System.Windows.Forms.Label();
            this.lblObjective1 = new System.Windows.Forms.Label();
            this.lblObjective2 = new System.Windows.Forms.Label();
            this.lblObjective3 = new System.Windows.Forms.Label();
            this.lblTipsHeader = new System.Windows.Forms.Label();
            this.lblTips1 = new System.Windows.Forms.Label();
            this.lblTips2 = new System.Windows.Forms.Label();
            this.btnBack = new System.Windows.Forms.Button();
            this.panelMain = new System.Windows.Forms.Panel();
            this.panelInstructions.SuspendLayout();
            this.panelMain.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblTitle
            // 
            this.lblTitle.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.lblTitle.Font = new System.Drawing.Font("Segoe UI", 36F, System.Drawing.FontStyle.Bold);
            this.lblTitle.ForeColor = System.Drawing.Color.White;
            this.lblTitle.Location = new System.Drawing.Point(0, 20);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(800, 60);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "INSTRUCTIONS";
            this.lblTitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // panelInstructions
            // 
            this.panelInstructions.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.panelInstructions.BackColor = System.Drawing.Color.FromArgb(35, 35, 55);
            this.panelInstructions.Controls.Add(this.lblMovementHeader);
            this.panelInstructions.Controls.Add(this.lblMovement1);
            this.panelInstructions.Controls.Add(this.lblMovement2);
            this.panelInstructions.Controls.Add(this.lblCombatHeader);
            this.panelInstructions.Controls.Add(this.lblCombat1);
            this.panelInstructions.Controls.Add(this.lblObjectiveHeader);
            this.panelInstructions.Controls.Add(this.lblObjective1);
            this.panelInstructions.Controls.Add(this.lblObjective2);
            this.panelInstructions.Controls.Add(this.lblObjective3);
            this.panelInstructions.Controls.Add(this.lblTipsHeader);
            this.panelInstructions.Controls.Add(this.lblTips1);
            this.panelInstructions.Controls.Add(this.lblTips2);
            this.panelInstructions.Location = new System.Drawing.Point(100, 90);
            this.panelInstructions.Name = "panelInstructions";
            this.panelInstructions.Size = new System.Drawing.Size(600, 380);
            this.panelInstructions.TabIndex = 1;

            // 
            // lblMovementHeader
            // 
            this.lblMovementHeader.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Bold);
            this.lblMovementHeader.ForeColor = System.Drawing.Color.FromArgb(255, 200, 50);
            this.lblMovementHeader.Location = new System.Drawing.Point(30, 20);
            this.lblMovementHeader.Name = "lblMovementHeader";
            this.lblMovementHeader.Size = new System.Drawing.Size(200, 25);
            this.lblMovementHeader.TabIndex = 0;
            this.lblMovementHeader.Text = "MOVEMENT";
            // 
            // lblMovement1
            // 
            this.lblMovement1.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.lblMovement1.ForeColor = System.Drawing.Color.White;
            this.lblMovement1.Location = new System.Drawing.Point(30, 50);
            this.lblMovement1.Name = "lblMovement1";
            this.lblMovement1.Size = new System.Drawing.Size(540, 22);
            this.lblMovement1.TabIndex = 1;
            this.lblMovement1.Text = "[Arrow Keys / WASD] - Move Left & Right";
            // 
            // lblMovement2
            // 
            this.lblMovement2.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.lblMovement2.ForeColor = System.Drawing.Color.White;
            this.lblMovement2.Location = new System.Drawing.Point(30, 75);
            this.lblMovement2.Name = "lblMovement2";
            this.lblMovement2.Size = new System.Drawing.Size(540, 22);
            this.lblMovement2.TabIndex = 2;
            this.lblMovement2.Text = "[Space / W / Up Arrow] - Jump";
            // 
            // lblCombatHeader
            // 
            this.lblCombatHeader.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Bold);
            this.lblCombatHeader.ForeColor = System.Drawing.Color.FromArgb(255, 200, 50);
            this.lblCombatHeader.Location = new System.Drawing.Point(30, 110);
            this.lblCombatHeader.Name = "lblCombatHeader";
            this.lblCombatHeader.Size = new System.Drawing.Size(200, 25);
            this.lblCombatHeader.TabIndex = 3;
            this.lblCombatHeader.Text = "COMBAT";
            // 
            // lblCombat1
            // 
            this.lblCombat1.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.lblCombat1.ForeColor = System.Drawing.Color.White;
            this.lblCombat1.Location = new System.Drawing.Point(30, 140);
            this.lblCombat1.Name = "lblCombat1";
            this.lblCombat1.Size = new System.Drawing.Size(540, 22);
            this.lblCombat1.TabIndex = 4;
            this.lblCombat1.Text = "[X / Ctrl] - Shoot";
            // 
            // lblObjectiveHeader
            // 
            this.lblObjectiveHeader.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Bold);
            this.lblObjectiveHeader.ForeColor = System.Drawing.Color.FromArgb(255, 200, 50);
            this.lblObjectiveHeader.Location = new System.Drawing.Point(30, 175);
            this.lblObjectiveHeader.Name = "lblObjectiveHeader";
            this.lblObjectiveHeader.Size = new System.Drawing.Size(200, 25);
            this.lblObjectiveHeader.TabIndex = 5;
            this.lblObjectiveHeader.Text = "OBJECTIVE";
            // 
            // lblObjective1
            // 
            this.lblObjective1.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.lblObjective1.ForeColor = System.Drawing.Color.White;
            this.lblObjective1.Location = new System.Drawing.Point(30, 205);
            this.lblObjective1.Name = "lblObjective1";
            this.lblObjective1.Size = new System.Drawing.Size(540, 22);
            this.lblObjective1.TabIndex = 6;
            this.lblObjective1.Text = "• Defeat all enemies to complete each level";
            // 
            // lblObjective2
            // 
            this.lblObjective2.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.lblObjective2.ForeColor = System.Drawing.Color.White;
            this.lblObjective2.Location = new System.Drawing.Point(30, 230);
            this.lblObjective2.Name = "lblObjective2";
            this.lblObjective2.Size = new System.Drawing.Size(540, 22);
            this.lblObjective2.TabIndex = 7;
            this.lblObjective2.Text = "• Collect health packs to restore health";
            // 
            // lblObjective3
            // 
            this.lblObjective3.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.lblObjective3.ForeColor = System.Drawing.Color.White;
            this.lblObjective3.Location = new System.Drawing.Point(30, 255);
            this.lblObjective3.Name = "lblObjective3";
            this.lblObjective3.Size = new System.Drawing.Size(540, 22);
            this.lblObjective3.TabIndex = 8;
            this.lblObjective3.Text = "• Avoid enemy projectiles and falling off platforms";

            // 
            // lblTipsHeader
            // 
            this.lblTipsHeader.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Bold);
            this.lblTipsHeader.ForeColor = System.Drawing.Color.FromArgb(255, 200, 50);
            this.lblTipsHeader.Location = new System.Drawing.Point(30, 290);
            this.lblTipsHeader.Name = "lblTipsHeader";
            this.lblTipsHeader.Size = new System.Drawing.Size(200, 25);
            this.lblTipsHeader.TabIndex = 9;
            this.lblTipsHeader.Text = "TIPS";
            // 
            // lblTips1
            // 
            this.lblTips1.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.lblTips1.ForeColor = System.Drawing.Color.White;
            this.lblTips1.Location = new System.Drawing.Point(30, 320);
            this.lblTips1.Name = "lblTips1";
            this.lblTips1.Size = new System.Drawing.Size(540, 22);
            this.lblTips1.TabIndex = 10;
            this.lblTips1.Text = "• Higher levels have faster and stronger enemies";
            // 
            // lblTips2
            // 
            this.lblTips2.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.lblTips2.ForeColor = System.Drawing.Color.White;
            this.lblTips2.Location = new System.Drawing.Point(30, 345);
            this.lblTips2.Name = "lblTips2";
            this.lblTips2.Size = new System.Drawing.Size(540, 22);
            this.lblTips2.TabIndex = 11;
            this.lblTips2.Text = "• Your score carries over between levels";
            // 
            // btnBack
            // 
            this.btnBack.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.btnBack.BackColor = System.Drawing.Color.FromArgb(60, 60, 100);
            this.btnBack.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnBack.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(100, 100, 140);
            this.btnBack.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(100, 100, 160);
            this.btnBack.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(80, 80, 140);
            this.btnBack.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnBack.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.btnBack.ForeColor = System.Drawing.Color.White;
            this.btnBack.Location = new System.Drawing.Point(300, 490);
            this.btnBack.Name = "btnBack";
            this.btnBack.Size = new System.Drawing.Size(200, 45);
            this.btnBack.TabIndex = 2;
            this.btnBack.Text = "Back to Menu";
            this.btnBack.UseVisualStyleBackColor = false;
            this.btnBack.Click += new System.EventHandler(this.btnBack_Click);
            // 
            // panelMain
            // 
            this.panelMain.BackColor = System.Drawing.Color.Transparent;
            this.panelMain.Controls.Add(this.lblTitle);
            this.panelMain.Controls.Add(this.panelInstructions);
            this.panelMain.Controls.Add(this.btnBack);
            this.panelMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelMain.Location = new System.Drawing.Point(0, 0);
            this.panelMain.Name = "panelMain";
            this.panelMain.Size = new System.Drawing.Size(800, 560);
            this.panelMain.TabIndex = 3;
            // 
            // InstructionsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(25, 25, 45);
            this.ClientSize = new System.Drawing.Size(800, 560);
            this.Controls.Add(this.panelMain);
            this.MinimumSize = new System.Drawing.Size(640, 480);
            this.Name = "InstructionsForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Shadow Hunter - Instructions";
            this.panelInstructions.ResumeLayout(false);
            this.panelMain.ResumeLayout(false);
            this.ResumeLayout(false);
        }

        #endregion

        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Panel panelInstructions;
        private System.Windows.Forms.Label lblMovementHeader;
        private System.Windows.Forms.Label lblMovement1;
        private System.Windows.Forms.Label lblMovement2;
        private System.Windows.Forms.Label lblCombatHeader;
        private System.Windows.Forms.Label lblCombat1;
        private System.Windows.Forms.Label lblObjectiveHeader;
        private System.Windows.Forms.Label lblObjective1;
        private System.Windows.Forms.Label lblObjective2;
        private System.Windows.Forms.Label lblObjective3;
        private System.Windows.Forms.Label lblTipsHeader;
        private System.Windows.Forms.Label lblTips1;
        private System.Windows.Forms.Label lblTips2;
        private System.Windows.Forms.Button btnBack;
        private System.Windows.Forms.Panel panelMain;
    }
}
