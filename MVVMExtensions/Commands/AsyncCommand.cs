﻿using System;
using System.Threading;
using System.Threading.Tasks;

using TommasoScalici.MVVMExtensions.Notifications;

namespace TommasoScalici.MVVMExtensions.Commands
{
    public class AsyncCommand : AsyncCommandBase, IAsyncCommand
    {
        readonly Func<CancellationToken, Task> execute;
        readonly Func<object, bool> canExecute;


        /// <summary>
        /// Raised after the command has executed.
        /// </summary>
        public event EventHandler<AsyncCommandEventArgs> Executed;


        public AsyncCommand(Func<Task> execute, Func<bool> canExecute = null)
        {
            this.execute = token => execute?.Invoke();
            this.canExecute = canExecute == null ? new Func<object, bool>(x => true) : x => canExecute();
        }

        public AsyncCommand(Func<Task> execute, Func<object, bool> canExecute)
        {
            this.execute = token => execute?.Invoke();
            this.canExecute = canExecute;
        }

        public AsyncCommand(Func<CancellationToken, Task> execute, Func<bool> canExecute = null)
        {
            this.execute = execute;
            this.canExecute = canExecute == null ? new Func<object, bool>(x => true) : x => canExecute();
        }

        public AsyncCommand(Func<CancellationToken, Task> execute, Func<object, bool> canExecute)
        {
            this.execute = execute;
            this.canExecute = canExecute;
        }


        public ObservableTask Execution { get; protected set; }


        public override bool CanExecute(object parameter = null) => canExecute?.Invoke(parameter) ?? false;

        public async override void Execute(object parameter, bool ignoreCanExecute = false)
        {
            if (ignoreCanExecute || CanExecute() || CanExecute(parameter))
            {
                await ExecuteAsync(parameter);
                OnExecuted(new AsyncCommandEventArgs(Execution, parameter));
            }
        }

        protected override async Task ExecuteAsync(object parameter = null)
        {
            IsExecuting = true;
            Execution = new ObservableTask(execute?.Invoke(CancellationToken));
            RaiseCanExecuteChanged();

            await Execution.TaskObserver;
            IsExecuting = false;
            RaiseCanExecuteChanged();

            await base.ExecuteAsync(parameter);
        }

        protected override void OnExecuted(EventArgs e)
        {
            Executed?.Invoke(this, e as AsyncCommandEventArgs);
            base.OnExecuted(e);
        }
    }


    public class AsyncCommand<TResult> : AsyncCommandBase, IAsyncCommand<TResult>
    {
        readonly Func<CancellationToken, Task<TResult>> execute;
        readonly Func<object, bool> canExecute;


        /// <summary>
        /// Raised after the command has executed.
        /// </summary>
        public event EventHandler<AsyncCommandEventArgs<TResult>> Executed;


        public AsyncCommand(Func<Task<TResult>> execute, Func<bool> canExecute = null)
        {
            this.execute = token => execute?.Invoke();
            this.canExecute = canExecute == null ? new Func<object, bool>(x => true) : x => canExecute();
        }

        public AsyncCommand(Func<Task<TResult>> execute, Func<object, bool> canExecute)
        {
            this.execute = token => execute?.Invoke();
            this.canExecute = canExecute;
        }

        public AsyncCommand(Func<CancellationToken, Task<TResult>> execute, Func<bool> canExecute = null)
        {
            this.execute = execute;
            this.canExecute = canExecute == null ? new Func<object, bool>(x => true) : x => canExecute();
        }

        public AsyncCommand(Func<CancellationToken, Task<TResult>> execute, Func<object, bool> canExecute)
        {
            this.execute = execute;
            this.canExecute = canExecute;
        }


        public ObservableTask<TResult> Execution { get; protected set; }


        public override bool CanExecute(object parameter = null) => canExecute?.Invoke(parameter) ?? false;

        public async override void Execute(object parameter, bool ignoreCanExecute = false)
        {
            if (ignoreCanExecute || CanExecute() || CanExecute(parameter))
            {
                await ExecuteAsync(parameter);
                OnExecuted(new AsyncCommandEventArgs<TResult>(Execution, parameter));
            }
        }

        protected override async Task ExecuteAsync(object parameter = null)
        {
            IsExecuting = true;
            Execution = new ObservableTask<TResult>(execute?.Invoke(CancellationToken));
            RaiseCanExecuteChanged();

            await Execution.TaskObserver;
            IsExecuting = false;
            RaiseCanExecuteChanged();

            await base.ExecuteAsync(parameter);
        }

        protected override void OnExecuted(EventArgs e)
        {
            Executed?.Invoke(this, e as AsyncCommandEventArgs<TResult>);
            base.OnExecuted(e);
        }
    }


    public class AsyncCommand<TParameter, TResult> : AsyncCommandBase, IAsyncCommand<TParameter, TResult>
    {
        readonly Func<CancellationToken, TParameter, Task<TResult>> execute;
        readonly Func<object, bool> canExecute;


        public event EventHandler<AsyncCommandEventArgs<TParameter, TResult>> Executed;


        public AsyncCommand(Func<TParameter, Task<TResult>> execute, Func<bool> canExecute = null)
        {
            this.execute = (token, parameter) => execute?.Invoke(parameter);
            this.canExecute = canExecute == null ? new Func<object, bool>(x => true) : x => canExecute();
        }

        public AsyncCommand(Func<TParameter, Task<TResult>> execute, Func<object, bool> canExecute)
        {
            this.execute = (token, parameter) => execute?.Invoke(parameter);
            this.canExecute = canExecute;
        }

        public AsyncCommand(Func<CancellationToken, TParameter, Task<TResult>> execute, Func<bool> canExecute = null)
        {
            this.execute = execute;
            this.canExecute = canExecute == null ? new Func<object, bool>(x => true) : x => canExecute();
        }

        public AsyncCommand(Func<CancellationToken, TParameter, Task<TResult>> execute, Func<object, bool> canExecute)
        {
            this.execute = execute;
            this.canExecute = canExecute;
        }


        public ObservableTask<TResult> Execution { get; protected set; }


        public override bool CanExecute(object parameter = null) => canExecute?.Invoke(parameter) ?? false;

        public async override void Execute(object parameter, bool ignoreCanExecute = false)
        {
            if (ignoreCanExecute || CanExecute() || CanExecute(parameter))
            {
                await ExecuteAsync(parameter);
                OnExecuted(new AsyncCommandEventArgs<TParameter, TResult>(Execution, (TParameter)parameter));
            }
        }

        protected override async Task ExecuteAsync(object parameter = null)
        {
            IsExecuting = true;
            Execution = new ObservableTask<TResult>(execute?.Invoke(CancellationToken, (TParameter)parameter));
            RaiseCanExecuteChanged();

            await Execution.TaskObserver;
            IsExecuting = false;
            RaiseCanExecuteChanged();

            await base.ExecuteAsync(parameter);
        }

        protected override void OnExecuted(EventArgs e)
        {
            Executed?.Invoke(this, e as AsyncCommandEventArgs<TParameter, TResult>);
            base.OnExecuted(e);
        }
    }
}
