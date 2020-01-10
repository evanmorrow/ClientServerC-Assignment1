// Made By Kelvin
using System;
using Dir;

class Activity
{
	public static void Main(string[] args)
	{
		if (args.Length == 0)
		{
			Console.WriteLine("Must provide a path");
			return;
		}

		Dir.DirClient dirClient = new Dir.DirClient(args[0]);
		dirClient.getListing();
	}
}