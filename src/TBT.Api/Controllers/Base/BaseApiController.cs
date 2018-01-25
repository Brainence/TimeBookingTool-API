using System.Web.Http;
using TBT.Business.Managers.Interfaces;

namespace TBT.Api.Controllers.Base
{
    [Authorize]
    public abstract class BaseApiController : ApiController
    {
        #region Fields

        #endregion

        #region Properties

        protected IManagerStore ManagerStore { get; private set; }

        #endregion

        #region Constructors

        public BaseApiController(
            IManagerStore managerStore)
        {
            ManagerStore = managerStore;
        }

        #endregion

        #region Methods

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (ManagerStore != null)
                {
                    ManagerStore.Dispose();
                    ManagerStore = null;
                }
            }

            base.Dispose(disposing);
        }

        #endregion
    }
}