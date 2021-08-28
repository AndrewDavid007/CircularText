using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Text;
using System.Windows.Forms;
using ControlExtensions;
using OptionControls;

namespace CircularTextEffect
{
	internal class OptionFontNameDropDown : OptionControl
	{
		private static class FontUtil
		{
			internal static readonly string[] UsableFontFamilies;

			internal static int FindFontIndex(string familyName)
			{
				for (int i = 0; i < UsableFontFamilies.Length; i++)
				{
					if (UsableFontFamilies[i].Equals(familyName, StringComparison.OrdinalIgnoreCase))
					{
						return i;
					}
				}
				return 0;
			}

			static FontUtil()
			{
				List<string> list = new List<string>();
				using (InstalledFontCollection installedFontCollection = new InstalledFontCollection())
				{
					FontFamily[] families = installedFontCollection.Families;
					foreach (FontFamily fontFamily in families)
					{
						if (fontFamily.IsStyleAvailable(FontStyle.Regular))
						{
							list.Add(fontFamily.Name);
						}
					}
				}
				UsableFontFamilies = list.ToArray();
			}
		}

		private readonly Button _resetButton;

		private readonly ControlFontComboBox _fontNameComboBox;

		private string _fontName;

		private string _defaultFontName;

		private int defaultIndex;

		public OptionFontNameDropDown(Enum optId, OptionContext optContext)
			: this(optId, optContext, "Arial")
		{
		}

		public OptionFontNameDropDown(Enum optId, OptionContext optContext, string defaultFontName)
			: base(optId, optContext)
		{
			_defaultFontName = defaultFontName;
			SuspendLayout();
			_resetButton = CreateResetButton();
			ControlFontComboBox fontNameComboBox = new ControlFontComboBox
			{
				Name = "fontNameComboBox",
				FlatStyle = FlatStyle.System,
				DropDownStyle = ComboBoxStyle.DropDownList
			};
			defaultIndex = FontUtil.FindFontIndex(defaultFontName);
			_fontNameComboBox = fontNameComboBox;
			_fontNameComboBox.LoadFontFamilies();
			_fontNameComboBox.SelectedIndexChanged += FontNameComboBox_SelectedIndexChanged;
			base.Controls.AddRange(new Control[5] { _displayNameControl, _labelControl, _fontNameComboBox, _resetButton, _descriptionControl });
			OnReset();
			ResumeLayout(performLayout: false);
			PerformLayout();
		}

		private void FontNameComboBox_SelectedIndexChanged(object sender, EventArgs e)
		{
			int selectedIndex = _fontNameComboBox.SelectedIndex;
			if (selectedIndex >= 0 && selectedIndex < _fontNameComboBox.Items.Count)
			{
				_fontName = (string)_fontNameComboBox.Items[selectedIndex];
			}
			else
			{
				_fontName = "";
			}
			OnValueChanged();
		}

		protected override void OnLayout(LayoutEventArgs e)
		{
			int vPos = LayoutBegin();
			Rectangle bounds;
			int num = LayoutLabel(vPos, out bounds);
			int num2 = LayoutResetButton(_resetButton, vPos);
			_fontNameComboBox.Location = new Point(num, vPos);
			_fontNameComboBox.Width = num2 - num;
			_fontNameComboBox.PerformLayout();
			bounds.Y += (_fontNameComboBox.Height - bounds.Height) / 2;
			_labelControl.Bounds = bounds;
			_resetButton.Height = _fontNameComboBox.Height;
			LayoutEnd(_fontNameComboBox.Bottom, _resetButton.Bottom);
			base.OnLayout(e);
		}

		protected override void OnReset()
		{
			_fontNameComboBox.SelectedIndex = defaultIndex;
		}

		public override void OptionDefaultToValues(OptionDictionary values)
		{
			values[base.Id] = FontUtil.UsableFontFamilies[defaultIndex];
		}

		public override void OptionToValues(OptionDictionary values)
		{
			values[base.Id] = FontUtil.UsableFontFamilies[_fontNameComboBox.SelectedIndex];
		}

		public override void ValuesToOption(OptionDictionary values)
		{
			string familyName = (string)values[base.Id];
			_fontNameComboBox.SelectedIndex = FontUtil.FindFontIndex(familyName);
		}

		internal static FontFamily GetOptionValue(Enum optId, OptionDictionary values)
		{
			return new FontFamily(Convert.ToString(values[optId.ToString()]));
		}
	}
}
