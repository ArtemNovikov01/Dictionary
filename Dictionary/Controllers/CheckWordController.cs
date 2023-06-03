using MetallAlloy.Models;
using Microsoft.AspNetCore.Mvc;
using Dictionary.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http.HttpResults;

namespace Dictionary.Controllers
{

    [ApiController]
    public class CheckWordController
    {
        ApplicationContext db;

        public CheckWordController(ApplicationContext context)
        {
            db=context;
        }

        [HttpGet]
        [Route("api/checkword/get")]
        public async Task<ActionResult<IEnumerable<Word>>> get()
        {
            return await db.words.ToListAsync();
        }

        [HttpPost]
        [Route("api/checkword/set")]
        public async Task<ActionResult<IEnumerable<Word>>> post(List<int>json)
        {
            List<Word>words = new List<Word>();
            foreach (var item in json)
            {
               var word = await db.words.FirstOrDefaultAsync(b=>b.Id==item);
                words.Add(word);
            }
            return words.ToList();
        }

        [HttpPost]
        [Route("api/checkword/check")]
        public async Task<ActionResult<IEnumerable<Word>>> checkPost(List<string> json)
        {
            List<Word> errorWord = new List<Word>();
            foreach (var word in json)
            {
                int num = word.IndexOf(")");
                string numId = word.Substring(0,num);
               int id = int.Parse(numId);
               var wordDb = await db.words.FirstOrDefaultAsync(w => w.Id == id);

                string engWord = word.Substring(num+1, word.Length - (num+1));
                string engWordLowCase = engWord.ToLower();

                if (!engWordLowCase.Equals(wordDb.EngWord))
                {
                    Word errorW = new Word();

                    errorW.Id = id;
                    errorW.EngWord = engWord;
                    errorW.RusWord = wordDb.RusWord;

                    errorWord.Add(errorW);
                   
                }

            }

            return errorWord.ToList();
        }
    }
}
