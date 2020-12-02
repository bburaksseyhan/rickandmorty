using System.Net.Http;
using System.Threading.Tasks;
using MovieApp.Api.Model;
using Newtonsoft.Json;

namespace MovieApp.Api.Services
{
    /// <summary>
    /// This class retreive data from rick and mort public web api
    /// </summary>
    public class CharacterService : ICharacterService<Character>
    {
        private readonly IHttpClientFactory _factory;
        public CharacterService(IHttpClientFactory factory) => _factory = factory;

        public async Task<Character> GetCharacter(int id)
        {
            var client = _factory.CreateClient("characterApi");

            var jsonResult = await client.GetStringAsync($"/api/character/{id}");

            return JsonConvert.DeserializeObject<Character>(jsonResult);
        }
    }
}