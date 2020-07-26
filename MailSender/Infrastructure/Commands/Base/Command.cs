using System;
using System.Windows.Input;

namespace MailSender.Infrastructure.Commands.Base
{
    internal abstract class Command : ICommand
    {
        public event EventHandler CanExecuteChanged
        {
            add => CommandManager.RequerySuggested += value;
            remove => CommandManager.RequerySuggested -= value;
        }

        /// <summary>Можно или нет выполнить команду</summary>
        /// <param name="parameter"></param>
        /// <returns>Да нет</returns>
        public abstract bool CanExecute(object parameter);

        /// <summary>
        /// Логика команды
        /// </summary>
        /// <param name="parameter"></param>
        public abstract void Execute(object parameter);

    }
}
