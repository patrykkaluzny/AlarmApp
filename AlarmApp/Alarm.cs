using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlarmApp
{
    class Data
    {
        private string alarmMessage;
        private DateTime alarmDate;
        bool mTrigged;


        public Data(DateTime Date, string Message, bool Trigged)
        {
            alarmMessage = Message;
            alarmDate = Date;
            mTrigged = Trigged;
        }
        public Data()
        {
           
        }
        public void setAlarm(DateTime Date, string Message)
        {
                alarmMessage = Message;
                alarmDate = Date;
        }
      
        public DateTime getAlarmDate()
        {
            return alarmDate;
        }
        public string getAlarmMessage()
        {
            return alarmMessage;
        }
        public void set_alarmDate (DateTime a)
        {
            alarmDate = a;
        }
        public void set_alarmMessage(string m)
        {
            alarmMessage = m;
        }
        public string getMesaage()
        {
            return alarmMessage;
        }
        public bool getTrigged()
        {
            return mTrigged;
        }
        public void setTrigged()
        {
            mTrigged = true;
        }
        public void resetTrigged()
        {
            mTrigged = false;
        }
    }
}
