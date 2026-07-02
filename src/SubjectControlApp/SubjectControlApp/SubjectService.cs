using System.IO;
using System.Windows;

namespace SubjectControlApp
{
    public class SubjectService
    {
        public async Task ImportToFileAsync(string data)
        {
            try
            {
                using (FileStream file = new FileStream("SortResult.txt", FileMode.Create))
                {
                    byte[] massString = System.Text.Encoding.Default.GetBytes(data);
                    await file.WriteAsync(massString, 0, massString.Length);
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show($"Ошибка сохранения {ex.Message}");
            }
        }
    }
}
