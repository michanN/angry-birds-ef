using AngryBirds.CORE.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AngryBirds.CORE.Models;
using System.Threading.Tasks;
using AngryBirds.DATA.Mappings;
using Microsoft.EntityFrameworkCore;

namespace AngryBirds.DATA.Repositories
{
    public class PlayerRepository : IPlayerRepository
    {
        private readonly AngryBirdContext _context;

        public PlayerRepository(AngryBirdContext context)
        {
            _context = context;
        }

        public async Task<Player> Get(Guid id)
        {
            return await _context.Players.FirstOrDefaultAsync(p => p.PlayerId == id);
        }

        public async Task<Player> GetByName(string name)
        {
            var playterToReturn = await _context.Players.FirstOrDefaultAsync(x => x.Name == name);
            return playterToReturn;
        }

        public async Task<List<Player>> GetAll()
        {
            return await _context.Players.ToListAsync();
        }

        public async Task<Player> AddPlayer(Player player)
        {
            player.PlayerId = Guid.NewGuid();
            await _context.Players.AddAsync(player);
            await _context.SaveChangesAsync();
            return player;
        }

        public async Task<List<Round>> GetAllRounds(Guid id)
        {
            return await _context.Rounds.Where(x => x.PlayerId == id).ToListAsync();
        }
    }
}
