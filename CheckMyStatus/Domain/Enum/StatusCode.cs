namespace CheckMyStatus.Domain.Enum
{
    public enum StatusCode
    {
        Ok = 200,
        UserNotFound = 01,
        ItemNotFound = 02,
        InternalServerError = 500
    }
}
