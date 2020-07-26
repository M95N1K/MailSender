using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MailSender.Models
{
    public struct SmtpServerOptions
    {
        public string smtpServer;
        public string sendersMail;
        public int smtpPort;
    }
}
