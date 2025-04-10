// Ignore Spelling: IAS

/*
****************************************************************************
*  Copyright (c) 2025,  Skyline Communications NV  All Rights Reserved.    *
****************************************************************************

By using this script, you expressly agree with the usage terms and
conditions set out below.
This script and all related materials are protected by copyrights and
other intellectual property rights that exclusively belong
to Skyline Communications.

A user license granted for this script is strictly for personal use only.
This script may not be used in any way by anyone without the prior
written consent of Skyline Communications. Any sublicensing of this
script is forbidden.

Any modifications to this script by the user are only allowed for
personal use and within the intended purpose of the script,
and will remain the sole responsibility of the user.
Skyline Communications will not be responsible for any damages or
malfunctions whatsoever of the script resulting from a modification
or adaptation by the user.

The content of this script is confidential information.
The user hereby agrees to keep this confidential information strictly
secret and confidential and not to disclose or reveal it, in whole
or in part, directly or indirectly to any person, entity, organization
or administration without the prior written consent of
Skyline Communications.

Any inquiries can be addressed to:

	Skyline Communications NV
	Ambachtenstraat 33
	B-8870 Izegem
	Belgium
	Tel.	: +32 51 31 35 69
	Fax.	: +32 51 31 01 29
	E-mail	: info@skyline.be
	Web		: www.skyline.be
	Contact	: Ben Vandenberghe

****************************************************************************
Revision History:

DATE		VERSION		AUTHOR			COMMENTS

10/04/2025	1.0.0.1		BDK, Skyline	Initial version
****************************************************************************
*/

namespace IAS_ModelViewPresenter_1
{
	using System;

	using IAS_ModelViewPresenter_1.Models;
	using IAS_ModelViewPresenter_1.Presenters;
	using IAS_ModelViewPresenter_1.Views;

	using Skyline.DataMiner.Automation;
	using Skyline.DataMiner.Utils.InteractiveAutomationScript;

	/// <summary>
	/// Represents a DataMiner Automation script.
	/// </summary>
	public class Script
	{
		/// <summary>
		/// The script entry point.
		/// </summary>
		/// <param name="engine">Link with SLAutomation process.</param>
		public void Run(IEngine engine)
		{
			/*
             *    Note:
             *    Do not remove the commented method below!
             *    The line is needed to execute an interactive automation script from the non-interactive automation script.
             *
             *    engine.ShowUI();
            */

			try
			{
				RunSafe(engine);
			}
			catch (ScriptAbortException)
			{
				// Catch normal abort exceptions (engine.ExitFail or engine.ExitSuccess)
				throw; // Comment if it should be treated as a normal exit of the script.
			}
			catch (ScriptForceAbortException)
			{
				// Catch forced abort exceptions, caused via external maintenance messages.
				throw;
			}
			catch (ScriptTimeoutException)
			{
				// Catch timeout exceptions for when a script has been running for too long.
				throw;
			}
			catch (InteractiveUserDetachedException)
			{
				// Catch a user detaching from the interactive script by closing the window.
				// Only applicable for interactive scripts, can be removed for non-interactive scripts.
				throw;
			}
			catch (Exception e)
			{
				engine.ExitFail("Run|Something went wrong: " + e);
			}
		}

		private void RunSafe(IEngine engine)
		{
			var controller = new InteractiveController(engine);

			// Define models
			var userModel = new UserInfoModel();

			// Define views
			var loginView = new LoginView(engine);
			var userInfoView = new UserInfoView(engine);
			var logoutView = new LogoutView(engine);

			// Define presenters
			var loginPresenter = new LoginPresenter(loginView, userModel);
			var userInfoPresenter = new UserInfoPresenter(userInfoView, userModel);
			var logoutPresenter = new LogoutPresenter(logoutView, userModel);

			// Define model-view-presenter events
			loginPresenter.Cancel += (s, e) =>
			{
				controller.Engine.ExitSuccess("Canceled");
			};
			loginPresenter.Login += (s, e) =>
			{
				userInfoPresenter.LoadFromModel();
				controller.ShowDialog(userInfoView);
			};

			userInfoPresenter.Logout += (s, e) =>
			{
				logoutPresenter.LoadFromModel();
				controller.ShowDialog(logoutView);
			};

			logoutPresenter.Finish += (s, e) =>
			{
				controller.Engine.ExitSuccess("Finish");
			};

			// Start first view
			controller.Run(loginView);
		}
	}
}