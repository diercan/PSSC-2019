namespace FrBaschet.Domain.Interfaces
{
    public interface IMessageSender
    {
        void SendNotificationEmail(string toAddress, string messageBody);
    }
}