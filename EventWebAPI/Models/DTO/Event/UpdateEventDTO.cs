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
        [StringLength(50, MinimumLength = 5, ErrorMessage = "Invalid name length")]
        public string SpeakerName { get; set; }
        [Required]
        public DateTime Date { get; set; }

        public bool Equals(UpdateEventDTO updateEventDTO)
        {
            return Id == updateEventDTO.Id
                && Title == updateEventDTO.Title
                && Description == updateEventDTO.Description
                && SpeakerId == updateEventDTO.SpeakerId
                && SpeakerName == updateEventDTO.SpeakerName;
        }
    }
}
