using System;

namespace MemoryEditingSoftware.Core.Attributes
{
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field)]
    public class EditableProperty : Attribute
    {
    }
}