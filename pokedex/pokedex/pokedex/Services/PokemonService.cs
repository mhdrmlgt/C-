using System.Collections.Generic;
using System.Linq;
using MongoDB.Driver;
using pokedex.Models;

namespace pokedex.Services
{
    public class PokemonService : IPokemonService
    {
        private readonly IMongoCollection<Pokemon> _pokemonCollection;
        public PokemonService(IConfiguration configuration)
        {
            var client = new MongoClient(configuration["MongoDbSettings:ConnectionString"]);
            var database = client.GetDatabase(configuration["MongoDbSettings:DatabaseName"]);
            _pokemonCollection = database.GetCollection<Pokemon>(configuration["MongoDbSettings:CollectionName"]);
        }

        
        public async Task<List<Pokemon>> GetAllAsync()
        {
            return await _pokemonCollection.Find(_ => true).ToListAsync();
        }

        public async Task<Pokemon> GetByIdAsync(int id)
        {
            return await _pokemonCollection.Find(p => p.Id == id).FirstOrDefaultAsync();
        }
       

        public async Task<Pokemon> AddAsync(Pokemon newPokemon)
        {
            await _pokemonCollection.InsertOneAsync(newPokemon);
            return newPokemon;
        }

        public async Task<Pokemon> UpdateAsync(int id, Pokemon updatedPokemon)
        {
            await _pokemonCollection.ReplaceOneAsync(p => p.Id == id, updatedPokemon);
            return updatedPokemon;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var result = await _pokemonCollection.DeleteOneAsync(p => p.Id == id);
            return result.DeletedCount > 0;
        }
    }
}
