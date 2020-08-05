using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
using Xceed.Wpf.Toolkit.PropertyGrid.Attributes;

namespace MailSender.View
{
    /// <summary>
    /// Логика взаимодействия для OptionSenderView.xaml
    /// </summary>
    public partial class OptionSenderView : UserControl
    {
        

        public OptionSenderView()
        {
            InitializeComponent();
            
        }

        //private void UpButton_Click(object sender, RoutedEventArgs e)
        //{
        //    int tmp;
        //    if (Int32.TryParse(tValue.Text,out tmp))
        //    {
        //        tValue.Text = (++tmp).ToString();
        //    }
        //}

        //private void DownButton_Click(object sender, RoutedEventArgs e)
        //{
        //    int tmp;
        //    if (Int32.TryParse(tValue.Text, out tmp))
        //    {
        //        tValue.Text = (--tmp).ToString();
        //    }
        //}
    }
}
