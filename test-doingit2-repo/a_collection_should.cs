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
        private static readonly string DATABASE = "doingit2";
        private readonly IMongoDatabase _mongoDb = new MongoClient().GetDatabase(DATABASE);

        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public async Task add_a_document()
        {
            // arrange
            var collection = new Collection<TestDocument>(_mongoDb);

            // act
            var id = await collection.CreateAsync(new TestDocument());

            // assert
            Assert.IsTrue(!string.IsNullOrWhiteSpace(id));
            Assert.IsNotNull(await collection.ReadOrDefaultAsync(id));
        }

        [Test]
        public async Task read_a_document_that_exists()
        {
            // arrange
            var collection = new Collection<TestDocument>(_mongoDb);
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
            var collection = new Collection<TestDocument>(_mongoDb);
            byte[] buffer = new byte[12];
            new Random().NextBytes(buffer);
            var randomId = string.Concat(buffer.Select(x => x.ToString("X2")).ToArray());

            // act
            var doc = await collection.ReadOrDefaultAsync(randomId);

            // assert
            Assert.IsNull(doc);
        }

        [Test]
        public async Task update_an_existing_document()
        {
            // arrange
            var collection = new Collection<TestDocument>(_mongoDb);
            var testDoc = new TestDocument();
            var id = await collection.CreateAsync(testDoc);

            // act
            testDoc.Secret = "ssh";
            await collection.UpdateAsync(testDoc);

            // assert
            testDoc = await collection.ReadOrDefaultAsync(testDoc.Id);
            Assert.AreEqual("ssh", testDoc.Secret);
        }

        [Test]
        public async Task throw_exception_if_update_non_existent_document()
        {
            // arrange
            var collection = new Collection<TestDocument>(_mongoDb);
            var testDoc = new TestDocument();

            // act
            Assert.ThrowsAsync<KeyNotFoundException>(async () => await collection.UpdateAsync(testDoc));
        }
    }
}