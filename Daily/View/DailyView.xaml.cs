using Daily.ViewModel;
using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Media;

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

        private bool _isUpdate = true;

        public DailyViewModel ViewModel
        {
            get
            {
                return DataContext as DailyViewModel;
            }
        }

        private readonly string _itemBoxWatermark = "항목 입력";

        public DailyView()
        {
            InitializeComponent();

            ItemBox.TextChanged += ItemBox_TextChanged;
            ItemBox.GotFocus += ItemBox_GotFocus;
            ItemBox.LostFocus += ItemBox_LostFocus;
            ItemBox.Text = _itemBoxWatermark;
            //ShowScrollBar(listView.Handle.ToInt64(), SB_HORZ, 0);
        }

        private void ItemBox_LostFocus(object sender, RoutedEventArgs e)
        {
            if (ItemBox.Text.Equals(""))
            {
                ItemBox.Text = _itemBoxWatermark;
            }
        }

        private void ItemBox_GotFocus(object sender, RoutedEventArgs e)
        {
            if (ItemBox.Text.Equals(_itemBoxWatermark))
            {
                _isUpdate = false;
                ItemBox.Text = "";
                ItemBox.Foreground = Brushes.Black;
            }
        }

        private void ItemBox_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {
            if (ItemBox.Text.Equals(_itemBoxWatermark) || ItemBox.Text.Equals(""))
            {
                if (ItemBox.Text.Equals(_itemBoxWatermark))
                {
                    ItemBox.Foreground = Brushes.Gray;
                }
                
                if (ItemBox.Text.Equals(""))
                {
                    if (_isUpdate == true && ItemBox.IsFocused == false)
                    {
                        ItemBox.Text = _itemBoxWatermark;
                    }
                    else
                    {
                        _isUpdate = true; // revert
                    }
                }
            }
            else
            {
                ItemBox.Foreground = Brushes.Black;
            }
        }
    }
}
