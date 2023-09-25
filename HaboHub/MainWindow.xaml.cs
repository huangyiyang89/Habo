using HaboHub.Injector;
using System.Windows;


namespace HaboHub
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

        private void Button_Click(object sender, RoutedEventArgs e)
        {
             var douyin=new DouyinInjector();
             douyin.Start("https://live.douyin.com/737168289964");
        }

    }
    
}
