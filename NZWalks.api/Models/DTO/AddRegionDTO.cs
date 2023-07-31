using System.ComponentModel.DataAnnotations;

namespace NZWalks.api.Models.DTO
{
    public class AddRegionDTO
    {
        [Required]
        [MinLength(3,ErrorMessage ="Code should have minimum 3 characters")]
        [MaxLength(3, ErrorMessage = "Code should have maximum 3 characters")]
        public string Code { get; set; }
        public string Name { get; set; }
        public string? RegionImageUrl { get; set; }
    }
}
