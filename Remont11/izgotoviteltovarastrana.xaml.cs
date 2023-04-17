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

namespace Remont11
{
    /// <summary>
    /// Логика взаимодействия для izgotoviteltovarastrana.xaml
    /// </summary>
    public partial class izgotoviteltovarastrana : Window
    {
        public izgotoviteltovarastrana()
        {
            InitializeComponent();
        }

        private void NewStrana_Click(object sender, RoutedEventArgs e)
        {
            Transfer.ValueString = TB2.Text;
            //Transfer.ValueInt = Convert.ToInt16(TB1.Text);
            this.Close();
        }

        private void Close_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
