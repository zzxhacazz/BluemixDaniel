using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using MyWebApi.Models;
using System.Linq;

namespace TodoApi.Controllers
{
    [Route("api/[controller]/[action]")]
    public class TodoController : Controller
    {
        private readonly MyWebApiContext _context;

        
        public TodoController(MyWebApiContext context)
        {
            _context = context;

            if (_context.Users.Count() == 0)
            {
                _context.Users.Add(new User { Name = "User1" });
                _context.SaveChanges();
            }
        } 
         

        [HttpGet]
        public  IActionResult GetAll2()
        {
            return new ObjectResult(new { value = "todo el rea", text = "hasta el fondo x2" });
            // return _context.TodoItems.ToList();
        }

        [HttpGet]
        public  IActionResult GetAll()
        {
            return new ObjectResult(new { value = "todo el rea", text = "hasta el fondo" });
            // return _context.TodoItems.ToList();
        }
        
        [HttpGet("{id}", Name = "GetTodo")]
        public IActionResult GetById(long id)
        {
            var item = _context.Users.FirstOrDefault(t => t.Id == id);
            if (item == null)
            {
                return NotFound();
            }
            return new ObjectResult(item);
        }     

          
       [HttpPost]
        public IActionResult Create([FromBody] User item)
        {
            if (item == null)
            {
                return BadRequest();
            }

            _context.Users.Add(item);
            _context.SaveChanges();

            return CreatedAtRoute("GetTodo", new { id = item.Id }, item);
        }


    }
}