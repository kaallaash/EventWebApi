namespace EventWebAPI.Models.DTO.Event
{
    public class EventDetailsModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string SpeakerName { get; set; }
        public DateTime Date { get; set; }
    }
}
