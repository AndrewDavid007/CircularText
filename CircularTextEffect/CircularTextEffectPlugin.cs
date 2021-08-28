using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Text;
using OptionBased.Effects;
using OptionControls;
using PaintDotNet;
using PaintDotNet.Effects;

namespace CircularTextEffect
{
	[PluginSupportInfo(typeof(PluginSupportInfo), DisplayName = "Circular Text")]
	public class CircularTextEffectPlugin : OptionBasedEffect
	{
		private enum OptionNames
		{
			MainPanel,
			StandardTab,
			TextBox,
			Font,
			FontStyle,
			FontSize,
			AngleControl,
			VectorPan,
			TextDirection,
			CharSpacing,
			RadiusSize,
			ExpandedMode,
			OptionsTab,
			CirclesThickness,
			InnerPenWidth,
			OuterPenWidth,
			OutlineThickness,
			FillTypeOption,
			Colors,
			FirstColorTab,
			FirstColorWheel,
			SecondColorTab,
			SecondColorWheel,
			InnerCircleColorTab,
			InnerCircleColorWheel,
			OuterCircleColorTab,
			OuterCircleColorWheel,
			OutlineColorTab,
			OutlineColorWheel,
			CreatesMask,
			Test
		}

		private enum FillTypeEnum
		{
			SolidFill,
			BackwardDiagonal,
			Cross,
			DiagonalBrick,
			DiagonalCross,
			Divot,
			DottedGrid,
			HorizontalBrick,
			LargeCheckerBoard,
			Min,
			Shingle,
			Plaid,
			SolidDiamond
		}

		private enum FontStyleEnum
		{
			Bold,
			Underline,
			Italic,
			Strikeout
		}

		private enum DrawingOptionEnum
		{
			AntiClockwise,
			DrawCircles
		}

		private static readonly Image StaticIcon = new Bitmap(typeof(CircularTextEffectPlugin), "CircularText.png");

		private string Amount1 = "";

		private FontFamily Amount2 = new FontFamily("Arial");

		private bool Amount3 = false;

		private bool Amount4 = false;

		private double Amount5 = 35.0;

		private ColorBgra Amount6 = ColorBgra.FromBgra(byte.MaxValue, 0, byte.MaxValue, byte.MaxValue);

		private double Amount7 = 0.0;

		private double Amount8 = 1.0;

		private double Amount9 = 0.7;

		private bool Amount10 = false;

		private ColorBgra Amount12 = ColorBgra.FromBgra(byte.MaxValue, 0, 0, 0);

		private double Amount13 = 0.0;

		private double Amount14 = 0.0;

		private ColorBgra Amount15 = ColorBgra.FromBgra(byte.MaxValue, 0, 0, 0);

		private int Amount16 = 5;

		private ColorBgra Amount17 = ColorBgra.FromBgra(byte.MaxValue, byte.MaxValue, byte.MaxValue, byte.MaxValue);

		private bool Amount18 = false;

		private ColorBgra Amount19 = ColorBgra.FromBgra(byte.MaxValue, byte.MaxValue, byte.MaxValue, byte.MaxValue);

		private float Amount20 = 3f;

		private bool Amount21 = false;

		private ColorBgra Amount22 = ColorBgra.FromBgra(byte.MaxValue, byte.MaxValue, byte.MaxValue, byte.MaxValue);

		private ColorBgra Amount23 = ColorBgra.FromBgra(byte.MaxValue, 0, 0, 0);

		private byte Amount24;

		private bool Amount25 = false;

		private int Amount26 = 5;

		private bool Amount30 = false;

		private Surface canvaSurface;

		public CircularTextEffectPlugin()
			: base(typeof(CircularTextEffectPlugin), StaticIcon, EffectFlags.Configurable)
		{
		}

		protected override ConfigurationOfUI OnCustomizeUI()
		{
			return new ConfigurationOfUI
			{
				PropertyBasedLook = false
			};
		}

		protected override ConfigurationOfDialog OnCustomizeDialog()
		{
			return new ConfigurationOfDialog
			{
				Title = "Circular Text",
				OKText = "",
				IsSizable = true,
				WidthScale = 1.0,
				OptionFocusedOnActivation = OptionNames.TextBox
			};
		}

