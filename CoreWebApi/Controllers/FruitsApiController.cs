using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CoreWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FruitsApiController : ControllerBase
    {
        public List<string> fruits = new List<string>()
        {
            "Apple",
            "Banana",
            "Orange",
            "Grapes",
            "Mango",
            "jamun"
        };

        [HttpGet]
        public  List<string> GetFruits() {

            return fruits;
        }

        // GET: api/FruitsApi/1
        [HttpGet("{id}")]
        public ActionResult<string> GetFruit(int id)
        {
            if (id < 0 || id >= fruits.Count)
            {
                return NotFound("Fruit not found.");
            }
            return Ok(fruits[id]);
        }

        // POST: api/FruitsApi
        [HttpPost]
        public ActionResult<List<string>> AddFruit([FromBody] string fruit)
        {
            fruits.Add(fruit);
            return Ok(fruits);
        }

        // PUT: api/FruitsApi/1
        [HttpPut("{id}")]
        public ActionResult<List<string>> UpdateFruit(int id, [FromBody] string newFruit)
        {
            if (id < 0 || id >= fruits.Count)
            {
                return NotFound("Fruit not found.");
            }
            fruits[id] = newFruit;
            return Ok(fruits);
        }

        // DELETE: api/FruitsApi/1
        [HttpDelete("{id}")]
        public ActionResult<List<string>> DeleteFruit(int id)
        {
            if (id < 0 || id >= fruits.Count)
            {
                return NotFound("Fruit not found.");
            }
            fruits.RemoveAt(id);
            return Ok(fruits);
        }
    }
}
