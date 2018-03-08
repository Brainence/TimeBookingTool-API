using System;
using TBT.Api.Common.Filters.Base;

namespace TBT.Api.Common.FluentValidation.Attributes
{
    public class Validator: Attribute
    {
        public Validator(ValidationMode mode)
        {
            Mode = mode;
        }

        public ValidationMode Mode { get; private set; }
    }
}