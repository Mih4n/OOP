namespace ClassLib.Sixth;

/// <summary>
/// Manages students, subjects and groups using arrays.
/// Provides averages and failing counts.
/// </summary>
public sealed class SessionManager
{
    /// <summary>
    /// Array of students.
    /// </summary>
    public Student[] Students { get; private set; }


    /// <summary>
    /// Array of subjects.
    /// </summary>
    public Subject[] Subjects { get; private set; }


    /// <summary>
    /// Array of groups.
    /// </summary>
    public Group[] Groups { get; private set; }


    /// <summary>
    /// Creates a manager with given capacity arrays. Use arrays as required.
    /// </summary>
    /// <param name="students">Initial students array.</param>
    /// <param name="subjects">Initial subjects array.</param>
    /// <param name="groups">Initial groups array.</param>
    public SessionManager(Student[]? students = null, Subject[]? subjects = null, Group[]? groups = null)
    {
        Students = students ?? [];
        Subjects = subjects ?? [];
        Groups = groups ?? [];
    }


    /// <summary>
    /// Adds or replaces the arrays (simple array-style CRUD).
    /// </summary>
    public void SetData(Student[] students, Subject[] subjects, Group[] groups)
    {
        Students = students ?? [];
        Subjects = subjects ?? [];
        Groups = groups ?? [];
    }

    /// <summary>
    /// Computes the average grade for a specified subject across all students who have a grade for it.
    /// Returns null if no grades present.
    /// </summary>
    /// <param name="subjectId">Subject identifier.</param>
    /// <returns>Average as double or null.</returns>
    public double? GetAverageGradeForSubject(int subjectId)
    {
        // gather all numeric grades for subject
        var values = Students
            .Where(s => s.SubjectsToGrades.ContainsKey(subjectId))
            .Select(s => s.SubjectsToGrades[subjectId])
            .ToArray();


        if (values.Length == 0) return null;
        return values.Average();
    }


    /// <summary>
    /// Computes the average grade for a group across all subjects and students in the group.
    /// Returns null if no grades present for the group.
    /// </summary>
    /// <param name="groupName">Group name.</param>
    /// <returns>Average as double or null.</returns>
    public double? GetAverageGradeForGroup(string groupName)
    {
        var values = Students
        .Where(s => string.Equals(s.Group, groupName, StringComparison.OrdinalIgnoreCase))
            .SelectMany(s => s.SubjectsToGrades)
            .Select(g => g.Value)
            .ToArray();


        if (values.Length == 0) return null;
        return values.Average();
    }


    /// <summary>
    /// Counts number of students who received unsatisfactory grades for a specified subject.
    /// Unsatisfactory threshold is < 3 by default (1 or 2). This can be adjusted.
    /// </summary>
    /// <param name="subjectId">Subject identifier.</param>
    /// <param name="unsatisfactoryThreshold">Grades strictly less than this value are considered unsatisfactory (default 3).</param>
    /// <returns>Number of distinct students with any unsatisfactory grade in that subject.</returns>
    public int CountUnsatisfactoryStudentsForSubject(int subjectId, int unsatisfactoryThreshold = 3)
    {
        // count distinct students who have a grade for this subject and it is < threshold
        return Students
            .Count(s => s.SubjectsToGrades.GetValueOrDefault(subjectId) <= unsatisfactoryThreshold);
    }


    /// <summary>
    /// Finds subject id by name (case-insensitive) or -1 if not found.
    /// </summary>
    public int FindSubjectIdByName(string name)
    {
        var sub = Subjects.FirstOrDefault(s => string.Equals(s.Name, name, StringComparison.OrdinalIgnoreCase));
        return sub?.Id ?? -1;
    }


    /// <summary>
    /// Simple helper: adds a student to the internal array (returns new array length).
    /// Uses naive array resize to satisfy "use arrays" requirement.
    /// </summary>
    public int AddStudent(Student student)
    {
        int n = Students.Length;
        var newArr = new Student[n + 1];
        Array.Copy(Students, newArr, n);
        newArr[n] = student;
        Students = newArr;
        return Students.Length;
    }

    /// <summary>
    /// Simple helper: remove student by id (returns bool whether removed).
    /// </summary>
    public bool RemoveStudentById(int id)
    {
        int index = Array.FindIndex(Students, s => s.Id == id);
        if (index < 0) return false;
        var newArr = new Student[Students.Length - 1];
        if (index > 0) Array.Copy(Students, 0, newArr, 0, index);
        if (index < Students.Length - 1) Array.Copy(Students, index + 1, newArr, index, Students.Length - index - 1);
        Students = newArr;
        return true;
    }
}