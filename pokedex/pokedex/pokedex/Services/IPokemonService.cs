using System.Collections.Generic;
using pokedex.Models;

namespace pokedex.Services
{
    public interface IPokemonService
    {
        Task<List<Pokemon>> GetAllAsync();
        Task<Pokemon> GetByIdAsync(int id);
     
        Task<Pokemon> AddAsync(Pokemon newPokemon);
        Task<Pokemon> UpdateAsync(int id, Pokemon updatedPokemon);
        Task<bool> DeleteAsync(int id);
    }
}
