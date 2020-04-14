using doingit2_repo;
using MongoDB.Bson;
using MongoDB.Driver;
using NUnit.Framework;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace test_doingit2_repo
{
    public class a_singleton_collection_should
    {
        private static readonly string DATABASE = "doingit2";
        private readonly IMongoDatabase _mongoDb = new MongoClient().GetDatabase(DATABASE);

        [SetUp]
        public async Task Setup()
        {
            // delete any existing documents from collection
            var testDocs = _mongoDb.GetCollection<TestDocument>(typeof(TestDocument).Name);
            var docCount = testDocs.AsQueryable().Count();
            if (docCount != 0)
            {
                await testDocs.DeleteManyAsync(Builders<TestDocument>.Filter.Empty);
            }
        }

        [Test]
        public async Task add_a_document()
        {
            // arrange
            var collection = new SingletonCollection<TestDocument>(_mongoDb);

            // act
            var id = await collection.CreateAsync(new TestDocument());

            // assert
            Assert.IsTrue(!string.IsNullOrWhiteSpace(id));
            Assert.IsNotNull(await collection.ReadOrDefaultAsync(id));
        }

        [Test]
        public async Task throw_exception_if_add_two_documents()
        {
            // arrange
            var collection = new SingletonCollection<TestDocument>(_mongoDb);
            if (!collection.GetAll().Any())
            {
                await collection.CreateAsync(new TestDocument());
            }

            // act
            Assert.ThrowsAsync<InvalidOperationException>( async () => await collection.CreateAsync(new TestDocument()));

        }

        [Test]
        public async Task return_single_document_if_exists()
        {
            // arrange
            var collection = new SingletonCollection<TestDocument>(_mongoDb);
            if (!collection.GetAll().Any())
            {
                await collection.CreateAsync(new TestDocument());
            }
            var docs = collection.GetAll();

            // act
            Assert.AreEqual(1, docs.Count());
        }
    }
}