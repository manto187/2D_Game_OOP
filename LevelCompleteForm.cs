using System;
using System.Windows.Forms;

namespace FirstDesktopApp
{
    public partial class LevelCompleteForm : Form
    {
        public enum LevelCompleteAction
        {
            NextLevel,
            Replay,
            MainMenu
        }

        public LevelCompleteAction SelectedAction { get; private set; } = LevelCompleteAction.MainMenu;
        
        public LevelCompleteForm(int level, int score, bool hasNextLevel)
        {
            InitializeComponent();
            
            lblLevel.Text = $"Level {level} Complete!";
            lblScore.Text = $"Score: {score}";
            
            // Hide next level button if no more levels
            btnNextLevel.Visible = hasNextLevel;
            btnNextLevel.Enabled = hasNextLevel;
        }

        private void btnNextLevel_Click(object sender, EventArgs e)
        {
            SelectedAction = LevelCompleteAction.NextLevel;
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void btnReplay_Click(object sender, EventArgs e)
        {
            SelectedAction = LevelCompleteAction.Replay;
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void btnMainMenu_Click(object sender, EventArgs e)
        {
            SelectedAction = LevelCompleteAction.MainMenu;
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
    }
}
