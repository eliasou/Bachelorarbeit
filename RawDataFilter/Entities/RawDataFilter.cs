namespace RawDataFilter.Entities
{

    public class RawDataFilter
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public  byte[] Binary { get; set; }
        public HashCode Hash { get; set; }
        public virtual DeviceTranslationTemplate DeviceTranslationTemplate { get; set; }
        public Guid DeviceTranslationTemplateId { get; set; }
    }
}