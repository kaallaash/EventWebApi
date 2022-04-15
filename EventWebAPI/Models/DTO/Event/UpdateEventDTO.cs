using System.ComponentModel.DataAnnotations;

namespace EventWebAPI.Models.DTO.Event
{
    public class UpdateEventDTO
    {
        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "Id must be greater than 0")]
        public int Id { get; set; }
        [Required]
        [StringLength(20, MinimumLength = 3, ErrorMessage = "Invalid title length")]
        public string Title { get; set; }
        [Required]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Invalid description length")]
        public string Description { get; set; }
        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "SpeakerId must be greater than 0")]
        public int SpeakerId { get; set; }
        [Required]
        [RegularExpression(@".{2,25}[ ].{2,25}", ErrorMessage = "The full name must consist of two words from 2 to 25 characters")]
        public string SpeakerFullName { get; set; }
        [Required]
        public DateTime Date { get; set; }

        public bool Equals(UpdateEventDTO updateEventDTO)
        {
            return Id == updateEventDTO.Id
                && Title == updateEventDTO.Title
                && Description == updateEventDTO.Description
                && SpeakerId == updateEventDTO.SpeakerId
                && SpeakerFullName == updateEventDTO.SpeakerFullName;
        }
    }
}
