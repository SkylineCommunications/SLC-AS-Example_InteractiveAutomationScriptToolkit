namespace IAS_HideUI_1
{
	using Skyline.DataMiner.Automation;
	using Skyline.DataMiner.Utils.InteractiveAutomationScript;

	public class HideUIPanel : Dialog
	{
		private readonly Label hideUiLabel = new Label("Hide UI:");

		private readonly Label showUiAgainLabel = new Label("Show UI again after background action?");

		public HideUIPanel(IEngine engine) : base(engine)
		{
			Title = "Hide UI";

			Initialize();
			GenerateUi();
		}

		public Button HideUiButton { get; set; }

		public CheckBox HideUiCheckBox { get; set; }

		private void Initialize()
		{
			HideUiButton = new Button("Hide");
			HideUiCheckBox = new CheckBox { IsChecked = false };
		}

		private void GenerateUi()
		{
			Clear();

			int row = -1;
			AddWidget(hideUiLabel, ++row, 0);
			AddWidget(HideUiButton, row, 1);
			AddWidget(showUiAgainLabel, ++row, 0);
			AddWidget(HideUiCheckBox, row, 1);
		}
	}
}