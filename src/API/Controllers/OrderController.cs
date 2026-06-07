using Application.DTOs.Orders;
using Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[ApiController]
[Route("api/orders")]
public class OrderController : ControllerBase
{
    private readonly OrderService _orderService;

    public OrderController(OrderService orderService)
    {
        _orderService = orderService;
    }

    // CREATE
    [HttpPost]
    public async Task<IActionResult> Create(CreateOrderDto dto)
    {
        var id = await _orderService.CreateAsync(dto);

        return CreatedAtAction(nameof(GetById), new { id }, null);
    }

    // GET ALL
    [HttpGet]
    public async Task<IActionResult> GetAll(
        [FromQuery] int page = 1,
        [FromQuery] int pageSize = 10)
    {
        var result = await _orderService.GetAllAsync(page, pageSize);
        return Ok(result);
    }

    // GET BY ID
    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetById(int id)
    {
        var result = await _orderService.GetByIdAsync(id);

        if (result == null)
            return NotFound();

        return Ok(result);
    }

    // UPDATE
    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update(int id, UpdateOrderDto dto)
    {
        var result = await _orderService.UpdateAsync(id, dto);

        if (!result)
            return NotFound();

        return NoContent();
    }

    // DELETE
    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        var result = await _orderService.DeleteAsync(id);

        if (!result)
            return NotFound();

        return NoContent();
    }
}