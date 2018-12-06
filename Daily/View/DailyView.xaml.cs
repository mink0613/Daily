using Daily.ViewModel;
using System.Windows;

namespace Daily.View
{
    /// <summary>
    /// DailyView.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class DailyView : Window
    {
        public DailyViewModel ViewModel
        {
            get
            {
                return DataContext as DailyViewModel;
            }
        }

        public DailyView()
        {
            InitializeComponent();
        }
    }
}
