using BlazorApp.Models;

namespace BlazorApp.Services
{
    public class StudentsApiClient
    {
        private readonly HttpClient _http;

        public StudentsApiClient(HttpClient http)
        {
            _http = http;
        }

        public async Task<List<StudentDto>> GetStudentsAsync()
        {
            return await _http.GetFromJsonAsync<List<StudentDto>>("api/students") ?? new();
        }

        public async Task<StudentDto?> GetStudentAsync(int id)
        {
            try
            {
                return await _http.GetFromJsonAsync<StudentDto>($"api/students/{id}");
            }
            catch (HttpRequestException)
            {
                return null;
            }
        }

        public async Task<StudentDto?> CreateStudentAsync(CreateStudentDto dto)
        {
            var response = await _http.PostAsJsonAsync("api/students", dto);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<StudentDto>();
        }

        public async Task<List<CourseDto>> GetCoursesAsync()
        {
            return await _http.GetFromJsonAsync<List<CourseDto>>("api/courses") ?? new();
        }

        public async Task<List<CourseDto>> GetStudentCoursesAsync(int studentId)
        {
            return await _http.GetFromJsonAsync<List<CourseDto>>(
                $"api/students/{studentId}/courses") ?? new();
        }

        public async Task AssignCourseAsync(int studentId, AssignCourseDto dto)
        {
            var response = await _http.PostAsJsonAsync(
                $"api/students/{studentId}/courses", dto);
            response.EnsureSuccessStatusCode();
        }
    }
}
