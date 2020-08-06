using System;
using System.Collections.Generic;
using MailSender.Models.DBModels;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using MailSender.Infrastructure.Commands;
using MailSender.Works;
using System.Data.Linq;

namespace MailSender.ViewModels
{
    internal class RecipientViewModel: Base.ViewModel
    {

        private int maxID;
        //private List<string> recipientList = new List<string>();
        private IEnumerable<Email> dataEmails;
        private readonly EmilesDataContext db = new EmilesDataContext();
        private Email email = new Email() { Value = "", Name = "" };

        //public List<string> RecipientList { get => recipientList; set => Set(ref recipientList, value); }
        public IEnumerable<Email> DataEmails{get =>dataEmails; set => Set(ref dataEmails, value);}
        public Email NewEmail { get => email; set => Set(ref email, value); }
        public Table<Email> Db { get => db.Email; }

        #region Команды

        #region Добавление получателя
        public ICommand AddRecipientCommand { get; }
        private void OnAddRecipientExecuted(object p)
        {
            AddRecipient();
            email.Name = "";
            email.Value = "";
            GetEmilesToRecipient();
        }
        private bool CanAddRecipientExecute(object p)
        {
            if (email.Value != "" && email.Name != "")
                return true;
            return false;
        }
        #endregion

        #endregion
        private int AddRecipient()
        {
            int result = -1;
            Email e = new Email();
            e.Value = email.Value;
            e.Name = email.Name;
            e.Id = ++maxID;
            result = e.Id;
            try
            {
                Db.InsertOnSubmit(e);
                db.SubmitChanges();
            }
            catch
            {
                AppErrors.AddError($"Ошибка добавления получателя ({email.Value} - {email.Name})");
            }

            if (AppErrors.Count > 0)
                AppErrors.ShowErrors();

            DataEmails = from c in db.Email select c;
            return result;
        }

        private void GetEmilesToRecipient()
        {
            //RecipientList = db.Email.Select(n => n.Value).ToList<string>();
            SendMails.RecipientList = db.Email.Select(n => n.Value).ToList<string>();
        }

        public RecipientViewModel()
        {
            #region Создание команд
            AddRecipientCommand = new LambdaCommand(OnAddRecipientExecuted, CanAddRecipientExecute);
            #endregion
            maxID = db.Email.Max(n => n.Id);
            DataEmails = from c in db.Email select c;
            GetEmilesToRecipient();
        }

    }
}
