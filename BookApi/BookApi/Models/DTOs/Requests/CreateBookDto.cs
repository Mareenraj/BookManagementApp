using System.ComponentModel.DataAnnotations;

namespace BookApi.Models.DTOs.Requests;

public record CreateBookDto(
        [Required(ErrorMessage ="Title is required")]
        string Title,

        [Required(ErrorMessage = "Author name is required"), MinLength(2, ErrorMessage = "Author name length should be atleast 2 characters.")]
        string Author,

        [Required(ErrorMessage = "ISBN is required")]
        [StringLength(13, MinimumLength = 13,ErrorMessage = "ISBN must be exactly 13 characters.")]
        [RegularExpression(@"^\d{13}$",ErrorMessage = "ISBN must contain exactly 13 digits (no hyphens or letters).")]
        string ISBN,

        [Required(ErrorMessage = "Publication date is required.")]
        DateOnly PublicationDate
);