		protected override OptionControlList OnSetupOptions(OptionContext optContext)
		{
			Image image = new Bitmap(typeof(CircularTextEffectPlugin), "InnerCircleTabIcon.png");
			Image image2 = new Bitmap(typeof(CircularTextEffectPlugin), "OuterCircleTabIcon.png");
			bool[] array = new bool[4];
			return new OptionControlList
			{
				new OptionPanelPagesAsTabs(OptionNames.MainPanel, optContext)
				{
					new OptionPanelPage(OptionNames.StandardTab, optContext)
					{
						new OptionStringEditBox(OptionNames.TextBox, optContext, LangString.DefaultString)
						{
							Rows = 2,
							ShowResetButton = false
						},
						new OptionFontNameDropDown(OptionNames.Font, optContext)
						{
							ShowResetButton = false,
							Visible = true
						},
						new OptionEnumCheckBoxes<FontStyleEnum>(OptionNames.FontStyle, optContext)
						{
							Columns = 2,
							ShowResetButton = false,
							Indent = 0,
							Visible = true
						},
						new OptionDoubleSlider(OptionNames.FontSize, optContext, 35.0, 5.0, 800.0)
						{
							UpDownIncrement = 0.1,
							SliderScaleExponential = false,
							SliderScaleZoom = false,
							DecimalPlaces = 2,
							SliderShowTickMarks = false,
							SliderSmallChange = 0.1,
							SliderLargeChange = 10.0,
							ShowResetButton = true
						},
						new OptionDoubleRotator(OptionNames.AngleControl, optContext, 0.0, -180.0, 180.0)
						{
							Enabled = true,
							AngleDirectionClockwise = true,
							ShowAngleZeroOnTop = true,
							ShowSemiRotator = false,
							ShowZeroMarker = true,
							SliderShow = true,
							UpDownIncrement = 0.05,
							DecimalPlaces = 2,
							SuppressTokenUpdate = false,
							ShowResetButton = true,
							IndividualResetButtons = false,
							Indent = 0,
							SliderSmallChange = 0.05,
							SliderLargeChange = 5.0,
							GadgetScale = 1.0
						},
						new OptionDoubleVectorPan(OptionNames.VectorPan, optContext, 0.0, -1.0, 1.0, 0.0, -1.0, 1.0)
						{
							StaticBitmapUnderlay = base.EnvironmentParameters.SourceSurface.CreateAliasedBitmap(),
							Indent = 0,
							UpDownIncrementX = 0.01,
							UpDownIncrementY = 0.01,
							ShowLineToCenter = true,
							ViewOffset = 0.0,
							ViewFactor = 1.0,
							DecimalPlaces = 2,
							SliderShowTickMarksX = false,
							SliderShowTickMarksY = false,
							ShowResetButton = false,
							SliderShowTickMarks = false,
							SliderSmallChangeX = 0.1,
							SliderSmallChangeY = 0.1,
							SliderLargeChangeX = 0.25,
							SliderLargeChangeY = 0.25,
							GadgetScale = 1.0,
							Visible = true,
							UpDownIncrement = 0.01,
							ValueX = 0.0,
							ValueY = 0.0
						},
						new OptionBooleanCheckBox(OptionNames.TextDirection, optContext)
						{
							ShowResetButton = false
						},
						new OptionDoubleSlider(OptionNames.CharSpacing, optContext, 1.0, 0.01, 10.0)
						{
							UpDownIncrement = 0.001,
							DecimalPlaces = 3,
							SliderSmallChange = 0.05,
							SliderLargeChange = 0.25,
							ShowResetButton = true,
							Visible = true
						},
						new OptionDoubleSlider(OptionNames.RadiusSize, optContext, 0.7, 0.01, 2.0)
						{
							UpDownIncrement = 0.01,
							DecimalPlaces = 2,
							SliderSmallChange = 0.01,
							SliderLargeChange = 0.05,
							ShowResetButton = true,
							Visible = true
						},
						new OptionBooleanCheckBox(OptionNames.ExpandedMode, optContext)
						{
							ShowResetButton = false
						}
					},
					new OptionPanelPage(OptionNames.OptionsTab, optContext)
					{
						new OptionPanelBox(OptionNames.CirclesThickness, optContext)
						{
							new OptionInt32Slider(OptionNames.OuterPenWidth, optContext, 5, 0, 400)
							{
								UpDownIncrement = 1,
								SliderSmallChange = 1,
								SliderLargeChange = 10,
								ShowResetButton = true
							},
							new OptionInt32Slider(OptionNames.InnerPenWidth, optContext, 5, 0, 400)
							{
								UpDownIncrement = 1,
								SliderSmallChange = 1,
								SliderLargeChange = 10,
								ShowResetButton = true
							}
						},
						new OptionInt32Slider(OptionNames.OutlineThickness, optContext, 5, 0, 100)
						{
							UpDownIncrement = 1,
							SliderLargeChange = 5,
							ShowResetButton = true
						},
						new OptionEnumDropDown<FillTypeEnum>(OptionNames.FillTypeOption, optContext)
						{
							ShowResetButton = false,
							Indent = 0,
							Value = FillTypeEnum.SolidFill
						},
						new OptionPanelPagesAsTabs(OptionNames.Colors, optContext)
						{
							new OptionPanelPage(OptionNames.FirstColorTab, optContext)
							{
								new OptionColorWheel(OptionNames.FirstColorWheel, optContext, Color.Red, (ColorWheelEnum)3)
								{
									DisplayName = ""
								}
							},
							new OptionPanelPage(OptionNames.SecondColorTab, optContext)
							{
								new OptionColorWheel(OptionNames.SecondColorWheel, optContext, Color.Yellow, (ColorWheelEnum)3, readOnly: true)
								{
									DisplayName = ""
								}
							},
							new OptionPanelPage(OptionNames.OutlineColorTab, optContext)
							{
								new OptionColorWheel(OptionNames.OutlineColorWheel, optContext, Color.Black, (ColorWheelEnum)3)
								{
									DisplayName = ""
								}
							},
							new OptionPanelPage(OptionNames.InnerCircleColorTab, optContext, image)
							{
								new OptionColorWheel(OptionNames.InnerCircleColorWheel, optContext, Color.Blue, (ColorWheelEnum)3)
								{
									DisplayName = ""
								}
							},
							new OptionPanelPage(OptionNames.OuterCircleColorTab, optContext, image2)
							{
								new OptionColorWheel(OptionNames.OuterCircleColorWheel, optContext, Color.Green, (ColorWheelEnum)3)
								{
									DisplayName = ""
								}
							}
						},
						new OptionBooleanCheckBox(OptionNames.CreatesMask, optContext)
						{
							ShowResetButton = false
						}
					}
				}
			};
		}

