namespace MemoryEditingSoftware.Core.Entities
{
    public class EditItem
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public bool IsRead { get; set; }
        public string Value { get; set; }
        public bool IsLoop { get; set; }
        public bool IsEnterValue { get; set; }

        public override string ToString()
        {
            return $"{ID}: {Name}";
        }
    }
}
