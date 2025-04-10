// Ignore Spelling: IAS

namespace IAS_DynamicButtonList_1
{
	using System;
	using System.Collections.Generic;
	using System.Linq;

	using Skyline.DataMiner.Automation;
	using Skyline.DataMiner.Core.DataMinerSystem.Common;
	using Skyline.DataMiner.Utils.InteractiveAutomationScript;

	public class DynamicButtonPanel : Dialog
	{
		private const int MaxColumns = 3;

		private readonly Dictionary<Button, IDmsElement> selectableElements = new Dictionary<Button, IDmsElement>();

		private readonly TextBox filterTextBox = new TextBox { PlaceHolder = "Filter on Element Name..." };

		public DynamicButtonPanel(IEngine engine, IEnumerable<IDmsElement> elements) : base(engine)
		{
			Title = "Select Element";

			Initialize(elements);
			GenerateUi();
		}

		public event EventHandler<ElementSelectedEventArgs> OnElementSelected;

		private void Initialize(IEnumerable<IDmsElement> elements)
		{
			foreach (var element in elements)
			{
				Button button = new Button(element.Name);
				button.Pressed += Button_Pressed;

				selectableElements[button] = element;
			}

			filterTextBox.Changed += FilterTextBox_Changed;
		}

		private void FilterTextBox_Changed(object sender, TextBox.TextBoxChangedEventArgs e)
		{
			GenerateUi();
		}

		private void Button_Pressed(object sender, EventArgs e)
		{
			// sender is button that got pressed
			Button pressedButton = (Button)sender;
			OnElementSelected?.Invoke(this, new ElementSelectedEventArgs(selectableElements[pressedButton]));
		}

		private void GenerateUi()
		{
			Clear();

			int row = -1;
			AddWidget(filterTextBox, ++row, 0, 1, MaxColumns);

			var filteredButtons = selectableElements.Where(x => x.Value.Name.Contains(filterTextBox.Text)).Select(x => x.Key).ToList();
			if (!filteredButtons.Any())
			{
				AddWidget(new Label("No elements matching filter"), row + 1, 0, 1, MaxColumns);
				return;
			}

			var orderedButtons = filteredButtons.OrderBy(x => x.Text).ToList();
			int buttonIndex = 0;
			row += 1;
			while (buttonIndex < orderedButtons.Count)
			{
				for (int column = 0; column < MaxColumns; column++)
				{
					if (buttonIndex < orderedButtons.Count)
					{
						AddWidget(orderedButtons[buttonIndex], row, column);
					}

					buttonIndex += 1;
				}

				row += 1;
			}
		}
	}
}