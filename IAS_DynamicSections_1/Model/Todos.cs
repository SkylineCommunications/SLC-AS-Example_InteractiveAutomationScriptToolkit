namespace IAS_DynamicSections_1
{
    using System.Collections.Generic;
    using System.Linq;

    internal class Todos
    {
        public Todos() : this(Enumerable.Empty<Todo>())
        {
        }

        public Todos(IEnumerable<Todo> todos)
        {
            Items = todos.ToList();
        }

        public List<Todo> Items { get; private set; } = new List<Todo>();
    }
}
