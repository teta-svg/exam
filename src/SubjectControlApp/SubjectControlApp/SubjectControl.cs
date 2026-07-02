using System.Collections.ObjectModel;

namespace SubjectControlApp
{
    public class SubjectControl
    {
        public ObservableCollection<Subject> Subjects { get; set; } = new ObservableCollection<Subject>();
    }
}
