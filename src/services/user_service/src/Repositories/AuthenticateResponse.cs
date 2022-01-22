namespace Repositories;

public class AuthenticateResponse
{
    public AuthenticateResponse(Guid id, string username)
    {
        Id = id;
        Username = username;
    }
    public Guid Id { get; set; }
    public string Username { get; set; }
}