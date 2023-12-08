using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using ND_2023_12_06.Controllers;

namespace ND_2023_12_06.Helpers;

public class ResponseHelper
{
    private readonly ILogger<ResponseHelper> _logger;

    public ResponseHelper(ILogger<ResponseHelper> logger)
    {
        _logger = logger;
    }

    public async Task<IActionResult> HandleErrors(Func<Task<IActionResult>> handler)
    {
		try
		{
            return await handler();
		}
		catch (KeyNotFoundException ex)
		{
            _logger.Log(LogLevel.Error, $"An exception occured: {ex.Message}");
            return new NotFoundResult();
		}
        catch (Exception ex)
        {
            _logger.Log(LogLevel.Error, $"An exception occured: {ex.Message}");
            return new BadRequestResult();
        }
    }
}
