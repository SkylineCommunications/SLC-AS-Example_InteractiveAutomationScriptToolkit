/*
****************************************************************************
*  Copyright (c) 2026,  Skyline Communications NV  All Rights Reserved.    *
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

dd/mm/2026	1.0.0.1		XXX, Skyline	Initial version
****************************************************************************
*/

namespace IAS_Widgets_Demo_1
{
	using System;
	using System.Collections.Generic;

	using Skyline.DataMiner.Automation;
	using Skyline.DataMiner.Net.AutomationUI.Objects;
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
			var dialog = new WidgetsDemoDialog(engine);
			dialog.Button.Pressed += (s, e) => engine.ExitSuccess("Button pressed");
			controller.ShowDialog(dialog);
		}
	}

	internal class WidgetsDemoDialog : Dialog
	{
		public WidgetsDemoDialog(IEngine engine) : base(engine)
		{
			int row = -1;
			AddWidget(Label_NoStyle, ++row, 0, 1, 2);
			AddWidget(Label_Heading, ++row, 0, 1, 2);
			AddWidget(Label_Bold, ++row, 0, 1, 2);
			AddWidget(Label_Title, ++row, 0, 1, 2);

			AddWidget(new Label("Button"), ++row, 0);
			AddWidget(Button, row, 1);

			AddWidget(new Label("Text Box"), ++row, 0);
			AddWidget(TextBox, row, 1);

			AddWidget(new Label("Multi Line Text Box"), ++row, 0);
			AddWidget(MultiLineTextBox, row, 1);

			AddWidget(new Label("Numeric"), ++row, 0);
			AddWidget(Numeric, row, 1);

			AddWidget(new Label("Drop Down"), ++row, 0);
			AddWidget(DropDown, row, 1);

			AddWidget(new Label("Check Box"), ++row, 0);
			AddWidget(CheckBox, row, 1);

			AddWidget(new Label("Check Box List"), ++row, 0);
			AddWidget(CheckBoxList, row, 1);

			AddWidget(new Label("Radio Button List"), ++row, 0);
			AddWidget(RadioButtonList, row, 1);

			AddWidget(new Label("Date Time Picker"), ++row, 0);
			AddWidget(DateTimePicker, row, 1);

			AddWidget(new Label("Time Picker"), ++row, 0);
			AddWidget(TimePicker, row, 1);

			AddWidget(new Label("Time"), ++row, 0);
			AddWidget(Time, row, 1);

			AddWidget(new Label("Collapse Button"), ++row, 0);
			AddWidget(CollapseButton, row, 1);

			AddWidget(new Label("Download Button"), ++row, 0);
			AddWidget(DownloadButton, row, 1);

			AddWidget(new Label("File Selector"), ++row, 0);
			AddWidget(FileSelector, row, 1);

			AddWidget(new Label("Hyperlink"), ++row, 0);
			AddWidget(Hyperlink, row, 1);

			AddWidget(new Label("Password Box"), ++row, 0);
			AddWidget(PasswordBox, row, 1);

			AddWidget(new Label("Tree View"), ++row, 0);
			AddWidget(TreeView, row, 1);
		}

		public Label Label_NoStyle { get; private set; } = new Label("This is a label with no style") { Style = TextStyle.None };

		public Label Label_Bold { get; private set; } = new Label("This is a label with Bold style") { Style = TextStyle.Bold };

		public Label Label_Heading { get; private set; } = new Label("This is a label with Heading style") { Style = TextStyle.Heading };

		public Label Label_Title { get; private set; } = new Label("This is a label with Title style") { Style = TextStyle.Title };

		public Button Button { get; private set; } = new Button("This is a button");

		public TextBox TextBox { get; private set; } = new TextBox("This is a text box");

		public TextBox MultiLineTextBox { get; private set; } = new TextBox("This is a multi-line text box") { IsMultiline = true, Height = 120 };

		public Numeric Numeric { get; private set; } = new Numeric(50);

		public DropDown DropDown { get; private set; } = new DropDown(new List<string> { "Option 1", "Option 2", "Option 3" });

		public CheckBox CheckBox { get; private set; } = new CheckBox("This is a check box");

		public CheckBoxList CheckBoxList { get; private set; } = new CheckBoxList(new List<string> { "Option 1", "Option 2", "Option 3" });

		public RadioButtonList RadioButtonList { get; private set; } = new RadioButtonList(new List<string> { "Option 1", "Option 2", "Option 3" });

		public DateTimePicker DateTimePicker { get; private set; } = new DateTimePicker();

		public TimePicker TimePicker { get; private set; } = new TimePicker();

		public Time Time { get; private set; } = new Time();

		public CollapseButton CollapseButton { get; private set; } = new CollapseButton();

		public DownloadButton DownloadButton { get; private set; } = new DownloadButton("Download");

		public FileSelector FileSelector { get; private set; } = new FileSelector();

		public Hyperlink Hyperlink { get; private set; } = new Hyperlink("Skyline.be", new Uri("https://www.skyline.be"));

		public PasswordBox PasswordBox { get; private set; } = new PasswordBox();

		public TreeView TreeView { get; private set; } = new TreeView(new List<TreeViewItem>
		{
			new TreeViewItem("Node 1", "1") { ItemType = TreeViewItem.TreeViewItemType.CheckBox },
			new TreeViewItem("Node 2", "2")
			{
				ItemType = TreeViewItem.TreeViewItemType.CheckBox,
				ChildItems = new List<TreeViewItem>
				{
					new TreeViewItem("Child Node 1", "1.1"),
					new TreeViewItem("Child Node 2", "1.2"),
				},
			},
			new TreeViewItem("Node 3", "3") { ItemType = TreeViewItem.TreeViewItemType.CheckBox },
		});
	}
}