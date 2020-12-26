using System;
using System.Windows.Forms;

namespace WeatherCollectorDesktop
{
    public partial class about : Form
    {
        public about()
        {
            InitializeComponent();

            //Hide the close buttons
            this.ControlBox = false;
        }

        private void SettingsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new settings().Show();
            this.Hide();
        }

        private void HistoryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new History().Show();
            this.Hide();
        }

        private void AboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string docs = Properties.Settings.Default.DocLink.ToString();
            _ = System.Diagnostics.Process.Start(docs);
        }

        private void AboutToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            new about().Show();
            this.Hide();
        }

        private void HomeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new WeatherCollector().Show();
            this.Hide();
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            if (CloseCancel.ConfirmCloseCancel() == true)
            {
                e.Cancel = false;
            }
            else
            {
                e.Cancel = true;
            }
        }
    }
}
