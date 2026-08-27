public class TaskItem : WorkItem
{
    public override void Display()
    {
        Console.WriteLine($"Task: {Title}, Priority: {Priority}");
    }
}