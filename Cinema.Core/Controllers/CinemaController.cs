using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Cinema.Core.Controllers
{
    [EnableCors("CorsPolicy")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [ApiController]
    [Route("api/[controller]")]
    public abstract class CinemaController : ControllerBase
    {
        /// <summary>
        /// Creates a <see cref="ObjectResult" /> object that produces an <see cref="StatusCodes.Status201Created" /> response.
        /// </summary>
        /// <param name="result">Object returned as method result.</param>
        /// <returns>The created <see cref="ObjectResult" /> for the response.</returns>
        protected IActionResult CreatedObject(object result)
        {
            return StatusCode(StatusCodes.Status201Created, result);
        }
    }
}
