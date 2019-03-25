using Daily.Common;
using System;
using System.Windows.Input;
using UILibrary.Base;

namespace Daily.ViewModel
{
    public class CalendarViewModel : BaseViewModel
    {
        private ICommand _okClick;

        private ICommand _cancelClick;

        private DateTime _selectedStartDate;

        private DateTime _selectedEndDate;

        public ICommand OKClick
        {
            get
            {
                return _okClick;
            }
        }

        public ICommand CancelClick
        {
            get
            {
                return _cancelClick;
            }
        }

        public DateTime SelectedStartDate
        {
            get
            {
                return _selectedStartDate;
            }
            set
            {
                _selectedStartDate = value;
                OnPropertyChanged();
            }
        }

        public DateTime SelectedEndDate
        {
            get
            {
                return _selectedEndDate;
            }
            set
            {
                _selectedEndDate = value;
                OnPropertyChanged();
            }
        }

        public event EventHandler<GenericEventArgs<Tuple<DateTime, DateTime>>> OKClicked;

        public event EventHandler Closing;

        private void RegisterCommands()
        {
            _okClick = new RelayCommand((param) => OnOKClick(), true);
            _cancelClick = new RelayCommand((param) => OnCancelClick(), true);
        }

        private void Initialize()
        {
            SelectedStartDate = DateTime.Today;
            SelectedEndDate = DateTime.Today;
        }

        private void OnOKClick()
        {
            OKClicked(this, new GenericEventArgs<Tuple<DateTime, DateTime>>(new Tuple<DateTime, DateTime>(SelectedStartDate, SelectedEndDate)));
            Closing(this, null);
        }

        private void OnCancelClick()
        {
            Closing(this, null);
        }

        public CalendarViewModel()
        {
            Initialize();
            RegisterCommands();
        }
    }
}
