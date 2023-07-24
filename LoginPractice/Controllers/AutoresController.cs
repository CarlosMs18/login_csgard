using AutoMapper;
using LoginPractice.DTOs;
using LoginPractice.Entity;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LoginPractice.Controllers
{
    [ApiController]
    [Route("api/autores")]
    public class AutoresController  : ControllerBase    
    {
        private readonly ApplicationDbContext context;
        private readonly IMapper mapper;

        public AutoresController(
            ApplicationDbContext context,
            IMapper mapper
            )
        {
            this.context = context;
            this.mapper = mapper;
        }

        [HttpGet]
        //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "EsAdmin")]
        [AllowAnonymous]
        public async Task<ActionResult<List<AutorDTO>>> Get() {
            var autores = await context.Autores.ToListAsync();
            return mapper.Map<List<AutorDTO>>(autores);
         
        }

        [HttpGet("{id:int}", Name = "ObtenerPorAutor")]
        [AllowAnonymous]
        public async Task<ActionResult<AutorDTO>> Get(int id)
        {
            var autor = await context.Autores.FirstOrDefaultAsync(autorDB => autorDB.Id == id);
            if(autor == null) {
                return NotFound($"No se ha encontrado un id con el numero {id}");
            }

            return mapper.Map<AutorDTO>(autor); 
        }


        [HttpPost]
        //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "EsAdmin")]
        [AllowAnonymous]
        public async Task<ActionResult> Post(AutorCreacionDTO autorCreacionDTO)
        {
            var autor = mapper.Map<Autor>(autorCreacionDTO);

            context.Add(autor);
            await context.SaveChangesAsync();

            var autorDto = mapper.Map<AutorDTO>(autor);
            return CreatedAtRoute("ObtenerPorAutor",new {id = autor.Id }, autorDto);  
          
        }

        [HttpPut("{id:int}")]
        [AllowAnonymous]
        public async Task<ActionResult> Put(int id, AutorCreacionDTO autorCreacionDTO)
        {
            var autor = await context.Autores.AnyAsync(autorDb => autorDb.Id == id);
            if (!autor) 
            {
                return NotFound();
            }
            var autorCreacionActualizacion = mapper.Map<Autor>(autorCreacionDTO);
            autorCreacionActualizacion.Id = id;
            context.Update(autorCreacionActualizacion);
            await context.SaveChangesAsync();
            return NoContent();


        }

        [HttpDelete("{id:int}")]
        [AllowAnonymous]
        public async Task<ActionResult> Delete(int id)
        {
            var autor = await context.Autores.AnyAsync(autorDB => autorDB.Id == id);
            if (!autor)
            {
                return NotFound();
            }

            context.Remove(new Autor() { Id = id });
            await context.SaveChangesAsync();
            return Ok();

        }
    }
}
