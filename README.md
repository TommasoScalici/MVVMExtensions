# MVVM Extensions
MVVM extensions for .NET Core with support for async commands, MVVM-friendly Task and UI notification.

This library is a useful collection of API designed for *.NET Core* for an extended and easier implementation of the MVVM design pattern.
Essentially there are various implementations of the *ICommand* and INotifyPropertyChanged* interfaces, serving different purposes.

Here's a brief list of the API you can find:
- *ObservableObject*: Extend from this class to have an object that supports notifications without worrying to implement INotifyPropertyChanged.
- *ObservableTask*: A wrapper of Task that supports UI notifications.
- *RelayCommand* and *RelayCommand\<T\>*: Standard implementations (non-generic and generic) of the ICommand interface.
- *AsyncCommand*, *AsyncCommand\<TResult\>* and *AsyncCommand\<TParameter, TResult\>*: Commands that execute asynchronous operations or Tasks.



## License

MVVM Extensions is released under [MIT License](https://github.com/TommasoScalici/MVVMExtensions/blob/master/LICENSE.md).
