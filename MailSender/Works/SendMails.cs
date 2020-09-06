using System;
using System.Collections.Generic;
using System.Net.Mail;
using System.Linq;
using System.Text;
using System.Threading;
using MailSender.Models;
using System.Net;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

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
        public static List<string> RecipientList { get; set; } = new List<string>();

        /// <summary>
        /// Отправляет письмо списку адрессов
        /// </summary>
        /// <param name="mail">Само письмо</param>
        /// <returns></returns>

        public async static void SendMailsAsync(StructMails mail)
        {
            List<Task> tasks = new List<Task>();
            Task allTask = null;
            count = 0;
            try
            {
                using (SmtpClient sc = new SmtpClient(SmtpConfig.SmtpServer, SmtpConfig.Port))
                {
                    sc.EnableSsl = true;
                    sc.DeliveryMethod = SmtpDeliveryMethod.Network;
                    sc.UseDefaultCredentials = false;
                    sc.Credentials = new NetworkCredential(SmtpConfig.SendersMail, SmtpConfig.SendersPass);
                    sc.Send(SmtpConfig.SendersMail, "mmgsd@mail.jp", "", "");
                }
            }
            catch (Exception e)
            {
                AppErrors.AddError($"Ошибка отправителя\n {e.Message}");
                OnSendMails?.Invoke(count);
                return;
            }
            
            try
            {
                foreach (var recipient in RecipientList)
                {
                    SendParam tmp = new SendParam(mail, recipient);
                    tasks.Add(Task.Run(() => SendOneMail(tmp)));
                }
                allTask = Task.WhenAll(tasks);
                await allTask;
            }
            catch (Exception)
            {
                foreach (var item in allTask.Exception.InnerExceptions)
                {
                    AppErrors.AddError(item.Message);
                }
            }
            finally
            {
                OnSendMails?.Invoke(count);
            }
        }

        /// <summary>
        /// Отправка письма на один адресс
        /// </summary>
        /// <exception cref="FormatException">Возникает при неверном объекте параметров</exception>
        /// <exception cref="SmtpException">Ошибка при работе с SMTP</exception>
        /// <param name="param"> Объект класса SendParam</param>
        private static void SendOneMail(object param)
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
            
            catch (Exception e)
            {
                throw new Exception($"Ошибка с почтой {recipient} \n{e.Message}");
            }
        }

        //private static void DelThread(Thread thread)
        //{
           
        //        OnSendMails?.Invoke(count);
        //}
    }
}
