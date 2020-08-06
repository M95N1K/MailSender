using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace MailSender.ViewModels.Base
{
    /// <summary>
    /// Базовый класс ViewModel. Для создания классов работающих с окном
    /// </summary>
    internal abstract class ViewModel : INotifyPropertyChanged
    {
        /// <summary> </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Вызывается при изменении поля
        /// </summary>
        /// <param name="PropertyName">Имя свойства которое вызывает метод (подставляется автоматически)</param>
        protected virtual void OnPropertyChenged([CallerMemberName] string PropertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(PropertyName));
        }
        /// <summary>
        /// Возвращает истину при смене значения в поле field на value
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="field">Поле в котором должно быть изменение</param>
        /// <param name="value">Новое значение поля</param>
        /// <param name="PropertyName">Имя свойства которое вызывает метод (подставляется автоматически)</param>
        /// <returns></returns>
        protected virtual bool Set<T>(ref T field, T value, [CallerMemberName] string PropertyName = null)
        {
            if (Equals(field, value)) return false;
            field = value;
            OnPropertyChenged(PropertyName);
            return true;
        }
    }
}
