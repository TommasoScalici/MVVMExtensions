# MVVM Extensions
MVVM power extensions for .NET with support for async commands, MVVM-friendly Task and UI notification.

There are some helper classes for MVVM pattern:
- ObservableObject and ObservableTask (wrapper of Task but more MVVM-friendly).
- Standard RelayCommand and RelayCommand\<T\>.
- AsyncCommand, AsyncCommand\<TResult\> and AsyncCommand\<TParameter, TResult\> for asynchronous Commands that take Tasks as execute action.

And also some useful converter for Windows Universal Apps / RT components:
- BitmapImageConverter
- BooleanNegationConverter
- BooleanToOpacityConverter
- BooleanToVisibilityConverter
- ColorToBrushConverter
- DateTimeToLocalizedStringConverter
- NullToBooleanConverter
- ResourceStringConverter

## License

MVVM Extensions is released under [MIT License](https://github.com/TommasoScalici/MVVMExtensions/blob/master/LICENSE.md).
