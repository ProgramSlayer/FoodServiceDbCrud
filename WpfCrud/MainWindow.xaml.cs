using System;
using System.Windows;

namespace WpfCrud
{
    public partial class MainWindow : Window
    {
        public MainWindow(ViewModels.MainViewModel mainViewModel)
        {
            if (mainViewModel is null)
            {
                throw new ArgumentNullException(nameof(mainViewModel));
            }

            InitializeComponent();
            DataContext = mainViewModel;
        }
    }
}
