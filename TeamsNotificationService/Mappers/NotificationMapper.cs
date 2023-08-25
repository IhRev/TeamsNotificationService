using AutoMapper;
using TeamsNotificationService.DTOs;
using TeamsNotificationService.Models;

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