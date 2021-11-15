using System.Collections.ObjectModel;

namespace SwiftResume.WPF.Extensions;

public static class IEnumerableToObservableCollection
{
    public static ObservableCollection<T> ToObservableCollection<T>(this IEnumerable<T> _LinqResult)
    {
        return new ObservableCollection<T>(_LinqResult);
    }
}