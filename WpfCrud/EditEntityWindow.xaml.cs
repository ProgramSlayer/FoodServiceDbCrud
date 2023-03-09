using System;
using System.Windows;
using WpfCrud.ViewModels;

namespace WpfCrud
{
    public partial class EditEntityWindow : Window
    {
        public EditEntityWindow(EditEntityViewModel editEntityViewModel)
        {
            if (editEntityViewModel is null)
            {
                throw new ArgumentNullException(nameof(editEntityViewModel));
            }

            InitializeComponent();
            DataContext = editEntityViewModel;
        }

        private void BtnSubmit_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
            Close();
        }

        private void BtnCancel_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            Close();
        }
    }
}
