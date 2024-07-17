using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;

public class User
{
    public int ID { get; set; }
    public string Name { get; set; }
    public string Surname { get; set; }
    public string Email { get; set; }
    public string Gender { get; set; }
    public string IpAddress { get; set; }
}

public class Program
{
    public static void Main()
    {
        string inputFile = @"C:\Users\MphoMmaako\Work\MphoMaakoBidvestTest\BDGConsoleApp\BDG_Input.txt";
        string outputFile = @"C:\Users\MphoMmaako\Work\MphoMaakoBidvestTest\BDGConsoleApp\BDG_Output.json";
        var users = new List<User>();

        try
        {
            foreach (var line in File.ReadLines(inputFile))
            {
                Console.WriteLine($"Processing line: '{line}'"); // Debug output

                if (string.IsNullOrWhiteSpace(line))
                {
                    Console.WriteLine("Skipping empty line.");
                    continue; // Skip empty lines
                }

                var parts = line.Split('|');

                if (parts.Length != 6)
                {
                    Console.WriteLine($"Invalid line format: '{line}'");
                    continue; // Skip this line
                }

                try
                {
                    users.Add(new User
                    {
                        ID = int.Parse(parts[0].Trim()),
                        Name = parts[1].Trim(),
                        Surname = parts[2].Trim(),
                        Email = parts[3].Trim(),
                        Gender = parts[4].Trim(),
                        IpAddress = parts[5].Trim()
                    });
                }
                catch (FormatException ex)
                {
                    Console.WriteLine($"Error parsing line: '{line}'. Error: {ex.Message}");
                }
            }

            var json = JsonConvert.SerializeObject(users, Formatting.Indented);
            File.WriteAllText(outputFile, json);

            Console.WriteLine("Conversion complete. JSON written to " + outputFile);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}
