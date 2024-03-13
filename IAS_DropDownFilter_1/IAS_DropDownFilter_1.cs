/*
****************************************************************************
*  Copyright (c) 2024,  Skyline Communications NV  All Rights Reserved.    *
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

27/02/2024	1.0.0.1		TRE, Skyline	Initial version
****************************************************************************
*/

namespace IAS_DropDownFilter_1
{
    using System;
    using Skyline.DataMiner.Automation;
    using Skyline.DataMiner.Utils.InteractiveAutomationScript;

    /// <summary>
    /// Represents a DataMiner Automation script.
    /// </summary>
    public class Script
    {
        private InteractiveController app;
        private IEngine engine;
        private DropDownFilterDialog dialog;

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

                this.engine = engine;
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
            catch (Exception e)
            {
                engine.Log("Run|Something went wrong: " + e);
                ShowExceptionDialog(engine, e);
            }
        }

        private void RunSafe(IEngine engine)
        {
            dialog = new DropDownFilterDialog(engine);
            dialog.OnElementSelected += Dialog_OnElementSelected;
            dialog.OnExitButtonPressed += Dialog_OnExitButtonPressed;
            app.Run(dialog);
        }

        private void Dialog_OnElementSelected(object sender, ElementSelectedEventArgs e)
        {
            var messageDialog = new MessageDialog(engine, $"You selected element {e.ElementName}.") { Title = "Selected Element" };
            messageDialog.OkButton.Pressed += MessageDialog_OkButton_Pressed;
            app.ShowDialog(messageDialog);
        }

        private void MessageDialog_OkButton_Pressed(object sender, EventArgs e)
        {
            app.ShowDialog(dialog);
        }

        private void Dialog_OnExitButtonPressed(object sender, EventArgs e)
        {
            engine.ExitSuccess("Exit");
        }

        private void ShowExceptionDialog(IEngine engine, Exception exception)
        {
            ExceptionDialog exceptionDialog = new ExceptionDialog(engine, exception);
            exceptionDialog.OkButton.Pressed += (sender, args) => engine.ExitFail("Something went wrong.");
            if (app.IsRunning) app.ShowDialog(exceptionDialog); else app.Run(exceptionDialog);
        }
    }
}