namespace RawDataFilter.Entities
{

    public class RawDataFilter
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public List<Binary> byte_ { get; set; }
        public HashCode hash { get; set; }

        public RawDataFilter(Guid _id, string _name, List<byte> _byte, HashCode _hash)
        {
            this.Id = _id;
            this.Name = _name;
            this.byte_ = new List<Binary>();
            this.hash = _hash;
        }

        private RawDataFilter(){}

        public RawDataFilter GetAllRawDataFilters()
        {
            var rawDataFilter = new RawDataFilter
            {
                Id = new Guid(),
                Name = "Test",
                hash = new HashCode(),
            };
                
            return rawDataFilter;
        }
    }
}