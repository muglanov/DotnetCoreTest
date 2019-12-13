using System.Collections.Generic;
using TestItemsRepository.Models;

namespace TestItemsRepository.Service
{
    public class TestDbService : ITestDbService
    {
        private readonly TestDbContext dbContext;
        public TestDbService(TestDbContext context)
        {
            dbContext = context;
        }

        public IEnumerable<TestItem> GetTestItems()
        {
            return dbContext.TestItem;
        }

        public TestItem GetTestItem(int id)
        {
            return dbContext.TestItem.FindAsync(id);
        }

        public void RefreshTestItem(TestItem testItem)
        {

        }

        public void CreateTestItem(TestItem testItem)
        {

        }

        public TestItem DeleteTestItem(int id)
        {

        }
    }
}