		protected override void OnAdaptOptions()
		{
			Option(OptionNames.FillTypeOption).ValueChanged += FillTypeOption_ValueChanged;
			Option(OptionNames.CreatesMask).ValueChanged += CreatesMask_ValueChanged;
		}

		private void FillTypeOption_ValueChanged(object sender, EventArgs e)
		{
			SecondColorWheel_Rule();
		}

		private void SecondColorWheel_Rule()
		{
			Option(OptionNames.SecondColorWheel).ReadOnly = ((OptionEnumDropDown<FillTypeEnum>)Option(OptionNames.FillTypeOption)).Value == FillTypeEnum.SolidFill || ((OptionBooleanCheckBox)Option(OptionNames.CreatesMask)).Value;
		}

		private void CreatesMask_ValueChanged(object sender, EventArgs e)
		{
			FirstColorWheel_Rule();
			SecondColorWheel_Rule();
			InnerCircleColorWheel_Rule();
			OuterCircleColorWheel_Rule();
			FillType_Rule();
			OutlineColorWheel_Rule();
			OutlineThickness_Rule();
		}

		private void FirstColorWheel_Rule()
		{
			Option(OptionNames.FirstColorWheel).ReadOnly = (((OptionBooleanCheckBox)Option(OptionNames.CreatesMask)).Value ? true : false);
		}

		private void InnerCircleColorWheel_Rule()
		{
			Option(OptionNames.InnerCircleColorWheel).ReadOnly = (((OptionBooleanCheckBox)Option(OptionNames.CreatesMask)).Value ? true : false);
		}

		private void OuterCircleColorWheel_Rule()
		{
			Option(OptionNames.OuterCircleColorWheel).ReadOnly = (((OptionBooleanCheckBox)Option(OptionNames.CreatesMask)).Value ? true : false);
		}

