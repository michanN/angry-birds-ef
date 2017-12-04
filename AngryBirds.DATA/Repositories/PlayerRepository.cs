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

        public async Task<Player> GetByIdAsync(Guid id)
        {
            return await _context.Players.FirstOrDefaultAsync(p => p.PlayerId == id);
        }

        public async Task<Player> GetByNameAsync(string name)
        {
            var playterToReturn = await _context.Players.FirstOrDefaultAsync(x => x.Name == name);
            return playterToReturn;
        }

        public async Task<List<Player>> GetAllPlayersAsync()
        {
            return await _context.Players.ToListAsync();
        }

        public async Task<Player> AddPlayerAsync(Player player)
        {
            player.PlayerId = Guid.NewGuid();
            await _context.Players.AddAsync(player);
            await _context.SaveChangesAsync();
            return player;
        }

        public async Task<Player> UpdatePlayerAsync(Player playerToUpdate)
        {
            _context.Players.Attach(playerToUpdate);
            _context.Entry(playerToUpdate).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return playerToUpdate;
        }

        public async Task<List<Round>> GetAllRoundsAsync(Guid id)
        {
            return await _context.Rounds.Where(x => x.PlayerId == id).ToListAsync();
        }

        public async Task<List<Round>> GetAllRoundsForMapAsync(Guid id)
        {
            return await _context.Rounds.Where(x => x.MapId == id).ToListAsync();
        }

        public async Task<Round> AddRoundAsync(Round round)
        {
            round.RoundId = Guid.NewGuid();
            await _context.Rounds.AddAsync(round);
            await _context.SaveChangesAsync();
            return round;
        }

        public async Task<List<Map>> GetAllMapsAsync()
        {
            return await _context.Maps.ToListAsync();
        }

        public async Task<Map> GetMapByIdAsync(Guid id)
        {
            return await _context.Maps.FirstOrDefaultAsync(p => p.MapId == id);
        }

        public async Task<Map> AddMapAsync(Map map)
        {
            map.MapId = Guid.NewGuid();
            await _context.Maps.AddAsync(map);
            await _context.SaveChangesAsync();
            return map;
        }
    }
}
