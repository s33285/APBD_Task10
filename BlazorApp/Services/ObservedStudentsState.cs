using BlazorApp.Models;

namespace BlazorApp.Services
{
    public class ObservedStudentsState
    {
        private readonly List<StudentDto> _observed = new();

        public IReadOnlyList<StudentDto> Observed => _observed;

        public event Action? OnChange;

        public void Add(StudentDto student)
        {
            if (_observed.Any(s => s.Id == student.Id)) return;
            _observed.Add(student);
            NotifyStateChanged();
        }

        public void Remove(int studentId)
        {
            _observed.RemoveAll(s => s.Id == studentId);
            NotifyStateChanged();
        }

        public bool IsObserved(int studentId) =>
            _observed.Any(s => s.Id == studentId);

        private void NotifyStateChanged() => OnChange?.Invoke();
    }
}
