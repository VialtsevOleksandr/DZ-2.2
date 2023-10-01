// See https://aka.ms/new-console-template for more information
abstract class Worker
{
    public string Name;
    public string Position;
    public int WorkDay;
    public Worker(string name, string position, int workDay)
    {
        Name = name;
        Position = position;
        WorkDay = workDay;
    }
    abstract public void FillWorkDay();
    public void Call() 
    {
    }
    public void WriteCode()
    {
    }
    public void Relax()
    {
    }
}
class Developer : Worker
{
    public Developer(string name, int workDay) : base(name, "Розробник", workDay)
    {
    }
    public override void FillWorkDay()
    {
        WriteCode();
        Call();
        Relax();
        WriteCode();
    }
}
class Manager : Worker
{
    private Random random = new Random();
    public Manager(string name, int workDay) : base(name, "Менеджер", workDay)
    {
    }
    public override void FillWorkDay()
    {
        int callCount1 = random.Next(1, 11);
        for (int i = 0; i < callCount1; i++)
        {
            Call();
        }
        Relax();
        int callCount2 = random.Next(1, 6);
        for (int i = 0; i < callCount2; i++)
        {
            Call();
        }
    }
}
class Team
{
    public string TeamName;
    private List<Worker> Workers = new List<Worker>();
    public Team(string teamName)
    {
        TeamName = teamName;
    }
    public void AddWorker(Worker worker)
    {
        Workers.Add(worker);
    }
    public void DisplayTeamInfo()
    {
        Console.WriteLine($"Назва команди: {TeamName}");
        Console.WriteLine("Спiвробiтники:");

        foreach (var worker in Workers)
        {
            Console.WriteLine(worker.Name);
        }
    }
    public void DisplayDetailedTeamInfo()
    {
        Console.WriteLine($"Назва команди: {TeamName}");
        Console.WriteLine("Детальна iнформацiя про спiвробiтникiв:");

        foreach (var worker in Workers)
        {
            Console.WriteLine($"{worker.Name} - {worker.Position} - {worker.WorkDay} годин");
        }
    }
}
class Program
{
    static void Main(string[] args)
    {
        List<Team> teams = new List<Team>();

        while (true)
        {
            Console.WriteLine("1. Створити нову команду");
            Console.WriteLine("2. Додати спiвробiтника до команди");
            Console.WriteLine("3. Вивести iнформацiю про команду");
            Console.WriteLine("4. Вивести детальну iнформацiю про команду");
            Console.WriteLine("5. Вийти");
            Console.WriteLine("Оберiть опцiю (1-5):");

            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    Console.Write("Введiть назву команди: ");
                    string teamName = Console.ReadLine();
                    Team newTeam = new Team(teamName);
                    teams.Add(newTeam);
                    Console.WriteLine($"Команда \"{teamName}\" створена.");
                    break;

                case "2":
                    if (teams.Count == 0)
                    {
                        Console.WriteLine("Не створено жодної команди.");
                        break;
                    }

                    Console.WriteLine("Оберiть команду для додавання спiвробiтника:");
                    for (int i = 0; i < teams.Count; i++)
                    {
                        Console.WriteLine($"{i + 1}. {teams[i].TeamName}");
                    }

                    int teamIndex;
                    if (int.TryParse(Console.ReadLine(), out teamIndex) && teamIndex >= 1 && teamIndex <= teams.Count)
                    {
                        Console.Write("Введiть iм'я спiвробiтника: ");
                        string workerName = Console.ReadLine();
                        Console.Write("Введiть посаду спiвробiтника: ");
                        string position = Console.ReadLine();
                        Console.Write("Введiть кiлькiсть годин робочого дня: ");
                        int workDay;
                        if (int.TryParse(Console.ReadLine(), out workDay) && (workDay <= 24))
                        {
                            Worker worker;

                            if (position == "Розробник")
                            {
                                worker = new Developer(workerName, workDay);
                            }
                            else if (position == "Менеджер")
                            {
                                worker = new Manager(workerName, workDay);
                            }
                            else
                            {
                                Console.WriteLine("Невiрна посада. Спiвробiтник не доданий до команди.");
                                break;
                            }

                            teams[teamIndex - 1].AddWorker(worker);
                            Console.WriteLine($"{workerName} доданий до команди \"{teams[teamIndex - 1].TeamName}\".");
                        }
                        else
                        {
                            Console.WriteLine("Невiрно введена кiлькiсть годин робочого дня.");
                        }
                    }
                    else
                    {
                        Console.WriteLine("Невiрний вибiр команди.");
                    }
                    break;

                case "3":
                    if (teams.Count == 0)
                    {
                        Console.WriteLine("Не створено жодної команди.");
                        break;
                    }

                    Console.WriteLine("Оберiть команду для виведення iнформацiї:");
                    for (int i = 0; i < teams.Count; i++)
                    {
                        Console.WriteLine($"{i + 1}. {teams[i].TeamName}");
                    }

                    int infoIndex;
                    if (int.TryParse(Console.ReadLine(), out infoIndex) && infoIndex >= 1 && infoIndex <= teams.Count)
                    {
                        teams[infoIndex - 1].DisplayTeamInfo();
                    }
                    else
                    {
                        Console.WriteLine("Невiрний вибiр команди.");
                    }
                    break;

                case "4":
                    if (teams.Count == 0)
                    {
                        Console.WriteLine("Не створено жодної команди.");
                        break;
                    }

                    Console.WriteLine("Оберiть команду для виведення детальної iнформацiї:");
                    for (int i = 0; i < teams.Count; i++)
                    {
                        Console.WriteLine($"{i + 1}. {teams[i].TeamName}");
                    }

                    int detailedInfoIndex;
                    if (int.TryParse(Console.ReadLine(), out detailedInfoIndex) && detailedInfoIndex >= 1 && detailedInfoIndex <= teams.Count)
                    {
                        teams[detailedInfoIndex - 1].DisplayDetailedTeamInfo();
                    }
                    else
                    {
                        Console.WriteLine("Невiрний вибiр команди.");
                    }
                    break;

                case "5":
                    Environment.Exit(0);
                    break;

                default:
                    Console.WriteLine("Невiрний вибiр опцiї.");
                    break;
            }
        }
    }
}