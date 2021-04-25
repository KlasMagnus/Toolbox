using System;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using Outlook = Microsoft.Office.Interop.Outlook;

namespace MajosDashboard.Models
{
    public class Appointment
    {

        public Appointment(string subject, string body, DateTime start, DateTime end)
        {
            Outlook.Application application = GetApplicationObject();
            Outlook.AppointmentItem appointment = application.CreateItem(Outlook.OlItemType.olAppointmentItem) as Outlook.AppointmentItem;
            appointment.Subject = subject;
            appointment.Body = body;
            appointment.AllDayEvent = false;
            appointment.Start = start;
            appointment.End = end;
            appointment.ReminderSet = false;
            appointment.BusyStatus = Outlook.OlBusyStatus.olFree;
            appointment.Display(false);
            //appointment.Save();
            appointment.Close(Outlook.OlInspectorClose.olSave);

        }

        Outlook.Application GetApplicationObject()
        {

            Outlook.Application application = null;

            // Check whether there is an Outlook process running.
            if (Process.GetProcessesByName("OUTLOOK").Count() > 0)
            {

                // If so, use the GetActiveObject method to obtain the process and cast it to an Application object.
                application = Marshal.GetActiveObject("Outlook.Application") as Outlook.Application;
            }
            else
            {

                // If not, create a new instance of Outlook and sign in to the default profile.
                application = new Outlook.Application();
                Outlook.NameSpace nameSpace = application.GetNamespace("MAPI");
                nameSpace.Logon("", "", Missing.Value, Missing.Value);
                nameSpace = null;
            }

            // Return the Outlook Application object.
            return application;
        }

    }
}

