using ClassLib.Sixth;

namespace WinForms;

public class MainForm : Form
{
    private SessionManager manager = new SessionManager();
    private ListBox lbStudents;
    private Button btnAddSample;
    private Button btnAvgSubject;
    private Button btnAvgGroup;
    private Button btnCountFail;
    private TextBox tbSubjectName;
    private TextBox tbGroupName;
    private Label lblOutput;


    public MainForm()
    {
        Text = "Student Session Manager";
        Width = 800;
        Height = 600;


        lbStudents = new ListBox { Top = 10, Left = 10, Width = 480, Height = 400 };
        Controls.Add(lbStudents);


        btnAddSample = new Button { Text = "Add sample data", Top = 420, Left = 10 };
        btnAddSample.Click += BtnAddSample_Click;
        Controls.Add(btnAddSample);


        var x = 500;
        Controls.Add(new Label { Text = "Subject name:", Top = 10, Left = x });
        tbSubjectName = new TextBox { Top = 30, Left = x, Width = 260 }; Controls.Add(tbSubjectName);
        btnAvgSubject = new Button { Text = "Avg for Subject", Top = 60, Left = x }; btnAvgSubject.Click += BtnAvgSubject_Click; Controls.Add(btnAvgSubject);


        Controls.Add(new Label { Text = "Group name:", Top = 110, Left = x });
        tbGroupName = new TextBox { Top = 130, Left = x, Width = 260 }; Controls.Add(tbGroupName);
        btnAvgGroup = new Button { Text = "Avg for Group", Top = 160, Left = x }; btnAvgGroup.Click += BtnAvgGroup_Click; Controls.Add(btnAvgGroup);


        btnCountFail = new Button { Text = "Count Unsatisfactory (subject)", Top = 210, Left = x }; btnCountFail.Click += BtnCountFail_Click; Controls.Add(btnCountFail);


        lblOutput = new Label { Top = 260, Left = x, Width = 260, Height = 200, AutoSize = false, BorderStyle = BorderStyle.FixedSingle }; Controls.Add(lblOutput);


        // start empty manager
        manager = new SessionManager();
    }

    private void BtnAddSample_Click(object? sender, EventArgs e)
    {
        var subjects = new[] { new Subject(1, "Math"), new Subject(2, "Physics"), new Subject(3, "History") };
        var students = new[]
        {
            new Student(1, "Pochevalou D.", "G1", new() { [1] = 1, [2] = 1, [3] = 1,}),
            new Student(1, "Loseu M.", "G1", new() { [1] = 1, [2] = 1, [3] = 1,}),
            new Student(1, "Alampiev D.", "G1", new() { [1] = 1, [2] = 1, [3] = 1,}),
        };

        var groups = new[] { new Group("G1"), new Group("G2") };
        manager.SetData(students, subjects, groups);
        RefreshStudentsList();
        lblOutput.Text = "Sample data added.";
    }


    private void RefreshStudentsList()
    {
        lbStudents.Items.Clear();
        foreach (var s in manager.Students)
        {
            var grades = string.Join(
                ", ", 
                s
                .SubjectsToGrades
                .Select(g => $"{manager
                    .Subjects
                    .FirstOrDefault(sub => sub.Id == g.Key)?.Name ?? g.Key.ToString()}: {g.Value}"
                )
            );

            lbStudents.Items.Add($"{s.Id}: {s.FullName} [{s.Group}] -> {grades}");
        }
    }


    private void BtnAvgSubject_Click(object? sender, EventArgs e)
    {
        var name = tbSubjectName.Text.Trim();
        if (string.IsNullOrEmpty(name)) { lblOutput.Text = "Enter subject name."; return; }
        int id = manager.FindSubjectIdByName(name);
        if (id < 0) { lblOutput.Text = "Subject not found."; return; }
        var avg = manager.GetAverageGradeForSubject(id);
        lblOutput.Text = avg.HasValue ? $"Average for '{name}': {avg.Value:F2}" : "No grades for subject.";
    }


    private void BtnAvgGroup_Click(object? sender, EventArgs e)
    {
        var name = tbGroupName.Text.Trim();
        if (string.IsNullOrEmpty(name)) { lblOutput.Text = "Enter group name."; return; }
        var avg = manager.GetAverageGradeForGroup(name);
        lblOutput.Text = avg.HasValue ? $"Average for group '{name}': {avg.Value:F2}" : "No grades for group or group not found.";
    }


    private void BtnCountFail_Click(object? sender, EventArgs e)
    {
        var name = tbSubjectName.Text.Trim();
        if (string.IsNullOrEmpty(name)) { lblOutput.Text = "Enter subject name."; return; }
        int id = manager.FindSubjectIdByName(name);
        if (id < 0) { lblOutput.Text = "Subject not found."; return; }
        int cnt = manager.CountUnsatisfactoryStudentsForSubject(id);
        lblOutput.Text = $"Students with unsatisfactory in '{name}': {cnt}";
    }
}