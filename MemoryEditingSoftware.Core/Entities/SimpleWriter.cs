using MemoryEditingSoftware.Core.Attributes;

namespace MemoryEditingSoftware.Core.Entities
{
    public class SimpleWriter : EditItem
    {
        [EditableProperty]
        public bool IsLoop { get; set; } = false;
        [EditableProperty]
        public bool IsEnterValue { get; set; } = false;

        #region Constructors

        public SimpleWriter(bool isLoop, bool isEnterValue, string address, string name, string value, bool isRead) : base(address, name, value, isRead)
        {
            this.IsLoop = isLoop;
            this.IsEnterValue = isEnterValue;
        }

        public SimpleWriter(bool isLoop, bool isEnterValue, EditItem editItem) : base(editItem.Address, editItem.Name, editItem.Value, editItem.IsRead) 
        {
            IsLoop = isLoop;
            IsEnterValue = isEnterValue;
        }

        public SimpleWriter()
        {
            
        }

        #endregion

        public override int Update(EditItem newItem)
        {
            if (newItem is SimpleWriter newWriter)
            {
                this.IsEnterValue = newWriter.IsEnterValue;
                this.IsLoop = newWriter.IsLoop;
            }

            return 0;
        }

        public override EditItem Copy(EditItem itemToCopy)
        {
            if (itemToCopy is SimpleWriter writerToCopy)
            {
                return new SimpleWriter(writerToCopy.IsLoop, writerToCopy.IsEnterValue, itemToCopy);
            }
            return null;
        }
    }
}
