using System;
using System.Drawing;
using System.Windows.Forms;

namespace FirstDesktopApp
{
    public partial class LevelSelectForm : Form
    {
        private int _highestUnlockedLevel;
        public int SelectedLevel { get; private set; } = 0;

        public LevelSelectForm(int highestUnlockedLevel = 1)
        {
            InitializeComponent();
            _highestUnlockedLevel = highestUnlockedLevel;
            UpdateLevelButtons();
        }

        private void UpdateLevelButtons()
        {
            btnLevel1.Enabled = true;
            btnLevel2.Enabled = _highestUnlockedLevel >= 2;
            btnLevel3.Enabled = _highestUnlockedLevel >= 3;

            // Update visual appearance for locked levels
            UpdateButtonAppearance(btnLevel2, _highestUnlockedLevel >= 2);
            UpdateButtonAppearance(btnLevel3, _highestUnlockedLevel >= 3);

            // Update lock labels
            lblLock2.Visible = _highestUnlockedLevel < 2;
            lblLock3.Visible = _highestUnlockedLevel < 3;
        }

        private void UpdateButtonAppearance(Button btn, bool unlocked)
        {
            if (unlocked)
            {
                btn.BackColor = Color.FromArgb(60, 60, 100);
                btn.ForeColor = Color.White;
            }
            else
            {
                btn.BackColor = Color.FromArgb(40, 40, 50);
                btn.ForeColor = Color.FromArgb(100, 100, 100);
            }
        }

        private void btnLevel1_Click(object sender, EventArgs e)
        {
            SelectedLevel = 1;
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void btnLevel2_Click(object sender, EventArgs e)
        {
            if (_highestUnlockedLevel >= 2)
            {
                SelectedLevel = 2;
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
        }

        private void btnLevel3_Click(object sender, EventArgs e)
        {
            if (_highestUnlockedLevel >= 3)
            {
                SelectedLevel = 3;
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            SelectedLevel = 0;
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
    }
}
