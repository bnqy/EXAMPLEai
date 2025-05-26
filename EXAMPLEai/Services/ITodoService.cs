using EXAMPLEai.DTOs;

namespace EXAMPLEai.Services
{
	public interface ITodoService
	{
		Task<List<TodoItemDto>> GetAllAsync();
		Task<TodoItemDto?> GetByIdAsync(int id);
		Task<TodoItemDto> CreateAsync(CreateTodoItemDto dto);
		Task<bool> UpdateAsync(int id, CreateTodoItemDto dto);
		Task<bool> DeleteAsync(int id);
	}
}
