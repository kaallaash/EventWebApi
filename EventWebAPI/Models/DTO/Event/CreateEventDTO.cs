using EventWebAPI.Validators;
using System.ComponentModel.DataAnnotations;

namespace EventWebAPI.Models.DTO.Event
{
    public class CreateEventDTO
    {
        [Required]
        [StringLength(20, MinimumLength = 3, ErrorMessage = "Invalid title length")]
        public string Title { get; set; }
        [Required]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Invalid description length")]
        public string Description { get; set; }
        [Required]
        [MinimumValue(1, ErrorMessage = "SpeakerId must be greater than 0")]
        public int SpeakerId { get; set; }
        [Required]
        [StringLength(50, MinimumLength = 5, ErrorMessage = "Invalid name length")]
        public string SpeakerName { get; set; }
        [Required]
        public DateTime Date { get; set; }
    }
}
