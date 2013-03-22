using System.Windows;

namespace WallOfShame
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            if (App.CurrentApp.FullScreen)
            {
                WindowState = WindowState.Normal;
                WindowStyle = WindowStyle.None;
                Topmost = true;
                WindowState = WindowState.Maximized;
            }
        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
