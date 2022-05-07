using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace SkillFactory_Module_8_Task_4
{
    
    internal class Program
    {
        [Serializable]
        public class Student
        {
            public string Name { get; set; }
            public string Group { get; set; }
            public DateTime DateTime { get; set; }
            public Student(string name, string group, DateTime dateTime)
            {
                Name = name;
                Group = group;
                DateTime = dateTime;
            }

        }
        static void Main(string[] args)
        {
            string dir = @"C:\Users\schek\Desktop\Students";

            if (!Directory.Exists(dir))
                Directory.CreateDirectory(dir);

            string path = @"C:\Users\schek\Desktop\Students.dat";

            
            BinaryFormatter formatter = new BinaryFormatter();

            using (var fs = new FileStream(path, FileMode.OpenOrCreate))
            {
                Student[] newS = (Student[])formatter.Deserialize(fs);
                foreach (var s in newS)
                {
                    Console.WriteLine(s.Name);
                    Console.WriteLine(s.Group);
                    Console.WriteLine(s.DateTime);
                }

            }


        }
    }
}
