// Ignore Spelling: IAS

namespace IAS_ModelViewPresenter_1.Views
{
	using Skyline.DataMiner.Automation;
	using Skyline.DataMiner.Utils.InteractiveAutomationScript;

	public class LoginView : Dialog
	{
		public LoginView(IEngine engine)
			: base(engine)
		{
			Title = "DataMiner Automation";
			Width = 500;

			BuildUi();
		}

		public TextBox UserName { get; } = new TextBox
		{
			Width = 450,
		};

		public PasswordBox Password { get; } = new PasswordBox
		{
			Width = 450,
			HasPeekIcon = true,
		};

		public Button CancelButton { get; } = new Button("Cancel")
		{
			Width = 110,
		};

		public Button LoginButton { get; } = new Button("Login")
		{
			Width = 110,
			IsEnabled = false,
		};

		private void BuildUi()
		{
			int row = 0;

			AddWidget(new Label("Login") { Style = TextStyle.Bold }, row++, 0);

			AddWidget(new Label("User Name") { Width = 450 }, row++, 0, 1, 2);
			AddWidget(UserName, row++, 0, 1, 2, HorizontalAlignment.Left);

			AddWidget(new Label("Password") { Width = 450 }, row++, 0, 1, 2);
			AddWidget(Password, row++, 0, 1, 2, HorizontalAlignment.Left);

			AddWidget(new WhiteSpace(), row++, 0);

			AddWidget(CancelButton, row, 0, HorizontalAlignment.Left);
			AddWidget(LoginButton, row, 1, HorizontalAlignment.Right);
		}
	}
}