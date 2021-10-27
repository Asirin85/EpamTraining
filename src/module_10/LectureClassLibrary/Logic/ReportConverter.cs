namespace BusinessLogic.Logic
{
    using BusinessLogic.Interfaces;
    using Newtonsoft.Json;
    using System.Collections.Generic;
    using System.Xml.Serialization;

    public class ReportConverter<T> : IFormatConverter<T>
    {
        public string ConvertToJSONString(IEnumerable<T> list)
        {
            return JsonConvert.SerializeObject(list);
        }

        public string ConvertToXMLString(IEnumerable<T> list)
        {
            using (var stringwriter = new System.IO.StringWriter())
            {
                var serializer = new XmlSerializer(list.GetType());
                serializer.Serialize(stringwriter, list);
                return stringwriter.ToString();
            }
        }
    }
}
