using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MailSender.Models;

namespace MailSender.Works
{
    static class SmtpConfig
    {
        private static SmtpServerOptions smtpConf;

        /// <summary> Возвращает SMTP сервер из настроек</summary>
        public static string SmtpServer { get => smtpConf.smtpServer; }
        /// <summary> Возвращает почтовый ящик отправителя из настроек</summary>
        public static string SendersMail { get => smtpConf.sendersMail; }
        /// <summary> Возвращает порт SMTP сервера из настроек</summary>
        public static int Port { get => smtpConf.smtpPort; }

        /// <summary>Устанавливает опции отправителя</summary>
        /// <param name="smtpServer">SMTP сервер отправителя</param>
        /// <param name="sendersMail">Почтовый ящик отправителя</param>
        /// <param name="port">Порт SMTP сервера</param>
        static public void SetConfig(string smtpServer,string sendersMail, int port)
        {
            smtpConf.smtpServer = smtpServer;
            smtpConf.sendersMail = sendersMail;
            smtpConf.smtpPort = port;
        }
        //static public SmtpServerOptions GetConfig()
        //{
        //    return smtpConf;
        //}
    }
}
