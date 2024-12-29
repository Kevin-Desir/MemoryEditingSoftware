using MemoryEditingSoftware.Core.Attributes;
using Newtonsoft.Json;
using System;

namespace MemoryEditingSoftware.Core.Entities
{
    public class EditItem
    {
        public string ID { get; set; } = Guid.NewGuid().ToString();
        [EditableProperty]
        public string Name { get; set; } = "";
        [EditableProperty]
        public string Address { get; set; } = "0x";
        [EditableProperty]
        public bool IsRead { get; set; } = false;
        [EditableProperty]
        public string Value { get; set; } = "";
        [EditableProperty]
        public VariableTypes VariableType { get; set; } = VariableTypes.vint;

        public EditItem()
        {

        }

        public EditItem(string address, string name, string value, bool isRead)
        {
            this.Address = address;
            this.Name = name;
            this.Value = value;
            this.IsRead = isRead;
        }

        public virtual int Update(EditItem newItem)
        {
            this.Address = newItem.Address;
            this.Name = newItem.Name;
            this.Value = newItem.Value;
            this.IsRead = newItem.IsRead;

            return 0;
        }

        public virtual EditItem Copy(EditItem itemToCopy)
        {
            return new EditItem(itemToCopy.Address, itemToCopy.Name, itemToCopy.Value, itemToCopy.IsRead);
        }

        public override string ToString()
        {
            return $"{Address}: {Name}";
        }

    }
}
