using Daily.ViewModel;
using System.Runtime.InteropServices;
using System.Windows;

namespace Daily.View
{
    /// <summary>
    /// DailyView.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class DailyView : Window
    {
        [DllImport("user32")]
        private static extern long ShowScrollBar(long hwnd, long wBar, long bShow);
        long SB_HORZ = 0;
        long SB_VERT = 1;
        long SB_BOTH = 3;

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

            //ShowScrollBar(listView.Handle.ToInt64(), SB_HORZ, 0);
        }
    }
}
