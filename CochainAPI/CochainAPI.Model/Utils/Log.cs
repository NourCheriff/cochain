using CochainAPI.Model.Authentication;

namespace CochainAPI.Model.Utils
{
    public class Log : Base
    {
        public string Severity { get; set; }
        public string Entity { get; set; }
        public string EntityId { get; set; }
        public string Action { get; set; }
        public string Message { get; set; }
        public DateTime Timestamp { get; set; }
        public string URL { get; set; }
        public string QueryString { get; set; }
        public string Cookies { get; set; }
        public User User { get; set; }
        public string UserId { get; set; }
    }
}
