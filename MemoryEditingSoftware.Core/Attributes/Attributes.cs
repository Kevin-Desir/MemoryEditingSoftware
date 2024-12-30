using System;

namespace MemoryEditingSoftware.Core.Attributes
{
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field)]
    public class EditableProperty : Attribute
    {
        public string Name { get; }

        public EditableProperty(string name = null)
        {
            Name = name;
        }
    }
}