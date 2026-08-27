public class Bug: WorkItem
{
    public override void Display()
    {
        Console.WriteLine($"Bug: {Title}, Priority: {Priority}");
    }
}
