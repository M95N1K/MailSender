using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MailSender.Infrastructure.Interfaces;
using MailSender.Models.DBModels;

namespace MailSender.Works
{
    class EmailDateBase : IEmailData,IMergeableClass
    {
        private EmilesDataContext emiles;
        private int maxId;

        public ObservableCollection<Email> EmailDate => new ObservableCollection<Email>(emiles.Email);

        public string Name { get => "EmailDate"; set { } }

        public EmailDateBase()
        {
            emiles = new EmilesDataContext();
            maxId = emiles.Email.Max(n => n.Id);
            UnionClass.AddMergClass(this);
        }

        public int AddRecipient(Email value)
        {
            int result = -1;

            Email newEmail = new Email();
            newEmail.Name = value.Name;
            newEmail.Value = value.Value;
            newEmail.Id = maxId + 1;

            try
            {
                emiles.Email.InsertOnSubmit(newEmail);
                emiles.SubmitChanges();
                maxId++;
                result = maxId;
            }
            catch (Exception e)
            {
                AppErrors.AddError($"Ошибка добавления получателя ({value.Value} - {value.Name})\n {e.Message}");
            }

            AppErrors.ShowErrors();

            return result;
        }
    }
}
