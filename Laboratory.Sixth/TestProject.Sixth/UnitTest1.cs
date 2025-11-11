using ClassLib.Sixth;

namespace TestProject.Sixth;

public class Tests
{
    private Subject[] subjects;
    private Student[] students;

    [SetUp]
    public void Setup()
    {
        subjects =
        [
            new Subject(1, "Math"),
            new Subject(2, "Physics"),
            new Subject(3, "CS")
        ];

        students =
        [
            new Student(1, "Alice Johnson", "A-01",
                new Dictionary<int, int> { {1, 5}, {2, 3}, {3, 4} }),

            new Student(2, "Bob Smith", "A-01",
                new Dictionary<int, int> { {1, 2}, {3, 3} }),

            new Student(3, "Charlie Brown", "B-02",
                new Dictionary<int, int> { {1, 4}, {2, 4} })
        ];
    }

    [Test]
    public void GetAverageGradeForSubject_ReturnsCorrectValue()
    {
        var manager = new SessionManager(students, subjects, null);
        var avg = manager.GetAverageGradeForSubject(1);

        Assert.That(avg, Is.Not.Null);
        Assert.That(avg.Value, Is.EqualTo((5 + 2 + 4) / 3.0).Within(0.0001));
    }

    [Test]
    public void GetAverageGradeForSubject_NoGrades_ReturnsNull()
    {
        var manager = new SessionManager(students, subjects, null);
        var avg = manager.GetAverageGradeForSubject(999);

        Assert.That(avg, Is.Null);
    }

    [Test]
    public void GetAverageGradeForGroup_ReturnsCorrectValue()
    {
        var manager = new SessionManager(students, subjects, null);

        var avg = manager.GetAverageGradeForGroup("A-01");
        Assert.That(avg.Value, Is.EqualTo((5 + 3 + 4 + 2 + 3) / 5.0).Within(0.0001));
    }

    [Test]
    public void GetAverageGradeForGroup_NoSuchGroup_ReturnsNull()
    {
        var manager = new SessionManager(students, subjects, null);

        var avg = manager.GetAverageGradeForGroup("NO-GROUP");
        Assert.That(avg, Is.Null);
    }

    [Test]
    public void CountUnsatisfactoryStudentsForSubject_Works()
    {
        var manager = new SessionManager(students, subjects, null);

        int count = manager.CountUnsatisfactoryStudentsForSubject(1);

        Assert.That(count, Is.EqualTo(1));
    }

    [Test]
    public void CountUnsatisfactoryStudentsForSubject_NoData_ReturnsZero()
    {
        var manager = new SessionManager(students, subjects, null);

        int count = manager.CountUnsatisfactoryStudentsForSubject(999);

        Assert.That(count, Is.EqualTo(0));
    }

    [Test]
    public void FindSubjectIdByName_Works()
    {
        var manager = new SessionManager(students, subjects, null);

        Assert.That(manager.FindSubjectIdByName("Physics"), Is.EqualTo(2));
        Assert.That(manager.FindSubjectIdByName("cs"), Is.EqualTo(3)); 
    }

    [Test]
    public void FindSubjectIdByName_NotFound_ReturnsMinusOne()
    {
        var manager = new SessionManager(students, subjects, null);
        Assert.That(manager.FindSubjectIdByName("Biology"), Is.EqualTo(-1));
    }

    [Test]
    public void AddStudent_AppendsToArray()
    {
        var manager = new SessionManager(students, subjects, null);

        var newStudent = new Student(10, "David", "A-01",
            new Dictionary<int, int> { { 1, 5 } });

        int newLength = manager.AddStudent(newStudent);

        Assert.That(newLength, Is.EqualTo(students.Length + 1));
        Assert.That(manager.Students.Last().Id, Is.EqualTo(10));
    }

    [Test]
    public void RemoveStudentById_RemovesCorrectStudent()
    {
        var manager = new SessionManager(students, subjects, null);

        bool removed = manager.RemoveStudentById(2);

        Assert.That(removed, Is.True);
        Assert.That(manager.Students.Any(s => s.Id == 2), Is.False);
    }

    [Test]
    public void RemoveStudentById_NotFound_ReturnsFalse()
    {
        var manager = new SessionManager(students, subjects, null);

        bool removed = manager.RemoveStudentById(999);

        Assert.That(removed, Is.False);
    }

    [Test]
    public void SetData_ReplacesAllArrays()
    {
        var manager = new SessionManager();

        manager.SetData(students, subjects, []);

        Assert.That(manager.Students.Length, Is.EqualTo(students.Length));
        Assert.That(manager.Subjects.Length, Is.EqualTo(subjects.Length));
    }
}
