namespace IAS_DynamicSections_1
{
    internal class TodosPresenter
    {
        private readonly TodosDialog _view;
        private readonly Todos _model;

        public TodosPresenter(TodosDialog view, Todos model)
        {
            _view = view;
            _model = model;

            BindEvents();

            foreach (var todo in model.Items)
            {
                AddTodo(todo);
            }

            _view.BuildUi();
        }

        private void BindEvents()
        {
            _view.AddTodoButton.Pressed += (s, e) =>
            {
                var todo = new Todo { Name = "New ToDo" };

                _model.Items.Add(todo);
                AddTodo(todo);
            };
        }

        private void AddTodo(Todo todo)
        {
            var section = new TodoSection();
            var subPresenter = new TodoPresenter(section, todo);

            section.DeleteButton.Pressed += (s, e) =>
            {
                _model.Items.Remove(todo);
                _view.RemoveSection(section);
            };

            _view.AddSection(section);
        }
    }
}
