using System;
using System.Collections.Generic;
using System.Net.Mail;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MailSender.Models;
using System.Net;

namespace MailSender.Works
{
    public static class SendMails
    {
        /// <summary>
        /// Отправляет письмо списку адрессов
        /// </summary>
        /// <param name="recipientsMail">Список адрессов</param>
        /// <param name="mail">Само письмо</param>
        /// <param name="pass">Пароль почтового ящика для отправки писем</param>
        /// <returns></returns>
        public static int SendsMail(List<string> recipientsMail, StructMails mail, string pass)
        {
            int count = 0;

            foreach (string recipient in recipientsMail)
            {
                try
                {
                    using (MailMessage mm = new MailMessage(SmtpConfig.SendersMail, recipient))
                    {
                        mm.Subject = mail.header;
                        mm.Body = mail.body;
                        mm.IsBodyHtml = false;
                        using (SmtpClient sc = new SmtpClient(SmtpConfig.SmtpServer, SmtpConfig.Port))
                        {
                            sc.EnableSsl = true;
                            sc.DeliveryMethod = SmtpDeliveryMethod.Network;
                            sc.UseDefaultCredentials = false;
                            sc.Credentials = new NetworkCredential(SmtpConfig.SendersMail, pass);
                            sc.Send(mm);
                        }
                    }
                    count++;
                }
                catch
                {
                    AppErrors.AddError($"Ошибка при отправке письма по адрессу \"{recipient}\"");
                }
            }
            return count;
        }
    }
}
