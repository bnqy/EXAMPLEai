using EXAMPLEai.DTOs;
using EXAMPLEai.Entities;
using EXAMPLEai.Repositories;

namespace EXAMPLEai.Services
{
	public class TodoService : ITodoService
	{
		private readonly ITodoRepository _repo;
		public TodoService(ITodoRepository repo) => _repo = repo;

		public async Task<List<TodoItemDto>> GetAllAsync()
		{
			var items = await _repo.GetAllAsync();
			return items.Select(x => new TodoItemDto
			{
				Id = x.Id,
				Title = x.Title,
				Description = x.Description
			}).ToList();
		}

		public async Task<TodoItemDto?> GetByIdAsync(int id)
		{
			var item = await _repo.GetByIdAsync(id);
			if (item == null) return null;
			return new TodoItemDto { Id = item.Id, Title = item.Title, Description = item.Description };
		}

		public async Task<TodoItemDto> CreateAsync(CreateTodoItemDto dto)
		{
			var entity = new TodoItem { Title = dto.Title, Description = dto.Description };
			entity = await _repo.AddAsync(entity);
			return new TodoItemDto { Id = entity.Id, Title = entity.Title, Description = entity.Description };
		}

		public async Task<bool> UpdateAsync(int id, CreateTodoItemDto dto)
		{
			var entity = await _repo.GetByIdAsync(id);
			if (entity == null) return false;
			entity.Title = dto.Title;
			entity.Description = dto.Description;
			await _repo.UpdateAsync(entity);
			return true;
		}

		public async Task<bool> DeleteAsync(int id)
		{
			var entity = await _repo.GetByIdAsync(id);
			if (entity == null) return false;
			await _repo.DeleteAsync(id);
			return true;
		}
	}
}
