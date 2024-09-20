namespace MemoryEditingSoftware.Core.Entities
{
    public class SimpleReader : EditItem
    {
        #region Constructors

        public SimpleReader()
        {
            
        }

        public SimpleReader(string address, string name, string value, bool isRead) : base(address, name, value, isRead)
        {
        }

        public SimpleReader(EditItem editItem) : base(editItem.Address, editItem.Name, editItem.Value, editItem.IsRead)
        {
        }

        #endregion

        public override int Update(EditItem newItem)
        {
            return base.Update(newItem);
        }

        public override EditItem Copy(EditItem readerToCopy)
        {
            return new SimpleReader(readerToCopy);
        }
    }
}
