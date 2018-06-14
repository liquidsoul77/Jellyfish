﻿using System;
using System.Windows.Input;

namespace Jellyfish
{
    /// <inheritdoc />
    /// <summary>
    ///     An <see cref="T:System.Windows.Input.ICommand" /> implementation
    /// </summary>
    public class RelayCommand : ICommand
    {
        private readonly Func<object, bool> _canExecute;
        private readonly Action<object> _execute;

        /// <summary>
        ///     Initialize a new instance of the <see cref="RelayCommand{T}" />, a <see cref="ICommand" /> implementation,
        ///     where <see cref="CanExecute" /> is always true
        /// </summary>
        /// <param name="execute">The callback to execute on command execution</param>
        public RelayCommand(Action<object> execute)
        {
            _execute = execute ?? throw new ArgumentNullException(nameof(execute));
            _canExecute = arg => true;
        }

        /// <summary>
        ///     Initialize a new instance of the <see cref="RelayCommand{T}" />, an <see cref="ICommand" /> implementation
        /// </summary>
        /// <param name="execute">The callback to execute on command execution</param>
        /// <param name="canExecute">The callback to check if this command can execute</param>
        public RelayCommand(Action<object> execute, Func<object, bool> canExecute)
        {
            _execute = execute ?? throw new ArgumentNullException(nameof(execute));
            _canExecute = canExecute ?? throw new ArgumentNullException(nameof(canExecute));
        }

        public event EventHandler CanExecuteChanged;

        /// <inheritdoc />
        /// <summary>
        ///     Check if this command can execute with the given parameter
        /// </summary>
        /// <param name="o">The parameter object</param>
        /// <returns>If this command can execute with this parameter</returns>
        public bool CanExecute(object o) => _canExecute?.Invoke(o) ?? false;

        /// <inheritdoc />
        /// <summary>
        ///     Execute the command with the given parameter
        /// </summary>
        /// <param name="o">The parameter object</param>
        public void Execute(object o) => _execute?.Invoke(o);

        /// <summary>
        ///     Raise the <see cref="CanExecuteChanged" /> event
        /// </summary>
        public void RaiseCanExecuteChanged() => CanExecuteChanged?.Invoke(this, new EventArgs());
    }
}