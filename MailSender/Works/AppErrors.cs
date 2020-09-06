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

        public delegate void ShowsErrors();
        /// <summary>Событие для вывода ошибок</summary>
        public static event ShowsErrors OnShowErrors;

        /// <summary>Возвращает количество ошибок</summary>
        public static int Count { get => listErrors.Count; }

        /// <summary>Добавляет ошибку в список</summary>
        /// <param name="errors">Текст ошибки</param>
        public static void AddError(string errors)
        {
            listErrors.Add(errors);
        }

        /// <summary>Возвращает список ошибок</summary>
        public static List<string> GetErrors()
        {
            return listErrors;
        }
        /// <summary>Очищает список ошибок</summary>
        public static void ClearErrors()
        {
            listErrors.Clear();
        }

        /// <summary>Запускает событие OnShowErrors для вывода ошибок</summary>
        public static void ShowErrors()
        {
            if (Count > 0)
                OnShowErrors?.Invoke();
        }
    }
}
