using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TBT.Api.Common.FluentValidation.Store.Interfaces;
using TBT.Api.Common.FluentValidation.Validators;
using TBT.Api.Common.FluentValidation.Interfaces;
using TBT.Api.Common.Filters.Base;
using TBT.Business.Interfaces;
using FluentValidation;
using TBT.Business.Infrastructure.CastleWindsor;
using TBT.Business.Models.BusinessModels;
using TBT.Api.Common.FluentValidation.Base;
using System.Reflection;

namespace TBT.Api.Common.FluentValidation.Store.Implementations
{
    public class ValidatorStore : IValidatorStore
    {
        #region Fields

        private IEnumerable<Type> _availbleTypes;

        #endregion

        #region Constructor

        public ValidatorStore()
        {
            _availbleTypes = typeof(ValidatorStore).Assembly.GetTypes().Where(x => x.GetInterface(nameof(IModelValidatorBase)) != null);
        }

        #endregion

        #region Interface members

        public IModelValidatorBase GetValidator(ValidationMode mode, Type model)
        {
            return ServiceLocator.Current.Get<IModelValidatorBase>(_availbleTypes.FirstOrDefault(x => x.BaseType.GetGenericArguments()[0] == model)?.Name, new { mode = mode });
        }

        #endregion
    }
}