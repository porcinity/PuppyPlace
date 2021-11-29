using PuppyPlace.Data;
using PuppyPlace.Domain;

namespace PuppyPlace.UI
{
    class Program
    {
        private static PuppyPlaceContext _context = new PuppyPlaceContext();
        static void Main(string[] args)
        {
            // Prompt.ShowMainMenu();
            _context.Database.EnsureCreated();
            GetPersons("Before Add");
            AddPersonDB();
            GetPersons("After Add");
            Console.ReadLine();
        }
        
        public static void AddPersonDB()
        {
            var person = new Person("Bell Peps");
            _context.Persons.Add(person);
            _context.SaveChanges();
        }
        
        public static void GetPersons(string text)
        {
            var persons = _context.Persons.ToList();
            Console.WriteLine($"{text}: Person count is {persons.Count}");
            foreach (var person in persons)
            {
                Console.WriteLine(person.Name);
            }
        }
    }
}