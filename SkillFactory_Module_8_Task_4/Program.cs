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
            string createFile;                             //По надобности создаю рабочий бинарный файл
            Console.WriteLine(@"Создать файл 'Да/Нет' ");
            createFile = Console.ReadLine();

            if (createFile == "Да")
            {            
                Student[] oldS = new Student[3];
                
                
                for (int i = 0; i < 3; i++)
                {
                    oldS[i] = new Student("", "", DateTime.Now);
                    FillTheData(oldS[i]);
                }

                string dir = @"C:\Users\schek\Desktop\Students.dat";
                FileStream fsin = new FileStream(dir, FileMode.Create, FileAccess.Write); 

                BinaryFormatter formatter = new BinaryFormatter();
                formatter.Serialize(fsin, oldS);                               //Запись в бинарный файл

                fsin.Close(); 
            }

                
            

            string path = @"C:\Users\schek\Desktop\Students.dat";

            
            BinaryFormatter formatterOut = new BinaryFormatter();

            using (var fs = new FileStream(path, FileMode.OpenOrCreate)) //Считываю из бинарного файла информацию
            {
                Student[] newS = (Student[])formatterOut.Deserialize(fs);
                //foreach (var s in newS)
                //{
                //    Console.WriteLine(s.Name);
                //    Console.WriteLine(s.Group);
                //    Console.WriteLine(s.DateTime);
                //}

                
                List <string> mylist = new List <string>(); //Определяю различные значения для свойства "Group"
                mylist.Add(newS[0].Group);

                for (int i = 1; i < newS.Length; i++)
                {
                    int k = 0;
                    for (int j = 0; j < mylist.Count; j++)
                    {
                        if (mylist[j] == newS[i].Group)
                        {
                            k++;
                            break;
                        }
                    }
                    if (k == 0)                            //Сохраняю уникальные значения свойства "Group" в список
                        mylist.Add(newS[i].Group);
                }

                string pathsorted = @"C:\Users\schek\Desktop\Students\"; //Создаю директорий "Students" на раб. столе при его отсутствии

                if (!Directory.Exists(pathsorted))
                {
                    Directory.CreateDirectory(pathsorted);
                }
                
                for (int i = 0; i < mylist.Count; i++)       //Для каждого уникального значения свойства "Group" создаем отдельный файл, и записываем туда информацию
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
            InList.DateTime = DateTime.Now;

        }
    }
}
