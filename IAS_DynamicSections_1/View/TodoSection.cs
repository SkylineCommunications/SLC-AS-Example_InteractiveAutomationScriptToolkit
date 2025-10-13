namespace IAS_DynamicSections_1
{
    using Skyline.DataMiner.Utils.InteractiveAutomationScript;

    internal class TodoSection : Section
    {
        public TodoSection()
        {
            AddWidget(TitleLabel, 0, 0);
            AddWidget(DeleteButton, 0, 1);
            AddWidget(NameTextBox, 1, 0);
            AddWidget(DescriptionTextBox, 2, 0);
            AddWidget(new WhiteSpace(), 3, 0);
        }

        public Label TitleLabel { get; private set; } = new Label() { Style = TextStyle.Bold };

        public TextBox NameTextBox { get; private set; } = new TextBox { PlaceHolder = "Name" };

        public TextBox DescriptionTextBox { get; private set; } = new TextBox() { PlaceHolder = "Description", Height = 140, IsMultiline = true };

        public Button DeleteButton { get; private set; } = new Button("X") { Width = 44 };
    }
}
