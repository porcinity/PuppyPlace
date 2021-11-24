namespace PuppyPlace
{
    class Program
    {
        static void Main(string[] args)
        {
            var lucy = new Dog("Lucy", 5,"Red Heeler");
            var clovis = new Dog("Clovis", 1, "Blue Heeler");


            var tessandra = new Person("Tessandra");
            var anthony = new Person("Anthony");

            Console.WriteLine($"Anthony's Id is: {anthony.Id}");

            tessandra.AddDog(lucy);
            tessandra.AddDog(clovis);

            lucy.AddOwner(tessandra);
            lucy.AddOwner(anthony);
            clovis.AddOwner(tessandra);

            tessandra.ShowDogs();
            lucy.ShowOwners();
            
            // Console.WriteLine($"We have a person named: {tessandra.Name}");
            // Console.WriteLine($"The dog's name is: {lucy.Name}");
            // Console.WriteLine($"The dog's age is: {lucy.Age}");
            // Console.WriteLine(lucy.Bark());
            

            Console.WriteLine("Add another dog? Choose: yes/no");
            var userChoice = Console.ReadLine();

            if (userChoice != "yes" && userChoice != "no")
            {
                Console.WriteLine("Invalid choice. Choose: yes/no");
            }

            if (userChoice == "yes")
            {
                Dog.AddDog();
            }
        }
    }
}