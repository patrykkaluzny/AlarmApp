using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;

namespace AlarmApp
{
    class MessageDialog
    {
        public void showDialog(string text, int labelHight, int dialogHeight)
        {
            Form dialog = new Form();
            dialog.StartPosition = FormStartPosition.CenterParent;
            dialog.Height = dialogHeight;
            dialog.Width = 380;
            dialog.Text = "Message";
            dialog.FormBorderStyle = FormBorderStyle.FixedSingle;
            Label textLabel = new Label()
            {
                AutoSize = false,
                TextAlign = ContentAlignment.MiddleCenter,
                Dock = DockStyle.None,
                Left = 20,
                Top = 10,
                Width = 290,
                Height = labelHight,
                Text = text
            };
            textLabel.Font = new Font("Segoe UI", 12);
            Button confirmation = new Button() { Text = "Ok", Left = 160, Width = 80, Top = labelHight+20, Height=30 };
            confirmation.Font = new Font("Segoe UI", 12);
            confirmation.Click += (sender, e) => { dialog.Close();};
            dialog.Controls.Add(confirmation);
            dialog.Controls.Add(textLabel);
            dialog.ControlBox = false;
            dialog.ShowDialog();
        }
    }
}
