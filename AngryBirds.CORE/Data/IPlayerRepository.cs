using AngryBirds.CORE.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AngryBirds.CORE.Data
{
    public interface IPlayerRepository
    {
        Task<Player> Get(Guid id);
        Task<Player> GetByName(string name);
        Task<List<Player>> GetAll();
        Task<Player> AddPlayer(Player player);

        Task<List<Round>> GetAllRounds(Guid id);
    }
}
