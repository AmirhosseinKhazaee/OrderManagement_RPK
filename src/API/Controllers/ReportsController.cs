using Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[ApiController]
[Route("api/reports")]
public class ReportsController : ControllerBase
{
    private readonly CustomerService _customerService;

    public ReportsController(CustomerService customerService)
    {
        _customerService = customerService;
    }

    [HttpGet("top-customers")]
    public async Task<IActionResult> GetTopCustomers([FromQuery] int count = 5)
    {
        var result = await _customerService.GetTopCustomersAsync(count);
        return Ok(result);
    }
}