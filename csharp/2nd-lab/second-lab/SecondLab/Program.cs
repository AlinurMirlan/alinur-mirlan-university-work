using Microsoft.Extensions.Configuration;
using SecondLab;
using SecondLab.entities;
using System.Text.Json;

// Configuration Initialization
string? configDirectory = Directory.GetParent(Directory.GetCurrentDirectory())?.Parent?.Parent?.FullName;
if (configDirectory is null)
{
    Console.WriteLine("Configuration file hasn't been initalized. Please, make sure the path is set correctly.");
    return;
}

string configPath = Path.Combine(configDirectory, "config.json");
IConfiguration config = new ConfigurationBuilder().AddJsonFile(configPath).Build();

// 1st task
int collectionLength = config.GetValue<int>("sequenceLength");
ICollection<string> sequence = Utility.GenerateStrings(collectionLength);
sequence.Display("1st task");

string initialSequence = sequence.Aggregate(string.Empty, (total, element) => string.Concat(total, element[0]));
Console.WriteLine($"Aggregated string: {initialSequence}\n");

// 2nd task
Random random = new();
int lastIndex = collectionLength - 1;
int leftLimit = random.Next(lastIndex);
int rightLimit = leftLimit + random.Next(lastIndex - leftLimit - 1) + 1;
Console.WriteLine($"2nd task\nExclude from: {leftLimit} to {rightLimit} inclusively");
int numerator = sequence
    .Where((element, index) => index < leftLimit || index > rightLimit)
    .Select((element, index) => element.Length)
    .Sum();
int denominator = collectionLength - (rightLimit - leftLimit) - 1;

Console.WriteLine($"Average: {numerator} / {denominator} = {(double)numerator / denominator}\n");

// 3rd task
int requiredLength = random.Next(12);
sequence = Utility.GenerateStrings(collectionLength, '.');
sequence.Display("3rd task");
var resultSequence =
    from element in sequence
    let words = element.Split('.')
    from word in words
    where word.Length == requiredLength
    orderby word ascending
    select word;

Console.WriteLine($"Words of length {requiredLength}:");
resultSequence.Display();

// 4th task
IEnumerable<int> numberSequence = Enumerable.Range(0, collectionLength).Select((_) => random.Next(999)).ToList();
numberSequence.Display("\n4th task");
resultSequence =
    from number in numberSequence
    let lastDigit = number % 10
    group number by lastDigit into numbersByLastDigit
    orderby numbersByLastDigit.Key ascending
    select $"{numbersByLastDigit.Key}:{numbersByLastDigit.Sum()}";
resultSequence.Display();


// 5th task
Client[] clients = JsonSerializer.Deserialize<Client[]>(File.OpenRead(config["clientsFilePath"]!))!;
clients.Display("5th task");

var clientSequence =
    from client in clients
    group client by (client.Year, client.Month) into clientsByTime
    let totalDuration = clientsByTime.Aggregate(0, (total, next) => total + next.LessonDuration)
    orderby totalDuration, clientsByTime.Key.Year descending, clientsByTime.Key.Month
    select new { Date = clientsByTime.Key, Duration = totalDuration };

foreach (var obj in clientSequence)
    Console.WriteLine($"{obj.Date}: {obj.Duration}");

// 6th task
Product[] prods = JsonSerializer.Deserialize<Product[]>(File.OpenRead(config["productsFilePath"]!))!;
Purchase[] purchs = JsonSerializer.Deserialize<Purchase[]>(File.OpenRead(config["purchasesCostFilePath"]!))!;
ProductCost[] prodCosts = JsonSerializer.Deserialize<ProductCost[]>(File.OpenRead(config["productsCostFilePath"]!))!;

var countryStatistics =
    from prod in prods
    from acq in (from pch in purchs
                 let cost = prodCosts.First(p => p.ProductCode == pch.ProductCode && p.ShopName == pch.ShopName).Cost
                 select new { Cost = cost, pch.ProductCode })
    where prod.Code == acq.ProductCode
    orderby prod.Country
    group acq by prod.Country;
 
Console.WriteLine("\n6th task");
foreach (var cst in countryStatistics)
    Console.WriteLine($"{cst.Key}-{cst.Count()}-{cst.Select(c => c.Cost).Sum()}");