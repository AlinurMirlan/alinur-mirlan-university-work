using FourthLab;
using FourthLab.Attributes;
using Microsoft.Extensions.Configuration;
using System.Reflection;
using System.Text;

string configPath = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.FullName;
configPath = Path.Combine(configPath, "config.json");
IConfiguration config = new ConfigurationBuilder().AddJsonFile(configPath).Build();

#pragma warning disable S3885 // "Assembly.Load" should be used
Assembly assembly = Assembly.LoadFrom(config["assemblyFullPath"]);
#pragma warning restore S3885 // "Assembly.Load" should be used
Console.WriteLine("All of the classes defined in the StudentLibrary assembly.");
foreach (Type type in assembly.GetTypes())
{
    if (type.IsClass)
        Console.WriteLine(type.Name);
}

Type studentType = assembly.GetType("StudentLibrary.Student");
Type educationType = assembly.GetType("StudentLibrary.Education");
if (studentType is null)
{
    Console.WriteLine("Student type doesn't exist.");
    return;
}
if (educationType is null)
{
    Console.WriteLine("Education type doesn't exist.");
    return;
}

Console.WriteLine("\nAccessing properties and methods during runtime:");
ConstructorInfo constructor = studentType.GetConstructor(new Type[] { typeof(string), typeof(string), typeof(DateTime),  educationType, typeof(string) });
object student = constructor.Invoke(new object[] { "Alinur", "Mirlan", new DateTime(2002, 11, 18), 2, "SE-2-20" });

string propertyName = config["Student:Property:Name"];
string propertyValue = config["Student:Property:Value"];
PropertyInfo groupProperty = student.GetType().GetProperty(propertyName);
Console.WriteLine($"{propertyName} before: {groupProperty.GetValue(student)}");
groupProperty.SetValue(student, propertyValue);
Console.WriteLine($"{propertyName} after: {groupProperty.GetValue(student)}");
MethodInfo printMethod = studentType.GetMethod("Print");
printMethod.Invoke(student, null);


ClassHierarchyAttribute hierarchy = studentType.GetCustomAttribute<ClassHierarchyAttribute>();
ImplementedInterfacesAttribute interfaces = studentType.GetCustomAttribute<ImplementedInterfacesAttribute>();
Console.WriteLine("\nImplemented interfaces:");
foreach (string implemented in interfaces.Interfaces)
    Console.WriteLine(implemented);
Console.WriteLine($"Type hierarhcy: {hierarchy.Hierarchy}");


Console.WriteLine("\nImplemented interfaces:");
foreach (Type interfaceType in studentType.GetInterfaces())
    Console.WriteLine(interfaceType.Name);

Type parentType = student.GetType();
StringBuilder text = new("\nType hierarchy: ");
Stack<string> textStack = new();
while (parentType is not null)
{
    textStack.Push($"{parentType.Name} -> ");
    parentType = parentType.BaseType;
}
foreach (string type in textStack)
    text.Append(type);

Console.WriteLine(text.ToString()[..^3]);


Type dictionaryType = typeof(Dictionary<string, int>);
object dictionary = Activator.CreateInstance(dictionaryType);
dictionaryType.GetMethod("Add").Invoke(dictionary, new object[] { "Alinur", 20 });
dictionaryType.GetMethod("Add").Invoke(dictionary, new object[] { "Erlan", 17 });
dictionaryType.GetMethod("Add").Invoke(dictionary, new object[] { "Kunduz", 9 });
PropertyInfo indexer = dictionaryType.GetProperties().FirstOrDefault(p => p.GetIndexParameters().Length == 1);
Console.WriteLine($"{indexer.GetValue(dictionary, new string[] { "Alinur" })}");

foreach (string filePath in Directory.EnumerateFiles(config["pluginsPath"]))
{
    if (Path.GetExtension(filePath) != ".dll")
        continue;

#pragma warning disable S3885 // "Assembly.Load" should be used
    Assembly pluginAssembly = Assembly.LoadFrom(filePath);
#pragma warning restore S3885 // "Assembly.Load" should be used
    foreach (Type type in pluginAssembly.GetTypes())
    {
        if (!type.IsClass)
            continue;
        
        if (type.GetInterface("ISound") != null)
        {
            ISound sound = (ISound)Activator.CreateInstance(type);
            sound.ProduceSound();
        }
    }
}

