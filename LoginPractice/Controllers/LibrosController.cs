using AutoMapper;
using LoginPractice.DTOs;
using LoginPractice.Entity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LoginPractice.Controllers
{
    [ApiController]
    [Route("api/libros")]
    public class LibrosController : ControllerBase
    {
        private readonly ApplicationDbContext context;
        private readonly IMapper mapper;

        public LibrosController(
            ApplicationDbContext context,
            IMapper mapper
            )
        {
            this.context = context;
            this.mapper = mapper;
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<ActionResult<List<LibroDTO>>> Get()
        {
            var libro = await context.Libros.ToListAsync();
            return mapper.Map<List<LibroDTO>>(libro);     

        }

        [HttpGet("{id:int}")]
        [AllowAnonymous]
        public async Task<ActionResult<LibroDTOConAutores>> Get(int id)
        {
            var libro = await context.Libros
                .Include(libroDb => libroDb.AutoresLibros)
                .ThenInclude(autorLibroDb => autorLibroDb.Autor)
                .FirstOrDefaultAsync(libroDb => libroDb.Id == id);
            if (libro == null)
            {
                return NotFound($"No se ha encontrado un libro con el id {id}");
            }

            return mapper.Map<LibroDTOConAutores>(libro);

        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<ActionResult> Post(LibroCreacionDTO libroCreacionDTO)
        {
            if(libroCreacionDTO.AutoresIds == null)
            {
                return BadRequest("No se puede crear un libro sin autores");
            }

            var autores = await context.Autores
                .Where(autorDb => libroCreacionDTO.AutoresIds
                .Contains(autorDb.Id)).Select(x => x.Id).ToListAsync();

            if(libroCreacionDTO.AutoresIds.Count != autores.Count)
            {
                return BadRequest("Uno de los autores enviados no existe");
            }

            var libro = mapper.Map<Libro>(libroCreacionDTO);

            context.Add(libro);
            await context.SaveChangesAsync();
            return Ok();

        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult> Put(int id, LibroCreacionDTO libroCreacionDTO )
        {//al actualizar libro es especial, ya que al crear un libro aparte de crear un libro
         //creamos a los autores que perteneceran a este
            var libroDb = await context.Libros
                .Include(libroDb => libroDb.AutoresLibros)
                
                .FirstOrDefaultAsync(libroDb => libroDb.Id == id);


            if (libroDb == null)
            {
                return NotFound();
            }

            libroDb = mapper.Map(libroCreacionDTO, libroDb);
            await context.SaveChangesAsync();

            return NoContent();
        }


        [HttpDelete("{id:int}")]
        public async Task<ActionResult> Delete(int id)
        {
            var libro = await context.Libros.AnyAsync();
            if (!libro)
            {
                return NotFound($"No se encuentra un libro con el identificador {id}");
            }

            context.Libros.Remove(new Libro() { Id = id });
            await context.SaveChangesAsync();
            return Ok();

        }
    }
}
