using Prism.Events;

namespace MemoryEditingSoftware.Core.Events
{
    /// <summary>
    /// Event to call the error dialog box and show a message to the user.
    /// </summary>
    public class ShowErrorMessageEvent : PubSubEvent<string> { }
}
