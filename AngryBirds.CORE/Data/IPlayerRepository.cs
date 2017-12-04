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
        Task<Player> AddPlayerAsync(Player player);
        Task<Player> UpdatePlayerAsync(Player playerToUpdate);
        Task<List<Round>> GetAllRoundsAsync(Guid id);
        Task<List<Round>> GetAllRoundsForMapAsync(Guid id);

        Task<Round> AddRoundAsync(Round round);

        Task<List<Map>> GetAllMapsAsync();
        Task<Map> GetMapByIdAsync(Guid id);
        Task<Map> AddMapAsync(Map map);
    }
}
