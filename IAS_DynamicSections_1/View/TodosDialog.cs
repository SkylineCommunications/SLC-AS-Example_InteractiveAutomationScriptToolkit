namespace IAS_DynamicSections_1
{
    using System.Collections.Generic;
    using Skyline.DataMiner.Automation;
    using Skyline.DataMiner.Utils.InteractiveAutomationScript;

    internal class TodosDialog : Dialog
    {
        private List<TodoSection> sections = new List<TodoSection>();

        public TodosDialog(IEngine engine) : base(engine)
        {
            Title = "ToDo's";
            ShowScriptAbortPopup = false;

            SetColumnWidth(0, 400);

            BuildUi();
        }

        public Button AddTodoButton { get; private set; } = new Button("Add ToDo") { Width = 100, Style = ButtonStyle.CallToAction };

        public void AddSection(TodoSection section)
        {
            sections.Add(section);
            BuildUi();
        }

        public void RemoveSection(TodoSection section)
        {
            sections.Remove(section);
            BuildUi();
        }

        internal void BuildUi()
        {
            Clear();

            int row = -1;

            foreach (var section in sections)
            {
                AddSection(section, ++row, 0);
                row += section.RowCount;
            }

            AddWidget(AddTodoButton, ++row, 0, 1, 2, HorizontalAlignment.Right);
        }
    }
}
