using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfApp1.Models;

namespace WpfApp1.DB
{
  public class LocalDB
    {
        public LocalDB()
        {
            Init();
        }
        private List<Student> students;
        private void Init()
        {
            students = new List<Student>();
            for (int i = 0; i < 30; i++)
            {
                students.Add(new Student() { ID = i, Name = $"Sample{i}" });
            }
        }
        public List<Student> GetData()
        {
            return students;
        }
        public void AddData(Student student)
        {
            students.Add(student);
        }
        public void DelData(int id)
        {
            var model = students.FirstOrDefault(t => t.ID == id);
            if (model != null)
            {
                students.Remove(model);
            }
        }
        public List<Student> FindDataByName(string name)
        {
            return students.Where(q => q.Name.Contains(name)).ToList();
        }
    }

}
