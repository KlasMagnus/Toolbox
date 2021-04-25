using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using MajosDashboard.Models;

namespace MajosDashboard.Viewmodels
{
    public class ReportViewModel : ObservableObject
    {
        private ObservableCollection<ReportListItem> _reportListItems;
        private DateTime _startTime = DateTime.Now;
        private DateTime _endTime = DateTime.Now.AddHours(1);

        public DateTime StartTime {set => _startTime = value; }
        public DateTime EndTime {set => _endTime = value; }
        public DataGrid DataGrid;
        public ObservableCollection<ReportListItem> ReportListItems
        {
            get{ return _reportListItems; }
            set{ _reportListItems = value; RaisePropertyChanged("ReportListItems"); }
        }
        public ICommand GenerateReportCommand { get; internal set; }

        public ReportViewModel()
        {
            CreateGenerateReportCommand();
        }


        private void CreateGenerateReportCommand()
        {
            GenerateReportCommand = new RelayCommand(GenerateReport, CanGenerateReport);
        }
        private bool CanGenerateReport()
        {
            if ((_startTime != null) && (_endTime != null))
                return true;
            else
                return false;
        }

        public void GenerateReport()
        {
            SetStartAndEndDate();

            ReportCreator reportCreator = new ReportCreator();
            ReportListItems = reportCreator.CreateReport(_startTime, _endTime);
            
        }

        private void SetStartAndEndDate()
        {
            System.Globalization.CultureInfo ci = System.Threading.Thread.CurrentThread.CurrentCulture;
            DayOfWeek firstDayOfWeek = ci.DateTimeFormat.FirstDayOfWeek;

            DayOfWeek today = DateTime.Now.DayOfWeek;
            _startTime = DateTime.Now.AddDays(-(today - firstDayOfWeek)).Date;
            _endTime = _startTime.AddDays(6);
            _startTime = _startTime.AddDays(-7);

        }

    }
}
