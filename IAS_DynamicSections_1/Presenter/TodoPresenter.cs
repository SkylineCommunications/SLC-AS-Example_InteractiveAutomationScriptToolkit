namespace IAS_DynamicSections_1
{
    internal class TodoPresenter
    {
        private readonly Todo _model;
        private readonly TodoSection _view;

        public TodoPresenter(TodoSection view, Todo model)
        {
            _view = view;
            _model = model;

            _view.TitleLabel.Text = model.Name;
            _view.NameTextBox.Text = model.Name;
            _view.DescriptionTextBox.Text = model.Description;

            BindEvents();
        }

        private void BindEvents()
        {
            _view.NameTextBox.FocusLost += (s, e) =>
            {
                _model.Name = e.Value;
                _view.TitleLabel.Text = e.Value;
            };

            _view.DescriptionTextBox.FocusLost += (s, e) => _model.Description = e.Value;
        }
    }
}
