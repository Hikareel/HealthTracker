namespace HealthTracker.Server.Core.Exceptions.Community
{
    /// <summary>
    /// Represents an error when a message cannot be found.
    /// </summary>
    public class MessageNotFoundException : Exception
    {
        public MessageNotFoundException()
            : base("Message not found!") { }
        public MessageNotFoundException(int id)
            : base($"Message not found: {id}") { }
        public MessageNotFoundException(string message)
            : base($"Message not found: {message}") { }


    }
}
