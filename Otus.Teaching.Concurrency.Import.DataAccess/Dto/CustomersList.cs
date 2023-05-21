using System.Collections.Generic;
using System.Xml.Serialization;
using Otus.Teaching.Concurrency.Import.Core.Entities;

namespace Otus.Teaching.Concurrency.Import.DataAccess.Dto;

[XmlRoot("Customers")]
public class CustomersList
{
    public List<Customer> Customers { get; set; }
}