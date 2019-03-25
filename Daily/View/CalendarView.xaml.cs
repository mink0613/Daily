using Daily.ViewModel;
using System.Windows;
using System.Windows.Input;

namespace Daily.View
{
    /// <summary>
    /// CalendarView.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class CalendarView : Window
    {
        public CalendarViewModel ViewModel
        {
            get
            {
                return DataContext as CalendarViewModel;
            }
        }

        public CalendarView()
        {
            InitializeComponent();
        }

        private void Calendar_SelectedDatesChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            Mouse.Capture(null);
        }
    }
}
