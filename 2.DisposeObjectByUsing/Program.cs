using System;
using System.IO;

namespace DisposeObjectByUsing
{
    // Данный класс реализует интерейс IDisposable
    class FinalizeObject : IDisposable
    {
        public int id { get; set; }

        FileStream file;
        BinaryWriter writer;
        public FinalizeObject(int id)
        {
            this.id = id;
            file = new FileStream(id + ".dat", FileMode.Create, FileAccess.Write);
            writer = new BinaryWriter(file);
            writer.Write("IDisposable");
        }

        // Реализуем метод Dispose()
        public void Dispose()
        {
            Console.WriteLine("Освобождение ресурсов объекта!");
            writer.Close();
            file.Close();
            // Освобождаем управляемые и неуправляемые ресурсы
        }
    }

    // В .NET существует два типа ресурсов: управляемые и неуправляемые. 
    // К неуправляемым ресурсам относятся только «сырые» ресурсы, типа IntPtr, дескрипторы сокетов или файлов.
    // Если же этот ресурс упаковали в объект, захватывающий его в конструкторе и освобождающий в методе Dispose, 
    // то такой ресурс уже является управляемым. 
    // По сути, управляемые ресурсы – это «умные оболочки» для неуправляемых ресурсов, для освобождения которых 
    // не нужно вызывать какие-то функции, а достаточно вызвать метод Dispose интерфейса IDisposable
    class Program
    {
        static void Main(string[] args)
        {
            FinalizeObject[] obj = new FinalizeObject[30];
            for (int i = 0; i < 30; i++)
                using (obj[i] = new FinalizeObject(i))
                {
                    // Выполнение действий на объектами
                }

            Console.Read();
        }
    }
}
