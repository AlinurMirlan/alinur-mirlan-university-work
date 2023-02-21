namespace StudentLibrary
{
    public class Person
    {
        private static readonly Person[] People =
        {
            new Person("Alinur", "Mirlan", new DateTime(2002, 11, 18)),
            new Person("Erlan", "Esengeldiev", new DateTime(2002, 11, 18)),
            new Person("Kunduz", "Esengeldieva", new DateTime(2002, 11, 18)),
            new Person("Kylych", "Akylov", new DateTime(2002, 11, 18)),
            new Person("Eleanor", "Michel", new DateTime(2002, 11, 18)),
            new Person("Daniel", "Honan", new DateTime(2002, 11, 18)),
        };
        protected string name = default!;
        protected string surname = default!;
        protected DateTime birthDate;

        public string Name
        {
            get => name;
            set => name = value ?? throw new ArgumentNullException(nameof(value));
        }

        public string Surname
        {
            get => surname;
            set => surname = value ?? throw new ArgumentNullException(nameof(value));
        }

        public int BirthYear
        {
            get => birthDate.Year;
            set
            {
                if (value < 1910 || value > DateTime.Now.Year)
                    throw new ArgumentException($"{nameof(value)} is invalid.");

                birthDate = new DateTime(value, birthDate.Month, birthDate.Day);
            }
        }

        public DateTime Date
        {
            get => birthDate;
            set
            {
                if (value.Year < 1910 || value.Year > DateTime.Now.Year)
                    throw new ArgumentException($"{nameof(value)} is invalid.");

                birthDate = value;
            }
        }

        public Person(string name, string surname, DateTime birthDate)
        {
            Name = name;
            Surname = surname;
            Date = birthDate;
        }

        public override bool Equals(object? obj)
        {
            if (obj is not Person person)
                return false;

            return birthDate == person.birthDate
                && person.Name == Name
                && person.Surname == Surname;
        }

#pragma warning disable S2328 // "GetHashCode" should not reference mutable fields
        public override int GetHashCode() => HashCode.Combine(Name, Surname, birthDate);

#pragma warning restore S2328 // "GetHashCode" should not reference mutable fields

        public override string ToString() => $"Name: {this.name} {this.surname}. Birth date: {this.birthDate:d}.";

        public virtual void Print() => Console.WriteLine(this.ToString());

        public static Person RandomPerson()
        {
            var random = new Random();
            return People[random.Next(People.Length)];
        }
    }
}