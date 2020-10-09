using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Tito.Services.Todoes.Api.Contollers
{
    [ApiController]
    [Route("")]
    public class HomeController: ControllerBase
    {
        private readonly IOptions<ApiOptions> _apiOptions;

        public HomeController(IOptions<ApiOptions> apiOptions)
        {
            _apiOptions = apiOptions;
        }

        [HttpGet]
        public IActionResult Get() => Ok(_apiOptions.Value.Name);
    }
}
