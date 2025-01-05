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

    [AttributeUsage(AttributeTargets.Class, Inherited = false, AllowMultiple = false)]
    public class DroppableView : Attribute
    {
        public Type ObjectType { get; }
        public string Name { get; }
        public DroppableView(Type objectType, string name = null)
        {
            ObjectType = objectType;
            Name = name;
        }
    }
}