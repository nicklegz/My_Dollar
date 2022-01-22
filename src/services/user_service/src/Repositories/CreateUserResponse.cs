namespace Repositories;

public class CreateUserResponse
{
    public CreateUserResponse(Guid id, string username)
    {
        Id = id;
        Username = username;
    }
    public Guid Id { get; set; }
    public string Username { get; set; }
}