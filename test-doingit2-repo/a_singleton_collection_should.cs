using doingit2_repo;
using doingit2_repo.Models;
using MongoDB.Driver;
using NUnit.Framework;
using System.Threading.Tasks;

namespace test_doingit2_repo
{
    public class a_singleton_collection_should
    {
        private readonly string DATABASE = "doingit2";
        private readonly string SETTINGS = "settings";

        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public async Task add_a_document()
        {
            //var repo = new SingletonCollection<Settings>();

            //await repo.CreateOrUpdateAsync(new Settings());

            //Assert.IsTrue(repo.GetAll().Count() == 1);
        }
    }
}