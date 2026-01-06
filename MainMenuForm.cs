using System;
using System.Drawing;
using System.Windows.Forms;

namespace FirstDesktopApp
{
    public partial class MainMenuForm : Form
    {
        private int _highestUnlockedLevel = 1;

        public MainMenuForm()
        {
            InitializeComponent();
        }

        public void SetUnlockedLevel(int level)
        {
            _highestUnlockedLevel = Math.Min(level, 3);
        }

        private void btnStartGame_Click(object sender, EventArgs e)
        {
            StartLevel(_highestUnlockedLevel);
        }

        private void btnLevelSelect_Click(object sender, EventArgs e)
        {
            using var levelSelect = new LevelSelectForm(_highestUnlockedLevel);
            this.Hide();
            var result = levelSelect.ShowDialog();
            
            if (levelSelect.SelectedLevel > 0)
            {
                StartLevel(levelSelect.SelectedLevel);
            }
            else
            {
                this.Show();
            }
        }

        private void btnInstructions_Click(object sender, EventArgs e)
        {
            using var instructions = new InstructionsForm();
            this.Hide();
            instructions.ShowDialog();
            this.Show();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void StartLevel(int level)
        {
            this.Hide();
            using var gameForm = new GameForm(level, _highestUnlockedLevel);
            gameForm.ShowDialog();
            
            // Update unlocked level after game
            if (gameForm.HighestUnlockedLevel > _highestUnlockedLevel)
            {
                _highestUnlockedLevel = gameForm.HighestUnlockedLevel;
            }
            
            this.Show();
        }

        private void MainMenuForm_Load(object sender, EventArgs e)
        {
            // Center the form on screen
            this.CenterToScreen();
        }
    }
}
