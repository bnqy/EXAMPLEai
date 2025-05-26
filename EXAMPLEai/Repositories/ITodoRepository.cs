using EXAMPLEai.Entities;

namespace EXAMPLEai.Repositories
{
	public interface ITodoRepository
	{
		Task<List<TodoItem>> GetAllAsync();
		Task<TodoItem?> GetByIdAsync(int id);
		Task<TodoItem> AddAsync(TodoItem item);
		Task UpdateAsync(TodoItem item);
		Task DeleteAsync(int id);
	}
}
