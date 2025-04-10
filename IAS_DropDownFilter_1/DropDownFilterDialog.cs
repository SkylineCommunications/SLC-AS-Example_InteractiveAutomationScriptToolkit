namespace IAS_DropDownFilter_1
{
	using System;
	using System.Linq;

	using Skyline.DataMiner.Automation;
	using Skyline.DataMiner.Core.DataMinerSystem.Automation;
	using Skyline.DataMiner.Utils.InteractiveAutomationScript;

	public class DropDownFilterDialog : Dialog
	{
		private readonly DropDown elementDropDown;

		private readonly Button exitButton;

		public DropDownFilterDialog(IEngine engine) : base(engine)
		{
			Title = "Select an Element";

			// Set up dropdown
			var dms = engine.GetDms();
			elementDropDown = new DropDown(dms.GetElements().Select(x => x.Name))
			{
				IsDisplayFilterShown = true, // Allows user to filter in dropdown options
				IsSorted = true, // Sorts the options alphabetically
			};

			elementDropDown.Changed += (s, e) => OnElementSelected?.Invoke(this, new ElementSelectedEventArgs(e.Selected));

			// Set up exit button
			exitButton = new Button("Exit");
			exitButton.Pressed += (s, e) => OnExitButtonPressed?.Invoke(this, EventArgs.Empty);

			// Generate Ui
			AddWidget(new Label("Hint: you can type in the dropdown to filter options"), 0, 0);
			AddWidget(elementDropDown, 1, 0);
			AddWidget(exitButton, 2, 0);
		}

		public event EventHandler<ElementSelectedEventArgs> OnElementSelected;

		public event EventHandler OnExitButtonPressed;
	}
}