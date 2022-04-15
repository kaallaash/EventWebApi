using AutoMapper;

namespace EventWebAPI.Services
{
    public interface IEventAPIMapperService
    {
        Mapper GetEventToEventDetailsDTOMapper();
        Mapper GetSpeakerToSpeakerDetailsDTOMapper();
        Mapper GetSpeakerToSpeakerDTOMapper();
        Mapper GetCreateSpeakerDTOToSpeakerMapper();
        Mapper GetCreateEventDTOToEventMapper();
        Mapper GetCreateEventDTOToSpeakerMapper();
        Mapper GetSpeakerDTOToSpeakerMapper();
        Mapper GetEventToUpdateEventDTOMapper();
        Mapper GetUpdateEventDTOToEventMapper();
    }
}
