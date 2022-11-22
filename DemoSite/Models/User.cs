using DemoSite.Exceptions;
using Microsoft.EntityFrameworkCore;
using Multiformats.Base;
using Multiformats.Hash;

namespace DemoSite.Models;

public class BaseUserPayload
{
    public required string Username { get; init; }
    public required string Password { get; init; }

    public User ToUser(UserData userData)
    {
        var user = new User { Username = Username, UserData = userData };
        user.SetPassword(Password);
        return user;
    }
}

public class FullUserPayload : BaseUserPayload
{
    public required UserDataPayload UserData { get; init; }

    public User ToUser()
    {
        var now = DateTime.UtcNow;
        return ToUser(new UserData { Id = 0, Avatar = UserData.Avatar, Email = UserData.Email, RegisterDate = now });
    }

    public static FullUserPayload From(User user)
    {
        return new FullUserPayload
            { Password = "", Username = user.Username, UserData = UserDataPayload.From(user.UserData) };
    }
}

[Index(nameof(Email), IsUnique = true)]
public class UserDataBase
{
    public string? Avatar { get; set; }
    public string? Email { get; set; }
}

public class UserDataPayload : UserDataBase
{
    public static UserDataPayload From(UserData userData)
    {
        return new UserDataPayload { Avatar = userData.Avatar, Email = userData.Email };
    }
}

public class UserData : UserDataBase
{
    public required long Id { get; set; }
    public required DateTime RegisterDate { get; set; }
}

[Index(nameof(Username), IsUnique = true)]
public class User
{
    public long Id { get; set; }
    public required string Username { get; set; }
    public string HashedPassword { get; set; } = null!;
    public required UserData UserData { get; set; }

    public void SetPassword(string rawPassword)
    {
        var multiHash = Multihash.Encode(rawPassword, HashType.SHA2_256);
        HashedPassword = multiHash.ToString(MultibaseEncoding.Base64);
    }

    public bool ValidatePassword(string rawPassword)
    {
        var multiHash = Multihash.Parse(HashedPassword);
        var pendingHash = Multihash.Encode(rawPassword, multiHash.Code);
        return multiHash.Equals(pendingHash);
    }
}

public class UsernameConflictException : PropertyConflictException<User>
{
    public UsernameConflictException(string message) : base(message, nameof(User.Username))
    {
    }

    public UsernameConflictException() : base(nameof(User.Username))
    {
    }
}