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
        public delegate void OnSend(int CountSendMail);
        public static event OnSend OnSendMails;

        public static List<string> RecipientList { get; set; } = new List<string>();

        /// <summary>
        /// Отправляет письмо списку адрессов
        /// </summary>
        /// <param name="mail">Само письмо</param>
        /// <returns></returns>
        public static int SendsMail(StructMails mail)
        {
            int count = 0;

            foreach (string recipient in RecipientList)
            {
                try
                {
                    if (SendOneMail(mail, recipient))
                        count++;
                }
                catch (SmtpException)
                {
                    break;
                }
                
            }
            OnSendMails?.Invoke(count);
            return count;
        }

        /// <summary>
        /// Отправка письма на один адресс
        /// </summary>
        /// <param name="mail">Структура письм</param>
        /// <param name="recipient">Получатель</param>
        /// <exception cref="SmtpException"></exception>
        /// <returns>Возвращает true если письмо отправленно</returns>
        private static bool SendOneMail(StructMails mail, string recipient)
        {
            bool result = false;
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
                        sc.Credentials = new NetworkCredential(SmtpConfig.SendersMail, SmtpConfig.SendersPass);
                        sc.Send(mm);
                    }
                }
                result = true;
            }
            catch (SmtpException e)
            {
                AppErrors.AddError("Ошибка работы с SMTP сервером");
                AppErrors.AddError(e.Message);
                throw;
            }
            catch (Exception e)
            {
                AppErrors.AddError($"Ошибка при отправке письма по адрессу \"{recipient}\"");
                AppErrors.AddError(e.Message);
            }
            return result;
        }
    }
}
