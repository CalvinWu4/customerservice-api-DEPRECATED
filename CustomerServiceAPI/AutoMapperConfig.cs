using System;
namespace CustomerServiceAPI
{
    public class AutoMapperConfig
    {
        public static void Setup()
        {
            AutoMapper.Mapper.Initialize(cfg =>
            {
                cfg.CreateMap<Entities.Ticket, Models.TicketDto>();
                cfg.CreateMap<Models.TicketForCreationDto, Entities.Ticket>();
            });
        }
    }
}
