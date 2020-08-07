using System;
using System.Collections.Generic;
using System.Net.Mail;
using System.Linq;
using System.Text;
using System.Threading;
using MailSender.Models;
using System.Net;

namespace MailSender.Works
{
    public class SendParam
    {
        public StructMails mail;
        public string recipient;

        public SendParam() { }

        public SendParam(StructMails mail, string recipient)
        {
            this.mail = mail;
            this.recipient = recipient;
        }
    }

    public static class SendMails
    {
        public delegate void OnSend(int CountSendMail);
        public static event OnSend OnSendMails;

        private static volatile int count;
        private static List<Thread> listThread;
        public static List<string> RecipientList { get; set; } = new List<string>();

        /// <summary>
        /// Отправляет письмо списку адрессов
        /// </summary>
        /// <param name="mail">Само письмо</param>
        /// <returns></returns>
        public static int SendsMail(StructMails mail)
        {
            count = 0;
            listThread = new List<Thread>();
            foreach (string recipient in RecipientList)
            {
                try
                {
                    SendParam tmp = new SendParam(mail, recipient);
                    Thread thread = new Thread(new ParameterizedThreadStart(SendOneMail));
                    thread.Start(tmp);
                    listThread.Add(thread);
                }
                catch (SmtpException)
                {
                    break;
                }
                catch (FormatException)
                {
                    break;
                }

            }
            #region Ожидаем окончания всех дочерних потоков
            bool flag = true;
            while (flag)
            {
                flag = false;
                foreach (var item in listThread)
                {
                    if (item.IsAlive)
                        flag = true;
                }
            } 
            #endregion

            OnSendMails?.Invoke(count);
            return count;
        }

        /// <summary>
        /// Отправка письма на один адресс
        /// </summary>
        /// <exception cref="FormatException">Возникает при неверном объекте параметров</exception>
        /// <exception cref="SmtpException">Ошибка при работе с SMTP</exception>
        /// <param name="param"> Объект класса SendParam</param>
        public static void SendOneMail(object param)
        {
            if (!(param is SendParam))
                throw new FormatException("Неверный входной параметр");
            string recipient = (param as SendParam).recipient;
            StructMails mail = (param as SendParam).mail;
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
                count++;
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
        }
    }
}
