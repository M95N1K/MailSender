using System;
using System.Collections.Generic;
using System.Linq;
using MailSender.Works;
using MailSender.Models;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using MailSender.Infrastructure.Commands;

namespace MailSender.ViewModels
{
    internal class MailVIewModel : Base.ViewModel
    {
        private string mailHeader = "Заголовок";
        private string mailBody = "Текст";
        

        public string MailHeader { get => mailHeader; set => Set(ref mailHeader, value); }
        public string MailBody { get => mailBody; set => Set(ref mailBody, value); }

        #region Команды
        #region SendMailCommand Команда отправки письма
        public ICommand SendMailCommand { get; }
        private void OnSendMailExecuted(object p)
        {
            StructMails mails;
            mails.body = MailBody;
            mails.header = MailHeader;
            SendMails.SendMailsAsync(mails);
            //AppErrors.ShowErrors();
        }
        private bool CanSendMailExecute(object p)
        {
            return (!MailHeader.Equals("") && MailBody != "" && SendMails.RecipientList.Count > 0);
        }
        #endregion
        #region ClearMailDataCommand Команда очистки полей письма
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
        #endregion

        public MailVIewModel()
        {
            ClearMailDataCommand = new LambdaCommand(OnClearMailDataExecuted, CanClearMailDataExecute);
            SendMailCommand = new LambdaCommand(OnSendMailExecuted, CanSendMailExecute);
        }
    }
}
