using LoginPractice.Entity;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LoginPractice.Controllers
{
    [ApiController]
    [Route("api/controller")]
    public class AutoresController  : ControllerBase    
    {
        private readonly ApplicationDbContext context;

        public AutoresController(
            ApplicationDbContext context
            )
        {
            this.context = context;
        }

        [HttpGet]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "EsAdmin")]
        //[AllowAnonymous]
        public async Task<ActionResult<List<Autor>>> Get() {
            return await context.Autores.ToListAsync();
        }
    }
}
