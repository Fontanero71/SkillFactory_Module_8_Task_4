using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Collections.Generic;

namespace SkillFactory_Module_8_Task_4
{
    
    internal class Program
    {
        [Serializable]
        public class Student
        {
            public string Name { get; set; }
            public string Group { get; set; }
            public string/*DateTime*/ DateTime { get; set; }
            public Student(string name, string group, /*DateTime*/string dateTime)
            {
                Name = name;
                Group = group;
                DateTime = dateTime;
            }

        }
        static void Main(string[] args)
        {
            string createFile;
            Console.WriteLine(@"Создать файл 'Да/Нет' ");
            createFile = Console.ReadLine();

            if (createFile == "Да")
            {            
                Student[] oldS = new Student[3];
                
                
                for (int i = 0; i < 3; i++)
                {
                    oldS[i] = new Student("", "", "");
                    FillTheData(oldS[i]);
                }

                string dir = @"C:\Users\schek\Desktop\Students.dat";
                FileStream fsin = new FileStream(dir, FileMode.Create, FileAccess.Write);

                BinaryFormatter formatter = new BinaryFormatter();
                formatter.Serialize(fsin, oldS);

                fsin.Close(); 
            }

                //if (!Directory.Exists(dir))
                //    Directory.CreateDirectory(dir);
            

            string path = @"C:\Users\schek\Desktop\Students.dat";

            
            BinaryFormatter formatterOut = new BinaryFormatter();

            using (var fs = new FileStream(path, FileMode.OpenOrCreate))
            {
                Student[] newS = (Student[])formatterOut.Deserialize(fs);
                foreach (var s in newS)
                {
                    Console.WriteLine(s.Name);
                    Console.WriteLine(s.Group);
                    Console.WriteLine(s.DateTime);
                }

                //int length = newS.Length;

                List <string> mylist = new List <string>();
                mylist.Add(newS[0].Group);

                for (int i = 1; i < newS.Length; i++)
                {
                    int k = 0;
                    for (int j = 0; j < mylist.Count; j++)
                    {
                        if (mylist[j] == newS[i].Group)
                            k++;
                    }
                    if (k == 0)
                        mylist.Add(newS[i].Group);
                }

                string pathsorted = @"C:\Users\schek\Desktop\Students\";

                if (!Directory.Exists(pathsorted))
                {
                    Directory.CreateDirectory(pathsorted);
                }
                
                for (int i = 0; i < mylist.Count; i++)
                {
                    StreamWriter subwr = File.CreateText(pathsorted + mylist[i] + ".dat");
                    for (int j = 0; j < newS.Length; j++)
                    {
                        if (newS[j].Group == mylist[i])
                        {
                            subwr.Write(newS[j].Name + " ");
                            subwr.Write(newS[j].Group + " ");
                            subwr.Write(newS[j].DateTime + " ");
                            subwr.WriteLine();
                        }
                    }
                    subwr.Close();
                }

            } 

            
        }
        static void FillTheData(Student InList)
        {
            Console.WriteLine("Введите имя");
            InList.Name = Console.ReadLine();
            Console.WriteLine("Введите группу");
            InList.Group = Console.ReadLine();
            Console.WriteLine("Введите дату");
            InList.DateTime = Console.ReadLine();

        }
    }
}
