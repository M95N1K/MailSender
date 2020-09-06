using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using MailSender.Infrastructure.Interfaces;
using MailSender.Works;

namespace MailSender
{
    /// <summary>
    /// Логика взаимодействия для App.xaml
    /// </summary>
    public partial class App : Application
    {
      private IEmailData emailTmp =  new EmailDateBase();
    }
}
