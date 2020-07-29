using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using MailSender.Infrastructure.Commands;
using MailSender.Models;
using MailSender.ViewModels.Base;
using MailSender.ViewModels;
using MailSender.Works;

namespace MailSender.ViewModels
{
    /// <summary>Класс взаимодействия с окном </summary>
    class MainWindowViewModel : ViewModel
    {
        #region Поля
        private string title = "WPFMailSender";
        private string mailHeader = "Заголовок";
        private string mailBody = "Текст";
        private string statusBarStatus = "Готово";
        private int countSendMail = 0;
        private List<string> recipientList = new List<string>()
        {
            "jonatan@mail.ru - вымышленно"
        };
        private OptionsViewModel optionsApp = new OptionsViewModel();
        #endregion

        #region Свойства
        public string Title { get => title; set => Set(ref title, value); }
        public string MailHeader { get => mailHeader; set => Set(ref mailHeader, value); }
        public string MailBody { get => mailBody; set => Set(ref mailBody, value); }
        public string StatusBarStatus { get => statusBarStatus; set => Set(ref statusBarStatus, value); }
        public int CountSendMail { get => countSendMail; set => Set(ref countSendMail, value); }
        public List<string> RecipientList { get => recipientList; set => Set(ref recipientList, value); }
        public OptionsViewModel OptionsApp { get => optionsApp; set => Set(ref optionsApp, value); } 
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

        #region Команда очистки полей письма
        public ICommand ClearMailDataCommand { get; }

        private void OnClearMailDataExecuted(object p)
        {
            MailBody = "";
            MailHeader = "";
        }

        private bool CanClearMailDataExecute(object p)
        {
            return (MailHeader != "" || MailBody != "");
        }
        #endregion

        #region Команда отправки письма
        public ICommand SendMailCommand { get; }
        private void OnSendMailExecuted(object p)
        {
            CountSendMail = 0;
            StructMails mails;
            mails.body = MailBody;
            mails.header = MailHeader;
            StatusBarStatus = "Отправка писем...";
            CountSendMail = SendMails.SendsMail(RecipientList, mails, "qwedsazxc");
            StatusBarStatus = "Писем отправленно"; 
            if (AppErrors.Count > 0)
                AppErrors.ShowErrors();
        }
        private bool CanSendMailExecute(object p)
        {
            return (MailHeader !="" && MailBody !="" && RecipientList.Count > 0);
        }
        #endregion

        #region Команда принятия изменений в настройках
        public ICommand ApllyChangeOptionCommand { get; }
        private void OnApllyChageOptionsExecute(object p)
        {
            SmtpConfig.SetConfig(OptionsApp.SmtpServ, OptionsApp.SendrsMail, OptionsApp.SmtpPort);
        }
        private bool CanApllyChageOptionsExecute(object p)
        {
            return (OptionsApp.SmtpPort != SmtpConfig.Port ||
                OptionsApp.SmtpServ != SmtpConfig.SmtpServer ||
                OptionsApp.SendrsMail != SmtpConfig.SendersMail);
        }
        #endregion

        #region Отмена изменений настроек
        public ICommand AbortChangeOptionsCommand { get; }
        private void OnAbortChangeOptionsExecuted(object p)
        {
            GetOptions();
        }
        private bool CanAbortChangeOptionsExecute(object p) => true;
        #endregion

        #region Увеличение номера порта
        public ICommand UpPortNumCommand { get; }
        private void OnUpPortClick(object p)
        {
            OptionsApp.SmtpPort++;
        }
        private bool CanUpPortClick(object p) => true;
        #endregion

        #region Уменьшение номера порта
        public ICommand DownPortNumCommand { get; }
        private void OnDownPortClick(object p) => OptionsApp.SmtpPort--;
        private bool CanDownPortClick(object p) => true; 
        #endregion

        #endregion

        public MainWindowViewModel()
        {
            #region Создание команд
            CloseApplicationCommand = new LambdaCommand(OnCloseApplicationCommandEcecuted, CanCloseApplicationCommandExecute);
            ClearMailDataCommand = new LambdaCommand(OnClearMailDataExecuted, CanClearMailDataExecute);
            SendMailCommand = new LambdaCommand(OnSendMailExecuted, CanSendMailExecute);
            ApllyChangeOptionCommand = new LambdaCommand(OnApllyChageOptionsExecute, CanApllyChageOptionsExecute);
            AbortChangeOptionsCommand = new LambdaCommand(OnAbortChangeOptionsExecuted, CanAbortChangeOptionsExecute);
            UpPortNumCommand = new LambdaCommand(OnUpPortClick, CanUpPortClick);
            DownPortNumCommand = new LambdaCommand(OnDownPortClick, CanDownPortClick);
            #endregion

            SmtpConfig.SetConfig("smtp.mail.ru", "vasilii_pupkin_83@mail.ru", 25);
            GetOptions();
            AppErrors.OnShowErrors += AppErrors_OnShowErrors;
        }

        private void AppErrors_OnShowErrors()
        {
            ErrorWindow tmp = new ErrorWindow();
            tmp.ShowDialog();
        }

        private void GetOptions()
        {
            #region SMTP Config
            OptionsApp.SendrsMail = SmtpConfig.SendersMail;
            OptionsApp.SmtpPort = SmtpConfig.Port;
            OptionsApp.SmtpServ = SmtpConfig.SmtpServer; 
            #endregion
        }
    }
}
