using System;
using System.Collections;
using System.Collections.Generic;

namespace TextAdventureGame
{
	public class Program
	{

		static void Main(string[] args)
		{
			bool running = true;
			var clock = new GameClock();
			var items = new List<Item>();
			items.Add(new UsableItem("Axe", "INSERT AXE"));
			items.Add(new UsableItem("Flint and steel", "INSERT FLINT AND STEEL"));
			items.Add(new UsableItem("Food", "INSERT GRANOLA BARS OR SOMMET IDK"));
			var inventory = new Inventory(items);
			inventory.AddItem(new UsableItem("Buff Axe", "SOME DESCRIPTION IDK"));
			var player = new Player(inventory);
			const int sleepTime = 8;
			while (running)
			{
				clock.OutputCurrentTime();
				string choice1 = Console.ReadLine();
				if (choice1 == "s")
				{
					clock.Tick(sleepTime);
				}
				else if (choice1.ToLower() == "add item")
				{
					Console.WriteLine("Enter item's name: ");
					string name = Console.ReadLine();
					string description = "text";
					player.inventory.AddItem(new UsableItem(name, description));
				}
				else if (choice1 == "y")
				{
					Console.WriteLine("Which format would you like the time to be displayed as:\n\n 1. 12 hour\n    2. 24 hour");
					string choice2 = Console.ReadLine();
					if (choice2 == "1")
						clock.Format = GameClock.TimeFormat.TwelveHourTime;
					else if (choice2 == "2")
						clock.Format = GameClock.TimeFormat.TwentyFourHourTime;
					else
						Console.WriteLine("Invalid input");
				}
				else if (choice1 == "p")
				{
					player.inventory.OutputContents();
				}
				else
					clock.Tick();
			}
		}
	}

	public class GameClock
	{
		private int _day;
		private int _hour;
		private TimeFormat _format;

		public GameClock(int initialHour = 0)
		{
			_day = 0;
			_hour = initialHour;
		}
		public void OutputCurrentTime()
		{
			switch (_format)
			{
				case TimeFormat.TwelveHourTime:
					string period = _hour < 12 ? "am" : "pm";
					string time;
					int h = _hour; // Copy value of hour so we can manipulate it for outputting
					if (period == "pm" && _hour != 12)
						h -= 12;
					time = $"{h}:00{period}";
					Console.WriteLine($"The current time is: {time}, and it is day {_day}");
					break;
				case TimeFormat.TwentyFourHourTime:
					time = $"{_hour}:00";
					Console.WriteLine($"The current time is: {time}, and it is day {_day}");
					break;
			}
		}

		public void Tick(int increment = 1)
		{
			if (increment < 24)
			{
				int timeToAdd = 24 - _hour; // Removes time from the rest of the day from the increment
				_hour += increment;
				if (_hour > 23) // If it is the end of the day
				{
					_day++;
					_hour = increment == 1 ? 0 : increment - timeToAdd; // If player has slept
				}
			}
			// If the increment is greater than a day an error is thrown
			// If at one point you need to pass by days sort out later
			else throw new Exception("No one sleeps for this long go away!");
		}
		// Properties
		public int Hour
		{
			get
			{
				return _hour;
			}
		}
		public int Day
		{
			get
			{
				return _day;
			}
		}
		public TimeFormat Format
		{
			get
			{
				return _format;
			}
			set
			{
				Console.WriteLine($"Setting format to {value}");
				_format = value;
			}
		}
		public enum TimeFormat
		{
			TwelveHourTime,
			TwentyFourHourTime
		};
	}
	
	public class Player
	{
		public int health = 0;
		public int hunger = 0;
		public int hydration = 0;
		public uint gold = 0;
		public Inventory inventory;
		public Player(Inventory i)
		{
			inventory = i;
		}
	}

	public class Inventory
	{
		public readonly Dictionary<string, List<Item>> items = new Dictionary<string, List<Item>>();

		public Inventory(List<Item> defaultItems)
		{
			foreach (var item in defaultItems)
			{
				items.Add(item.Name, new List<Item>() { item });
			}
		}

		public void AddItem(Item item)
		{
			if (items.ContainsKey(item.Name))
			{
				items[item.Name].Add(item);
				Console.WriteLine($"{item.Name} has been added to inventory");
			}
			else items[item.Name] = new List<Item>() { item };
		}

		public bool Contains(Item item)
		{
			foreach (var kvp in items)
			{
				//if (kvp.Value == item)
				// return true;
			}
			return false;
		}

		public void OutputContents()
		{
			Console.WriteLine("Your inventory contains the following items: ");
			foreach (var kvp in items)
			{
				Console.WriteLine(kvp.Key);
			}
		}
	}

	public abstract class Item
	{
		private readonly string _name;
		private readonly string _description;

		public Item(string name = "Item", string description = "This is an Item")
		{
			_name = name;
			_description = description;
		}

		public abstract void Use(Player player);

		public void OutputDescription()
		{
			Console.WriteLine(_description);
		}

		public string Name
		{
			get
			{
				return _name;
			}
		}
	}

	public abstract class Potion : Item
	{
		public int quantity = 5;

		public Type type;

		public Potion(string name, string description, Type type) : base(name, description)
		{
			this.type = type;
		}

		public abstract override void Use(Player player);

		public enum Type
		{
			Heal,
			Regenerate,
			DelHarmfulEffects
		}
	}

	public class UsableItem : Item
	{

		public UsableItem(string name, string description) : base(name, description)
		{

		}

		public override void Use(Player player)
		{

		}
	}
}