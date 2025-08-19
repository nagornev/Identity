namespace Auth.Application.DTOs
{
    public class RequestContext
    {
        public RequestContext(string device,
                              string ipAddress)
        {
            Device = device;
            IpAddress = ipAddress;
        }

        public string Device { get; }

        public string IpAddress { get; }
    }
}
