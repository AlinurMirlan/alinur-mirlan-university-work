using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ThirdLab.extensions
{
    public static class ConfigurationBuilderExtensions
    {
        public static IConfiguration BuildJsonConfiguration(this ConfigurationBuilder builder)
        {
            string projectPath = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.ToString();
            string configFilePath = Path.Combine(projectPath, "config.json");
            return builder.AddJsonFile(configFilePath).Build();
        }
    }
}
