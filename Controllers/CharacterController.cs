using System;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using MovieApp.Api.Model;
using MovieApp.Api.Services;
using Newtonsoft.Json;

namespace MovieApp.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CharacterController : ControllerBase
    {
        private readonly ICharacterService<Character> _characterService;
        private readonly IDistributedCache _distributedCache;

        public CharacterController(ICharacterService<Character> characterService,
                                   IDistributedCache distributedCache)
        {
            _characterService = characterService;
            _distributedCache = distributedCache;
        }

        // GET: api/Character/5
        [HttpGet("{id}", Name = "Get")]
        public async Task<Character> Get(int id)
        {
            var character = new Character();
            var serializedCharacter = string.Empty;

            //redis cache...
            byte[] encodedCharacters = await _distributedCache.GetAsync($"_character_{id}");

            if(encodedCharacters != null)
            {
                serializedCharacter = Encoding.UTF8.GetString(encodedCharacters);
                return JsonConvert.DeserializeObject<Character>(serializedCharacter);
            }

            character = await _characterService.GetCharacter(id);

            serializedCharacter=  JsonConvert.SerializeObject(character);
            encodedCharacters = Encoding.UTF8.GetBytes(serializedCharacter);

            var options = new DistributedCacheEntryOptions()
            {
                  SlidingExpiration = TimeSpan.FromMinutes(10),
                  AbsoluteExpiration = DateTime.Now.AddMinutes(10)
            };

            await _distributedCache.SetAsync($"_character_{id}", encodedCharacters, options);

            return character;
        }
    }
}
