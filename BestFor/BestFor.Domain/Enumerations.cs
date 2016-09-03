namespace BestFor.Domain
{
    public enum Roles
    {
        Admin,
        Moderator
    }

    public enum ObjectState
    {
        Detached = 0,
        Unchanged = 1,
        Modified = 3,
        Deleted = 2,
        Added = 4
    }
}
