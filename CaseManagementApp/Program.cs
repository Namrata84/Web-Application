namespace CaseManagementApp
{
    class Program
    {
        static void Main(string[] args)
        {
            Database.Initialize();
            Console.WriteLine("Database initialized.");
        }
    }
}
