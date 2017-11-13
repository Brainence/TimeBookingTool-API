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

        public IModelBaseValidator GetValidator(ValidationMode mode, Type model)
        {
            return ServiceLocator.Current.Get<IModelBaseValidator>(GetType().GetProperties().FirstOrDefault(x => x.PropertyType.BaseType.GetGenericArguments()[0] == model)?.Name, new { mode = mode });
            //return (IModelBaseValidator)(GetType().GetProperties().FirstOrDefault(x => x.PropertyType.BaseType.GetGenericArguments()[0] == model)?.GetValue(this));
        }


        public ActivityValidator ActivityValidator
        {
            get { return _activityValidator ?? (_activityValidator = (ActivityValidator)ServiceLocator.Current.Get<ModelBaseValidator<ActivityModel>>()); }
        }

        public CustomerValidator CustomerValidator
        {
            get { return _customerValidator ?? (_customerValidator = (CustomerValidator)ServiceLocator.Current.Get<ModelBaseValidator<CustomerModel>>()); }
        }

        public ProjectValidator ProjectValidator
        {
            get { return _projectValidator ?? (_projectValidator = (ProjectValidator)ServiceLocator.Current.Get<ModelBaseValidator<ProjectModel>>()); }
        }

        public ResetTicketValidator ResetTicketValidator
        {
            get { return _resetTicketValidator ?? (_resetTicketValidator = (ResetTicketValidator)ServiceLocator.Current.Get<ModelBaseValidator<ResetTicketModel>>()); }
        }

        public TimeEntryValidator TimeEntryValidator
        {
            get { return _timeEntryValidator ?? (_timeEntryValidator = (TimeEntryValidator)ServiceLocator.Current.Get<ModelBaseValidator<TimeEntryModel>>()); }
        }

        public UserValidator UserValidator
        {
            get { return _userValidator ?? (_userValidator = (UserValidator)ServiceLocator.Current.Get<ModelBaseValidator<UserModel>>()); }
        }
    }
}