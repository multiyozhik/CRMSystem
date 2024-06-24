namespace CRMSystem.Models
{
    public record Order(Guid Id, DateTime TimeStamp, string Name, string Text, string email) { };


}