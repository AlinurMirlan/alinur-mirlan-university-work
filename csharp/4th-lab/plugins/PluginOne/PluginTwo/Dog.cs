using FourthLab;

namespace PluginTwo
{
    public class Dog : ISound
    {
        public void ProduceSound() => Console.WriteLine("Bark");
    }
}