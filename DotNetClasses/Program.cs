
class Task
{
    public int id;
    public string status;
    public string department;
}

class Program
{
    static List<Task> tasks = new List<Task>();

    static void AddTask()
    {
        Task task = new Task();


        Console.WriteLine("Enter ID: ");
        if (!int.TryParse(Console.ReadLine(), out int id))
        {
            Console.WriteLine("Invalid ID");
            return;
        }
        task.id = id;

        Console.WriteLine("Enter department");
        task.department = Console.ReadLine();

        task.status = "Pending";

        tasks.Add(task);
    }

    static void ListTask()
    {
        
        foreach (Task task in tasks)
        {
            Console.WriteLine("Id: " + task.id);
            Console.WriteLine("Department: " + task.department);
            Console.WriteLine("Status: " + task.status);
        }
    }

    static void CompleteTask()
    {
        Console.WriteLine("Enter the task id to complete: ");
        if (!int.TryParse(Console.ReadLine(), out int id))
        {
            Console.WriteLine("Invalid ID");
            return;
        }

        foreach (Task task in tasks)
        {
            if (task.id == id)
            {
                task.status = "Completed";
                Console.WriteLine("Task status was changed to completed");
                return;
            }
        }
        Console.WriteLine("Didn't find the id try again");
    }

    static void DeleteTask()
    {
        Console.WriteLine("Enter the id to delete: ");
        if (!int.TryParse(Console.ReadLine(), out int id))
        {
            Console.WriteLine("Invalid ID");
            return;
        }

        foreach (Task task in tasks)
        {
            if (task.id == id)
            {
                tasks.Remove(task);
                return;
            }
        }
    }

    static void SearchStatus()
    {
        Console.WriteLine("Enter status:");
        string status = Console.ReadLine() ?? "";

        var filteredTasks = tasks.Where(t =>
            t.status.Equals(status, StringComparison.OrdinalIgnoreCase));

        if (!filteredTasks.Any())
        {
            Console.WriteLine("No tasks found.");
            return;
        }

        foreach (Task task in filteredTasks)
        {
            Console.WriteLine($"ID: {task.id}");
            Console.WriteLine($"Department: {task.department}");
            Console.WriteLine($"Status: {task.status}");
        }
    }

    static void Main()
    {
        while (true)
        {
            Console.WriteLine("\n1-> Add new Task");
            Console.WriteLine("2-> SHow all Tasks");
            Console.WriteLine("3-> Complete a task");
            Console.WriteLine("4-> Delete a task");
            Console.WriteLine("5-> Search By Status");
            Console.WriteLine("6-> EXIT");

            Console.WriteLine("Enter a choice: ");
            if (!int.TryParse(Console.ReadLine(), out int choice))
            {
                Console.WriteLine("Invalid choice");
                continue;
            };

            switch (choice)
            {
                case 1:
                    AddTask();
                    break;
                case 2:
                    ListTask();
                    break;
                case 3:
                    CompleteTask();
                    break;
                case 4:
                    DeleteTask();
                    break;
                case 5:
                    SearchStatus();
                    break;
                case 6:
                    return;

                default:
                    Console.WriteLine("Invalid choice see menu again");
                    break;
            }
       }
    }
}