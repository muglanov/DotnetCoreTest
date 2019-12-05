using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TestItemsRepository.Models;

namespace TestWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TestItemsController : ControllerBase
    {
        private readonly TestDbContext _context;

        public TestItemsController(TestDbContext context)
        {
            _context = context;
        }

        // GET: api/TestItems
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TestItem>>> GetTestItem()
        {
            return await _context.TestItem.ToListAsync();
        }

        // GET: api/TestItems/5
        [HttpGet("{id}")]
        public async Task<ActionResult<TestItem>> GetTestItem(int id)
        {
            var testItem = await _context.TestItem.FindAsync(id);

            if (testItem == null)
            {
                return NotFound();
            }

            return testItem;
        }

        // PUT: api/TestItems
        [HttpPut]
        public async Task<IActionResult> PutTestItem(TestItem testItem)
        {
            _context.Entry(testItem).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TestItemExists(testItem.Id))
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

        // POST: api/TestItems
        [HttpPost]
        public async Task<ActionResult<TestItem>> PostTestItem(TestItem testItem)
        {
            _context.TestItem.Add(testItem);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetTestItem), new { id = testItem.Id }, testItem);
        }

        // DELETE: api/TestItems/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<TestItem>> DeleteTestItem(int id)
        {
            var testItem = await _context.TestItem.FindAsync(id);
            if (testItem == null)
            {
                return NotFound();
            }

            _context.TestItem.Remove(testItem);
            await _context.SaveChangesAsync();

            return testItem;
        }

        private bool TestItemExists(int id)
        {
            return _context.TestItem.Any(e => e.Id == id);
        }
    }
}
