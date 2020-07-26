using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MailSender.ViewModels
{
    class OptionsViewModel : Base.ViewModel
    {
        private string smtpServ;
        private string sendrsMail;
        private int smtpPort;

        public string SmtpServ { get => smtpServ; set => Set(ref smtpServ, value); }
        public string SendrsMail { get => sendrsMail; set => Set(ref sendrsMail, value); }
        public int SmtpPort { get => smtpPort; set => Set(ref smtpPort, value); }
    }
}
