using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Driver;

namespace api.DatabaseProviders
{
    public class MongoDatabaseRepository<T> : IDatabaseRepository<T> where T : IHaveAnId<string>, new() 
    {
        //TODO: Get this from Config
        private const string ConnectionString = "mongodb://localhost:27017";

        private readonly IMongoCollection<T> _mongoCollection;

        public MongoDatabaseRepository(string collectionName)
        {
            var client = new MongoClient(ConnectionString);

            var db = client.GetDatabase("CTMImmuneSystemScores");

            _mongoCollection = db.GetCollection<T>(collectionName);

//            var credential = MongoCredential.CreateMongoCRCredential("CTMImmuneSystemScores", "myUserAdmin", "abc123");
//
//            var settings = new MongoClientSettings
//            {
//                Credentials = new[] { credential },
//                Server = new MongoServerAddress("localhost:27017")
//            };
//            
//            var mongoClient = new MongoClient(settings);
//            mongoServer = mongoClient.GetServer();
        }

        public async Task<List<T>> FindAllAsync()
        {
            try
            {
                //TODO: What happens if there are no records in the database?
                var results = await _mongoCollection.Find(new BsonDocument()).ToListAsync().ConfigureAwait(false);

                return results;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        public async Task<T> FindByUniqueElementAsync(string elementName, string elementValue)
        {
            try
            {
                var filter = new BsonDocument(elementName, elementValue);
                var result = await _mongoCollection.Find(filter).Limit(1).ToListAsync().ConfigureAwait(false);

                if (DocumentFound(result))
                    return result[0];
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }

            //No records found!
            return new T();

        }

        public async Task<T> FindByIdAsync(string id)
        {
            try
            {
                var filter = new BsonDocument("_id", new ObjectId(id));
                var result = await _mongoCollection.Find(filter).ToListAsync().ConfigureAwait(false);

                if (DocumentFound(result))
                    return result[0];
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }

            //No records found!
            return new T();
        }

        private static bool DocumentFound(List<T> result)
        {
            return result != null && result.Count >= 1;
        }

        public async Task<bool> InsertAsync(T thingToInsert)
        {
            try
            {
                await _mongoCollection.InsertOneAsync(thingToInsert);

//                if (!string.IsNullOrEmpty(thingToInsert.Id))
//                    throw new Exception("InsertAsync didn't happen");

                return true;
            }
            catch (Exception e)
            {
                //log or do something
                return false;
            }
        }

        public async Task<bool> DeleteAsync(string id)
        {
            try
            {
                var filter = new BsonDocument("_id", new ObjectId(id));
                var deleteResult = await _mongoCollection.DeleteOneAsync(filter);
                
                return deleteResult.DeletedCount >= 1;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        public async Task<bool> UpdateAsync(T thingToUpdate)
        {
            try
            {
                var filter = new BsonDocument("_id", new ObjectId(thingToUpdate.Id));
               
                var updateResult = await _mongoCollection.ReplaceOneAsync( filter, thingToUpdate);

                return updateResult.ModifiedCount > 0;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
    }
}