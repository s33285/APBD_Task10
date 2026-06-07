var builder = WebApplication.CreateBuilder(args);


builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policy =>
        policy.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
});

var app = builder.Build();
app.UseCors();

var students = new List<StudentDto>
{
    new(1, "s00001", "Jan", "Kowalski", "jan@example.com", 3),
    new(2, "s00002", "Anna", "Nowak", "anna@example.com", 5),
};

var courses = new List<CourseDto>
{
    new(1, "Mathematics", 6),
    new(2, "Physics", 4),
    new(3, "Databases", 5),
};

var studentCourses = new List<StudentCourseDto>();
int nextStudentId = 3;

app.MapGet("/api/students", () => students);

app.MapGet("/api/students/{id:int}", (int id) =>
{
    var s = students.FirstOrDefault(x => x.Id == id);
    return s is null ? Results.NotFound() : Results.Ok(s);
});

app.MapPost("/api/students", (CreateStudentDto dto) =>
{
    var s = new StudentDto(
        nextStudentId++,
        dto.IndexNumber,
        dto.FirstName,
        dto.LastName,
        dto.Email,
        dto.Semester);
    students.Add(s);
    return Results.Created($"/api/students/{s.Id}", s);
});

app.MapGet("/api/courses", () => courses);

app.MapGet("/api/students/{id:int}/courses", (int id) =>
{
    var assignedIds = studentCourses
        .Where(x => x.StudentId == id)
        .Select(x => x.CourseId);
    return courses.Where(c => assignedIds.Contains(c.Id));
});

app.MapPost("/api/students/{id:int}/courses", (int id, AssignCourseDto dto) =>
{
    if (!students.Any(x => x.Id == id))
        return Results.NotFound();
    studentCourses.Add(new StudentCourseDto(id, dto.CourseId, DateTime.UtcNow));
    return Results.Ok();
});


app.Run();


record StudentDto(int Id, string IndexNumber, string FirstName,
    string LastName, string Email, int Semester);
record CourseDto(int Id, string Name, int Ects);
record StudentCourseDto(int StudentId, int CourseId, DateTime AssignedAt);
record CreateStudentDto(string IndexNumber, string FirstName,
    string LastName, string Email, int Semester);
record AssignCourseDto(int CourseId);

