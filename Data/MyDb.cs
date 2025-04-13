using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using ToDoApp.Models;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace ToDoApp.Data
{

	internal class MyDb : DbContext
	{
		public DbSet<MyTask> Tasks { get; set; } 
		public DbSet<Category> Categories { get; set; } 

		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		{
			var config = new ConfigurationBuilder()
				.AddJsonFile("appsettings.json")
				.Build();

			var connectionString = config.GetConnectionString("DefaultConnection");

			optionsBuilder.UseSqlServer(connectionString);
		}
	}
}
