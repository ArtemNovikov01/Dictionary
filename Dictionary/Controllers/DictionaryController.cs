using MetallAlloy.Models;
using Microsoft.AspNetCore.Mvc;
using Dictionary.Models;
using Microsoft.EntityFrameworkCore;

namespace Dictionary.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DictionaryController : ControllerBase
    {
        ApplicationContext db;
        public DictionaryController(ApplicationContext context) 
        {
            db = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Word>>> get() 
        {
            return await db.words.ToListAsync();
        }

        [HttpPost]
        public async Task<ActionResult<Word>> post(Word word)
        {
            await db.words.AddAsync(word);
            db.SaveChanges();
            return Ok(word);
        }

        [HttpPut]
        public async Task<ActionResult<Word>> put(Word word)
        {
            if (word == null)
            {
                return BadRequest();
            }
            if (!db.words.Any(x => x.Id == word.Id))
            {
                return NotFound();
            }

            db.words.Update(word);
            await db.SaveChangesAsync();
            return Ok(word);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<Word>> del(int id)
        {
            var word = await db.words.FirstOrDefaultAsync(x => x.Id == id);
            db.words.Remove(word);
            await db.SaveChangesAsync();

            return Ok(word);

        }

    }
}
