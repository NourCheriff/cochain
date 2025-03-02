using CochainAPI.Model.Authentication;

namespace CochainAPI.Model.Documents
{
    public abstract class BaseDocument: Base
    {
        public string Path { get; set; }
        public string Type { get; set; }
        public string UserEmitterId { get; set; }
        public string UserReceiverId { get; set; }
        public User UserEmitter { get; set; }
        public User UserReceiver { get; set; }
    }
}
