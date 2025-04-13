using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToDoApp.Models
{
	internal class MyTask
	{
		public int Id { get; set; }
		public string Name { get; set; }
		public string Description { get; set; }
		public DateTime DateTime { get; set; }
		public bool IsCompleted { get; set; }
		public int CategoryId { get; set; }
		public Category Category { get; set; }

		
	}
}
