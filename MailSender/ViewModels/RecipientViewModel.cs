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

        private int maxID;
        private string filterName;
        private ObservableCollection<Email> dataEmails;
        private CollectionViewSource _emailsView;
        private readonly EmilesDataContext db = new EmilesDataContext();
        private Email email = new Email() { Value = "", Name = "" };
        private string _name= "RecipientVM";

        public ICollectionView DataEmails => _emailsView?.View;
        public ObservableCollection<Email> Emails{get =>dataEmails;
            set
            {
                if (!Set(ref dataEmails, value)) return;
                _emailsView = new CollectionViewSource { Source = value };
                _emailsView.Filter += EmailsView_Filter;
                OnPropertyChenged(nameof(DataEmails));
            }
                
                }
        public string Name { get => _name; set => _name=value; }
        private void EmailsView_Filter(object sender, FilterEventArgs e)
        {
            if (!(e.Item is Email email) || string.IsNullOrWhiteSpace(filterName)) return;
            if (!email.Name.Contains(filterName))
                e.Accepted = false;
        }
        public Email NewEmail { get => email; set => Set(ref email, value); }
        public Table<Email> Db { get => db.Email; }
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

            Emails = GetEmails();
            return result;
        }

        private void GetEmilesToRecipient()
        {
            SendMails.RecipientList = db.Email.Select(n => n.Value).ToList<string>();
        }

        private ObservableCollection<Email> GetEmails()
        {
            ObservableCollection<Email> result = new ObservableCollection<Email>();

            foreach (var item in db.Email)
            {
                result.Add(item);
            }

            return result;
        }

        public RecipientViewModel()
        {
            #region Создание команд
            AddRecipientCommand = new LambdaCommand(OnAddRecipientExecuted, CanAddRecipientExecute);
            #endregion
            maxID = db.Email.Max(n => n.Id);
            Emails = GetEmails();
            GetEmilesToRecipient();
            AddMergClass(this);
        }

    }
}
