namespace CRMSystem.Models
{
    public record Order(
        Guid Id, DateTime TimeStamp, string Name, string Email, string Text, OrderStatus Status)
    { };

    public enum OrderStatus
    {
        IsReceived,   //"��������"
        InWork,       //"� ������"
        IsDone,      //"���������"
        IsRejected,  //"���������"
        IsCanceled   //"��������"
    }
}