using SwiftResume.COMMON.Models;
using System;
using System.Collections.Generic;

namespace SwiftResume.WPF.Wrapper
{
    public class ResumeWrapper : ModelWrapper<Resume>
    {
        public ResumeWrapper(Resume model) : base (model)
        {
        }

        public int Id { get { return Model.Id; } }

        public string Nombres
        {
            get { return GetValue<string>(); }
            set
            {
                SetValue(value);
            }
        }

        public string Apellidos
        {
            get { return GetValue<string>(); }
            set
            {
                SetValue(value);
            }
        }

        protected override IEnumerable<string> ValidateProperty(string propertyName)
        {
            switch (propertyName)
            {
                case nameof(Nombres):
                    if (string.Equals(Nombres, "Gustavo", StringComparison.OrdinalIgnoreCase))
                    {
                        yield return "Gustavo es un nombre baneado momentaneamente.";
                    }
                    break;
            }
        }
    }
}
