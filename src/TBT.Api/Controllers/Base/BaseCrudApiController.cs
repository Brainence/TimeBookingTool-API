using TBT.Business.Interfaces;
using TBT.Business.Managers.Interfaces;

namespace TBT.Api.Controllers.Base
{
    public abstract class BaseApiController<TModel> : BaseApiController
    where TModel : class, IModel
    {
        #region Fields

        private ICrudManager<TModel> _manager;

        #endregion

        #region Properties

        protected ICrudManager<TModel> Manager => _manager;

        #endregion

        #region Constructors

        public BaseApiController(
            IManagerStore managerStore,
            ICrudManager<TModel> manager)
            : base(managerStore)
        {
            _manager = manager;
        }

        #endregion

        #region Methods

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (_manager != null)
                {
                    _manager.Dispose();
                    _manager = null;
                }
            }

            base.Dispose(disposing);
        }

        #endregion
    }
}