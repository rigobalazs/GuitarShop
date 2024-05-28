using Domain.Models;

namespace Services.Core.Interfaces
{
    public interface IGuitarService
    {
        Task<IEnumerable<Guitar>> GetGuitars();

        Task<Guitar> GetGuitar(int id);

        Task CreateGuitar(Guitar guitar);

        Task UpdateGuitar(int id, Guitar guitar);

        Task DeleteGuitar(int id);

    }

}
