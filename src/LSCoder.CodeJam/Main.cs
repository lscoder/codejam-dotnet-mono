using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
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
			
			Trace.WriteLine(string.Format("\nFile `{0}` created!", problemInputFile.Name));
			
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
			var workers = scanner.ReadInt ();
			var jobs = scanner.ReadLineTokens ().Select (Int32.Parse).ToArray ();

			int max = jobs[0];
			for (int i = 1; i < workers; i++) {
				max = Math.Max (max, jobs[i]);
			}

			//Console.WriteLine ("max: " + max);
			
			var best = max;

			for (int i = 1; i < max; i++) {
				//Console.WriteLine("i: " + i);
				var time = 0;
				for(int j = 0; j < workers; j++) {
					//Console.WriteLine("\t" + j + " = " + jobs[j] + " => " + (jobs[j] / i));
					time += jobs[j] / i;

					if((jobs[j] % i) == 0) {
						time--;
					}
				}

				//Console.WriteLine("\ttime: " + (time + i));
				best = Math.Min(best, time + i);
			}

			return best.ToString();
		}

		private static void WriteResult(int testCaseId, string result, TextWriter outputFile)
		{
			var formatedLine = string.Format("Case #{0}: {1}", testCaseId, result);
			
			Console.WriteLine(formatedLine);
			outputFile.WriteLine(formatedLine);
		}
	}
}
