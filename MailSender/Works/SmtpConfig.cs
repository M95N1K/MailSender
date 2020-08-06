using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MailSender.Models;

namespace MailSender.Works
{
    internal static class SmtpConfig
    {
        private static SmtpServerOptions smtpConf;

        public static SmtpServerOptions GetConfig { get => smtpConf; }

        /// <summary> Возвращает SMTP сервер из настроек</summary>
        public static string SmtpServer { get => smtpConf.smtpServer; }
        /// <summary> Возвращает почтовый ящик отправителя из настроек</summary>
        public static string SendersMail { get => smtpConf.sendersMail; }
        /// <summary> Возвращает пароль почтового ящика отправителя из настроек</summary>
        public static string SendersPass { get => smtpConf.sendersPass; }
        /// <summary> Возвращает порт SMTP сервера из настроек</summary>
        public static int Port { get => smtpConf.smtpPort; }

        /// <summary>Устанавливает опции отправителя</summary>
        /// <param name="smtpServer">SMTP сервер отправителя</param>
        /// <param name="sendersMail">Почтовый ящик отправителя</param>
        /// <param name="port">Порт SMTP сервера</param>
        static public void SetConfig(string smtpServer,string sendersMail, string senderPass, int port)
        {
            smtpConf.smtpServer = smtpServer;
            smtpConf.sendersPass = senderPass;
            smtpConf.sendersMail = sendersMail;
            smtpConf.smtpPort = port;
        }
        
    }
}
