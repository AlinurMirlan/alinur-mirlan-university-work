using Microsoft.Extensions.Configuration;
using StudentLibrary;
using System.Runtime.Serialization.Formatters.Binary;
using ThirdLab.extensions;

IConfiguration config = new ConfigurationBuilder().BuildJsonConfiguration();
var student = Student.RandomStudent();
student.Print();

BinaryFormatter binaryFormatter = new();
#pragma warning disable SYSLIB0011 // Type or member is obsolete
string filePath = Path.Combine(config["destinationPath"], "student.bin");
FileStream stream = File.OpenWrite(filePath);
binaryFormatter.Serialize(stream, student);
stream.Dispose();

stream = File.OpenRead(filePath);
var deserializedStudent = (Student)binaryFormatter.Deserialize(stream);
stream.Dispose();
#pragma warning restore SYSLIB0011 // Type or member is obsolete

deserializedStudent.Print();

