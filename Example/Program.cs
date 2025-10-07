using LoLINI;

class Program
{
    static RFile rFile = null!;
    static string CurrentSection = "";

    static void Main(string[] args)
    {
        if (args.Length == 0)
        {
            Console.WriteLine("Please drag and drop an .inibin file to the executable!");
            Console.ReadKey();
            return;
        }

        rFile = Cache.Instance.GetFile(args[0])!;
        
        if(!rFile.BinaryCached)
        {
            Console.WriteLine($"File '{args[0]}' not found or could not be opened.");
            Console.ReadKey();
            return;
        }

        Menu();
    }

    static void Menu()
    {
        while (true)
        {
            Console.Clear();
            DisplayMenu();
            string input = Console.ReadLine()!;
            switch (input.First())
            {
                case '1':
                    HandleSetSection();
                    break;
                case '2':
                    HandleGetData();
                    break;
                case '3':
                    return;
                default:
                    continue;
            }
        }
    }

    static void DisplayMenu()
    {
        Console.WriteLine(
            $"""
            1)Set Section ('{CurrentSection}')
            2)Get Data
            3)Exit
            """);
    }

    static void HandleSetSection()
    {
        Console.Clear();

        Console.Write("Section Name: ");
        CurrentSection = Console.ReadLine()!;
    }

    static void HandleGetData()
    {
        Console.Clear();

        Console.WriteLine($"Current Section: {CurrentSection}");
        Console.Write("Key: ");
        string key = Console.ReadLine()!;

        rFile.GetValue(CurrentSection, key, out string value, "NOT_FOUND_AKA_DEFAULT_VALUE");
        Console.WriteLine($"[{CurrentSection}][{key}] = {value}");

        Console.WriteLine("Press any key to continue...");
        Console.ReadKey();
    }
}