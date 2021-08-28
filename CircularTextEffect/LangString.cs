using System.Globalization;

namespace CircularTextEffect
{
	internal static class LangString
	{
		private static readonly string UICulture = CultureInfo.CurrentUICulture.Name;

		internal static string DefaultString
		{
			get
			{
				string uICulture = UICulture;
				if (uICulture == "ro")
				{
					return "TASTEAZĂ TEXTUL";
				}
				return "TYPE YOUR TEXT";
			}
		}
	}
}
