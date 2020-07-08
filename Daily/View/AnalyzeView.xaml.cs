using Daily.ViewModel;
using System.Windows;

namespace Daily.View
{
    /// <summary>
    /// Interaction logic for AnalyzeView.xaml
    /// </summary>
    public partial class AnalyzeView : Window
    {
        public AnalyzeViewModel ViewModel
        {
            get
            {
                return DataContext as AnalyzeViewModel;
            }
        }

        public AnalyzeView()
        {
            InitializeComponent();
        }
    }
}
