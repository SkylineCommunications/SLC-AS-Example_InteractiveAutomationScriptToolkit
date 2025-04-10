// Ignore Spelling: IAS

namespace IAS_ModelViewPresenter_1.Presenters
{
	using System;

	using IAS_ModelViewPresenter_1.Models;
	using IAS_ModelViewPresenter_1.Views;

	public class UserInfoPresenter : AUserModelPresenter
	{
		private readonly UserInfoView view;

		public UserInfoPresenter(UserInfoView view, UserInfoModel model)
			: base(model)
		{
			this.view = view ?? throw new ArgumentNullException(nameof(view));

			LoadFromModel();
			InitEventHandlers();
		}

		public event EventHandler<EventArgs> Logout;

		public void LoadFromModel()
		{
			view.Label.Text = $"Hi {Model.UserName}, welcome to DataMiner automation!";
		}

		private void InitEventHandlers()
		{
			view.LogoutButton.Pressed += LogoutButton_Pressed;
		}

		private void LogoutButton_Pressed(object sender, EventArgs e)
		{
			Logout?.Invoke(this, e);
		}
	}
}