		private void FillType_Rule()
		{
			Option(OptionNames.FillTypeOption).ReadOnly = (((OptionBooleanCheckBox)Option(OptionNames.CreatesMask)).Value ? true : false);
		}

		private void OutlineColorWheel_Rule()
		{
			Option(OptionNames.OutlineColorWheel).ReadOnly = (((OptionBooleanCheckBox)Option(OptionNames.CreatesMask)).Value ? true : false);
		}

		private void OutlineThickness_Rule()
		{
			Option(OptionNames.OutlineThickness).ReadOnly = (((OptionBooleanCheckBox)Option(OptionNames.CreatesMask)).Value ? true : false);
		}

		protected override void OnSetRenderInfo(OptionBasedEffectConfigToken newToken, RenderArgs dstArgs, RenderArgs srcArgs)
		{
			Amount1 = OptionStringEditBox.GetOptionValue(OptionNames.TextBox, newToken.Items);
			Amount2 = OptionFontNameDropDown.GetOptionValue(OptionNames.Font, newToken.Items);
			Amount3 = OptionEnumCheckBoxes<FontStyleEnum>.GetOptionChecked(OptionNames.FontStyle, FontStyleEnum.Bold, newToken.Items);
			Amount4 = OptionEnumCheckBoxes<FontStyleEnum>.GetOptionChecked(OptionNames.FontStyle, FontStyleEnum.Italic, newToken.Items);
			Amount5 = OptionTypeSlider<double>.GetOptionValue(OptionNames.FontSize, newToken.Items);
			Amount6 = OptionColorWheel.GetOptionValue(OptionNames.FirstColorWheel, newToken.Items);
			Amount7 = OptionDoubleRotator.GetOptionValue(OptionNames.AngleControl, newToken.Items);
			Amount8 = OptionTypeSlider<double>.GetOptionValue(OptionNames.CharSpacing, newToken.Items);
			Amount9 = OptionTypeSlider<double>.GetOptionValue(OptionNames.RadiusSize, newToken.Items);
			Amount10 = OptionBooleanCheckBox.GetOptionValue(OptionNames.TextDirection, newToken.Items);
			Amount12 = OptionColorWheel.GetOptionValue(OptionNames.InnerCircleColorWheel, newToken.Items);
			Amount13 = OptionDoubleVectorPan.GetOptionValueX(OptionNames.VectorPan, newToken.Items);
			Amount14 = OptionDoubleVectorPan.GetOptionValueY(OptionNames.VectorPan, newToken.Items);
			Amount15 = OptionColorWheel.GetOptionValue(OptionNames.OuterCircleColorWheel, newToken.Items);
			Amount16 = OptionTypeSlider<int>.GetOptionValue(OptionNames.InnerPenWidth, newToken.Items);
			Amount18 = OptionEnumCheckBoxes<FontStyleEnum>.GetOptionChecked(OptionNames.FontStyle, FontStyleEnum.Underline, newToken.Items);
			Amount19 = OptionColorWheel.GetOptionValue(OptionNames.OutlineColorWheel, newToken.Items);
			Amount20 = OptionTypeSlider<int>.GetOptionValue(OptionNames.OutlineThickness, newToken.Items);
			Amount21 = OptionEnumCheckBoxes<FontStyleEnum>.GetOptionChecked(OptionNames.FontStyle, FontStyleEnum.Strikeout, newToken.Items);
			Amount23 = OptionColorWheel.GetOptionValue(OptionNames.SecondColorWheel, newToken.Items);
			Amount26 = OptionTypeSlider<int>.GetOptionValue(OptionNames.OuterPenWidth, newToken.Items);
			switch (OptionEnumDropDown<FillTypeEnum>.GetOptionValue(OptionNames.FillTypeOption, newToken.Items))
			{
			case FillTypeEnum.SolidFill:
				Amount24 = 0;
				break;
			case FillTypeEnum.BackwardDiagonal:
				Amount24 = 1;
				break;
			case FillTypeEnum.Cross:
				Amount24 = 2;
				break;
			case FillTypeEnum.DiagonalBrick:
				Amount24 = 3;
				break;
			case FillTypeEnum.DiagonalCross:
				Amount24 = 4;
				break;
			case FillTypeEnum.Divot:
				Amount24 = 5;
				break;
			case FillTypeEnum.DottedGrid:
				Amount24 = 6;
				break;
			case FillTypeEnum.HorizontalBrick:
				Amount24 = 7;
				break;
			case FillTypeEnum.LargeCheckerBoard:
				Amount24 = 8;
				break;
			case FillTypeEnum.Min:
				Amount24 = 9;
				break;
			case FillTypeEnum.Plaid:
				Amount24 = 10;
				break;
			case FillTypeEnum.Shingle:
				Amount24 = 11;
				break;
			case FillTypeEnum.SolidDiamond:
				Amount24 = 12;
				break;
			}
			Amount25 = OptionBooleanCheckBox.GetOptionValue(OptionNames.ExpandedMode, newToken.Items);
			Amount30 = OptionBooleanCheckBox.GetOptionValue(OptionNames.CreatesMask, newToken.Items);
			if (Amount30)
			{
				Amount12 = Color.Black;
				Amount15 = Color.Black;
				Amount19 = Color.Black;
				Amount6 = Color.Black;
				Amount23 = Color.Black;
				Amount20 = 0f;
			}
			Rectangle boundsInt = EnvironmentParameters.GetSelectionAsPdnRegion().GetBoundsInt();
			PointF pointF = new PointF(((float)Amount13 + 1f) / 2f * (float)(boundsInt.Right - boundsInt.Left), ((float)Amount14 + 1f) / 2f * (float)(boundsInt.Bottom - boundsInt.Top));
			string text = Amount1 + " ";
			if (canvaSurface == null)
			{
				canvaSurface = new Surface(srcArgs.Surface.Size);
			}
			else
			{
                canvaSurface.Fill(Color.Transparent);
			}
			float num = (float)Amount5;
			float num2 = (Amount25 ? ((float)(Math.Min(boundsInt.Width, boundsInt.Height) / 2 * 2) * (float)Amount9) : ((float)(Math.Min(boundsInt.Width, boundsInt.Height) / 2) * (float)Amount9));
			float num3 = 2f * num2;
			float num4 = (Amount25 ? ((float)(Math.Min(boundsInt.Width, boundsInt.Height) / 2 * 2) * (float)Amount9 + 2.5f * (float)Amount5) : ((float)(Math.Min(boundsInt.Width, boundsInt.Height) / 2) * (float)Amount9 + 2.5f * (float)Amount5));
			float num5 = 2f * num4;
			float num6 = (Amount25 ? ((float)Math.Round((4.0 * Amount13 + 1.0) / 2.0 * (double)(boundsInt.Right - boundsInt.Left) + (double)boundsInt.Left)) : ((float)Math.Round((Amount13 + 1.0) / 2.0 * (double)(boundsInt.Right - boundsInt.Left) + (double)boundsInt.Left)));
			float num7 = (Amount25 ? ((float)Math.Round((4.0 * Amount14 + 1.0) / 2.0 * (double)(boundsInt.Bottom - boundsInt.Top) + (double)boundsInt.Top)) : ((float)Math.Round((Amount14 + 1.0) / 2.0 * (double)(boundsInt.Bottom - boundsInt.Top) + (double)boundsInt.Top)));
			StringFormat stringFormat = new StringFormat();
			stringFormat.Alignment = StringAlignment.Center;
			stringFormat.LineAlignment = StringAlignment.Center;
			FontStyle fontStyle = FontStyle.Regular;
			if (Amount3)
			{
				fontStyle |= FontStyle.Bold;
			}
			if (Amount4)
			{
				fontStyle |= FontStyle.Italic;
			}
			if (Amount18)
			{
				fontStyle |= FontStyle.Underline;
			}
			if (Amount21)
			{
				fontStyle |= FontStyle.Strikeout;
			}
			using (Graphics graphics = new RenderArgs(canvaSurface).Graphics)
			{
				if (!Amount30)
				{
					graphics.Clear(Amount17);
				}
				graphics.SmoothingMode = SmoothingMode.AntiAlias;
				graphics.TextRenderingHint = TextRenderingHint.AntiAlias;
				using (GraphicsPath path = new GraphicsPath())
				{
					using (FontFamily ff = new FontFamily(Amount2.Name))
					{
						using (Font prototype = new Font(Amount2, (float)Amount5))
						{
							using (SolidBrush brush2 = new SolidBrush(Amount6))
							{
								using (HatchBrush brush = new HatchBrush(HStyles(), Amount23, Amount6))
								{
									using (Pen pen = new Pen(Amount19, Amount20))
									{
										using (Pen pen2 = new Pen(Amount12, Amount16))
										{
											using (Pen pen3 = new Pen(Amount15, Amount26))
											{
												using (Font font = new Font(prototype, fontStyle))
												{
													pen.LineJoin = LineJoin.Round;
													if (Amount16 > 0)
													{
														graphics.DrawEllipse(pen2, num6 - num2, num7 - num2, num3, num3);
													}
													if (Amount26 > 0)
													{
														graphics.DrawEllipse(pen3, num6 - num4, num7 - num4, num5, num5);
													}
													switch (Amount24)
													{
													case 0:
														DrawTextOnCircle(graphics, path, font, ff, fontStyle, brush2, pen, num2, num6, num7, Amount1);
														break;
													case 1:
													case 2:
													case 3:
													case 4:
													case 5:
													case 6:
													case 7:
													case 8:
													case 9:
													case 10:
													case 11:
													case 12:
														DrawTextOnCircle(graphics, path, font, ff, fontStyle, brush, pen, num2, num6, num7, Amount1);
														break;
													}
												}
											}
										}
									}
								}
							}
						}
					}
				}
			}
			base.OnSetRenderInfo(newToken, dstArgs, srcArgs);
		}

