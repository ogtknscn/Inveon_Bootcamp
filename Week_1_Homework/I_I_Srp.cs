// This class handles both user data storage and notification responsibilities
// Violates Single Responsibility Principle

public class UserManager
{
    public void AddUser(string name, string email)
    {
        Console.WriteLine($"User {name} added to the database.");
        Console.WriteLine($"Email sent to {email}.");
    }
}

/* Refactored code */
// This class is responsible for storing user data
public class UserRepository
{
    public void AddUser(string name)
    {
        Console.WriteLine($"User {name} added to the database.");
    }
}

// Class responsible for sending notifications
public class NotificationService
{
    public void SendEmail(string email, string message)
    {
        Console.WriteLine($"Email sent to {email}: {message}");
    }
}

public class UserManagerRefactored
{
    private readonly UserRepository _userRepository = new UserRepository();
    private readonly NotificationService _notificationService = new NotificationService();

    public void AddUser(string name, string email)
    {
        _userRepository.AddUser(name);
        _notificationService.SendEmail(email, $"Welcome, {name}!");
    }
}