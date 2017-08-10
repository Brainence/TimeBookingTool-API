using System.Web.Http;
using TBT.Business.Managers.Interfaces;

namespace TBT.Api.Controllers.Base
{
    [Authorize]
    public abstract class BaseApiController : ApiController
    {
        #region Fields

        private IManagerStore _managerStore;

        #endregion

        #region Properties

        protected IManagerStore ManagerStore
        {
            get { return _managerStore; }
        }

        #endregion

        #region Constructors

        public BaseApiController(
            IManagerStore managerStore)
        {
            _managerStore = managerStore;
        }

        #endregion

        #region Methods

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (_managerStore != null)
                {
                    _managerStore.Dispose();
                    _managerStore = null;
                }
            }

            base.Dispose(disposing);
        }

        #endregion
    }
}