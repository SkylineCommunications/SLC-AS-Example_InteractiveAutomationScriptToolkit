// Ignore Spelling: IAS

namespace IAS_ModelViewPresenter_1.Views
{
	using Skyline.DataMiner.Automation;
	using Skyline.DataMiner.Utils.InteractiveAutomationScript;

	public class UserInfoView : Dialog
	{
		public UserInfoView(IEngine engine)
			: base(engine)
		{
			Title = "DataMiner Automation";
			Width = 500;

			BuildUi();
		}

		public Label Label { get; } = new Label
		{
			Width = 450,
		};

		public Button LogoutButton { get; } = new Button("Logout")
		{
			Width = 110,
		};

		private void BuildUi()
		{
			int row = 0;

			AddWidget(new Label("Welcome") { Style = TextStyle.Bold }, row++, 0);

			AddWidget(Label, row++, 0, 1, 2, HorizontalAlignment.Left);
			AddWidget(new WhiteSpace(), row++, 0);

			AddWidget(LogoutButton, row, 1, HorizontalAlignment.Right);
		}
	}
}