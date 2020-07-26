using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using MailSender.Infrastructure.Commands;
using MailSender.ViewModels.Base;

namespace MailSender.ViewModels
{
    /// <summary>Класс взаимодействия с окном </summary>
    class MainWindowViewModel : ViewModel
    {
        private string _title = "Title Main Window";

        public string Title { get => _title; set => Set(ref _title, value); }

        #region Команды

        #region Команда закрытия (Command={Binding CloseApplicationCommand})
        public ICommand CloseApplicationCommand { get; }

        private void OnCloseApplicationCommandEcecuted(object p)
        {
            Application.Current.Shutdown();
        }

        private bool CanCloseApplicationCommandExecute(object p) => true;
        #endregion

        #endregion

        public MainWindowViewModel()
        {
            #region Создание команд
            CloseApplicationCommand = new LambdaCommand(OnCloseApplicationCommandEcecuted, CanCloseApplicationCommandExecute);
            #endregion
        }
    }
}
