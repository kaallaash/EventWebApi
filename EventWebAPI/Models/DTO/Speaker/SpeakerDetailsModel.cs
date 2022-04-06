namespace EventWebAPI.Models.DTO.Speaker
{
    public class SpeakerDetailsModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<int> EventId { get; set; }
    }
}
