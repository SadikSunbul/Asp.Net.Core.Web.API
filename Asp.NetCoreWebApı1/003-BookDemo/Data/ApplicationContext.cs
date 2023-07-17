using _004_BookDemo.Models;

namespace _004_BookDemo.Data
{
    public static class ApplicationContext
    {
        public static List<Book> Books { get; set; }
        static ApplicationContext()
        {
            Books = new List<Book>()
            {
                new Book(){ Id=1, Price=75, Title="kara göz"},
                new Book(){ Id=2, Price=75, Title="mesnevi"},
                new Book(){ Id=3, Price=25, Title="Mevlana"},
            };
        }
    }
}
