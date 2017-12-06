using AngryBirds.CORE.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AngryBirds.CORE.Data
{
    public interface IPlayerRepository
    {
        Task<Player> GetByIdAsync(Guid id);
        Task<Player> GetByNameAsync(string name);
        Task<List<Player>> GetAllPlayersAsync();
        Task<bool> CheckIfPlayerNameExists(string name);
        Task<Player> AddPlayerAsync(Player player);
        Task<Player> UpdatePlayerAsync(Player playerToUpdate);
        Task<List<Round>> GetAllRoundsForPlayerAsync(Guid id);
        Task<List<Round>> GetAllRoundsForMapAsync(Guid id);

        Task<Round> GetRoundById(Guid id);
        Task<List<Round>> GetAllRoundsAsync();
        Task<Round> AddRoundAsync(Round round);

        Task<List<Map>> GetAllMapsAsync();
        Task<Map> GetMapByIdAsync(Guid id);
        Task<Map> AddMapAsync(Map map);
        Task<Map> UpdateMapAsync(Map map);
    }
}
