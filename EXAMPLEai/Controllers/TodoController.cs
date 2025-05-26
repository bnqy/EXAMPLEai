﻿using EXAMPLEai.DTOs;
using EXAMPLEai.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EXAMPLEai.Controllers
{
	[ApiController]
	[Route("api/[controller]")]
	public class TodoController : ControllerBase
	{
		private readonly ITodoService _service;
		public TodoController(ITodoService service) => _service = service;

		[HttpGet]
		public async Task<IActionResult> GetAll() =>
			Ok(await _service.GetAllAsync());

		[HttpGet("{id}")]
		public async Task<IActionResult> GetById(int id)
		{
			var item = await _service.GetByIdAsync(id);
			return item == null ? NotFound() : Ok(item);
		}

		[HttpPost]
		public async Task<IActionResult> Create(CreateTodoItemDto dto)
		{
			var item = await _service.CreateAsync(dto);
			return CreatedAtAction(nameof(GetById), new { id = item.Id }, item);
		}

		[HttpPut("{id}")]
		public async Task<IActionResult> Update(int id, CreateTodoItemDto dto)
		{
			var updated = await _service.UpdateAsync(id, dto);
			return updated ? NoContent() : NotFound();
		}

		[HttpDelete("{id}")]
		public async Task<IActionResult> Delete(int id)
		{
			var deleted = await _service.DeleteAsync(id);
			return deleted ? NoContent() : NotFound();
		}
	}
}
