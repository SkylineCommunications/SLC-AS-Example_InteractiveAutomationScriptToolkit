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

dd/mm/2025	1.0.0.1		XXX, Skyline	Initial version
****************************************************************************
*/

namespace IAS_LazyLoadDropDown_1
{
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using Skyline.DataMiner.Automation;
	using Skyline.DataMiner.Utils.InteractiveAutomationScript;

	/// <summary>
	/// Represents a DataMiner Automation script.
	/// </summary>
	public class Script
	{
		private InteractiveController app;

		/// <summary>
		/// The Script entry point.
		/// IEngine.ShowUI();.
		/// </summary>
		/// <param name="engine">Link with SLAutomation process.</param>
		public void Run(IEngine engine)
		{
			try
			{
				app = new InteractiveController(engine);

				engine.WebUIVersion = WebUIVersion.V2; // requires DM 10.5.12, in older versions add "useNewIASInputComponents=true" to the URL query
				engine.SetFlag(RunTimeFlags.NoKeyCaching);
				engine.Timeout = TimeSpan.FromHours(10);

				RunSafe(engine);
			}
			catch (ScriptAbortException)
			{
				throw;
			}
			catch (ScriptForceAbortException)
			{
				throw;
			}
			catch (ScriptTimeoutException)
			{
				throw;
			}
			catch (InteractiveUserDetachedException)
			{
				throw;
			}
			catch (Exception ex)
			{
				engine.Log($"Run|Something went wrong: {ex}");
				ShowExceptionDialog(engine, ex);
			}
		}

		private void RunSafe(IEngine engine)
		{
			LazyLoadDropDownDialog dialog = new LazyLoadDropDownDialog(engine);
			app.ShowDialog(dialog);
		}

		private void ShowExceptionDialog(IEngine engine, Exception exception)
		{
			ExceptionDialog exceptionDialog = new ExceptionDialog(engine, exception);
			exceptionDialog.OkButton.Pressed += (sender, args) => engine.ExitSuccess("Something went wrong.");
			exceptionDialog.Show(true);
		}
	}

	public class LazyLoadDropDownDialog : Dialog
	{
		private readonly List<string> options; // contains 50K options

		public LazyLoadDropDownDialog(IEngine engine) : base(engine)
		{
			Title = "LazyLoading DropDown";

			options = GenerateOptions();

			DropDown.Options = options.Take(50);
			DropDown.Changed += DropDown_Changed;
			DropDown.FilterChanged += DropDown_FilterChanged;

			// Optional workaround: Using the DropDown validation state here to convey to the user that not all options are shown
			DropDown.ValidationText = "Not all options are displayed, please use the filter to narrow down.";
			DropDown.ValidationState = UIValidationState.Invalid;

			OkButton.Pressed += (sender, args) => engine.ExitSuccess("OK");

			int row = -1;

			AddWidget(Label, ++row, 0);
			AddWidget(DropDown, ++row, 0);
			AddWidget(OkButton, ++row, 0);
		}

		public Label Label { get; private set; } = new Label("Select an option from the drop-down list:");

		public DropDown DropDown { get; private set; } = new DropDown { Width = 250 };

		public Button OkButton { get; private set; } = new Button("OK") { Style = ButtonStyle.CallToAction };

		private static List<string> GenerateOptions(int count = 50000)
		{
			var options = new List<string>();
			for (int i = 0; i < count; i++)
			{
				options.Add($"Option {i + 1}");
			}

			return options;
		}

		private void DropDown_FilterChanged(object sender, DropDownBase.DropDownFilterChangedEventArgs e)
		{
			// Use the currently entered filter value to filter the options
			// NOTE: you are not able to change the selected item of the DropDown firing this event, as this breaks the filtering behavior and as such is blocked by the toolkit.
			var filteredOptions = options.Where(x => x.IndexOf(e.FilterValue ?? String.Empty, StringComparison.OrdinalIgnoreCase) >= 0).ToList();
			DropDown.Options = filteredOptions.Take(50);
			DropDown.ValidationState = filteredOptions.Count > 50 ? UIValidationState.Invalid : UIValidationState.Valid;
		}

		private void DropDown_Changed(object sender, DropDown.DropDownChangedEventArgs e)
		{
			// Clear validation state when user makes a selection
			DropDown.ValidationState = UIValidationState.Valid;

			// Optional: add another couple of options next the selected one in case the filter caused only a few options to be shown
			var filteredOptions = options.Take(50).ToHashSet();
			filteredOptions.Add(DropDown.Selected);
			DropDown.Options = filteredOptions;
		}
	}
}