namespace EventWebAPI.Models
{
    public class Event
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }   
        public int SpeakerId { get; set; }
        public Speaker? Speaker { get; set; }
        public DateTime Date { get; set; }
    }
}
