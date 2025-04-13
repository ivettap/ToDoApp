using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToDoApp.Models
{
	internal class Category
	{
		public int Id { get; set; }
		public string Name { get; set; }
		public List<MyTask> Tasks { get; set; }
	}
}
