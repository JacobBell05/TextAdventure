﻿using System;
using System.Collections;
using System.Collections.Generic;

namespace TextAdventureGame
{
	public class Program
	{
		static void Main(string[] args)
		{
			bool running = false;
			var clock = new GameClock();
			while (running)
			{
				clock.OutputCurrentTime();
				if (Console.ReadLine() == "y")
				{
					Console.WriteLine("Which format would you like the time to be displayed as:\n\n	1. 12 hour\n	2. 24 hour");
					string choice = Console.ReadLine();
					if (choice == "1")
						clock.Format = GameClock.TimeFormat.TwelveHourTime;
					else if (choice == "2")
						clock.Format = GameClock.TimeFormat.TwentyFourHourTime;
					else
						Console.WriteLine("Invalid input");
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
			int timeToAdd = 24 - _hour; // Removes time from the rest of the day from the increment
			_hour += increment;

			if (_hour > 23) // If it is the end of the day
			{
				_day++;
				_hour = increment == 1 ? 0 : increment - timeToAdd; // If player has slept
			}

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

		public Player()
		{
			Console.WriteLine("You exist now bitch");
		}
	}
}
