namespace BridgeDogs.Models
{
    public class DogshouseRateLimitOptions
    {
        // Handle 10 requests per second
        public const string DogshouseRateLimit = "DogshouseRateLimit";
        public int PermitLimit { get; set; } = 10;
        public int Window { get; set; } = 5;
        public int RejectionStatusCode { get; set; } = 429;
    }
}
