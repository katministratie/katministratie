using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KatministratieApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SuperkattenController : Controller
    {
        private readonly SuperkatDbContext _context;

        public SuperkattenController(SuperkatDbContext context)
        {
            _context = context;
        }

        // GET: Superkatten
        [HttpGet]
        [Route("list")]
        public ICollection<SuperkatDto> GetAllSuperkatten()
        {
            var superkatten = _context.Superkatten.ToList();
            return superkatten;
        }

        [HttpPut]
        [Route("{name}")]
        public void CreateSuperkat(string name)
        {
            var count = _context.Superkatten.Count();

            var superkat = new SuperkatDto()
            {
                Id = 22000 + count + 1,
                Name = name
            };
            _context.Superkatten.Add(superkat);
            _context.SaveChanges();
        }

        [HttpGet]
        [Route("{id}")]
        public SuperkatDto GetSuperkatDetails(int id)
        {
            var superkat = _context.Superkatten.Where(k => k.Id == id).FirstOrDefault();
            return superkat;
        }
    }
}
