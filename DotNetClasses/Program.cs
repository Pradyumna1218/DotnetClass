using System.Text.RegularExpressions;

class Entity
{
    public int Id { get; set; }
    public static int count = 1;

}


class Task : Entity
{
    private string _status = "";
    
    private DateTime _createdDate; 
    private DateTime _updatedDate; 
    private DateOnly _effectiveDate; 
    public string Status {
        get => _status;
        set => _status = value == "pending" || value == "completed"
         ? value
         : throw new ArgumentException("Status must be Pending or Completed");
    }
    public string Department { get; set; }

    public DateTime CreatedDate
    {
        get => _createdDate;
        set => _createdDate = value;
    }
    public DateTime UpdatedDate
    {
        get => _updatedDate;
        set => _updatedDate = value;
    }
    public DateOnly EffectiveDate
    {
        get => _effectiveDate;
        set => _effectiveDate = value;
    }
    public Task()
    {
        CreatedDate = DateTime.Now;
    }
}


abstract class SearchTask
{
    public abstract Task FindByID(int id);
}


class TaskMethods: SearchTask
{
    private static List<Task> tasks = new List<Task>();

    public override Task FindByID(int id)
    {
        return tasks.FirstOrDefault(t => t.Id == id);
    }

    public void AddTask()

    {

        Task task = new Task();

        task.Id = Entity.count++;

        Console.Write("Enter Department: ");
        task.Department = Console.ReadLine();

        task.Status = "pending";

        DateOnly effectiveDate;

        while (true)
        {
            Console.Write("Enter Effective Date (yyyy-MM-dd): ");
            string inputDate = Console.ReadLine();

            if (!Regex.IsMatch(inputDate, @"^\d{4}-\d{2}-\d{2}$"))
            {
                Console.WriteLine("Invalid date format. Use yyyy-MM-dd");
                continue;
            }

            if (!DateOnly.TryParse(inputDate, out effectiveDate))
            {
                Console.WriteLine("Invalid calendar date.");
                continue;
            }

            break;
        }

        task.EffectiveDate = effectiveDate;
        task.CreatedDate = DateTime.Now;
        task.UpdatedDate = DateTime.Now;

        tasks.Add(task);

        Console.WriteLine("Task added successfully!");
        Console.WriteLine($"Task ID: {task.Id}");
    }

    public void ListTask()
    {
        if(tasks.Count == 0)
        {
            Console.WriteLine("NO tasks available");
            return;
        }
        DateOnly today = DateOnly.FromDateTime(DateTime.Today);

        foreach (Task task in tasks)
        {
            if(task.EffectiveDate <= today)
            {
                Console.WriteLine("Id: " + task.Id);
                Console.WriteLine("Department: " + task.Department);
                Console.WriteLine("Status: " + task.Status);
                Console.WriteLine($"Created Date: {task.CreatedDate}");
                Console.WriteLine($"Updated Date: {task.UpdatedDate}");
                Console.WriteLine($"Effective Date: {task.EffectiveDate}");
                Console.WriteLine();
            }
            
        }
    }

    public void CompleteTask()
    {
        Console.WriteLine("Enter the task id to complete: ");
        if (!int.TryParse(Console.ReadLine(), out int id))
        {
            Console.WriteLine("Invalid ID");
            return;
        }

        Task task = FindByID(id);
        
        if (task == null)
        {
            Console.WriteLine("Task not found");
            return;
        }
        task.Status = "completed";
        task.UpdatedDate = DateTime.Now;
        Console.WriteLine("Task status was changed to completed");
    }

    public void DeleteTask()
    {
        Console.WriteLine("Enter the id to delete: ");
        if (!int.TryParse(Console.ReadLine(), out int id))
        {
            Console.WriteLine("Invalid ID");
            return;
        }
        Task task = FindByID(id);
        if(task == null)
        {
            Console.WriteLine("Task not found");
            return;
        }
        tasks.Remove(task);
        Console.WriteLine("Task deleted successfully");
        
    }

    public void SearchStatus()
    {
        Console.WriteLine("\n1. Completed");
        Console.WriteLine("2. Pending");

        Console.Write("Enter Choice: ");

        if (!int.TryParse(Console.ReadLine(), out int choice))
        {
            Console.WriteLine("Invalid choice.");
            return;
        }

        string status = "";

        switch (choice)
        {
            case 1:
                status = "completed";
                break;

            case 2:
                status = "pending";
                break;

            default:
                Console.WriteLine("Invalid choice.");
                return;
        }

        var filteredTasks = tasks.Where(t =>
            t.Status.Equals(status, StringComparison.OrdinalIgnoreCase));

        if (!filteredTasks.Any())
        {
            Console.WriteLine("No tasks found.");
            return;
        }

        foreach (Task task in filteredTasks)
        {
            Console.WriteLine("Id: " + task.Id);
            Console.WriteLine("Department: " + task.Department);
            Console.WriteLine("Status: " + task.Status);
            Console.WriteLine("Created Date: " + task.CreatedDate);
            Console.WriteLine("Updated Date: " + task.UpdatedDate);
            Console.WriteLine("Effective Date: " + task.EffectiveDate);
            Console.WriteLine();
        }
    }




}


class Program
{
    static void Main()
    {
        TaskMethods t1 = new TaskMethods();

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
            }
            ;

            switch (choice)
            {
                case 1:
                    t1.AddTask();
                    break;
                case 2:
                    t1.ListTask();
                    break;
                case 3:
                    t1.CompleteTask();
                    break;
                case 4:
                    t1.DeleteTask();
                    break;
                case 5:
                    t1.SearchStatus();
                    break;
                case 6:
                    return;

                default:
                    Console.WriteLine("Invalid choice see menu again");
                    break;
            }

        }

        List<WorkItem> items = new()
        {
            WorkItemFactory.Create(
                "task",
                "This is task 1",
                2
                ),
            WorkItemFactory.Create(
                "bug",
                "This is bug 1",
                5
                ),
            WorkItemFactory.Create(
                "task",
                "DO task",
                3
                )
        };

        items.Sort();
        Console.WriteLine("This is sorted by priority");
        Console.WriteLine("High num = Higher Priority");

        foreach(var item in items)
        {
            item.Display();
        }

        //Enqueue
        Queue<WorkItem> queue = new ();
        foreach(var item in items)
        {
            queue.Enqueue(item);
        }

        //Dequeue
        Console.WriteLine("Review Queue");
        while(queue.Count > 0)
        {
            WorkItem current = queue.Dequeue();
            Console.WriteLine("Reviewing: ");
            current.Display();
        }
       
    }
}