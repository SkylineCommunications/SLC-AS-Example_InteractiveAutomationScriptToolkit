// Ignore Spelling: IAS

namespace IAS_ModelViewPresenter_1.Validation
{
	using System;

	using Skyline.DataMiner.Automation;

	public class ValidationResult
	{
		internal ValidationResult(UIValidationState state)
			: this(state, string.Empty)
		{
		}

		internal ValidationResult(UIValidationState state, string errorMessage)
		{
			State = state;
			ErrorMessage = errorMessage ?? throw new ArgumentNullException(nameof(errorMessage));
		}

		public UIValidationState State { get; private set; }

		public string ErrorMessage { get; private set; }
	}
}