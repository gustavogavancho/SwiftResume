using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.CompilerServices;

namespace SwiftResume.WPF.Wrapper
{
    public class ModelWrapper<T> : NotifyDataErrorInfoBase
    {
        public T Model { get; }

        public ModelWrapper(T model)
        {
            Model = model;
            Validate();
        }

        protected virtual TValue GetValue<TValue>([CallerMemberName] string propertyName = null)
        {
            return (TValue)typeof(T).GetProperty(propertyName).GetValue(Model);
        }

        protected virtual void SetValue<TValue>(TValue value, [CallerMemberName] string propertyName = null)
        {
            typeof(T).GetProperty(propertyName).SetValue(Model, value);
            OnPropertyChanged(propertyName);
            ValidatePropertyInternal(propertyName);
        }

        private void ValidatePropertyInternal(string propertyName)
        {
            ClearErrors();

            Validate();

            ValidateCustomErrors(propertyName);
        }

        private void Validate()
        {
            var results = new List<ValidationResult>();
            var context = new ValidationContext(this.Model);
            Validator.TryValidateObject(this.Model, context, results, true);

            foreach (var result in results.SelectMany(r=> r.MemberNames).Distinct())
            {
                AddError(result, results.FirstOrDefault(r => r.MemberNames.Contains(result)).ErrorMessage);
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

        protected virtual IEnumerable<string> ValidateProperty(string propertyName)
        {
            return null;
        }
    }
}
