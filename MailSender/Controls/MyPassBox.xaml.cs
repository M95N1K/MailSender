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
    /// Логика взаимодействия для MyPassBox.xaml
    /// </summary>
    public partial class MyPassBox : UserControl
    {

        //private string _pass = "";
        
        public string Pass 
        { 
            get
            {
                return (string)GetValue(PassProperty);
            }
            set
            {
                SetValue(PassProperty, value);
            }
        }

        public MyPassBox()
        {
            InitializeComponent();
        }

        public static DependencyProperty PassProperty =
            DependencyProperty.Register("Pass", typeof(string), typeof(MyPassBox),
                new FrameworkPropertyMetadata("", 
                    FrameworkPropertyMetadataOptions.BindsTwoWayByDefault,
                    PassChenged));

        private static void PassChenged(DependencyObject obj, DependencyPropertyChangedEventArgs arg)
        {
            MyPassBox tmp = (MyPassBox)obj;
            PasswordBox pb = tmp.pbPass;
            if (!pb.IsKeyboardFocused)
            pb.Password = (string)arg.NewValue;
        }

        private void pbPass_PasswordChanged(object sender, RoutedEventArgs e)
        {
            if (Pass.Equals(pbPass.Password))
                return;
            Pass = pbPass.Password;
        }
    }
}
