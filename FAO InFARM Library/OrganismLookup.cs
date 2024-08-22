namespace FAO_InFARM_Library
{
	internal class OrganismLookup
	{
		/// <summary>
		/// Map the InFARM code set to the WHONET organism codes.
		/// </summary>
		public static readonly Dictionary<string, string> InFARM_To_WHONET_Map = new()
		{
			["ESCCOL"] = "eco",
			["SALSPP"] = "sal",
			["ACTPLE"] = "apl",
			["ACIBAU"] = "aba",
			["AERCAV"] = "acv",
			["AERHYD"] = "aeh",
			["AERSAL"] = "asa",
			["AERSOB"] = "aso",
			["AERSPP"] = "aer",
			["AERVER"] = "ave",
			["AVBPAR"] = "hpg",
			["CAMCOL"] = "cco",
			["CAMJEJ"] = "caj",
			["CAMSPP"] = "cam",
			["EDWANG"] = "eda",
			["EDWICT"] = "eic",
			["EDWPIS"] = "edc",
			["EDWSPP"] = "edw",
			["EDWTAR"] = "eta",
			["ENTFCL"] = "efa",
			["ENTFCM"] = "efm",
			["ENTSPP"] = "ent",
			["KLEPNE"] = "kpn",
			["LISMON"] = "lmo",
			["MANHAE"] = "pha",
			["MYCGAL"] = "mgs",
			["MYCHYO"] = "myh",
			["MYCSPP"] = "myp",
			["PASMUL"] = "pam",
			["PSEUAER"] = "pae",
			["PSEUSPP"] = "ps-",
			["STAAUR"] = "sau",
			["STAHYI"] = "shy",
			["STAPSE"] = "psd",
			["STREPAGA"] = "sgc",
			["STREPDYS"] = "sdy",
			["STREPIN"] = "std",
			["STREPHO"] = "srh",
			["STREPSPP"] = "str",
			["STREPSUI"] = "sui",
			["STREPUBE"] = "sub",
			["VIBALG"] = "val",
			["VIBANG"] = "via",
			["VIBCHO"] = "vic",
			["VIBPAR"] = "vip",
			["VIBSPP"] = "vi-",
			["VIBVUL"] = "vvu",
			["YERRUS"] = "yru"
		};
	}
}
