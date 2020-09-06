using MailSender.Models.DBModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MailSender.Infrastructure.Interfaces
{
    public interface IEmailData
    {
        ObservableCollection<Email> EmailDate { get; }

        int AddRecipient(Email value);
    }
}
