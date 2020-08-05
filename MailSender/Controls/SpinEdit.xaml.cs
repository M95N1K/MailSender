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
            get { return (int)GetValue(ValuesProperty); }
            set
            {
               SetValue(ValuesProperty,value);
            }
        }

        public SpinEdit()
        {
            InitializeComponent();
        }

        private void Up_Click(object sender, RoutedEventArgs e)
        {
            values = (int)GetValue(ValuesProperty);
            values++;
            SetValue(ValuesProperty, values);
        }

        private void DownButton_Click(object sender, RoutedEventArgs e)
        {
            values = (int)GetValue(ValuesProperty);
            values--;
            SetValue(ValuesProperty, values);
        }

        private void tValue_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (!Int32.TryParse(tValue.Text, out values))
                tValue.Text = (0).ToString();
            else
                Values = Int32.Parse(tValue.Text);
        }

        private FrameworkPropertyMetadata valuesMeta =
            new FrameworkPropertyMetadata(0, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault);

        public static readonly DependencyProperty ValuesProperty = DependencyProperty.Register("Values", typeof(int),
            typeof(SpinEdit), 
            new FrameworkPropertyMetadata(0, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault,CurrentNumberChanged), 
            new ValidateValueCallback(ValidateCurrentValues));

        public static bool ValidateCurrentValues(object value)
        {
            try
            {
                Convert.ToInt32(value);
            }
            catch 
            {
                return false;
            }
            return true;
        }

        private static void CurrentNumberChanged(DependencyObject depOgj, DependencyPropertyChangedEventArgs args)
        {
            SpinEdit s = (SpinEdit)depOgj;
            TextBox theBox = s.tValue;
            theBox.Text = args.NewValue.ToString();
        }

    }
}
