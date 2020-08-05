using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Code;
using System.Threading.Tasks;
using MailSender.Works;

namespace MailSender.ViewModels
{
    class OptionsViewModel : Base.ViewModel
    {
        private string smtpServ;
        private string sendrsMail;
        private string sendersPass;
        private int smtpPort;

        public string SmtpServ { 
            get { 
                return smtpServ;
            } 
            set => Set(ref smtpServ, value); }
        public string SendrsMail {
            get 
            {
                return sendrsMail; 
            } 
            set => Set(ref sendrsMail, value); }
        public string SendrsPass {
            get
            {
                return sendersPass;
            }
            set => Set(ref sendersPass, value); }
        public int SmtpPort {
            get
            {
                return smtpPort;
            }
            set => Set(ref smtpPort, value); }
    }
}
