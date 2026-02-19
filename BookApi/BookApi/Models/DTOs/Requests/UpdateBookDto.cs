using System.ComponentModel.DataAnnotations;

namespace BookApi.Models.DTOs.Requests;

public record UpdateBookDto
(
    [Required(ErrorMessage ="Title is required")]
        string Title,

    [Required(ErrorMessage = "Author name is required"), MinLength(2, ErrorMessage = "Author name length should be atleast 2 characters.")]
        string Author,

    [Required(ErrorMessage = "ISBN is required")]
        [StringLength(13, MinimumLength = 13,ErrorMessage = "ISBN number must be exactly 13 digits.")]
        [RegularExpression(@"^\d{13}$",ErrorMessage = "ISBN number must contain exactly 13 digits (no hyphens or spaces).")]
        string ISBN,

    [Required(ErrorMessage = "Publication date is required.")]
        DateOnly PublicationDate
);

