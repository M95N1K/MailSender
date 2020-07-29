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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace MailSender.Controls
{
    /// <summary>
    /// Логика взаимодействия для SpinEdit.xaml
    /// </summary>
    public partial class SpinEdit : UserControl
    {
        private int values = 0;
        public int Values
        {
            get => values;
            set
            {
                values = value;
                tValue.Text = value.ToString();
            }
        }

        public SpinEdit()
        {
            InitializeComponent();
        }

        private void Up_Click(object sender, RoutedEventArgs e)
        {
            if (Int32.TryParse(tValue.Text, out values))
                tValue.Text = (Values + 1).ToString();
        }

        private void DownButton_Click(object sender, RoutedEventArgs e)
        {
            if (Int32.TryParse(tValue.Text, out values))
                tValue.Text = (Values - 1).ToString();
        }

        private void tValue_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (!Int32.TryParse(tValue.Text, out values))
                tValue.Text = (0).ToString();
        }

        
    }
}
