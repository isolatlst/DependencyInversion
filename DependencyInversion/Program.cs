using System;

namespace Dependency_Inversion
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            Book book = new Book("Алхимик");
            book.SaveBook = new SaveToCloud();
            book.SaveBook.Save(book);

            Console.WriteLine();

            book.SaveBook = new SaveToDisk();
            book.SaveBook.Save(book);
        }
    }

    #region Abstractions
        public interface ISave
        {
            bool Save(File file);
        }

        public abstract class File
        {
            public string Name { get; private set; }

            protected File(string fileName)
            {
                Name = fileName;
            }
        }
    #endregion


    public class Book : File // модуль верхнего уровня, зависящий от абстрактного File
    {
        public Book(string name) : base(name)
        {
        }

        public ISave
            SaveBook
        {
            get;
            set;
        } // не зависящий от нижнего уровня, а использующий агрегацию имплементаторов интерфейса
    }

    public class SaveToDisk : ISave // модули нижнего уровня, зависящие от абстракции
    {
        public bool Save(File file)
        {
            Console.WriteLine("File saved to YDisk"); //детали, зависящие от абстракции 
            return true;
        }
    }

    public class SaveToCloud : ISave
    {
        public bool Save(File file)
        {
            Console.WriteLine("File saved to Cloud");
            return true;
        }
    }
}