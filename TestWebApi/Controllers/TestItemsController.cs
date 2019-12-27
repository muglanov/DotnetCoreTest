using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TestItemsRepository.Models;
using TestData.Services;

namespace TestWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TestItemsController : ControllerBase
    {
        private readonly ITestDataService _dataService;

        public TestItemsController(ITestDataService dataService)
        {
            _dataService = dataService;
        }

        // GET: api/TestItems
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TestItem>>> GetTestItem()
        {
            return await _dataService.GetTestItems();
        }

        // GET: api/TestItems/5
        [HttpGet("{id}")]
        public async Task<ActionResult<TestItem>> GetTestItem(int id)
        {
            return await _dataService.GetTestItem(id);
        }

        // PUT: api/TestItems
        [HttpPut]
        public async Task<IActionResult> PutTestItem(TestItem testItem)
        {
            TestItem changedTestItem = null;
            try
            {
                changedTestItem = await _dataService.RefreshTestItem(testItem);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (changedTestItem is null)
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
            await _dataService.CreateTestItem(testItem);
            return CreatedAtAction(nameof(GetTestItem), new { id = testItem.Id }, testItem);
        }

        // DELETE: api/TestItems/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<TestItem>> DeleteTestItem(int id)
        {
            var testItem = await _dataService.DeleteTestItem(id);
            if (testItem == null)
            {
                return NotFound();
            }

            return testItem;
        }   
    }
}
