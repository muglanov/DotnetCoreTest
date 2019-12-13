using System.Collections.Generic;
using TestItemsRepository.Models;

namespace TestItemsRepository.Service
{
    public interface ITestDbService
    {
        IEnumerable<TestItem> GetTestItems();

        TestItem GetTestItem(int id);

        void RefreshTestItem(TestItem testItem);

        void CreateTestItem(TestItem testItem);

        TestItem DeleteTestItem(int id);
    }
}
