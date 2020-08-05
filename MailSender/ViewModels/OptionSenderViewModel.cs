using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using MailSender.Infrastructure.Commands;
using MailSender.Works;

namespace MailSender.ViewModels
{
    internal class OptionSenderViewModel: Base.ViewModel
    {
        private OptionsViewModel optionsApp = new OptionsViewModel();

        public OptionsViewModel OptionsApp { get => optionsApp; set => Set(ref optionsApp, value); }

        #region Команда принятия изменений в настройках
        public ICommand ApllyChangeOptionCommand { get; }
        private void OnApllyChageOptionsExecute(object p)
        {
            SmtpConfig.SetConfig(OptionsApp.SmtpServ, OptionsApp.SendrsMail,OptionsApp.SendrsPass, OptionsApp.SmtpPort);
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

        private void GetOptions()
        {
            #region SMTP Config
            OptionsApp.SendrsMail = SmtpConfig.SendersMail;
            OptionsApp.SendrsPass = SmtpConfig.SendersPass;
            OptionsApp.SmtpPort = SmtpConfig.Port;
            OptionsApp.SmtpServ = SmtpConfig.SmtpServer;
            #endregion
        }

        public OptionSenderViewModel()
        {
            #region Инициализация команд
            AbortChangeOptionsCommand = new LambdaCommand(OnAbortChangeOptionsExecuted, CanAbortChangeOptionsExecute);
            ApllyChangeOptionCommand = new LambdaCommand(OnApllyChageOptionsExecute, CanApllyChageOptionsExecute);
            #endregion

            GetOptions();
        }
    }
}
