using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace SklepWPF.Views
{
    /// <summary>
    /// Interaction logic for ListaProduktow.xaml
    /// </summary>
    public partial class ProductList : UserControl
    {
        public ProductList()
        {
            InitializeComponent();
        }

        private void produktyListBox_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if(produktyListBox.SelectedIndex != -1)
            {
                produktyListBox.UnselectAll();
                nazwaTextBox.Clear();
                opisTextBox.Clear();
                cenaTextBox.Clear();
                markaTextBox.Clear();
                iloscTextBox.Clear();
                CategoryComboBox.SelectedIndex = -1;
            }
        }
    }
}
