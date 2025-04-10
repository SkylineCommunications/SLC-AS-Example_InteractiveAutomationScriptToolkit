// Ignore Spelling: IAS

namespace IAS_ModelViewPresenter_1.Presenters
{
	using System;

	using IAS_ModelViewPresenter_1.Models;

	public class AUserModelPresenter
	{
		private readonly UserInfoModel model;

		protected AUserModelPresenter(UserInfoModel model)
		{
			this.model = model ?? throw new ArgumentNullException(nameof(model));
		}

		public UserInfoModel Model => model;
	}
}