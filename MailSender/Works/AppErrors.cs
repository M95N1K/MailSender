using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MailSender.Works
{
    static class AppErrors
    {
        private static List<string> listErrors = new List<string>();

        public static int Count { get => listErrors.Count; }

        public static void AddError(string errors)
        {
            listErrors.Add(errors);
        }

        public static List<string> GetErrors()
        {
            return listErrors;
        }
        public static void ClearErrors()
        {
            listErrors.Clear();
        }

        public static void ShowErrors()
        {
            ErrorWindow tmp = new ErrorWindow();
            tmp.ShowDialog();
        }
    }
}
