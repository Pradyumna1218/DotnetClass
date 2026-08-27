public abstract class WorkItem: IWorkItem, IComparable<WorkItem>
{
    public string Title { get; set; }
    public int Priority { get; set; }
    public virtual void Display()
    {
        Console.WriteLine($"Title: {Title}, Priority: {Priority}");
    }

    public int CompareTo(WorkItem? other)
    {
        if(other == null) return -1;

        return other.Priority.CompareTo(Priority);

    }
}
