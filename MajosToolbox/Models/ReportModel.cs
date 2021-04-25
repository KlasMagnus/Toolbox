using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using Outlook = Microsoft.Office.Interop.Outlook;


namespace MajosDashboard.Models
{
    public class ReportListItem
    {
        private string _customerName;
        private string _workPerformed;
        private int _minutesWorked;

        public string CustomerName { get => _customerName;}
        public string WorkPerformed { get => _workPerformed;}
        public int MinutesWorked { get => _minutesWorked;}

        public ReportListItem(string customerName, string workPerformed, int minutesWorked)
        {
            _customerName = customerName;
            _workPerformed = workPerformed;
            _minutesWorked = minutesWorked;
        }

        
    }

    public class ReportCreator
    {
        
        public ObservableCollection<ReportListItem>CreateReport(DateTime startDate, DateTime endDate)
        {
            ObservableCollection<ReportListItem> reportListItems = new ObservableCollection<ReportListItem>();

            CustomerModel customerModel = new CustomerModel();
            string[] customers = customerModel.customers;
                        
            Outlook.Application OutlookApp = GetApplicationObject();
            Outlook.Folder folder = OutlookApp.Session.GetDefaultFolder(Outlook.OlDefaultFolders.olFolderCalendar) as Outlook.Folder;
            Outlook.Items rangeAppointments = GetAppointmentsInRange(folder, startDate, endDate);

            if (rangeAppointments != null)
            {
                foreach (Outlook.AppointmentItem appointmentItem in rangeAppointments)
                {
                    if (customers.Contains(appointmentItem.Subject))
                    {
                        reportListItems.Add(new ReportListItem(appointmentItem.Subject, appointmentItem.Body, appointmentItem.Duration));
                    }
                }
            }
            return reportListItems;
        }


        private Outlook.Application GetApplicationObject()
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

        private Outlook.Items GetAppointmentsInRange(Outlook.Folder folder, DateTime startTime, DateTime endTime)
        {
            string filter = "[Start] >= '"
                + startTime.ToString("g")
                + "' AND [End] <= '"
                + endTime.ToString("g") + "'";
            Debug.WriteLine(filter);
            try
            {
                Outlook.Items calItems = folder.Items;
                //calItems.IncludeRecurrences = true;
                calItems.Sort("[Start]", Type.Missing);
                Outlook.Items restrictItems = calItems.Restrict(filter);
                if (restrictItems.Count > 0)
                {
                    return restrictItems;
                }
                else
                {
                    return null;
                }
            }
            catch { return null; }
        }
    }
}