		private HatchStyle HStyles()
		{
			switch (Amount24)
			{
			case 1:
				return HatchStyle.BackwardDiagonal;
			case 2:
				return HatchStyle.Cross;
			case 3:
				return HatchStyle.DiagonalBrick;
			case 4:
				return HatchStyle.DiagonalCross;
			case 5:
				return HatchStyle.Divot;
			case 6:
				return HatchStyle.DottedGrid;
			case 7:
				return HatchStyle.HorizontalBrick;
			case 8:
				return HatchStyle.LargeCheckerBoard;
			case 9:
				return HatchStyle.Horizontal;
			case 10:
				return HatchStyle.Shingle;
			case 11:
				return HatchStyle.Plaid;
			case 12:
				return HatchStyle.SolidDiamond;
			default:
				return HatchStyle.BackwardDiagonal;
			}
		}

		protected override void OnRender(Rectangle[] rois, int startIndex, int length)
		{
			if (length != 0)
			{
				for (int i = startIndex; i < startIndex + length; i++)
				{
					Render(base.DstArgs.Surface, base.SrcArgs.Surface, rois[i]);
				}
			}
		}

		private void Render(Surface dst, Surface src, Rectangle rect)
		{
			if (!Amount30)
			{
				Rectangle boundsInt = EnvironmentParameters.GetSelectionAsPdnRegion().GetBoundsInt();
				dst.CopySurface(src, rect.Location, rect);
				float num = (float)Amount5;
				float num2 = (Amount25 ? ((float)(Math.Min(boundsInt.Width, boundsInt.Height) / 2 * 2) * (float)Amount9) : ((float)(Math.Min(boundsInt.Width, boundsInt.Height) / 2) * (float)Amount9));
				float num3 = 2f * num2;
				float num4 = (Amount25 ? ((float)(Math.Min(boundsInt.Width, boundsInt.Height) / 2 * 2) * (float)Amount9 + 2.5f * (float)Amount5) : ((float)(Math.Min(boundsInt.Width, boundsInt.Height) / 2) * (float)Amount9 + 2.5f * (float)Amount5));
				float num5 = 2f * num4;
				float num6 = (Amount25 ? ((float)Math.Round((4.0 * Amount13 + 1.0) / 2.0 * (double)(boundsInt.Right - boundsInt.Left) + (double)boundsInt.Left)) : ((float)Math.Round((Amount13 + 1.0) / 2.0 * (double)(boundsInt.Right - boundsInt.Left) + (double)boundsInt.Left)));
				float num7 = (Amount25 ? ((float)Math.Round((4.0 * Amount14 + 1.0) / 2.0 * (double)(boundsInt.Bottom - boundsInt.Top) + (double)boundsInt.Top)) : ((float)Math.Round((Amount14 + 1.0) / 2.0 * (double)(boundsInt.Bottom - boundsInt.Top) + (double)boundsInt.Top)));
				StringFormat stringFormat = new StringFormat();
				stringFormat.Alignment = StringAlignment.Center;
				stringFormat.LineAlignment = StringAlignment.Center;
				FontStyle fontStyle = FontStyle.Regular;
				if (Amount3)
				{
					fontStyle |= FontStyle.Bold;
				}
				if (Amount4)
				{
					fontStyle |= FontStyle.Italic;
				}
				if (Amount18)
				{
					fontStyle |= FontStyle.Underline;
				}
				if (Amount21)
				{
					fontStyle |= FontStyle.Strikeout;
				}
				using (RenderArgs renderArgs = new RenderArgs(dst))
				{
					Graphics graphics = renderArgs.Graphics;
					graphics.Clip = new Region(rect);
					graphics.SmoothingMode = SmoothingMode.AntiAlias;
					graphics.TextRenderingHint = TextRenderingHint.AntiAlias;
					using (GraphicsPath path = new GraphicsPath())
					{
						using (FontFamily ff = new FontFamily(Amount2.Name))
						{
							using (Font prototype = new Font(Amount2, (float)Amount5))
							{
								using (SolidBrush brush2 = new SolidBrush(Amount6))
								{
									using (HatchBrush brush = new HatchBrush(HStyles(), Amount23, Amount6))
									{
										using (Pen pen = new Pen(Amount19, Amount20))
										{
											using (Pen pen2 = new Pen(Amount12, Amount16))
											{
												using (Pen pen3 = new Pen(Amount15, Amount26))
												{
													using (Font font = new Font(prototype, fontStyle))
													{
														pen.LineJoin = LineJoin.Round;
														if (Amount16 > 0)
														{
															graphics.DrawEllipse(pen2, num6 - num2, num7 - num2, num3, num3);
														}
														if (Amount26 > 0)
														{
															graphics.DrawEllipse(pen3, num6 - num4, num7 - num4, num5, num5);
														}
														switch (Amount24)
														{
														case 0:
															DrawTextOnCircle(graphics, path, font, ff, fontStyle, brush2, pen, num2, num6, num7, Amount1);
															break;
														case 1:
														case 2:
														case 3:
														case 4:
														case 5:
														case 6:
														case 7:
														case 8:
														case 9:
														case 10:
														case 11:
														case 12:
															DrawTextOnCircle(graphics, path, font, ff, fontStyle, brush, pen, num2, num6, num7, Amount1);
															break;
														}
													}
												}
											}
										}
									}
								}
							}
						}
					}
				}
			}
			else
			{
				if (!Amount30)
				{
					return;
				}
				for (int i = rect.Top; i < rect.Bottom; i++)
				{
					if (base.IsCancelRequested)
					{
						break;
					}
					for (int j = rect.Left; j < rect.Right; j++)
					{
						ColorBgra value = src[j, i];
						value.A = (Amount30 ? Int32Util.ClampToByte(canvaSurface[j, i].A - (byte)(uint)canvaSurface[j, i]) : Int32Util.ClampToByte(255 - canvaSurface[j, i].A));
						dst[j, i] = value;
					}
				}
			}
		}

