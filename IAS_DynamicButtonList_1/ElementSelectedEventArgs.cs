namespace IAS_DynamicButtonList_1
{
    using System;
    using Skyline.DataMiner.Core.DataMinerSystem.Common;

    public class ElementSelectedEventArgs : EventArgs
    {
        public ElementSelectedEventArgs(IDmsElement element)
        {
            SelectedElement = element;
        }

        public IDmsElement SelectedElement { get; private set; }
    }
}
