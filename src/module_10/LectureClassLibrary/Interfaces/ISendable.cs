namespace BusinessLogic.Interfaces
{
    public interface ISendable
    {
        void Send(string address, string subject, string message);
    }
}
