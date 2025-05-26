
using EXAMPLEai.Entities;
using Microsoft.EntityFrameworkCore;
namespace EXAMPLEai.Data;


public class AppDbContext : DbContext
{
	public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

	public DbSet<TodoItem> TodoItems => Set<TodoItem>();
}
