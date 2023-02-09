using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Recolletction
{
#pragma warning disable S1210 // "Equals" and the comparison operators should be overridden when implementing "IComparable"
    internal class Person : IDateAndCopy, IComparable<Person>, IComparer<Person>
#pragma warning restore S1210 // "Equals" and the comparison operators should be overridden when implementing "IComparable"
    {
        protected string? name;
        protected string? surname;
        protected DateTime birthDate;

        public string Name
        {
            get => name!;
            set => name = value ?? throw new ArgumentNullException(nameof(value));
        }

        public string Surname
        {
            get => surname!;
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
        public override int GetHashCode()
        {
            int a = HashCode.Combine(Name, Surname);
            int b = birthDate.GetHashCode();
            return a + b;
        }

#pragma warning restore S2328 // "GetHashCode" should not reference mutable fields

        public override string ToString() => $"Name: {this.name} {this.surname}. Birth date: {this.birthDate:d}.";

        public virtual string ToShortString() => $"Name: {this.name} {this.surname:d}.";

        public virtual object DeepCopy() => new Person(Name, Surname, this.birthDate);

        public int CompareTo(Person? other)
        {
            if (other is null)
                return -1;

            return this.Surname.CompareTo(other.Surname);
        }

        public int Compare(Person? left, Person? right)
        {
            if (left is null && right is null)
                return 0;
            else if (left is null)
                return -1;
            else if (right is null)
                return 1;

            return left.BirthYear.CompareTo(right.BirthYear);
        }
    }
}
