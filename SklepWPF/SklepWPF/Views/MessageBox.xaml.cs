using SklepWPF.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace SklepWPF.Views
{
    /// <summary>
    /// Interaction logic for MessageBox.xaml
    /// </summary>
    public partial class MessageBox : UserControl
    {
        public MessageBox()
        {
            InitializeComponent();
        }

        private void SearchButton_Click(object sender, RoutedEventArgs e)
        {
            BindingExpression be = SearchQueryTextBox.GetBindingExpression(TextBox.TextProperty);
            be.UpdateSource();
        }

        private void ClearMessageBoxButton_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = System.Windows.MessageBox.Show("Czy na pewno chcesz usunąć wszystkie wiadomości?",
                                          "Uwaga",
                                          MessageBoxButton.YesNo,
                                          MessageBoxImage.Warning);

            if (result == MessageBoxResult.Yes)
            {
                var viewModel = (MessageBoxViewModel)DataContext;
                if (viewModel.ClearMessageBoxCommand.CanExecute(null))
                    viewModel.ClearMessageBoxCommand.Execute(null);
            }
        }
    }
}
