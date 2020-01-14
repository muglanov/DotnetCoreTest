using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using TestItemsRepository.Models;

namespace TestData.Services
{
    public class TestDataService : ITestDataService
    {
        private readonly TestDbContext dbContext;
        // public TestDataService(TestDbContext context)
        public TestDataService(TestDbContext context)
        {
            dbContext = context;
        }

        public async Task<List<TestItem>> GetTestItems()
        {
            return await dbContext.TestItem.ToListAsync();
        }

        public async Task<TestItem> GetTestItem(int id)
        {
            return await dbContext.TestItem.FindAsync(id);
        }

        public async Task<TestItem> RefreshTestItem(TestItem testItem)
        {
            dbContext.Entry(testItem).State = EntityState.Modified;

            try
            {
                await dbContext.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TestItemExists(testItem.Id))
                {
                    return null;
                }
                else
                {
                    throw;
                }
            }
            return testItem;    
        }

        public async Task<int> CreateTestItem(TestItem testItem)
        {
            dbContext.TestItem.Add(testItem);
            return await dbContext.SaveChangesAsync();
        }

        public async Task<TestItem> DeleteTestItem(int id)
        {
            var testItem = await dbContext.TestItem.FindAsync(id);
            if (testItem != null)
            {
                dbContext.TestItem.Remove(testItem);
                await dbContext.SaveChangesAsync();
            }
            return testItem;
        }

        private bool TestItemExists(int id)
        {
            return dbContext.TestItem.Any(e => e.Id == id);
        }
    }
}