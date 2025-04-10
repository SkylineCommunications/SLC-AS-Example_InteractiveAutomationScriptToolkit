// Ignore Spelling: IAS

namespace IAS_ModelViewPresenter_1.Presenters
{
	using System;

	using IAS_ModelViewPresenter_1.Models;
	using IAS_ModelViewPresenter_1.Validation;
	using IAS_ModelViewPresenter_1.Views;

	using Skyline.DataMiner.Utils.InteractiveAutomationScript;

	public class LoginPresenter : AUserModelPresenter
	{
		private readonly LoginView view;

		private readonly UserInfoValidation validation = new UserInfoValidation();

		public LoginPresenter(LoginView view, UserInfoModel model)
			: base(model)
		{
			this.view = view ?? throw new ArgumentNullException(nameof(view));

			LoadFromModel();
			InitEventHandlers();
		}

		public event EventHandler<EventArgs> Cancel;

		public event EventHandler<EventArgs> Login;

		public void LoadFromModel()
		{
			// Do nothing: no data needs to be loaded from the model
		}

		private void InitEventHandlers()
		{
			view.UserName.Changed += UserName_Changed;

			view.CancelButton.Pressed += CancelButton_Pressed;
			view.LoginButton.Pressed += LoginButton_Pressed;
		}

		private void UserName_Changed(object sender, TextBox.TextBoxChangedEventArgs e)
		{
			// Update the model
			Model.UserName = e.Value;

			// Validate the user name
			var validationResult = validation.ValidateUserName(Model);
			view.UserName.ValidationState = validationResult.State;
			view.UserName.ValidationText = validationResult.ErrorMessage;

			// Update the button state based on the overall validation result
			view.LoginButton.IsEnabled = validation.IsValid;
		}

		private void CancelButton_Pressed(object sender, EventArgs e)
		{
			Cancel?.Invoke(this, e);
		}

		private void LoginButton_Pressed(object sender, EventArgs e)
		{
			// Update the model
			Model.Password = view.Password.Password;

			// Validate the password
			var validationResult = validation.ValidatePassword(Model);
			view.Password.ValidationState = validationResult.State;
			view.Password.ValidationText = validationResult.ErrorMessage;

			if (validationResult.State != Skyline.DataMiner.Automation.UIValidationState.Valid)
			{
				return;
			}

			if (!TryLogin())
			{
				return;
			}

			Login?.Invoke(this, e);
		}

		private bool TryLogin()
		{
			var isValidUserResult = validation.IsValidUser(Model);
			if (isValidUserResult.State == Skyline.DataMiner.Automation.UIValidationState.Valid)
			{
				return true;
			}

			view.UserName.ValidationState = isValidUserResult.State;
			view.UserName.ValidationText = isValidUserResult.ErrorMessage;
			view.Password.ValidationState = isValidUserResult.State;
			view.Password.ValidationText = isValidUserResult.ErrorMessage;

			return false;
		}
	}
}