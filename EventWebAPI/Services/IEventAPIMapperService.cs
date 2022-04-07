using AutoMapper;

namespace EventWebAPI.Services
{
    public interface IEventAPIMapperService
    {
        Mapper GetEventToEventDetailsModelMapper();
        Mapper GetSpeakerToSpeakerDetailsModelMapper();
        Mapper GetSpeakerToSpeakerDTOMapper();
        Mapper GetCreateSpeakerModelToSpeakerMapper();
        Mapper GetCreateEventModelToEventMapper();
        Mapper GetCreateEventModelToSpeakerMapper();
        Mapper GetSpeakerDTOToSpeakerMapper();
        Mapper GetEventToUpdateEventModelMapper();
        Mapper GetUpdateEventModelToEventMapper();
    }
}
