﻿namespace DW_Final_Project.Models
{
	public class User
	{
		public int id { get; set; }
		public string email { get; set; }
		public string password { get; set; }
		public string? token { get; set; }
		//fk
		public int type { get; set; }
	}
}
