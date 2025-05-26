using Xunit;
using Moq;
using System.Threading.Tasks;
using System.Collections.Generic;
using EXAMPLEai.DTOs;
using EXAMPLEai.Entities;
using EXAMPLEai.Repositories;
using EXAMPLEai.Services;

namespace EXAMPLEai.Tests.Tests
{
	public class TodoServiceTests
	{
		private readonly Mock<ITodoRepository> _repoMock;
		private readonly TodoService _service;

		public TodoServiceTests()
		{
			_repoMock = new Mock<ITodoRepository>();
			_service = new TodoService(_repoMock.Object);
		}

		[Fact]
		public async Task GetAllAsync_ReturnsAllTodos()
		{
			// Arrange
			var todos = new List<TodoItem> {
			new TodoItem { Id = 1, Title = "Test 1", Description = "Desc 1" },
			new TodoItem { Id = 2, Title = "Test 2", Description = "Desc 2" }
		};
			_repoMock.Setup(r => r.GetAllAsync()).ReturnsAsync(todos);

			// Act
			var result = await _service.GetAllAsync();

			// Assert
			Assert.Equal(2, result.Count);
			Assert.Equal("Test 1", result[0].Title);
		}

		[Fact]
		public async Task GetByIdAsync_ItemExists_ReturnsItem()
		{
			_repoMock.Setup(r => r.GetByIdAsync(1)).ReturnsAsync(
				new TodoItem { Id = 1, Title = "Sample", Description = "Desc" });

			var result = await _service.GetByIdAsync(1);

			Assert.NotNull(result);
			Assert.Equal("Sample", result.Title);
		}

		[Fact]
		public async Task GetByIdAsync_ItemNotFound_ReturnsNull()
		{
			_repoMock.Setup(r => r.GetByIdAsync(2)).ReturnsAsync((TodoItem?)null);

			var result = await _service.GetByIdAsync(2);

			Assert.Null(result);
		}

		[Fact]
		public async Task CreateAsync_CreatesTodoItem()
		{
			var dto = new CreateTodoItemDto { Title = "New", Description = "Desc" };
			_repoMock.Setup(r => r.AddAsync(It.IsAny<TodoItem>()))
				.ReturnsAsync((TodoItem t) => { t.Id = 42; return t; });

			var result = await _service.CreateAsync(dto);

			Assert.Equal(42, result.Id);
			Assert.Equal("New", result.Title);
		}

		[Fact]
		public async Task UpdateAsync_ItemExists_UpdatesAndReturnsTrue()
		{
			var entity = new TodoItem { Id = 5, Title = "Old", Description = "Old" };
			_repoMock.Setup(r => r.GetByIdAsync(5)).ReturnsAsync(entity);

			var dto = new CreateTodoItemDto { Title = "Updated", Description = "Updated Desc" };
			_repoMock.Setup(r => r.UpdateAsync(It.IsAny<TodoItem>())).Returns(Task.CompletedTask);

			var result = await _service.UpdateAsync(5, dto);

			Assert.True(result);
			_repoMock.Verify(r => r.UpdateAsync(It.Is<TodoItem>(t => t.Title == "Updated" && t.Description == "Updated Desc")), Times.Once);
		}

		[Fact]
		public async Task UpdateAsync_ItemNotFound_ReturnsFalse()
		{
			_repoMock.Setup(r => r.GetByIdAsync(8)).ReturnsAsync((TodoItem?)null);

			var dto = new CreateTodoItemDto { Title = "x", Description = "y" };

			var result = await _service.UpdateAsync(8, dto);

			Assert.False(result);
			_repoMock.Verify(r => r.UpdateAsync(It.IsAny<TodoItem>()), Times.Never);
		}

		[Fact]
		public async Task DeleteAsync_ItemExists_DeletesAndReturnsTrue()
		{
			var entity = new TodoItem { Id = 10, Title = "t", Description = "d" };
			_repoMock.Setup(r => r.GetByIdAsync(10)).ReturnsAsync(entity);
			_repoMock.Setup(r => r.DeleteAsync(10)).Returns(Task.CompletedTask);

			var result = await _service.DeleteAsync(10);

			Assert.True(result);
			_repoMock.Verify(r => r.DeleteAsync(10), Times.Once);
		}

		[Fact]
		public async Task DeleteAsync_ItemNotFound_ReturnsFalse()
		{
			_repoMock.Setup(r => r.GetByIdAsync(12)).ReturnsAsync((TodoItem?)null);

			var result = await _service.DeleteAsync(12);

			Assert.False(result);
			_repoMock.Verify(r => r.DeleteAsync(It.IsAny<int>()), Times.Never);
		}
	}

}
