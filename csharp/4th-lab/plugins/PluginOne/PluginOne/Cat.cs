using FourthLab;

namespace PluginOne
{
    public class Cat : ISound
    {
        public void ProduceSound() => Console.WriteLine("Meow");
    }
}