		private void DrawTextOnCircle(Graphics g, GraphicsPath path, Font font, FontFamily ff, FontStyle myStyle, Brush brush, Pen OutlinePen, float radius1, float centerX, float centerY, string Amount1)
		{
			double num = 1f / radius1;
			using (StringFormat stringFormat = new StringFormat())
			{
				stringFormat.Alignment = StringAlignment.Center;
				stringFormat.LineAlignment = StringAlignment.Far;
				float num2 = (0f - (float)Amount7) * (float)Math.PI / 180f;
				double num3 = 180.0 / Math.PI;
				if (!Amount10)
				{
					PointF origin = new PointF(0f, 0f);
					List<RectangleF> list = MeasureCharacters(g, font, Amount1);
					double num4 = -Math.PI / 2.0 - (double)num2;
					for (int i = 0; i < Amount1.Length; i++)
					{
						num4 -= (double)(list[i].Width / 2f) * num;
					}
					double num5 = num4;
					for (int j = 0; j < Amount1.Length; j++)
					{
						num5 += (double)(list[j].Width / 2f) * num * Amount8;
						double num6 = (double)centerX + (double)radius1 * Math.Cos(num5);
						double num7 = (double)centerY + (double)radius1 * Math.Sin(num5);
						g.RotateTransform((float)(num3 * (num5 + Math.PI / 2.0)));
						g.TranslateTransform((float)num6, (float)num7, MatrixOrder.Append);
						path.AddString(Amount1[j].ToString(), ff, (int)myStyle, g.DpiY * (float)Amount5 / 72f, origin, stringFormat);
						if (Amount20 > 0f)
						{
							g.DrawPath(OutlinePen, path);
						}
						g.DrawString(Amount1[j].ToString(), font, brush, 0f, 0f, stringFormat);
						path.Reset();
						g.ResetTransform();
						num5 += (double)(list[j].Width / 2f) * num * Amount8;
					}
				}
				if (!Amount10)
				{
					return;
				}
				PointF origin2 = new PointF(0f, 0f);
				num2 = (0f - (float)Amount7) * (float)Math.PI / 180f;
				List<RectangleF> list2 = MeasureCharacters(g, font, Amount1);
				double num8 = Math.PI / 2.0 - (double)num2;
				for (int k = 0; k < Amount1.Length; k++)
				{
					num8 += (double)(list2[k].Width / 2f) * num;
				}
				double num9 = num8;
				stringFormat.Alignment = StringAlignment.Center;
				stringFormat.LineAlignment = StringAlignment.Near;
				for (int l = 0; l < Amount1.Length; l++)
				{
					num9 -= (double)(list2[l].Width / 2f) * num * Amount8;
					double num10 = (double)centerX + (double)radius1 * Math.Cos(num9);
					double num11 = (double)centerY + (double)radius1 * Math.Sin(num9);
					g.RotateTransform((float)(num3 * (num9 - Math.PI / 2.0)));
					g.TranslateTransform((float)num10, (float)num11, MatrixOrder.Append);
					path.AddString(Amount1[l].ToString(), ff, (int)myStyle, g.DpiY * (float)Amount5 / 72f, origin2, stringFormat);
					if (Amount20 > 0f)
					{
						g.DrawPath(OutlinePen, path);
					}
					g.DrawString(Amount1[l].ToString(), font, brush, 0f, 0f, stringFormat);
					path.Reset();
					g.ResetTransform();
					num9 -= (double)(list2[l].Width / 2f) * num * Amount8;
				}
			}
		}

