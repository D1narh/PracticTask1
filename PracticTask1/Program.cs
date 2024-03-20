using System;
using System.Collections.Generic;
using System.Linq;

namespace PracticTask1
{
	class Program
	{
		static void Main(string[] args)
		{
			var vacationDictionary = new Dictionary<string, List<DateTime>>()
			{
				["Иванов Иван Иванович"] = new List<DateTime>(),
				["Петров Петр Петрович"] = new List<DateTime>(),
				["Юлина Юлия Юлиановна"] = new List<DateTime>(),
				["Сидоров Сидор Сидорович"] = new List<DateTime>(),
				["Павлов Павел Павлович"] = new List<DateTime>(),
				["Георгиев Георг Георгиевич"] = new List<DateTime>()
			};
			var availableWorkingDaysOfWeekWithoutWeekends = new List<string>() { "Monday", "Tuesday", "Wednesday", "Thursday", "Friday" };
			var vacations = new List<DateTime>();
			var allVacationCount = 0;

			Random gen = new Random();
			DateTime start = new DateTime(DateTime.Now.Year, 1, 1);
			DateTime end = new DateTime(DateTime.Today.Year, 12, 31);

			foreach (var vacationList in vacationDictionary)
			{
				var dateList = vacationList.Value;

				if (dateList.Any())
					continue; // Пропустить, если у сотрудника уже есть отпуск

				int vacationCount = 7; // Отпуск длится максимум 14 дней

				while (vacationCount > 0)
				{
					int range = (end - start).Days;
					var startDate = start.AddDays(gen.Next(range));

					if (availableWorkingDaysOfWeekWithoutWeekends.Contains(startDate.DayOfWeek.ToString()))
					{
						string[] vacationSteps = { "7", "14" };
						int vacIndex = gen.Next(vacationSteps.Length);
						var endDate = startDate.AddDays(int.Parse(vacationSteps[vacIndex]));

						if (endDate > end)
							endDate = end;

						bool canCreateVacation = true;
						if (canCreateVacation)
						{
							for (DateTime dt = startDate; dt < endDate; dt = dt.AddDays(1))
							{
								vacations.Add(dt);
								dateList.Add(dt);
							}
							allVacationCount++;
						}

						if (canCreateVacation)
						{
							for (DateTime dt = startDate; dt <= endDate; dt = dt.AddDays(1))
							{
								vacations.Add(dt);
								dateList.Add(dt);
							}
							allVacationCount += (endDate - startDate).Days + 1;
							vacationCount -= (endDate - startDate).Days + 1;
						}
					}
				}
			}

			foreach (var vacationList in vacationDictionary)
			{
				var setDateList = vacationList.Value.ToList();
				Console.WriteLine("Дни отпуска " + vacationList.Key + " : ");
				foreach (var date in setDateList)
				{
					Console.WriteLine(date);
				}
			}

			Console.ReadKey();
		}
	}
}