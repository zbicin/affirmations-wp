using Microsoft.Phone.Scheduler;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Affirmations.ViewModel
{
    public class ScheduledRemindersHelper
    {
        private const string REMINDER_ID = "affirmationsReminder";

        public void TryScheduleReminder(DateTime LastRepetitionDate)
        {
            if (ScheduledActionService.Find(REMINDER_ID) != null)
            {
                ScheduledActionService.Remove(REMINDER_ID);
            }

            DateTime BeginTime;

            if (LastRepetitionDate.Date.CompareTo(DateTime.Now.Date) < 0
                && DateTime.Now.TimeOfDay.CompareTo(App.ViewModel.ReminderDateTime.TimeOfDay) < 0)
            {
                // if we repeated yesterday
                // and the reminder time hasn't passed today yet
                // so we can set it today
                BeginTime = DateTime.Now.Date.Add(App.ViewModel.ReminderDateTime.TimeOfDay);
            }
            else
            {
                // otherwise - tommorow
                BeginTime = DateTime.Now.Date.AddDays(1).Add(App.ViewModel.ReminderDateTime.TimeOfDay);
            }

            Reminder scheduledReminder = new Reminder(REMINDER_ID);

            scheduledReminder.BeginTime = BeginTime;
            scheduledReminder.Content = "Nie powtarzałeś jeszcze dzisiaj afirmacji.";
            scheduledReminder.RecurrenceType = RecurrenceInterval.None;

            ScheduledActionService.Add(scheduledReminder);
        }

        public void TryUnscheduleReminder()
        {
            if (ScheduledActionService.Find(REMINDER_ID) != null)
            {
                ScheduledActionService.Remove(REMINDER_ID);
            }
        }
    }
}
