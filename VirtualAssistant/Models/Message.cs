namespace VirtualAssistant.Models
{
    public class Message
    {
        public string Text { get; set; }           // The message content
        public string Sender { get; set; }         // "User" or "Assistant"
        public DateTime Timestamp { get; set; }    // When the message was sent

        public Message(string text, string sender)
        {
            Text = text;
            Sender = sender;
            Timestamp = DateTime.Now;
        }
    }
}