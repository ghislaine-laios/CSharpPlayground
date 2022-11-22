using System.ComponentModel.DataAnnotations.Schema;
using DemoSite.Exceptions;
using Microsoft.EntityFrameworkCore;
using Multiformats.Base;
using Multiformats.Hash;

namespace DemoSite.Models.Domain;

[Index(nameof(Email), IsUnique = true)]
public class UserDataBase
{
    public Guid? AvatarId { get; set; }
    public string? Email { get; set; }
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