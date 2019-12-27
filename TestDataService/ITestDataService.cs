using System.Collections.Generic;
using System.Threading.Tasks;
using TestItemsRepository.Models;

namespace TestData.Services
{
    public interface ITestDataService
    {
        Task<List<TestItem>> GetTestItems();

        Task<TestItem> GetTestItem(int id);

        Task<TestItem> RefreshTestItem(TestItem testItem);

        Task<int> CreateTestItem(TestItem testItem);

        Task<TestItem> DeleteTestItem(int id);
    }
}
