using System;
using System.Windows.Forms;

namespace FirstDesktopApp
{
    public partial class LevelFailedForm : Form
    {
        public enum LevelFailedAction
        {
            Retry,
            MainMenu
        }

        public LevelFailedAction SelectedAction { get; private set; } = LevelFailedAction.MainMenu;
        
        public LevelFailedForm(int level, int score)
        {
            InitializeComponent();
            
            lblLevel.Text = $"Level {level}";
            lblScore.Text = $"Score: {score}";
        }

        private void btnRetry_Click(object sender, EventArgs e)
        {
            SelectedAction = LevelFailedAction.Retry;
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void btnMainMenu_Click(object sender, EventArgs e)
        {
            SelectedAction = LevelFailedAction.MainMenu;
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
    }
}
