using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace PracticTask1
{
    class Program
    {
        static void Main(string[] args)
        {
            SetVacaion();
        }
            static void SetVacaion()
            {
                var VacationDictionary = new Dictionary<string, List<DateTime>>()
                {
                    ["Иванов Иван Иванович"] = new List<DateTime>(),
                    ["Петров Петр Петрович"] = new List<DateTime>(),
                    ["Юлина Юлия Юлиановна"] = new List<DateTime>(),
                    ["Сидоров Сидор Сидорович"] = new List<DateTime>(),
                    ["Павлов Павел Павлович"] = new List<DateTime>(),
                    ["Георгиев Георг Георгиевич"] = new List<DateTime>()
                };
                List<string> AviableWorkingDaysOfWeekWithoutWeekends = new List<string>() { "Monday", "Tuesday", "Wednesday", "Thursday", "Friday" };
                // Список отпусков сотрудников
                List<DateTime> Vacations = new List<DateTime>();
                var AllVacationCount = 0;
                List<DateTime> dateList = new List<DateTime>();
                List<DateTime> SetDateList = new List<DateTime>();

                foreach (var VacationList in VacationDictionary)
                {
                    Random gen = new Random();
                    Random step = new Random();
                    DateTime start = new DateTime(DateTime.Now.Year, 1, 1);
                    DateTime end = new DateTime(DateTime.Today.Year, 12, 31);
                    dateList = VacationList.Value;
                    int vacationCount = 28;
                    int range = (end - start).Days;
                    var startDate = start.AddDays(gen.Next(range));
                    if (!AviableWorkingDaysOfWeekWithoutWeekends.Contains(startDate.DayOfWeek.ToString()))
                    {
                        int day = (int)startDate.DayOfWeek;
                        if (day == 6) startDate = startDate.AddDays(2);
                        if (day == 0) startDate = startDate.AddDays(1);
                    }
                    if (AviableWorkingDaysOfWeekWithoutWeekends.Contains(startDate.DayOfWeek.ToString()))
                    {
                        int[] vacationSteps = { 7, 14 };
                        int vacIndex = gen.Next(vacationSteps.Length);
                        var endDate = new DateTime(DateTime.Now.Year, 12, 31);
                        int difference = 0;
                        if (vacationCount < 7) vacationCount = 7;
                        if (vacationSteps[vacIndex] == 7)
                        {
                            endDate = startDate.AddDays(7);
                            difference = 7;
                        }
                        else if (vacationSteps[vacIndex] == 14)
                        {
                            endDate = startDate.AddDays(14);
                            difference = 14;
                        }
                        // Проверка условий по отпуску
                        bool CanCreateVacation = false;
                        bool existStart = false;
                        bool existEnd = false;
                        while (Vacations.Any(element => element.AddDays(3) >= startDate && element.AddDays(3) <= endDate) || Vacations.Any(element => element >= startDate && element <= endDate))
                        {
                            startDate = startDate.AddDays(15);
                            endDate = endDate.AddDays(15);
                        }
                        if (!Vacations.Any(element => element >= startDate && element <= endDate))
                        {
                            if (!Vacations.Any(element => element.AddDays(3) >= startDate && element.AddDays(3) <= endDate))
                            {
                                existStart = dateList.Any(element => element.AddMonths(1) >= startDate && element.AddMonths(1) >= endDate);
                                existEnd = dateList.Any(element => element.AddMonths(-1) <= startDate && element.AddMonths(-1) <= endDate);
                                if (!existStart || !existEnd)
                                    CanCreateVacation = true;
                            }
                        }

                        if (CanCreateVacation)
                        {
                            for (DateTime dt = startDate; dt < endDate; dt = dt.AddDays(1))
                            {
                                Vacations.Add(dt);
                                dateList.Add(dt);
                            }
                            AllVacationCount++;
                            vacationCount -= vacationCount - difference;
                        }
                    }

                }
                foreach (var VacationList in VacationDictionary)
                {
                    SetDateList = VacationList.Value;
                    Console.WriteLine("Дни отпуска " + VacationList.Key + " : ");
                    for (int i = 0; i < SetDateList.Count; i++) { Console.WriteLine(SetDateList[i]); }
                }
            Console.ReadLine();
        }
    }
    }

