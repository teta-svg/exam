using System.Collections.ObjectModel;
using System.Windows;

namespace SubjectControlApp
{
    public partial class MainWindow : Window
    {
        private readonly SubjectService _subjectService;
        private int _count = -1;
        private int _countAdd = 0;

        public ObservableCollection<Subject> Subjects { get; set; } = new ObservableCollection<Subject>();
        public ObservableCollection<Subject> SubjectsSort { get; set; } = new ObservableCollection<Subject>();

        private SubjectControl _subjectControl = new();

        public MainWindow()
        {
            InitializeComponent();
            DataContext = this;

            _subjectService = new SubjectService();
        }

        private async void SortBtn_Click(object sender, RoutedEventArgs e)
        {
            MassLengh.Text = string.Empty;

            for (int i = 0; i < _subjectControl.Subjects.Count - 1; i++)
            {
                for (int j = 0; j < _subjectControl.Subjects.Count - 1 - i; j++)
                {

                    if (_subjectControl.Subjects[j].AttestationType == (_subjectControl.Subjects[j + 1].AttestationType))
                    {
                        if (Subjects[j].Name.CompareTo(_subjectControl.Subjects[j + 1].Name) < 0)
                        {
                            Subject tempSub = _subjectControl.Subjects[j];
                            Subjects[j] = _subjectControl.Subjects[j + 1];
                            _subjectControl.Subjects[j + 1] = tempSub;
                        }
                    }
                    if (_subjectControl.Subjects[j].AttestationType.CompareTo(_subjectControl.Subjects[j + 1].AttestationType) < 0)
                    {
                        Subject tempSub = _subjectControl.Subjects[j];
                        Subjects[j] = _subjectControl.Subjects[j + 1];
                        _subjectControl.Subjects[j + 1] = tempSub;
                    }
                }
            }

            string temp = "Название;Семестр;Тип аттестации\n";

            foreach (Subject subject in _subjectControl.Subjects)
            {
                temp += $"{subject.Name};{subject.Semestr};{subject.AttestationType}\n";
                SubjectsSort.Add(subject);
            }
            await _subjectService.ImportToFileAsync(temp);
            temp = string.Empty;
            MessageBox.Show("Отсортированный массив успешно записан в файл");
        }

        private void CountBtn_Click(object sender, RoutedEventArgs e)
        {
            if(!int.TryParse(CountBox.Text, out int count))
            {
                MessageBox.Show("Введите целое число!");
                return;
            }

            if(count <= 0)
            {
                MessageBox.Show("Введите целое число больше 0!");
                return;
            }

            _count = count;
            MassLengh.Text = _count.ToString();

            CountBox.Text = string.Empty;
        }

        private void AddBtn_Click(object sender, RoutedEventArgs e)
        {
            if (_count < 0)
            {
                MessageBox.Show("Введите размер массива!");
                return;
            }

            if (string.IsNullOrEmpty(NameBox.Text) && string.IsNullOrEmpty(TypeAttBox.Text) && string.IsNullOrEmpty(SemestrBox.Text))
            {
                MessageBox.Show("Заполните все поля!");
                return;
            }

            if (_countAdd >= _count)
            {
                MessageBox.Show("Массив полностью заполнен!");
                return;
            }

            if (!int.TryParse(SemestrBox.Text, out int semester))
            {
                MessageBox.Show("Введите целое число!");
                return;
            }

            if (semester <= 0)
            {
                MessageBox.Show("Введите целое число больше 0!");
                return;
            }

            Subject subjectTemp = new Subject()
            {
                Name = NameBox.Text,
                Semestr = semester,
                AttestationType = TypeAttBox.Text
            };

            Subjects.Add(subjectTemp);
            _subjectControl.Subjects.Add(subjectTemp);
            _countAdd++;

            NameBox.Text = string.Empty;
            TypeAttBox.Text = string.Empty;
            SemestrBox.Text = string.Empty;
        }
    }
}