using Bookstore.Models.Models;
using Bookstore.Models.ViewModel;
using Bookstore.repository;
using BookStore.Models.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Net;

namespace myWebapi2.Controllers
{
    [Route("api/book")]
    [ApiController]
    public class BookController : ControllerBase
    {
        BookRepository _bookrepository = new BookRepository();
        [Route("list")]
        [HttpGet]
        public IActionResult GetBooks(int pageIndex = 1, int pageSize = 10, string keyword = "")
        {
            var books = _bookrepository.GetBooks(pageIndex, pageSize, keyword);
            ListResponse<BookModel1> listResponse = new ListResponse<BookModel1>()
            {
                Records = books.Records.Select(c => new BookModel1(c)),
                TotalRecords = books.TotalRecords,
            };

            return Ok(listResponse);

        }
        [Route("{id}")]
        [HttpGet]
        [ProducesResponseType(typeof(BookModel), (int)HttpStatusCode.OK)]
        public IActionResult GetBook(int id)
        {
            var book = _bookrepository.GetBook(id);
            BookModel bookModel = new BookModel(book);
            return Ok(bookModel);
        }

        [Route("add")]
        [HttpPost]
        [ProducesResponseType(typeof(BookModel), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(BadRequestObjectResult), (int)HttpStatusCode.BadRequest)]
        public IActionResult AddBook(BookModel model)
        {
            if (model == null)
            {
                return BadRequest("model is null");
            }
            Book book = new Book()
            {
                //Id = model.Id,
                Name = model.Name,
                Price = model.Price,
                Description = model.Description,
                Base64image = model.Base64image,
                Categoryid = (int)model.Categoryid,
                Publisherid = model.Publisherid,
                Quantity = model.Quantity,
            };
            var response = _bookrepository.AddBook(book);
            BookModel bookModel = new BookModel(response);

            return Ok(bookModel);
        }

        [Route("update")]
        [HttpPut]
        [ProducesResponseType(typeof(BookModel1), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(BadRequestObjectResult), (int)HttpStatusCode.BadRequest)]
        public IActionResult UpdateBook(BookModel1 model)
        {
            if (model == null)
            {
                return BadRequest("model is null");
            }
            Book book = new Book()
            {
                Id = model.Id,
                Name = model.Name,
                Price = model.Price,
                Description = model.Description,
                Base64image = model.Base64image,
                Categoryid = (int)model.Categoryid,
                Publisherid = model.Publisherid,
                Quantity = model.Quantity,
            };
            var response = _bookrepository.UpdateBook(book);
            BookModel1 bookModel = new BookModel1(response);

            return Ok(bookModel);
        }

        [Route("delete/{id}")]
        [HttpDelete]
        [ProducesResponseType(typeof(bool), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(BadRequestObjectResult), (int)HttpStatusCode.BadRequest)]
        public IActionResult DeleteBook(int id)
        {
            if (id == 0)
            {
                return BadRequest("Id is null");
            }
            var response = _bookrepository.DeleteBook(id);
            return Ok(response);
        }
    }
}
