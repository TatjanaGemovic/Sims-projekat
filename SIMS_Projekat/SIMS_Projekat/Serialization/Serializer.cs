using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Text;
using SIMS_Projekat.Serialization;

namespace ConsoleApp.serialization
{
    class Serializer<T> where T: Serializable, new()
    {
        private static char DELIMITER = '|';
        public void toCSV(string fileName, ObservableCollection<T> objects) 
        {
            using StreamWriter streamWriter = new StreamWriter(fileName);

            foreach(Serializable obj in objects)
            {
                string line = string.Join(DELIMITER, obj.toCSV());
                streamWriter.WriteLine(line);
            }
        }

        public ObservableCollection<T> fromCSV(string fileName)
        {
            ObservableCollection<T> objects = new ObservableCollection<T>();

            foreach(string line in File.ReadLines(fileName))
            {
                string[] csvValues = line.Split(DELIMITER);
                T obj = new T();
                obj.fromCSV(csvValues);
                objects.Add(obj);
            }

            return objects;
        }
    }
}
