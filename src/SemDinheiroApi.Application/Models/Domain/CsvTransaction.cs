using CsvHelper.Configuration;
using CsvHelper.Configuration.Attributes;
using CsvHelper.TypeConversion;

namespace SemDinheiroApi.Databases.Models.Domain;

public class CsvTransaction
{
    [Name("Data")]
    [Format("dd/MM/yyyy")]
    public DateTime Data { get; set; }

    [Name("Estabelecimento")]
    public string Estabelecimento { get; set; }

    [Name("Portador")]
    public string Portador { get; set; }

    [Name("Valor")]
    public string Valor { get; set; }

    [Name("Parcela")]
    public string Parcela { get; set; }
}

public class TransacaoCsvMap : ClassMap<CsvTransaction>
{
    public TransacaoCsvMap()
    {
        Map(m => m.Data).Name("Data").TypeConverterOption.Format("dd/MM/yyyy");
        Map(m => m.Estabelecimento).Name("Estabelecimento");
        Map(m => m.Valor).Name("Valor").TypeConverter<DecimalConverter>();
    }
}