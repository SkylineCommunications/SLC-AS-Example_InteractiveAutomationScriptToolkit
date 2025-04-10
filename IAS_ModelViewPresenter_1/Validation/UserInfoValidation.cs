// Ignore Spelling: IAS

namespace IAS_ModelViewPresenter_1.Validation
{
	using IAS_ModelViewPresenter_1.Models;

	using Skyline.DataMiner.Automation;

	public class UserInfoValidation
	{
		private readonly bool isPasswordValid = true;

		private bool isUserNameValid;

		public bool IsValid => isUserNameValid && isPasswordValid;

		public ValidationResult IsValidUser(UserInfoModel model)
		{
			/*
			 * Verify if the user is known in the system.
			 * For this exercise we'll assume the provided user information is valid.
			 *
			 * return new ValidationResult(UIValidationState.Invalid, "Invalid user name and/or password.");
			 */

			return new ValidationResult(UIValidationState.Valid);
		}

		public ValidationResult ValidateUserName(UserInfoModel model)
		{
			if (string.IsNullOrEmpty(model.UserName))
			{
				isUserNameValid = false;
				return new ValidationResult(UIValidationState.Invalid, "The user name cannot be empty.");
			}

			isUserNameValid = true;

			return new ValidationResult(UIValidationState.Valid);
		}

		public ValidationResult ValidatePassword(UserInfoModel model)
		{
			if (string.IsNullOrEmpty(model.Password))
			{
				return new ValidationResult(UIValidationState.Invalid, "The password cannot be empty.");
			}

			return new ValidationResult(UIValidationState.Valid);
		}
	}
}