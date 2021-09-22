using BookAPI.Models;
using BookAPI.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        private readonly IBookRepository _bookRepository;

        public BooksController(IBookRepository bookRepository)
        {
            _bookRepository = bookRepository;
        }

        [HttpGet]
        public async Task<IEnumerable<Book>> GetBooks()
        {
            return await _bookRepository.Get();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Book>> GetBooks(int id)
        {
            return await _bookRepository.Get(id);
        }

        [HttpPost]
        public async Task<ActionResult<Book>>PostBooks([FromBody] Book book)
        {
            int startIndex = 1;
            String value = book.Author;

            String substring = value.Substring(startIndex);

            if (substring.Trim().EndsWith("]"))
            {
                int endTagStartPosition = substring.LastIndexOf("]");
                if (endTagStartPosition >= 0)
                    substring = substring.Substring(0, endTagStartPosition);
            }

            int startIndex2 = 1;
            String value2 = book.Title;
            String substring2 = value2.Substring(startIndex2);

            if (substring2.Trim().EndsWith("]"))
            {
                int endTagStartPosition = substring2.LastIndexOf("]");
                if (endTagStartPosition >= 0)
                    substring2 = substring2.Substring(0, endTagStartPosition);
            }

            String value3 = book.Description;

            String substring3 = value3.Substring(startIndex);

            if (substring3.Trim().EndsWith("]"))
            {
                int endTagStartPosition = substring3.LastIndexOf("]");
                if (endTagStartPosition >= 0)
                    substring3 = substring3.Substring(0, endTagStartPosition);
            }

            book.Author = substring;
            book.Title = substring2;
            book.Description = substring3;
            var newBook = await _bookRepository.Create(book);
            return CreatedAtAction(nameof(GetBooks), new { id = newBook.Id }, newBook);
        }
    }
}
