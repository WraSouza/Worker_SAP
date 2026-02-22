namespace Worker_SAP.Model
{
    public class LoginResponse(string sessionId, string version, int sessionTimeout)

    {
        public string SessionId { get; } = sessionId;
        public string Version { get; } = version;
        public int SessionTimeout { get; } = sessionTimeout;

    }
}
