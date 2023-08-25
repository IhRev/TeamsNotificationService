using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using TeamsNotificationService.Core;
using TeamsNotificationService.DTOs;
using TeamsNotificationService.Framework;

namespace TeamsNotificationService.Controllers
{
    [ApiController]
    [Route("notifications")]
    public class NotificationController : ControllerBase
    {
        private readonly INotificator notificator;
        private readonly IMapper mapper;

        public NotificationController(INotificator notificator, IMapper mapper)
		{
            this.notificator = notificator;
            this.mapper = mapper;
        }

        [HttpPost(Name = "send_notificaction")]
		public async Task<ActionResult> SendNotificaction([FromBody] NotificationDTO notificationData)
		{
            try
            {
               await notificator.SendNotification(mapper.Map<Notification>(notificationData));
			   return Ok();

            }
            catch (Exception ex)
            {
                return BadRequest();
            }
		}
	}
}