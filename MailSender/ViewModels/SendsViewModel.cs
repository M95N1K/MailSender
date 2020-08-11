using MailSender.Infrastructure.Commands;
using MailSender.Infrastructure.Interfaces;
using System.Windows.Input;
using static MailSender.Works.UnionClass;

namespace MailSender.ViewModels
{
    class SendsViewModel : Base.ViewModel, IMergeableClass
    {
        #region Поля

        private string _name = "PlaneVM";

        #endregion

        #region Свойства

        public string Name { get => _name; set => _name = value; }

        #endregion

        #region Команды


        #region Команда срочной отправки
        public ICommand SendPlaneCommand { get; }
        private void OnSendPlaneCommandExecuted(object p) { }
        private bool CanSentPlaneCommandExecute(object p)
        {
            if (!MergClasses.ContainsKey("RecipientVM")) return false;
            RecipientViewModel tmp = (RecipientViewModel)MergClasses["RecipientVM"];
            if (tmp.Emails.Count > 0) return true;
            else return false;
        }
        #endregion

        #endregion

        public SendsViewModel()
        {
            #region Создание комманд

            SendPlaneCommand = new LambdaCommand(OnSendPlaneCommandExecuted, CanSentPlaneCommandExecute);

            #endregion

            AddMergClass(this);
        }

    }
}
