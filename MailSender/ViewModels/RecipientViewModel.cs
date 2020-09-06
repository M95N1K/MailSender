using System;
using System.Collections.Generic;
using MailSender.Models.DBModels;
using System.Linq;
using static MailSender.Works.UnionClass;
using System.Threading.Tasks;
using System.Windows.Input;
using MailSender.Infrastructure.Commands;
using MailSender.Infrastructure.Interfaces;
using MailSender.Works;
using System.Data.Linq;
using System.Windows.Data;
using System.ComponentModel;
using System.Collections.ObjectModel;

namespace MailSender.ViewModels
{
    internal class RecipientViewModel: Base.ViewModel, IMergeableClass
    {
        private string filterName;
        private ObservableCollection<Email> dataEmails;
        private readonly IEmailData emailData;
        private CollectionViewSource _emailsView;
        private Email newEmail = new Email() { Value = "", Name = "" };

        public ICollectionView DataEmails => _emailsView?.View;
        public ObservableCollection<Email> Emails
        {
            get => dataEmails;
            set
            {
                if (!Set(ref dataEmails, value)) return;
                _emailsView = new CollectionViewSource { Source = value };
                _emailsView.Filter += EmailsView_Filter;
                OnPropertyChenged(nameof(DataEmails));
            }
                
        }
        public string Name { get => "RecipientVM"; set { } }
        private void EmailsView_Filter(object sender, FilterEventArgs e)
        {
            if (!(e.Item is Email email) || string.IsNullOrWhiteSpace(filterName)) return;
            if (!email.Name.Contains(filterName))
                e.Accepted = false;
        }
        public Email NewEmail { get => newEmail; set => Set(ref newEmail, value); }
       
        public string FilterName { get => filterName;
            set 
            {
                if (!Set(ref filterName, value)) return;
                DataEmails.Refresh();
            } 
        }

        #region Команды

        #region Добавление получателя
        public ICommand AddRecipientCommand { get; }
        

        private void OnAddRecipientExecuted(object p)
        {
            emailData.AddRecipient(newEmail);
            newEmail.Name = "";
            newEmail.Value = "";
            GetEmilesToRecipient();
        }
        private bool CanAddRecipientExecute(object p)
        {
            if (newEmail.Value != "" && newEmail.Name != "")
                return true;
            return false;
        }
        #endregion

        #endregion

        private void GetEmilesToRecipient()
        {
            //SendMails.RecipientList = emailData.EmailDate.Select(n => n.Value).ToList<string>();
        }

        

        public RecipientViewModel()
        {
            #region Создание команд
            AddRecipientCommand = new LambdaCommand(OnAddRecipientExecuted, CanAddRecipientExecute);
            #endregion
            emailData = new EmailDateBase();
            GetEmilesToRecipient();
            Emails = emailData.EmailDate;
            AddMergClass(this);
        }

    }
}
