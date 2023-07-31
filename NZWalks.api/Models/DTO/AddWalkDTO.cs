using System.ComponentModel.DataAnnotations;

namespace NZWalks.api.Models.DTO
{
    public class AddWalkDTO
    {
        [Required]
        [MaxLength(100)]
        public string Name { get; set; }
        [Required]
        [MaxLength(100)]
        public string Description { get; set; }
        [Required]
        [Range(0, 50)]
        public double LengthInKm { get; set; }
        [Required]
        public string? WalkimageURL { get; set; }
        [Required]
        public Guid DifficultyId { get; set; }
        [Required]
        public Guid RegionId { get; set; } 
    }
}
