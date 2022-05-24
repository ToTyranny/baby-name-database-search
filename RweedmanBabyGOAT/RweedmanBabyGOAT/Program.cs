using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using System.IO;
/* Robert Jesse Weedman
 * CSC 3020
 * Assignment 2
 * 07-19-2020 (late under agreement)
 * 
 */

namespace RweedmanBabyGOAT
{
	class Program
	{
		static Dictionary<string, BabyNamesData> bNames = new Dictionary<string, BabyNamesData>();
		static void Main(string[] args)
		{
			// The years to be ran are input.
			ReadYears(1880, 2016);

			//This function was just for testing.
			//PrintBabyNames();

			// Part 1 and 2.
            HighestCountTotal();

			// Part 3.
            LowestMaleNearHundred();

			// Part 4.
            NameVersusName("Vegeta", "Goku");

			// Part 5.
            NameVersusName("Daenerys", "Hermoine");

			// Part 6.
            LowAndUnique();

			// Part 7.
            RankOfName("Robert");
			// My middle name.
			RankOfName("Jesse");

			// This holds the results.
            Console.ReadKey();

		}
		
		// This reads a single year's file.
		static void ReadYear(int year)
		{
			
			using (StreamReader reader = new StreamReader($"C:\\Users\\totyr\\OneDrive\\Desktop\\CS3020 C# and .NET\\names\\names\\yob{year.ToString()}.txt"))
			{
				string line;
				reader.ReadLine();
				while ((line = reader.ReadLine()) != null)
				{
					// name = data[0]; gender = data[1]; count = data[2];
					var data = line.Split(',');

					// Taken from lecture 9,
					// the count is split up into M and F sections.
					if (bNames.ContainsKey(data[0]))
					{
						if (data[1].Equals("F"))
						{
							bNames[data[0]].FemaleInstances += int.Parse(data[2]);
						}
						else
						{
							bNames[data[0]].MaleInstances += int.Parse(data[2]);

						}
					}
					else
					{
						if (data[1].Equals("F"))
						{
							bNames.Add(data[0], new BabyNamesData(0, int.Parse(data[2])));
						}
						else
						{
							bNames.Add(data[0], new BabyNamesData(int.Parse(data[2]), 0));
						}

					}
				}
			}
		}

		// All the years are read and saved.
		static void ReadYears(int yearStart, int yearEnd)
		{
			for (int year = yearStart; year <= yearEnd; year++)
			{
				ReadYear(year);
			}
		}

		// This function was used to print names so I knew what I was doing
		// while programming.
		static void PrintBabyNames()
		{
			var babyCount = from results in bNames
							where results.Value.SumInstances > 99
							orderby results.Value.SumInstances
							select results;

			foreach (KeyValuePair<string, BabyNamesData> b in babyCount)
			{
				if (b.Value.SumInstances >= 100 && b.Value.SumInstances < 105)
                {
					Console.WriteLine($"{ b.Key} {b.Value.SumInstances}");
				}
			}

		}

		// This function finds part one and part two of the Assignment.
		// - Find the highest name coounts for total and male & female.
		static void HighestCountTotal()
		{
			string highestCountName = "Robert";
			double highestCountM = 100;
			double highestCountF = 100;
			double highestCountT = 100;
			string highestCountMaleName = "Roberto";
			string highestCountFemaleName = "Roberta";

			foreach (KeyValuePair<string, BabyNamesData> b in bNames)
			{
				if (b.Value.MaleInstances > highestCountM)
				{
					highestCountM = b.Value.MaleInstances;
					highestCountMaleName = b.Key;


					if (b.Value.FemaleInstances > highestCountF)
					{
						highestCountF = b.Value.FemaleInstances;
						highestCountFemaleName = b.Key;
					}

					if (b.Value.SumInstances > highestCountT)
					{
						highestCountT = b.Value.SumInstances;
						highestCountName = b.Key;
					}


				}

			}
			Console.WriteLine($"{highestCountName} has a highest total name count of: {highestCountT}.");
			Console.WriteLine($"{highestCountMaleName} has a highest male name count of: {highestCountM}.");
			Console.WriteLine($"{highestCountFemaleName} has a highest female name count of: {highestCountF}.");

		}

