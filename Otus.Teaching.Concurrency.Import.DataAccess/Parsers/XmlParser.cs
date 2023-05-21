using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;
using Otus.Teaching.Concurrency.Import.Core.Entities;
using Otus.Teaching.Concurrency.Import.Core.Parsers;
using Otus.Teaching.Concurrency.Import.DataAccess.Dto;

namespace Otus.Teaching.Concurrency.Import.DataAccess.Parsers;

public class XmlParser : IDataParser<List<Customer>>
{
    private readonly string _fileName;

    public XmlParser(string fileName, string directory)
    {
        _fileName = Path.Combine(directory, $"{fileName}.xml");
    }

    public List<Customer> Parse()
    {
        XmlSerializer serializer = new XmlSerializer(typeof(CustomersList));
        CustomersList list;
        using (Stream reader = new FileStream(_fileName, FileMode.Open, FileAccess.Read))
        {
            list = (CustomersList) serializer.Deserialize(reader);
        }

        return list?.Customers ?? new List<Customer>();
    }
}