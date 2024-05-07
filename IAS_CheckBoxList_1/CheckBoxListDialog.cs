namespace IAS_CheckBoxList_1
{
    using Skyline.DataMiner.Automation;
    using Skyline.DataMiner.Utils.InteractiveAutomationScript;
    using System;
    using System.Collections.Generic;

    public class CheckBoxListDialog : Dialog
    {
        private readonly CheckBoxList checkBoxList;
        private readonly TextBox checkedOptionsTextBox;
        private readonly Button exitButton;

        private readonly IEnumerable<string> options = new string[]
        {
            "Yes",
            "An Option",
            "Another One",
            "Option X",
            "Something something Dark Side"
        };

        public CheckBoxListDialog(IEngine engine) : base(engine)
        {
            Title = "Select Option(s)";

            // Set up checkboxlist
            checkBoxList = new CheckBoxList(options) { IsSorted = true };
            checkBoxList.Changed += (s, e) => checkedOptionsTextBox.Text = String.Join(Environment.NewLine, checkBoxList.Checked);

            // Set up textbox
            checkedOptionsTextBox = new TextBox { IsMultiline = true, Height = 150 };

            // Set up exit button
            exitButton = new Button("Exit");
            exitButton.Pressed += (s, e) => OnExitButtonPressed?.Invoke(this, EventArgs.Empty);

            // Generate Ui
            AddWidget(new Label("Select Option(s)"), 0, 0, verticalAlignment: VerticalAlignment.Top);
            AddWidget(checkBoxList, 0, 1);
            AddWidget(checkedOptionsTextBox, 1, 0, 1, 2);
            AddWidget(exitButton, 2, 0, 1, 2);
        }

        public event EventHandler OnExitButtonPressed;
    }
}
