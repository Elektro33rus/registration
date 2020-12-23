using Microsoft.AspNetCore.Mvc;
using System;
using System.Diagnostics.CodeAnalysis;
using System.Net;
using System.Threading.Tasks;

namespace AuthProject.Controllers
{
    [Route("api/[controller]")]
    [Consumes("application/json")]
    [Produces("application/json")]
    [ApiController]
    public abstract class BaseController : ControllerBase
    {
        protected async Task<IActionResult> GetResultAsync<T>([NotNull]Func<Task<T>> getDataFunc)
        {
            T result = await getDataFunc();

            if (result == null)
                return NotFound();

            return new JsonResult(result);
        }

        internal IActionResult GenerateForbiddenResponse()
        {
            return StatusCode((int)HttpStatusCode.Forbidden);
        }
    }
}
