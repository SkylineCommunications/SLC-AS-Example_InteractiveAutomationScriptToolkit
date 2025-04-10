// Ignore Spelling: IAS

namespace IAS_ModelViewPresenter_1.Views
{
	using Skyline.DataMiner.Automation;
	using Skyline.DataMiner.Utils.InteractiveAutomationScript;

	public class LogoutView : Dialog
	{
		public LogoutView(IEngine engine)
			: base(engine)
		{
			Title = "DataMiner Automation";
			Width = 500;

			BuildUi();
		}

		public Button FinishButton { get; } = new Button("Finish")
		{
			Width = 110,
		};

		private void BuildUi()
		{
			int row = 0;

			AddWidget(new Label("Logout") { Style = TextStyle.Bold }, row++, 0);

			AddWidget(new Label("All done! Have a nice day.") { Width = 450 }, row++, 0, 1, 2);
			AddWidget(new WhiteSpace(), row++, 0);

			AddWidget(FinishButton, row, 1, HorizontalAlignment.Right);
		}
	}
}