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
    /// Interaction logic for CreateMessage.xaml
    /// </summary>
    public partial class CreateMessage : UserControl
    {
        public CreateMessage()
        {
            InitializeComponent();
        }

        private void ShowReplyingMessageButton_Click(object sender, RoutedEventArgs e)
        {
            if(MessageTextBox.Visibility == Visibility.Visible)
            {
                MessageTextBox.Visibility = Visibility.Hidden;
                ReplyingMessageTextBox.Visibility = Visibility.Visible;
                ShowReplyingMessageButton.Content = "Ukryj wiadomość";
            }
            else
            {
                MessageTextBox.Visibility = Visibility.Visible;
                ReplyingMessageTextBox.Visibility = Visibility.Hidden;
                ShowReplyingMessageButton.Content = "Pokaż wiadomość";
            }
        }

        private void MessageTextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            BindingExpression be = MessageTextBox.GetBindingExpression(TextBox.TextProperty);
            be.UpdateSource();
        }

        private void TitleTextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            BindingExpression be = TitleTextBox.GetBindingExpression(TextBox.TextProperty);
            be.UpdateSource();
        }
    }
}
