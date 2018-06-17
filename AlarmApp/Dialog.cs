using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;


namespace AlarmApp
{
    public class Dialog
    {
        DateTime date = new DateTime();
        string message;
        ComboBox hourBox;
        ComboBox minuteBox;
        Form dialog;
        bool added;
        Label textLabel1;
        Label textLabel2;
        Label textLabel3;
        Label textLabel4;
        Label textLabel5;
        Button confirmation;
        Button cancel;
        DateTimePicker datePicker;
        TextBox messageBox;
        public bool showDialog(string text, string caption, int hour, int minute)
        {

            added = false;
            initControls(text, hour, minute);
            setFont();
            initDilog(caption);
            dialog.ShowDialog();     
            return added;
        }
        private void setFont()
        {
            cancel.Font = new Font("Segoe UI", 12);
            confirmation.Font = new Font("Segoe UI", 12);
            textLabel1.Font = new Font("Segoe UI", 20);
            textLabel2.Font = new Font("Segoe UI", 14);
            textLabel3.Font = new Font("Segoe UI", 14);
            textLabel4.Font = new Font("Segoe UI", 14);
            textLabel5.Font = new Font("Segoe UI", 14);
            datePicker.Font = new Font("Segoe UI", 8);
            hourBox.Font = new Font("Segoe UI", 8);
            minuteBox.Font = new Font("Segoe UI", 8);
            messageBox.Font = new Font("Segoe UI", 14);

        }
        public void initControls(string text, int hour, int minute)
        {
            textLabel1 = new Label()
            {
                AutoSize = false,
                TextAlign = ContentAlignment.MiddleCenter,
                Dock = DockStyle.None,
                Left = 20,
                Top = 20,
                Width = 490,
                Height = 30,
                Text = text};
            textLabel2 = new Label()
            {
                AutoSize = false,
                TextAlign = ContentAlignment.MiddleCenter,
                Dock = DockStyle.None,
                Left = 20,
                Top = 115,
                Width = 490,
                Height = 30,
                Text = "Alarm message:" };
            textLabel3 = new Label()
            {
                AutoSize = false,
                TextAlign = ContentAlignment.MiddleCenter,
                Dock = DockStyle.None,
                Left = 30,
                Top = 60,
                Width = 200,
                Height = 30,
                Text = "Date:"};
            textLabel4 = new Label()
            {
                AutoSize = false,
                TextAlign = ContentAlignment.MiddleCenter,
                Dock = DockStyle.None,
                Left = 260,
                Top = 60,
                Width = 80,
                Height = 30,
                Text = "Hour:"};
            textLabel5 = new Label()
            {
                AutoSize = false,
                TextAlign = ContentAlignment.MiddleCenter,
                Dock = DockStyle.None,
                Left = 360,
                Top = 60,
                Width = 80,
                Height = 30,
                Text = "Minute:"};
            datePicker = new DateTimePicker() { Left = 30, Top = 90, Width = 200, Height = 30 };
            datePicker.MinDate = DateTime.Today;
            hourBox = new ComboBox() { Left = 280, Top = 90, Width = 40, Height = 30, };
            for (int i = 0; i < 24; i++)
            {
                hourBox.Items.Add(i.ToString());
            }
            hourBox.SelectedIndex = hour;
            hourBox.DropDownStyle = ComboBoxStyle.DropDownList; minuteBox = new ComboBox() { Left = 380, Top = 90, Width = 40, Height = 30 };
            for (int i = 0; i < 60; i++)
            {
                minuteBox.Items.Add(i.ToString());
            }
            minuteBox.SelectedIndex = minute;
            minuteBox.DropDownStyle = ComboBoxStyle.DropDownList;
            confirmation = new Button() { Text = "Ok", Left = 150, Width = 80, Height = 30, Top = 210, TextAlign = ContentAlignment.MiddleCenter, };
            cancel = new Button() { Text = "Cancel", Left = 270, Height = 30, Width = 80, Top = 210, TextAlign = ContentAlignment.MiddleCenter, };

            cancel.Click += (sender, e) => { dialog.Close(); added = false; };
            confirmation.Click += (sender, e) => {
                added = true;
                date = DateConvert(datePicker.Value, int.Parse(hourBox.SelectedItem.ToString()), int.Parse(minuteBox.SelectedItem.ToString()));
                if (date.CompareTo(DateTime.Now) <= 0)
                {
                    MessageDialog dialog_temp = new MessageDialog();
                    dialog_temp.showDialog("It's date from the past, please choose another date.", 60, 165);
                }
                else
                {
                    message = messageBox.Text.ToString();
                    added = true;
                    dialog.Close();
                }
            };
            messageBox = new TextBox()
            {
                Dock = DockStyle.None,
                Left = 100,
                Top = 150,
                Width = 300,
                Height = 30,
                Text = "Alarm"
            };
        }
        private void initDilog(string caption)
        {
            dialog = new Form();
            dialog.StartPosition = FormStartPosition.CenterParent;
            dialog.Height = 300;
            dialog.Width = 500;
            dialog.Text = caption;
            dialog.FormBorderStyle = FormBorderStyle.FixedSingle;
            dialog.Controls.Add(confirmation);
            dialog.Controls.Add(cancel);
            dialog.Controls.Add(textLabel1);
            dialog.Controls.Add(datePicker);
            dialog.Controls.Add(hourBox);
            dialog.Controls.Add(minuteBox);
            dialog.Controls.Add(messageBox);
            dialog.Controls.Add(textLabel2);
            dialog.Controls.Add(textLabel3);
            dialog.Controls.Add(textLabel4);
            dialog.Controls.Add(textLabel5);
            dialog.ControlBox = false;
        }
        public string getMessage()
        {
            return message;
        }
        public DateTime getDate()
        {
            return date;
        }
        private DateTime DateConvert(DateTime date, int hour, int minute)
        {
            return new DateTime(date.Year, date.Month, date.Day, hour, minute, 0);

        }
}



    
}
