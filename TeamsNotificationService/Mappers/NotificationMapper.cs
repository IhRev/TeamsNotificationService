using AutoMapper;
using TeamsNotificationService.Core;
using TeamsNotificationService.DTOs;

namespace TeamsNotificationService.Mappers
{
	public class NotificationMapper : Profile
	{
		public NotificationMapper()
		{
			CreateMap<NotificationDTO, Notification>();
		}
	}
}