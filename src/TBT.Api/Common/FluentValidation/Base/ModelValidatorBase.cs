using FluentValidation;
using TBT.Business.Interfaces;
using TBT.Api.Common.Filters.Base;
using System.Threading.Tasks;
using TBT.Api.Common.FluentValidation.Interfaces;
using FluentValidation.Results;

namespace TBT.Api.Common.FluentValidation.Base
{
    public abstract class ModelValidatorBase<T> :AbstractValidator<T>, IModelValidatorBase where T : class, IModel
    {
        protected readonly ValidationMode _mode;
        protected ICrudManager<T> _manager;

        protected ModelValidatorBase(ICrudManager<T> manager, ValidationMode mode)
        {
            _mode = mode;
            _manager = manager;
            CascadeMode = CascadeMode.StopOnFirstFailure;
            RuleFor(x => x.Id).GreaterThan(0).When(x => HasFlag(ValidationMode.None & ~(ValidationMode.Add | ValidationMode.DataRelevance)))
                .WithMessage("{PropertyName} can't be less or equal 0.");
            RuleFor(x => x.Id).MustAsync(async (id, token) => await ExistsAsync(id, _manager))
                .When(x => HasFlag(ValidationMode.Exist | ValidationMode.Delete | ValidationMode.Update))
                .WithMessage("{PropertyValue}th item isn't exists.");
        }

        public ValidationMode Mode => _mode;

        protected bool HasFlag(ValidationMode mode)
        {
            return mode.HasFlag(_mode);
        }

        protected Task<bool> ExistsAsync<TModel>(int id, ICrudManager<TModel> manager) where TModel : class, IModel
        {
            return manager.ExistAsync(id);
        }

        public Task<ValidationResult> ValidateAsync(object value)
        {
            return Task.FromResult(Validate((T)value));
        }
    }
}