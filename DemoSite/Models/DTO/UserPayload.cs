using DemoSite.Models.Domain;

namespace DemoSite.Models.DTO;

public class UserDataPayload : UserDataBase
{
    public static UserDataPayload From(UserData userData)
    {
        return new UserDataPayload { AvatarId = userData.AvatarId, Email = userData.Email };
    }
}

public class FullUserPayload : BaseUserPayload
{
    public required UserDataPayload UserData { get; init; }

    public User ToUser()
    {
        var now = DateTime.UtcNow;
        return ToUser(new UserData
            { Id = 0, AvatarId = UserData.AvatarId, Email = UserData.Email, RegisterDate = now });
    }

    public static FullUserPayload From(User user)
    {
        return new FullUserPayload
            { Password = "", Username = user.Username, UserData = UserDataPayload.From(user.UserData) };
    }
}

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