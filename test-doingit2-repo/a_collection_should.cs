using doingit2_repo;
using doingit2_repo.Models;
using MongoDB.Driver;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace test_doingit2_repo
{
    public class a_collection_should
    {
        private readonly string DATABASE = "doingit2";
        private readonly string SETTINGS = "settings";

        private IMongoDatabase _mongoDb;

        [SetUp]
        public void Setup()
        {
            var mongoClient = new MongoClient();
            _mongoDb = mongoClient.GetDatabase(DATABASE); // will create if not exists
        }

        [Test]
        public async Task add_a_document()
        {
            // arrange
            var collection = new Collection<Board>(_mongoDb);

            // act
            var id = await collection.CreateAsync(new Board());

            // assert
            Assert.IsTrue(!string.IsNullOrWhiteSpace(id));
            Assert.IsNotNull(await collection.ReadOrDefaultAsync(id));
        }

        [Test]
        public async Task read_a_document_that_exists()
        {
            // arrange
            var collection = new Collection<Board>(_mongoDb);
            var anyDoc = collection.GetAll().FirstOrDefault();
            Assert.IsNotNull(anyDoc);
            var id = anyDoc.Id;

            // act
            var doc = await collection.ReadOrDefaultAsync(id);

            // assert
            Assert.IsNotNull(doc);
            Assert.AreEqual(anyDoc.Id, doc.Id);
        }

        [Test]
        public async Task return_null_when_no_doc()
        {
            // arrange
            var collection = new Collection<Board>(_mongoDb);

            // act
            byte[] buffer = new byte[12];
            new Random().NextBytes(buffer);
            var randomId = string.Concat(buffer.Select(x => x.ToString("X2")).ToArray());
            var doc = await collection.ReadOrDefaultAsync(randomId);

            // assert
            Assert.IsNull(doc);
        }

        [Test]
        public async Task update_an_existing_document()
        {
            // arrange
            var collection = new Collection<Board>(_mongoDb);
            var board = new Board();
            var id = await collection.CreateAsync(board);

            // act
            board.Monday = new[] { "Empty dishwasher" };
            await collection.UpdateAsync(board);

            // assert
            board = await collection.ReadOrDefaultAsync(board.Id);
            Assert.AreEqual("Empty dishwasher", board.Monday[0]);
        }

        [Test]
        public async Task throw_exception_if_update_non_existent_document()
        {
            // arrange
            var collection = new Collection<Board>(_mongoDb);
            var board = new Board();

            // act
            Assert.ThrowsAsync<KeyNotFoundException>(async () => await collection.UpdateAsync(board));
        }
    }
}