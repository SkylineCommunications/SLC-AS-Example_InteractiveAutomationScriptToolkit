namespace IAS_DropDownFilter_1
{
	using System;

	public class ElementSelectedEventArgs : EventArgs
	{
		public ElementSelectedEventArgs(string elementName)
		{
			ElementName = elementName;
		}

		public string ElementName { get; private set; }
	}
}