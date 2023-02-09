using Recolletction;

// 1st task
Person person = new("Alinur", "Mirlan", new DateTime(2002, 11, 18));
Person personReplica = new("Alinur", "Mirlan", new DateTime(2002, 11, 18));
Console.WriteLine($"person: {person}\npersonReplica: {personReplica}");
Console.WriteLine($"Do the objects have the same reference? {person == personReplica}");
Console.WriteLine($"Do the objects share their properties? {person.Equals(personReplica)}");
Console.WriteLine($"Hash code of the person: {person.GetHashCode()}, personReplica: {personReplica.GetHashCode()}\n");

// 2nd task
Student student = new(person, Education.Bachelor, 202);

student.AddExams(
    new Exam() { Subject = "Maths", Grade = 5, Date = new DateTime(2022, 11, 11) },
    new Exam() { Subject = "Maths", Grade = 5, Date = new DateTime(2022, 12, 11) },
    new Exam() { Subject = "Computer Science", Grade = 2, Date = new DateTime(2022, 1, 12) },
    new Exam() { Subject = "Computer Science", Grade = 2, Date = new DateTime(2022, 2, 12) },
    new Exam() { Subject = "Computer Science", Grade = 3, Date = new DateTime(2022, 3, 12) }
);

student.AddTests(
    new Test() { Subject = "Maths", IsCredited = true },
    new Test() { Subject = "Computer Science", IsCredited = false }
);

// 3rd task
Console.WriteLine($"{student.Person}\n");

// 4th task
Student studentReplica = (student.DeepCopy() as Student)!;
studentReplica.BirthYear = 2020;
Console.WriteLine($"student: {student.ToShortString()}\nstudentReplica: {studentReplica.ToShortString()}\n");

// 5th task
try
{
    student.Group = 2;
}
catch (ArgumentException e)
{
    Console.WriteLine(e.Message);
}

// 6th task
foreach (object element in student.Enumerate())
{
    if (element is Exam exam)
        Console.WriteLine($"Exam: {exam}");
    else if (element is Test test)
        Console.WriteLine($"Test: {test}");
}
Console.WriteLine();

// 7th task
foreach (Exam element in student.EnumerateExamsAboveGrade(3))
    Console.WriteLine($"Exam: {element}");




