using Microsoft.AspNetCore.Mvc;
using WebAPI.Filters;

namespace WebAPI.Controllers
{
    [ApiController]
    [ApiValidationFilter]
    public class ApiControllerBase : ControllerBase
    {
    }
}