		// This function finds the least popular male name of count 100 or more.
		static void LowestMaleNearHundred()
		{
			string lowestCountMaleName = "Roberto";
			double lowestMaleCount = 110;
			double lowestLimit = 100;

			foreach (KeyValuePair<string, BabyNamesData> b in bNames)
			{
				if (b.Value.MaleInstances >= lowestLimit)
				{
					if (b.Value.MaleInstances < lowestMaleCount)
					{
						lowestMaleCount = b.Value.MaleInstances;
						lowestCountMaleName = b.Key;
					}

				}

			}
			Console.WriteLine($"{lowestCountMaleName} is the least popular male name of at least 100 count at a count of {lowestMaleCount}.");
		}

		// This functions compares names. Each count is also given when found.
		static void NameVersusName(string name, string name2)
        {
			double nameOneStore = 0;
			double nameTwoStore = 0;
			foreach (KeyValuePair<string, BabyNamesData> b in bNames)
            {
				if (b.Key == $"{name}")
                {
					nameOneStore = b.Value.SumInstances;
                }
				if (b.Key == $"{name2}")
				{
					nameTwoStore = b.Value.SumInstances;
				}
			}
			if (nameOneStore > nameTwoStore)
            {
				Console.WriteLine($"{name} ({nameOneStore}) has a higher total than {name2} ({nameTwoStore}).");

			}
            else if (nameOneStore < nameTwoStore)
			{
				Console.WriteLine($"{name2} ({nameTwoStore}) has a higher total than {name} ({nameOneStore}).");
			}
            else
            {
				Console.WriteLine($"{name} ({nameOneStore}) has an equal total to {name2} ({nameTwoStore}).");

			}

		}

		// This function finds the lowest name with the most unique count total.
		static void LowAndUnique()
        {
			// Numbers are stored at 100 to ease processing.
			double numberStoreOne = 100;
			double numberStoreTwo = 100;
			double numberStoreThree = 100;
			// This stores the number of the unique name once found.
			double uniqueNameCount = 100;

			// Strings are initaillized to something it can't be.
			string uniqueName = "Robert";
			string uniqueNameCheck = "Roberto";

			// 'int count' allows me to skip saving every name on the third count.
			// I needed a 3-2-1 pattern to analyze the 1st, 2nd, and 3rd dictionary entries.
			// 'int count' allows me to do that.
			int count = 0;

			// 'int countBlock' allows me to save only the first name
			// of ansending order to memory.
			int countBlock = 0;

			// The names are ordered by ansending order based on total count.
			var babyCount = from results in bNames
							where results.Value.SumInstances > 100
		                    orderby results.Value.SumInstances
		                    select results;

			// Please notice only one foreach loop is used.
			foreach (KeyValuePair<string, BabyNamesData> aOrder in babyCount)
			{
				// Stores the third entries once the mod check on count passes (every two loops).
				numberStoreOne = aOrder.Value.SumInstances;
				count++;
				if (numberStoreOne > numberStoreTwo)
				{

						if (numberStoreTwo > numberStoreThree && countBlock < 1)
						{
							uniqueName = uniqueNameCheck;
							uniqueNameCount = numberStoreTwo;
							countBlock++;
											
						}
					
						}
				// Stores the second entries once the mod check on count passes.
				numberStoreTwo = aOrder.Value.SumInstances;
				uniqueNameCheck = aOrder.Key;
				if (count % 2 == 0)
                {
					// The mod forces this int to save the first entries that are to be compared.
					numberStoreThree = aOrder.Value.SumInstances;
				}
				
			}
			
			// Once the result is found, it is printed.
			Console.WriteLine($"{uniqueName} has the lowest unique number count at {uniqueNameCount}.");
		}

		// This function finds name rank.
		static void RankOfName(string name)
		{
			double count = 1;
			double nameOneStore = 0;

			// The name the user entered is found in the dictionary.
			foreach (KeyValuePair<string, BabyNamesData> b in bNames)
			{
				if (b.Key == $"{name}")
				{
					nameOneStore = b.Value.SumInstances;
				}
			}
			// count Stores the rank without having to use a sorted dictionary.
			foreach (KeyValuePair<string, BabyNamesData> b in bNames)
			{
				if (b.Value.SumInstances > nameOneStore)
				{
					count++;
				}

			}
			Console.WriteLine($"{name} is rank {count}, total of times named being {nameOneStore}.");
		}
	}
}
