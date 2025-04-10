// Ignore Spelling: IAS

namespace IAS_ModelViewPresenter_1.Presenters
{
	using System;

	using IAS_ModelViewPresenter_1.Models;
	using IAS_ModelViewPresenter_1.Views;

	public class LogoutPresenter : AUserModelPresenter
	{
		private readonly LogoutView view;

		public LogoutPresenter(LogoutView view, UserInfoModel model)
			: base(model)
		{
			this.view = view ?? throw new ArgumentNullException(nameof(view));

			LoadFromModel();
			InitEventHandlers();
		}

		public event EventHandler<EventArgs> Finish;

		public void LoadFromModel()
		{
			// Do nothing: no data needs to be loaded from the model
		}

		private void InitEventHandlers()
		{
			view.FinishButton.Pressed += FinishButton_Pressed;
		}

		private void FinishButton_Pressed(object sender, EventArgs e)
		{
			Finish?.Invoke(this, e);
		}
	}
}