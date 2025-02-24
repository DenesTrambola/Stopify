using System.ComponentModel;

namespace Stopify.Presentation.ViewModels.Base;

abstract class ViewModelBase : INotifyPropertyChanged
{
    public event PropertyChangedEventHandler? PropertyChanged;
}
