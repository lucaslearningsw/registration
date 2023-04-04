using Microsoft.AspNetCore.Mvc;
using registration.Business.Interfaces;

namespace resgistration.App.Extensions
{
    public class SummaryViewComponent : ViewComponent
    {
        private readonly INotificator _notificator;
        public SummaryViewComponent(INotificator notificator)
        {
           _notificator = notificator;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var notifications = await Task.FromResult(_notificator.GetNotifications());

            notifications.ForEach(notification => ViewData.ModelState.AddModelError(string.Empty, notification.Mensagem));

            return View();
        }
    }
}
