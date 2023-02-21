using Microsoft.Extensions.Configuration;
using PersonLibrary;
using StudentLibrary;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization.Formatters.Soap;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.Xml.Serialization;
using ThirdLab;
using ThirlLab.Extensions;

namespace ThirlLab
{
    public class Program
    {
        static void Main(string[] args)
        {
            // Configuration Initialization
            IConfiguration config = new ConfigurationBuilder().BuildJsonConfiguration();
            var student = Student.RandomStudent();
            student.Print();

            // Binary Serialization
            var binaryFormatter = new BinaryFormatter();
            string filePath = Path.Combine(config["destinationPath"], "student.bin");
            FileStream stream = File.OpenWrite(filePath);
            binaryFormatter.Serialize(stream, student);
            stream.Dispose();
            // Deserialization
            stream = File.OpenRead(filePath);
#pragma warning disable S5773 // Types allowed to be deserialized should be restricted
            var deserializedStudent = (Student)binaryFormatter.Deserialize(stream);
            stream.Dispose();
            deserializedStudent.Print();

            // Soap Serialization
            var node = new Node(1, new Node(2, new Node(3, new Node(4, null))));
            Console.WriteLine(node);
            var soapFormatter = new SoapFormatter();
            stream = File.OpenWrite(filePath);
            filePath = Path.Combine(config["destinationPath"], "nodes.xml");
            soapFormatter.Serialize(stream, node);
            stream.Dispose();
            // Deserialization
            stream = File.OpenRead(filePath);
            var deserializedNode = (Node)soapFormatter.Deserialize(stream);
            Console.WriteLine(deserializedNode);
            stream.Dispose();
#pragma warning restore S5773 // Types allowed to be deserialized should be restricted

            // XML Serialization
            filePath = Path.Combine(config["destinationPath"], "student.xml");
            stream = new FileStream(filePath, FileMode.Create, FileAccess.Write);
            var xmlSerializer = new XmlSerializer(typeof(Student));
            xmlSerializer.Serialize(stream, student);
            student.Print();
            stream.Dispose();
            // Deserialization
            stream = File.OpenRead(filePath);
            student = (Student)xmlSerializer.Deserialize(stream);
            student.Print();
            stream.Dispose();

            // DataContract Serialization
            var dataContractSerializer = new DataContractSerializer(typeof(Student));
            var seniorStudent = SeniorStudent.RandomSeniorStudent();
            seniorStudent.Print();
            filePath = Path.Combine(config["destinationPath"], "seniorStudent.xml");
            stream = new FileStream(filePath, FileMode.Create, FileAccess.Write);
            dataContractSerializer.WriteObject(stream, seniorStudent);
            stream.Dispose();
            // Deserialization
            stream = File.OpenRead(filePath);
            seniorStudent = (SeniorStudent)dataContractSerializer.ReadObject(stream);
            seniorStudent.Print();
            stream.Close();
            Console.ReadKey();
        }
    }
}
