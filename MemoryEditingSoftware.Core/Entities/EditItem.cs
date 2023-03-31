using System;

namespace MemoryEditingSoftware.Core.Entities
{
    public class EditItem
    {
        public string ID { get; set; } = Guid.NewGuid().ToString();
        public string Name { get; set; } = "";
        public string Address { get; set; } = "0x";
        public bool IsRead { get; set; } = false;
        public string Value { get; set; } = "";
        public bool IsLoop { get; set; } = false;
        public bool IsEnterValue { get; set; } = false;

        public int UpdateEditItem(EditItem newItem)
        {
            this.Address = newItem.Address;
            this.IsEnterValue = newItem.IsEnterValue;
            //this.ID = newItem.ID;
            this.IsLoop = newItem.IsLoop;
            this.Name = newItem.Name;
            this.Value = newItem.Value;
            this.IsRead = newItem.IsRead;

            return 0;
        }

        public EditItem Copy(EditItem ei)
        {
            return new EditItem()
            {
                Address = ei.Address,
                //ID = ei.ID,
                IsEnterValue = ei.IsEnterValue,
                IsLoop = ei.IsLoop,
                IsRead = ei.IsRead,
                Name = ei.Name,
                Value = ei.Value
            };
        }

        public override string ToString()
        {
            return $"{ID}: {Name}";
        }

    }
}
