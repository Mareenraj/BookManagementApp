namespace BookApi.Models.DTOs.Responses;

public record BookResponseDto
(
    int Id,
    string Title,
    string Author,
    string ISBN,
    DateOnly PublicationDate
);




