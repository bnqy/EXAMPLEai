namespace EXAMPLEai.DTOs
{
	public class TodoItemDto
	{
		public int Id { get; set; }
		public string Title { get; set; } = string.Empty;
		public string? Description { get; set; }
	}

	public class CreateTodoItemDto
	{
		public string Title { get; set; } = string.Empty;
		public string? Description { get; set; }
	}
}
