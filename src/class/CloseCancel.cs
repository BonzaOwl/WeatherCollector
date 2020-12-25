using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WeatherCollectorDesktop
{
    class CloseCancel
    {
        public static bool ConfirmCloseCancel()
        {
            const string message = "Are you sure that you want to exit?";
            const string caption = "Confirm exit";

            var result = MessageBox.Show(message, caption, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
