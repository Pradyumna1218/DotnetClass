public static class WorkItemFactory
{
    public static WorkItem Create(
        string type, 
        string title,
        int priority    
    )
    {
        return type.ToLower() switch
        {
            "bug" => new Bug { Title = title, Priority = priority },
            _ => new TaskItem { Title = title, Priority = priority},
        };
    }
}