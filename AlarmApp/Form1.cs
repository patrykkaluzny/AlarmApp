using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Globalization;
using System.IO;

namespace AlarmApp
{
    public partial class AlarmApp : Form
    {
        private System.Timers.Timer clockTimer;
        private List<Data> alarmsList = new List<Data>();
        string savePath; 
        delegate void StringArgReturningVoidDelegate(string text);
        public AlarmApp()
        {
           
            InitializeComponent();
            initClock();
            SetText(DateTime.Now.ToString("HH:mm:ss"));
            savePath = Path.Combine(Environment.SpecialFolder.MyDocuments.ToString(), "AlarmAppFiles", "SavedAlarms");
            addButton.Click += new EventHandler(addButtonClicked);
            removeButton.Click += new EventHandler(removeButtonClicked);
            editButton.Click += new EventHandler(editButtonClicked);
            CultureInfo.DefaultThreadCurrentCulture= CultureInfo.CreateSpecificCulture("pl-PL");
            this.CenterToScreen();
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            loadFileToList();
            fillListBox();
        }
        private void initClock()
        {
            clockTimer = new System.Timers.Timer(1000);
            clockTimer.Elapsed += new System.Timers.ElapsedEventHandler(clockEvent);
            clockTimer.Start();
        }
        public void clockEvent(object sender, EventArgs e)
        {
            DateTime dt = DateTime.Now;
            SetText(DateTime.Now.ToString("HH:mm:ss"));
            if(DateTime.Now.Second==0)checkAlarms();
            
        }
        private void SetText(string text)
        { 
            if (this.clockLabel.InvokeRequired)
            {
                StringArgReturningVoidDelegate d = new StringArgReturningVoidDelegate(SetText);
                this.Invoke(d, new object[] { text });
            }
            else
            {
                this.clockLabel.Text = text;
            }
        }
        private void addButtonClicked(Object sender, EventArgs e)
        {
            Dialog addDailog = new Dialog();            
            bool wynik = addDailog.showDialog("Add new alarm", "New alarm", DateTime.Now.Hour,DateTime.Now.Minute+1);
            if(wynik)
            {
                alarmsList.Add(new Data(addDailog.getDate(), addDailog.getMessage(),false));
                fillListBox();
                saveListToFile();
            }
        }
        private void removeButtonClicked(Object sender, EventArgs e)
        {           
            if (alarmListBox.SelectedIndex != -1)
            {
                alarmsList.RemoveAt(alarmListBox.SelectedIndex);
                fillListBox();
                saveListToFile();
            }
            else
            {
                MessageDialog dialog = new MessageDialog();
                dialog.showDialog("You must choose alarm to delete it.", 60,165);
            }
        }
        private void editButtonClicked(Object sender, EventArgs e)
        {
            if (alarmListBox.SelectedIndex != -1)
             {
                 Dialog editDailog = new Dialog();
                 bool wynik = editDailog.showDialog("Edit existing alarm","Edit ", alarmsList[alarmListBox.SelectedIndex].getAlarmDate().Hour, alarmsList[alarmListBox.SelectedIndex].getAlarmDate().Minute);
                 if (wynik)
                 {
                     alarmsList[alarmListBox.SelectedIndex]=(new Data(editDailog.getDate(), editDailog.getMessage(),false));
                     fillListBox();
                     saveListToFile();
                 }
             }
             else
            {
                MessageDialog dialog = new MessageDialog();
                dialog.showDialog("You must choose alarm to edit it.", 60, 165);
            }
        }
        private void fillListBox()
        {
            alarmListBox.Items.Clear();
            foreach(Data temp in alarmsList)
            {
                alarmListBox.Items.Add(temp.getAlarmDate().ToString());
            }
        }
        private void saveListToFile()
        {
            if (!File.Exists(savePath))
            {
                Directory.CreateDirectory(savePath);
            }
            var filePath= Path.Combine(savePath, "data.txt");
            if (File.Exists(filePath))
            {
                File.WriteAllText(filePath, null);
            }
            foreach (Data temp in alarmsList)
            {
                string toSave = temp.getAlarmDate().ToString() + ";" + temp.getAlarmMessage() + ";"+"\n";
                File.AppendAllText(filePath, toSave);
            }
        }
        private void loadFileToList()
        {
            var filePath = Path.Combine(savePath, "data.txt");
            alarmsList.Clear();
            if (File.Exists(filePath))
            {
                string[] readText = File.ReadAllLines(filePath);
                foreach(string temp in readText)
                {
                    bool tempBool;
                    string[] line = temp.Split(';');
                    if (DateTime.Parse(line[0]).CompareTo(DateTime.Now) == -1)
                    {
                        tempBool = true;
                    }
                    else { tempBool = false; }
                    alarmsList.Add(new Data(DateTime.Parse(line[0]), line[1],tempBool)); 
                }              
            }
        }
        private void checkAlarms()
        {
            for (int i=0;i<alarmsList.Count;i++)
            {
                if (!alarmsList[i].getTrigged())
                {
                    if (DateTime.Now.CompareTo(alarmsList[i].getAlarmDate())==1)
                    {
                        alarmsList[i].setTrigged();
                        string alertMessage = "ALARM!!!\n" + alarmsList[i].getAlarmDate().ToString() + "\n" + "Message: " + alarmsList[i].getAlarmMessage();
                        Task.Run(() => {
                            MessageDialog dialog = new MessageDialog();
                            dialog.showDialog(alertMessage,150,270);
                        });
                    }
                }
            }
        }
    }
}
