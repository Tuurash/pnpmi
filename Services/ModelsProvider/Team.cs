namespace ModelsProvider;

public record Team(int Id)
{
    public string Name { get; set; } = null!;
    public string Description { get; set; } = null!;
    public List<User> Users { get; set; } = new List<User>();
}

//Task: Id, Title, Description, Status (Todo / InProgress / Done), AssignedToUserId,CreatedByUserId, TeamId, DueDate


