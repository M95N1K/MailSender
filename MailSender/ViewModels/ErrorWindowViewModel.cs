using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using MailSender;
using MailSender.Infrastructure.Commands;
using MailSender.Works;

namespace MailSender.ViewModels
{
    class ErrorWindowViewModel : ViewModels.Base.ViewModel
    {
        private List<string> errors;
        

        public List<string> Errors { get => errors; set => Set(ref errors, value); }

        #region Команды

        #region Закрытие окна
        public ICommand CloseWindowCommand { get; }
        private void OnCloseWindowExecuted(object p)
        {
            WindowCollection tmp = new WindowCollection();
            tmp = Application.Current.Windows;
            AppErrors.ClearErrors();
            foreach (Window item in tmp)
            {
                if (item.IsActive)
                    item.Close();
            }
        }
        private bool CanCloseWindowExecute(object p) => true;  
        #endregion

        #endregion

        public ErrorWindowViewModel()
        {
            #region Создание команд
            CloseWindowCommand = new LambdaCommand(OnCloseWindowExecuted, CanCloseWindowExecute);
            #endregion

            Errors = AppErrors.GetErrors();
        }
    }
}
