using BookApi.Models.DTOs.Requests;
using BookApi.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace BookApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class BookController : ControllerBase
{
    private readonly IBookService _service;

    public BookController(IBookService service)
    {
        _service = service;
    }

    [HttpGet]
    public IActionResult GetAll() => Ok(_service.GetAllBooks());


    [HttpGet("{id:int}")]
    public IActionResult GetById(int id)
    {
        var book = _service.GetBookById(id);
        return book is null ? NotFound() : Ok(book);
    }


    [HttpPost]
    public IActionResult Create([FromBody] CreateBookDto dto)
    {
        var (ok, error, created) = _service.AddBook(dto);
        if (!ok) return BadRequest(new { message = error });

        return CreatedAtAction(nameof(GetById), new { id = created!.Id }, created);
    }


    [HttpPut("{id:int}")]
    public IActionResult Update(int id, [FromBody] UpdateBookDto dto)
    {
        var (ok, error) = _service.UpdateBook(id, dto);

        if (!ok && error == "Book not found.") return NotFound(new { message = error });
        if (!ok) return BadRequest(new { message = error });

        return NoContent();
    }


    [HttpDelete("{id:int}")]
    public IActionResult Delete(int id) => _service.DeleteBook(id) ? NoContent() : NotFound();
}

