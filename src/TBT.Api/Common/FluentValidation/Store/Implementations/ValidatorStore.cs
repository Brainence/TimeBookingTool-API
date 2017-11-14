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

        private ActivityValidator _activityValidator;
        private CustomerValidator _customerValidator;
        private ProjectValidator _projectValidator;
        private ResetTicketValidator _resetTicketValidator;
        private TimeEntryValidator _timeEntryValidator;
        private UserValidator _userValidator;

        #endregion

        public IModelValidatorBase GetValidator(ValidationMode mode, Type model)
        {
            return ServiceLocator.Current.Get<IModelValidatorBase>(GetType().GetProperties().FirstOrDefault(x => x.PropertyType.BaseType.GetGenericArguments()[0] == model)?.Name, new { mode = mode });
        }


        public ActivityValidator ActivityValidator
        {
            get { return _activityValidator ?? (_activityValidator = (ActivityValidator)ServiceLocator.Current.Get<ModelValidatorBase<ActivityModel>>()); }
        }

        public CustomerValidator CustomerValidator
        {
            get { return _customerValidator ?? (_customerValidator = (CustomerValidator)ServiceLocator.Current.Get<ModelValidatorBase<CustomerModel>>()); }
        }

        public ProjectValidator ProjectValidator
        {
            get { return _projectValidator ?? (_projectValidator = (ProjectValidator)ServiceLocator.Current.Get<ModelValidatorBase<ProjectModel>>()); }
        }

        public ResetTicketValidator ResetTicketValidator
        {
            get { return _resetTicketValidator ?? (_resetTicketValidator = (ResetTicketValidator)ServiceLocator.Current.Get<ModelValidatorBase<ResetTicketModel>>()); }
        }

        public TimeEntryValidator TimeEntryValidator
        {
            get { return _timeEntryValidator ?? (_timeEntryValidator = (TimeEntryValidator)ServiceLocator.Current.Get<ModelValidatorBase<TimeEntryModel>>()); }
        }

        public UserValidator UserValidator
        {
            get { return _userValidator ?? (_userValidator = (UserValidator)ServiceLocator.Current.Get<ModelValidatorBase<UserModel>>()); }
        }
    }
}