﻿using AMR_Engine;

namespace FAO_InFARM_Library
{
	internal class DataFields
	{
		public class DataField
		{
			public DataField(string infarm_Name_, string whonet_Name_)
			{
				InFARM_Name = infarm_Name_;
				WHONET_Name = whonet_Name_;
			}

			public readonly string InFARM_Name;
			public readonly string WHONET_Name;
		}

		public static readonly DataField MICROORG = 
			new(nameof(MICROORG), "ORGANISM");

		public static readonly DataField GUIDELINE =
			new(nameof(GUIDELINE), string.Empty);

		public static readonly DataField MET_AST =
			new(nameof(MET_AST), string.Empty);

		public static readonly List<string> InFARM_Antibiotics = 
			new List<string>() { 
				"AMC", "AMX", "AMP", "AZM", "PEN", "CHL", "CIP", "CLI", "COL", "FEP", "CTX",
				"TIO", "CRO", "CAZ", "LEX", "CZO", "FOX", "DAN", "DAP", "DOX", "ENR", "ETP",
				"ERY", "FLR", "FOS", "GEN", "IPM", "KAN", "LNZ", "MAR", "MEM", "NAL", "NEO",
				"NOR", "OXA", "OXY", "QDA", "RIF", "SOX", "SMX", "SPT", "STR", "TEC", "TCY",
				"TIA", "TGC", "TIL", "TMP", "SXT", "TUL", "TYL", "VAN"
			};

		/// <summary>
		/// These InFARM fields can vary within an isolate (one isolate may have many rows).
		/// </summary>
		public static readonly List<string> AntibioticFields = 
			InFARM_Antibiotics.Select(a => GetInFARM_DrugName(a, true)).ToList().
			Concat(InFARM_Antibiotics.Select(a => GetInFARM_DrugName(a, false))).ToList().
			Concat([GUIDELINE.InFARM_Name, MET_AST.InFARM_Name]).ToList();

		/// <summary>
		/// Determine the corresponding WHONET drug column code given the inputs.
		/// </summary>
		/// <param name="drugCode"></param>
		/// <param name="guideline"></param>
		/// <param name="testMethod"></param>
		/// <returns></returns>
		public static string GetWHONET_DrugCode(string drugCode, string guideline, string testMethod)
		{
			char guideLineChar;
			switch (guideline)
			{
				case "CLSI":
					guideLineChar = 'N';
					break;

				case "EUCAST":
					guideLineChar = 'E';
					break;

				default:
					return string.Empty;
			}

			string testMethodAndPotency;
			switch (testMethod)
			{
				case "AUTO":
				case "BD":
				case "BMICRO":
					testMethodAndPotency = "M";
					break;
					
				case "CGT":
					testMethodAndPotency = "E";
					break;

				case "DD":
					testMethodAndPotency = "D";

					// Fetch the disk potency or abort.
					Antibiotic? matchingDrug =
						Antibiotic.AllAntibiotics.
						FirstOrDefault(a => a.WHONET_ABX_CODE == drugCode && ((guideLineChar == 'N' && a.CLSI) || (guideLineChar == 'E' && a.EUCAST)));

					if (matchingDrug is null)
						return string.Empty;

					string tempPotency =
						matchingDrug.POTENCY.
						Replace("µg", string.Empty).
						Replace("units", string.Empty).
						Replace(".", "_");

					if (tempPotency.Contains("/"))
						tempPotency = tempPotency.Substring(0, tempPotency.IndexOf("/"));
						
					testMethodAndPotency += tempPotency;

					break;

				default: 
					return string.Empty;
			}

			// Return the drug code in the WHONET format.
			return string.Format("{0}_{1}{2}", drugCode, guideLineChar, testMethodAndPotency);
		}

		/// <summary>
		/// Given a WHONET drug code, return the corresponding InFARM codes so that we can determine
		/// which InFARM row the result belongs to.
		/// </summary>
		/// <returns></returns>
		public static Tuple<string, HashSet<string>>? GetInFARM_GuidelineAndMethod(string whonetDrugCode)
		{
			// Guideline is always at index 4.
			string infarmGuideline;
			char guideLineChar = whonetDrugCode[4];
			switch (guideLineChar)
			{
				case 'N':
					infarmGuideline = "CLSI";
					break;

				case 'E':
					infarmGuideline = "EUCAST";
					break;

				default:
					return null;
			}

			// Test method is always at index 5.
			char testMethod = whonetDrugCode[5];
			HashSet<string> infarmTestMethods = new();
			switch (testMethod)
			{
				case 'M':
					// We will look for rows matching any of these. There should only be one match in practice.
					infarmTestMethods.Add("AUTO");
					infarmTestMethods.Add("BD");
					infarmTestMethods.Add("BMICRO");
					break;

				case 'E':
					infarmTestMethods.Add("CGT");
					break;

				case 'D':
					infarmTestMethods.Add("DD");
					break;

				default:
					return null;
			}

			return new Tuple<string, HashSet<string>>(infarmGuideline, infarmTestMethods);
		}

		/// <summary>
		/// Each drug has measurement (VALUE_XXX) and interpretation (INT_XXX) fields.
		/// </summary>
		/// <param name="drugCode"></param>
		/// <param name="measurementColumn"></param>
		/// <returns></returns>
		public static string GetInFARM_DrugName(string drugCode, bool measurementColumn)
		{
			if (measurementColumn)
				return string.Format("VALUE_{0}", drugCode);
			else
				return string.Format("INT_{0}", drugCode);
		}
	}
}
