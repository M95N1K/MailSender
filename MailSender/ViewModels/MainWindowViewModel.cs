using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using MailSender.Infrastructure.Commands;
using MailSender.Models;
using MailSender.Models.DBModels;
using MailSender.ViewModels.Base;
using MailSender.ViewModels;
using MailSender.Works;
using System.Data.Linq;
using System.Xml.Linq;

namespace MailSender.ViewModels
{
    /// <summary>Класс взаимодействия с окном </summary>
    internal class MainWindowViewModel : ViewModel
    {
        #region Поля
        private string title = "WPFMailSender";
        private int countSendMail = 0;
        private string statusBarStatus = "Загрузка";
        
        #endregion

        #region Свойства
        public string Title { get => title; set => Set(ref title, value); }
        public string CountSendMail 
        { 
            get 
            {
                if (countSendMail == 0)
                    return "";
                else return countSendMail.ToString();
            }
            set
            {
                int tmp;
                if (!Int32.TryParse(value, out tmp))
                    tmp = 0;
                Set(ref countSendMail, tmp);
            }
        }
        public string StatusBarStatus { get => statusBarStatus; set => Set(ref statusBarStatus, value); }
        
        #endregion

        #region Команды

        #region Команда закрытия (Command={Binding CloseApplicationCommand})
        public ICommand CloseApplicationCommand { get; }

        private void OnCloseApplicationCommandEcecuted(object p)
        {
            Application.Current.Shutdown();
        }

        private bool CanCloseApplicationCommandExecute(object p) => true;
        #endregion

        #endregion

        public MainWindowViewModel()
        {
            #region Создание команд
            CloseApplicationCommand = new LambdaCommand(OnCloseApplicationCommandEcecuted, CanCloseApplicationCommandExecute);
            #endregion

            SmtpConfig.SetConfig("smtp.mail.ru", "vasilii_pupkin_83@mail.ru", "qwedsazxc", 25);
            AppErrors.OnShowErrors += AppErrors_OnShowErrors;
            SendMails.OnSendMails += SendMails_OnSendMails;
            StatusBarStatus = "Готово";
        }

        private void SendMails_OnSendMails(int CountSendMail)
        {
            StatusBarStatus = "Писем отправленно ";
            this.CountSendMail = CountSendMail.ToString();
            if (CountSendMail == 0)
                StatusBarStatus = "Ошибка при отправке";
            if (AppErrors.Count > 0)
                AppErrors.ShowErrors();
        }

        private void AppErrors_OnShowErrors()
        {
            ErrorWindow tmp = new ErrorWindow();
            tmp.ShowDialog();
        }

        

        

        
    }
}
