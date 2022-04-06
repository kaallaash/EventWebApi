﻿using System.ComponentModel.DataAnnotations;

namespace EventWebAPI.Models.DTO.Speaker
{
    public class CreateSpeakerModel
    {
        [Required]
        [StringLength(50, MinimumLength = 2, ErrorMessage = "Invalid first name length")]
        public string FirstName { get; set; }
        [Required]
        [StringLength(50, MinimumLength = 2, ErrorMessage = "Invalid last name length")]
        public string LastName { get; set; }
    }
}
