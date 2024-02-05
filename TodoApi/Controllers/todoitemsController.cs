using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TodoApi.Models;

namespace TodoApi.Controllers
{
    [Route("api/TodoItems")]
    [ApiController]
    public class todoitemsController : ControllerBase
    {
        private readonly TodoContext _context;

        public todoitemsController(TodoContext context)
        {
            _context = context;
        }

        // GET: api/todoitems
        [HttpGet]
        public async Task<ActionResult<IEnumerable<todoitem>>> Gettodoitems()
        {
            return await _context.todoitems.ToListAsync();
        }

        // GET: api/todoitems/5
        [HttpGet("{id}")]
        public async Task<ActionResult<todoitem>> Gettodoitem(long id)
        {
            var todoitem = await _context.todoitems.FindAsync(id);

            if (todoitem == null)
            {
                return NotFound();
            }

            return todoitem;
        }

        // PUT: api/todoitems/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> Puttodoitem(long id, todoitem todoitem)
        {
            if (id != todoitem.id)
            {
                return BadRequest();
            }

            _context.Entry(todoitem).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!todoitemExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/todoitems
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<todoitem>> Posttodoitem(todoitem todoitem)
        {
            _context.todoitems.Add(todoitem);
            await _context.SaveChangesAsync();

            // return CreatedAtAction("Gettodoitem", new { id = todoitem.id }, todoitem);

            return CreatedAtAction(nameof(Gettodoitem), new { id = todoitem.id}, todoitem);
        }

        // DELETE: api/todoitems/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Deletetodoitem(long id)
        {
            var todoitem = await _context.todoitems.FindAsync(id);
            if (todoitem == null)
            {
                return NotFound();
            }

            _context.todoitems.Remove(todoitem);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool todoitemExists(long id)
        {
            return _context.todoitems.Any(e => e.id == id);
        }
    }
}
