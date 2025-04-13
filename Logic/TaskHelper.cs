using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDoApp.Data;
using ToDoApp.Models;

namespace ToDoApp.Services
{
	internal class TaskHelper
	{
		private readonly MyDb _db;

		public TaskHelper()
		{
			_db = new MyDb();
		}

		public void AddTask()
		{
			Console.WriteLine("Enter task title: ");
			string name = Console.ReadLine();

			Console.WriteLine("Enter description: ");
			string description = Console.ReadLine();

			Console.WriteLine("Enter due date (dd.MM.yyyy): ");
			string input = Console.ReadLine();

			if (!DateTime.TryParseExact(
					input,
					"dd.MM.yyyy",
					null,
					System.Globalization.DateTimeStyles.None,
					out DateTime dueDate))
			{
				Console.WriteLine("Invalid date format! Use dd.MM.yyyy (e.g. 14.05.2025)");
				return; 		   				   
			}

			Console.WriteLine("\nAvailable categories:");
			var categories = _db.Categories.ToList();
			foreach (var cat in categories)
			{
				Console.WriteLine($"  {cat.Id}. {cat.Name}");
			}

			Console.Write("Enter category ID: ");
			if (!int.TryParse(Console.ReadLine(), out int CategoryId))
			{
				Console.WriteLine("Please enter a valid number!");
				return;
			}

			MyTask task = new MyTask
			{
				Name = name,
				Description = description,
				DateTime = dueDate,
				IsCompleted = false,
				CategoryId = CategoryId
			};

			try
			{
				_db.Tasks.Add(task); 
				_db.SaveChanges();
			}
			catch (Exception ex)
			{
				Console.WriteLine("Could not save the task!");
				Console.WriteLine($"Error: {ex.Message}");
			}
		}

		public void ShowTasks()
		{
			try
			{
				var tasks = _db.Tasks
					.Include(t => t.Category)
					.ToList();

				if (tasks.Count == 0)
				{
					Console.WriteLine("No tasks found.");
					return;
				}

				Console.WriteLine("\nAll tasks:");
				Console.WriteLine(new string('-', 50));

				foreach (var task in tasks)
				{
					Console.WriteLine($"ID: {task.Id}");
					Console.WriteLine($"Title: {task.Name}");
					Console.WriteLine($"Description: {task.Description}");
					Console.WriteLine($"Due date: {task.DateTime:dd.MM.yyyy}");
					Console.WriteLine($"Category: {task.Category.Name}");
					Console.WriteLine($"Status: {(task.IsCompleted? "Done" : "In progress")}");
					Console.WriteLine(new string('-', 50));
				}
			}
			catch (Exception ex)
			{
				Console.WriteLine("Failed to load tasks!");
				Console.WriteLine($"Error: {ex.Message}");
			}
		}

		public void MarkAsDone()
		{
			var tasks = _db.Tasks.Where(t => !t.IsCompleted).ToList();

			if (!tasks.Any())
			{
				Console.WriteLine("There are no tasks to mark as done.");
				return;
			}

			Console.WriteLine("Available tasks:");
			foreach (var task in tasks)
			{
				Console.WriteLine($"{task.Id}. {task.Name}");
			}

			Console.Write("Enter the ID of the task you want to mark as done: ");

			if (!int.TryParse(Console.ReadLine(), out int id))
			{
				Console.WriteLine("Please enter a valid number.");
				return;
			}

			try
			{
				var task = _db.Tasks.FirstOrDefault(t => t.Id == id);

				if (task == null)
				{
					Console.WriteLine($"Task with ID {id} was not found.");
					return;
				}
				if (task.IsCompleted)
				{
					Console.WriteLine("This task is already marked as done.");
				}

				task.IsCompleted = true;
				_db.SaveChanges();

				Console.WriteLine($"Task \"{task.Name}\" has been marked as done.");
			}
			catch (Exception ex)
			{
				Console.WriteLine("Could not mark the task as done");
				Console.WriteLine($"Error: {ex.Message}");
			}
		}