		private List<RectangleF> MeasureCharacters(Graphics g, Font font, string Amount1)
		{
			List<RectangleF> list = new List<RectangleF>();
			float num = 0f;
			for (int i = 0; i < Amount1.Length; i += 32)
			{
				int num2 = 32;
				if (i + num2 >= Amount1.Length)
				{
					num2 = Amount1.Length - i;
				}
				string amount = Amount1.Substring(i, num2);
				List<RectangleF> list2 = MeasureCharactersInWord(g, font, amount);
				if (i == 0)
				{
					num += list2[0].Left;
				}
				for (int j = 0; j < list2.Count + 1 - 1; j++)
				{
					RectangleF item = new RectangleF(num, list2[j].Top, list2[j].Width, list2[j].Height);
					list.Add(item);
					num += list2[j].Width;
				}
			}
			return list;
		}

		private List<RectangleF> MeasureCharactersInWord(Graphics g, Font font, string Amount1)
		{
			List<RectangleF> list = new List<RectangleF>();
			using (StringFormat stringFormat = new StringFormat())
			{
				stringFormat.Alignment = StringAlignment.Near;
				stringFormat.LineAlignment = StringAlignment.Near;
				stringFormat.Trimming = StringTrimming.None;
				stringFormat.FormatFlags = StringFormatFlags.MeasureTrailingSpaces;
				CharacterRange[] array = new CharacterRange[Amount1.Length];
				for (int i = 0; i < Amount1.Length; i++)
				{
					array[i] = new CharacterRange(i, 1);
				}
				stringFormat.SetMeasurableCharacterRanges(array);
				RectangleF layoutRect = new RectangleF(0f, 0f, 30000f, 100f);
				Region[] array2 = g.MeasureCharacterRanges(Amount1, font, layoutRect, stringFormat);
				Region[] array3 = array2;
				foreach (Region region in array3)
				{
					list.Add(region.GetBounds(g));
				}
			}
			return list;
		}
	}
}
