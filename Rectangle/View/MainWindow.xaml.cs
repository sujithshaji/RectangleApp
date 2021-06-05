#region Namespace
using System.Windows;
#endregion
namespace RectangleApp
{
    #region MainWindow Class
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            DataContext = new MainWindowViewModel();
        }
    }
    #endregion
}