		public void MarkAsUndone()
		{
			var tasks = _db.Tasks.Where(t => t.IsCompleted).ToList();

			if (!tasks.Any())
			{
				Console.WriteLine("There are no completed tasks to unmark.");
				return;
			}

			Console.WriteLine("Completed tasks:");
			foreach (var task in tasks)
			{
				Console.WriteLine($"{task.Id}. {task.Name}");
			}

			Console.Write("Enter the ID of the task you want to mark as not done: ");

			if (!int.TryParse(Console.ReadLine(), out int id))
			{
				Console.WriteLine("Please enter a valid number.");
				return;
			}

			try
			{
				var task = _db.Tasks.FirstOrDefault(t => t.Id == id && t.IsCompleted);

				if (task == null)
				{
					Console.WriteLine($"Task with ID {id} was not found or is already not completed.");
					return;
				}

				task.IsCompleted = false;
				_db.SaveChanges();

				Console.WriteLine($"Task \"{task.Name}\" has been marked as not done.");
			}
			catch (Exception ex)
			{
				Console.WriteLine("Could not unmark the task.");
				Console.WriteLine($"Error: {ex.Message}");
			}
		}


		public void DeleteTask()
		{
			var tasks = _db.Tasks.ToList();

			if (!tasks.Any())
			{
				Console.WriteLine("There are no tasks to delete.");
				return;
			}

			Console.WriteLine("Available tasks:");
			foreach (var task in tasks)
			{
				Console.WriteLine($"{task.Id}. {task.Name}");
			}

			Console.WriteLine("Enter the ID of the task you want to delete: ");

			if (!int.TryParse(Console.ReadLine(), out int id))
			{
				Console.WriteLine("Please enter a valid number.");
				return;
			}

			try
			{
				var task = _db.Tasks.FirstOrDefault(t => t.Id == id);

				if (task == null)
				{
					Console.WriteLine($"Task with ID {id} was not found.");
					return;
				}

				Console.WriteLine($"Are you sure you want to delete \"{task.Name}\"? (y/n): ");
				string confirmation = Console.ReadLine()?.Trim().ToLower();

				if (confirmation != "y")
				{
					Console.WriteLine("Task deletion canceled.");
					return;
				}

				_db.Tasks.Remove(task);
				_db.SaveChanges();

				Console.WriteLine($"Task \"{task.Name}\" has been deleted.");
			}
			catch (Exception ex)
			{
				Console.WriteLine("Could not delete the task!");
				Console.WriteLine($"Error: {ex.Message}");
			}

		}

		public void SeedData()
		{
			if (!_db.Categories.Any()) 
			{
				var home = new Category { Name = "Home" };
				var work = new Category { Name = "Work" };
				var personal = new Category { Name = "Personal" };

				_db.Categories.AddRange(home, work, personal); 
				_db.SaveChanges();

				var tasks = new List<MyTask>
				{
					new MyTask
					{
						Name = "Clean the kitchen",
						Description = "Wipe surfaces, wash dishes and take out the trash.",
						DateTime = DateTime.Now.AddDays(3),
						IsCompleted = false,
						CategoryId = home.Id
					},
					new MyTask
					{
						Name = "Finish C# project",
						Description = "Complete it by the end of the week",
						DateTime = DateTime.Now.AddDays(1),
						IsCompleted = false,
						CategoryId = work.Id
					},
					new MyTask
					{
						Name = "Finish reading your book",
						Description = "Take some time to relax.",
						DateTime = DateTime.Now.AddDays(5),
						IsCompleted = false,
						CategoryId = personal.Id
					}
				};

				_db.Tasks.AddRange(tasks); 
				_db.SaveChanges();

				Console.WriteLine("Sample categories and tasks have been loaded.");
			}
			else
			{
				Console.WriteLine("Data already exists, seeding skipped.");
			}
		}
	}
}
