using CsvHelper.Configuration;
using CsvHelper;
using System.Globalization;

namespace CsvReaderDemo;

class Person
{
    public string Name { get; set; }
    public int Age { get; set; }
    public string Country { get; set; }
}

public class Program
{
    static void WriteToCsv(string filePath)
    {

        try
        {
            if (!File.Exists(filePath))
            {
                using (File.Create(filePath)) { }
            }


            List<Person> people = new List<Person>
        {
          new Person { Name = "Jane", Age = 25, Country = "UK" },
          new Person { Name = "Any", Age = 30, Country = "Canada" },
          new Person { Name = "Mike", Age = 35, Country = "USA" }
        };


            var configPersons = new CsvConfiguration(CultureInfo.InvariantCulture)
            {
                HasHeaderRecord = false
            };

            using (StreamWriter streamWriter = new StreamWriter(filePath,true))
            using (CsvWriter csvWriter = new CsvWriter(streamWriter, configPersons))
            {
                csvWriter.WriteRecords(people);
            }

            Console.WriteLine("Data written to CSV successfully.");
        }

        catch(Exception ex)
        {
            throw;
        }

    }



    static void ReadFromCsv(string filePath)
    {
        try
        {
            if (File.Exists(filePath))
            {

                var config = new CsvConfiguration(CultureInfo.InvariantCulture)
                {
                    HasHeaderRecord = false
                };
                using (StreamReader streamReader = new StreamReader(filePath))
                using (CsvReader csvReader = new CsvReader(streamReader, config))
                {

                    // Read records from the CSV file
                    IEnumerable<Person> records = csvReader.GetRecords<Person>();

                    // Process each record
                    foreach (Person person in records)
                    {
                        Console.WriteLine($"Name: {person.Name}, Age: {person.Age}, Country: {person.Country}");
                    }
                }
            }
        }
        catch (Exception ex)
        {
            throw;
        }
    }


    static void Main()
    {
        string directoryPath = @"C:\Users\RD\Documents\Projects";
        string fileName = "data.csv";
        string filePath = Path.Combine(directoryPath, fileName);

        // WriteToCsv(filePath);
        ReadFromCsv(filePath);
        Console.ReadKey();
        // like ✅ and leave the comment 💬
    }
}