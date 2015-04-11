using System;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Threading;

namespace LSCoder.CodeJam
{
	class MainClass
	{
		static void Main(string[] args)
		{
			Thread.CurrentThread.CurrentCulture = new CultureInfo("en-us");
			
			var fileManager = new FileManager();
			var inputFileSelector = new AsciiMenuFileSelector(fileManager, Console.In, Console.Out);
			
			var problemInputFile = inputFileSelector.Select();
			if (problemInputFile == null)
				return;
			
			var scanner = fileManager.OpenInputFileScanner(problemInputFile);
			var outputFile = fileManager.CreateOutputFile(problemInputFile);
			
			Solve(scanner, outputFile);
			
			scanner.Dispose();
			outputFile.Dispose();
			
			Console.WriteLine(string.Format("\nFile `{0}` created!", problemInputFile.Name));
			
			Console.ReadKey(true);
		}
		
		private static void Solve(Scanner scanner, TextWriter outputFile)
		{
			var testCasesCount = scanner.ReadInt();
			
			Console.WriteLine(string.Format("\nRunning `{0}` test cases...\n", testCasesCount));
			
			for (var testCaseId = 1; testCaseId <= testCasesCount; testCaseId++)
			{
				var result = Solve(scanner);
				WriteResult(testCaseId, result, outputFile);
			}
		}
		
		private static string Solve(Scanner scanner)
		{
			var lineTokens = scanner.ReadLineTokens();
			var peopleCount = Int32.Parse(lineTokens[0]) + 1;
			var peopleShyness = lineTokens[1];
			var stoodUpCount = peopleShyness[0] - '0';
			var friendsCount = 0;

			for (var i = 1; i < peopleCount; i++) {
				if(stoodUpCount < i) {
					var newFriends = i - stoodUpCount;
					friendsCount += newFriends;
					stoodUpCount += newFriends;
				}
				stoodUpCount += peopleShyness[i] - '0';
			}

			return friendsCount.ToString();
		}
		
		private static void WriteResult(int testCaseId, string result, TextWriter outputFile)
		{
			var formatedLine = string.Format("Case #{0}: {1}", testCaseId, result);
			
			Console.WriteLine(formatedLine);
			outputFile.WriteLine(formatedLine);
		}
	}
}
