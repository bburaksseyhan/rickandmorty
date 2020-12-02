using System.Threading.Tasks;

namespace MovieApp.Api.Services
{
    public interface ICharacterService<T> where T:class
    {
        Task<T> GetCharacter(int id);
    }
}
