using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDoApp.Data;
using ToDoApp.Services;

namespace ToDoApp.UI
{
	internal class Menu
	{
		public static void Show()
		{
			TaskHelper helper = new TaskHelper();
			helper.SeedData();

			while (true)
			{
				Console.Clear(); 
				Console.WriteLine(("==================== TO-DO LIST APP ===================="));
				Console.WriteLine("What would you like to do?\n");
				Console.WriteLine("1. Add a new task");
				Console.WriteLine("2. View all tasks");
				Console.WriteLine("3. Mark task as done");
				Console.WriteLine("4. Mark a completed task as undone");
				Console.WriteLine("5. Delete task");
				Console.WriteLine("6. Exit\n");

				Console.Write("Choice: ");
				string choice = Console.ReadLine();

				switch (choice)
				{
					case "1":
						helper.AddTask();
						break;
					case "2":
						helper.ShowTasks(); 
						break;
					case "3":
						helper.MarkAsDone();
						break;
					case "4":
						helper.MarkAsUndone();
						break;
					case "5":
						helper.DeleteTask();
						break;
					case "6":
						Console.WriteLine("Exiting the app. See you!");
						Thread.Sleep(1000);
						return;
					default:
						Console.WriteLine("Invalid choice! Try again.");
						break;
				}

				Console.WriteLine("\nPress Enter to continue...");
				Console.ReadLine();

			}
		}
	}
}
