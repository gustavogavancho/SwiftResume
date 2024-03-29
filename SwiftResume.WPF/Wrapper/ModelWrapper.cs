﻿using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;

namespace SwiftResume.WPF.Wrapper;

public class ModelWrapper<T> : NotifyDataErrorInfoBase
{
    public T Model { get; }

    public ModelWrapper(T model)
    {
        Model = model;
    }

    protected virtual TValue GetValue<TValue>([CallerMemberName] string propertyName = null)
    {
        return (TValue)typeof(T).GetProperty(propertyName).GetValue(Model);
    }

    protected virtual void SetValue<TValue>(TValue value, [CallerMemberName] string propertyName = null)
    {
        typeof(T).GetProperty(propertyName).SetValue(Model, value);
        OnPropertyChanged(propertyName);
        ValidatePropertyInternal(propertyName, value);
    }

    private void ValidatePropertyInternal(string propertyName, object currentValue)
    {
        ClearErrors(propertyName);

        ValidateDataAnnotations(propertyName, currentValue);

        ValidateCustomErrors(propertyName);
    }

    public void Validate()
    {
        var results = new List<ValidationResult>();
        var context = new ValidationContext(this.Model);
        Validator.TryValidateObject(this.Model, context, results, true);

        foreach (var result in results.SelectMany(r => r.MemberNames).Distinct())
        {
            AddError(result, results.FirstOrDefault(r => r.MemberNames.Contains(result)).ErrorMessage);
        }
    }

    private void ValidateDataAnnotations(string propertyName, object currentValue)
    {
        var results = new List<ValidationResult>();
        var context = new ValidationContext(Model) { MemberName = propertyName };
        Validator.TryValidateProperty(currentValue, context, results);

        foreach (var result in results)
        {
            AddError(propertyName, result.ErrorMessage);
        }
    }

    private void ValidateCustomErrors(string propertyName)
    {
        var errors = ValidateProperty(propertyName);
        if (errors != null)
        {
            foreach (var error in errors)
            {
                AddError(propertyName, error);
            }
        }
    }

    private void ValidateCustomErrors()
    {
        var errors = ValidateProperty();
        if (errors != null)
        {
            foreach (var error in errors)
            {
                AddError(error.Item1, error.Item2);
            }
        }
    }

    protected virtual IEnumerable<Tuple<string, string>> ValidateProperty()
    {
        return null;
    }

    protected virtual IEnumerable<string> ValidateProperty(string propertyName)
    {
        return null;
    }
}
