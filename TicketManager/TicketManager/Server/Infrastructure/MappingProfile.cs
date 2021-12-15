namespace TicketManager.Server.Infrastructure
{
    using AutoMapper;
    using TicketManager.Server.Models;
    using TicketManager.Shared.ViewModels;

    public class MappingProfile : Profile
    {

        public MappingProfile()
        {
            CreateMap<Ticket, TicketListItem>();

            CreateMap<Ticket, TicketDetailsModel>();

            CreateMap<Comment, TicketCommentModel>();
        }
    }
}
