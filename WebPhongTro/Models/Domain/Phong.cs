using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebPhongTro.Models.Domain
{
    public class Phong
    {
        public int Id { get; set; }


        [Required]

        public string Title { get; set; } // Tên phòng

        public string RoomImage { get; set; } // Ảnh phòng

        [Required]
        public decimal Price { get; set; } // Giá phòng

        [Required]
        public string Size { get; set; } // Diện tích

        [Required]
        public string? Amenities { get; set; } // Tiện nghi

        [NotMapped]
        public IFormFile? ImageFile { get; set; }
        [NotMapped]
        [Required]
        public List<string>? AmenitiesList { get; set; }

        [NotMapped]
        public IEnumerable<SelectListItem>? AmenityOptions { get; set; }

        [NotMapped]
        public string? SelectedAmenities { get; set; }

        [NotMapped]
        public MultiSelectList? AmenitySelectList { get; set; }

        [NotMapped]
        [Required]
        public List<int>? Genres { get; set; }
        [NotMapped]
        public IEnumerable<SelectListItem>? GenreList { get; set; }
        [NotMapped]
        public string? GenreNames { get; set; }

        [NotMapped]
        public MultiSelectList? MultiGenreList
        {
            get; set;
        }
    }
}

