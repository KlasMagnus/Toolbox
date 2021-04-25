using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using MajosDashboard.Models;

namespace MajosDashboard.Viewmodels
{
    public class AppointmentViewModel : ObservableObject
    {
        private string[] _customers = { "Customer1", "Customer2", "Customer3" };
        private DateTime _startTime = DateTime.Now;
        private DateTime _endTime = DateTime.Now.AddHours(1);
        private string _message;
        private string _subject;
        private DateTime _selectedDate = DateTime.Today;
        
        

        public string[] Customers
        {
            get { return _customers; }
        }

        public string Message { get => _message; set => _message = value; }
        public DateTime StartTime { get => _startTime; set => _startTime = value; }
        public DateTime EndTime { get => _endTime; set => _endTime = value; }
        public string Subject
        {
            get => _subject;
            set
            {
                _subject = value;
                
            }
           
        }

        public AppointmentViewModel()
        {
            CustomerModel customerModel = new CustomerModel();
            _customers = customerModel.customers;

            CreateNewAppointmentCommand();
        }

        public ICommand NewAppointmentCommand { get; internal set; }
        public DateTime SelectedDate { get => _selectedDate; set => _selectedDate = value; }

        private bool CanMakeNewAppointment() {
            if (!String.IsNullOrEmpty(_message) && !String.IsNullOrEmpty(_subject) && (_startTime != null) && (_endTime != null) && (_selectedDate != null))
                return true;
            else
                return false;
        }

        

        private void CreateNewAppointmentCommand()
        {
            NewAppointmentCommand = new RelayCommand(CreateAppointment, CanMakeNewAppointment);
        }

        public void CreateAppointment()
        {
            Appointment appointment = new Appointment(_subject, _message, _startTime, _endTime);
            //TODO: log result (expose message)
        }
    }
}
