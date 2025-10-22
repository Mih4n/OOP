namespace ClassLib.Sixth;

public record Student(
    int Id, 
    string FullName, 
    string Group, 
    Dictionary<int, int> SubjectsToGrades
);