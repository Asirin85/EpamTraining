namespace BusinessLogic.Interfaces
{
    using System.Collections.Generic;

    public interface IFormatConverter<T>
    {
        string ConvertToJSONString(IEnumerable<T> list);
        string ConvertToXMLString(IEnumerable<T> list);
    }
}
