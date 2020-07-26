using System;
using MailSender.Infrastructure.Commands.Base;

namespace MailSender.Infrastructure.Commands
{
    internal class LambdaCommand : Command
    {
        private readonly Action<object> _execute;
        private readonly Func<object, bool> _canExecute;

        public LambdaCommand(Action<object> Execute, Func<object, bool> CanExecute = null)
        {
            _execute = Execute ?? throw new ArgumentException(nameof(Execute));
            _canExecute = CanExecute;
        }

        /// <summary>Логика метода </summary>
        /// <param name="parameter"></param>
        /// <returns>Да нет</returns>
        public override bool CanExecute(object parameter) => _canExecute?.Invoke(parameter) ?? true;

        /// <summary>Логика метода </summary>
        public override void Execute(object parameter) => _execute(parameter);
    }
}
