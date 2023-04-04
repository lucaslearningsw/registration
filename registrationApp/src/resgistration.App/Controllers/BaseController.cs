using Microsoft.AspNetCore.Mvc;
using registration.Business.Interfaces;

namespace resgistration.App.Controllers
{
    public abstract class BaseController : Controller
    {
        private readonly INotificator _notificator;

        protected BaseController(INotificator notificator)
        {
            _notificator = notificator;
        }

        protected bool OperationValid()
        {
            return !_notificator.HasNotification();
        }
    }
}
