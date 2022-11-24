namespace RawDataFilter.Entities
{

    public class DeviceTranslationTemplate
    {
        public Guid Vivavis_ID { get; set; }
        public string Identifier { get; set; }
        public string DeviceTypeInformation { get; set; }
        public Guid ID { get; set; }
        public string Description { get; set; }

        public DeviceTranslationTemplate(Guid vivavisId, string identifier, string deviceTypeInformation, Guid id, string description)
        {
            this.Vivavis_ID = vivavisId;
            this.Identifier = identifier;
            this.DeviceTypeInformation = deviceTypeInformation;
            this.ID = id;
            this.Description = description;
        }
    }
}