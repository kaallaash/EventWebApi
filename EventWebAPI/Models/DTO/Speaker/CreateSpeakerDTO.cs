using System.ComponentModel.DataAnnotations;

namespace EventWebAPI.Models.DTO.Speaker
{
    public class CreateSpeakerDTO
    {
        [Required]
        [StringLength(25, MinimumLength = 2, ErrorMessage = "Invalid name length")]
        public string FirstName { get; set; }
        [Required]
        [StringLength(25, MinimumLength = 2, ErrorMessage = "Invalid last name length")]
        public string LastName { get; set; }
    }
}
