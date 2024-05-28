using Domain.Models;
using Infrastructure;
using Microsoft.EntityFrameworkCore;
using Services.Core.Interfaces;

namespace Services.Application.Services
{
    public class GuitarService : IGuitarService
    {
        private readonly ApplicationDbContext _dbContext;

        public GuitarService(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<IEnumerable<Guitar>> GetGuitars()
        {
            var guitars = await _dbContext.Guitars.ToListAsync();
            if (guitars == null)
            {
                throw new Exception("No guitars found");
            }
            return guitars;
            
        }
        public async Task<Guitar> GetGuitar(int id)
        {
            var guitar = await _dbContext.Guitars.FindAsync(id);
            if (guitar == null)
            {
                throw new Exception($"No guitar with {id} found");
            }
            return guitar;
        }
        public async Task CreateGuitar(Guitar guitar)
        {
            try
            {
                _dbContext.Add(guitar);
                await _dbContext.SaveChangesAsync();
            } catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task UpdateGuitar(int id, Guitar guitar)
        {
            Guitar guitarItemFromDb = await _dbContext.Guitars.FindAsync(id) 
                ?? throw new Exception($"No guitar with {id} found");
            guitarItemFromDb.Name = guitar.Name;
            guitarItemFromDb.Price = guitar.Price;
            guitarItemFromDb.Category = guitar.Category;
            guitarItemFromDb.Description = guitar.Description;
            
            await _dbContext.SaveChangesAsync();
        }
        public async Task DeleteGuitar(int id)
        {
            Guitar guitarItemFromDb = await _dbContext.Guitars.FindAsync(id) ?? 
                throw new Exception($"No guitar with {id} found");

            _dbContext.Guitars.Remove(guitarItemFromDb);
            await _dbContext.SaveChangesAsync();
        }
    }
}
