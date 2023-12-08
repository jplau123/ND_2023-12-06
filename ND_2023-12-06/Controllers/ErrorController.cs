using Microsoft.AspNetCore.Mvc;

namespace ND_2023_12_06.Controllers;

[Route("Error")]
public class ErrorController : ControllerBase
{
    [ApiExplorerSettings(IgnoreApi = true)]
    public IActionResult Error()
    {
        return Problem();
    }
}
