using System.ComponentModel.DataAnnotations.Schema;

namespace EventWebAPI.Models
{
    public class Speaker
    {
        public int Id { get; set; }
        public string Name { get; set; }
        [NotMapped]
        public List<int> EventsId { get; set; }
    }
}
