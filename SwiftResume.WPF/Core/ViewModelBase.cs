﻿namespace SwiftResume.WPF.Core;

public delegate TViewModel CreateViewModel<out TViewModel>() where TViewModel : ViewModelBase;

public class ViewModelBase : INotifyPropertyChanged
{
    public virtual void Dispose() { }

    public event PropertyChangedEventHandler PropertyChanged;

    protected void OnPropertyChanged(string propertyName)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
