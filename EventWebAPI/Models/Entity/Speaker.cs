using System.ComponentModel.DataAnnotations.Schema;

namespace EventWebAPI.Models.Entity
{
    public class Speaker
    {
        public int Id { get; set; }
        public string Name { get; set; }   
        public List<Event> Events { get; set; }
    }
}
