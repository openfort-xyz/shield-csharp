using Shield.SDK.Enums;

namespace Shield.SDK.Models
{
    public class ShieldAuthOptions
    {
        public ShieldAuthProvider AuthProvider { get; set; }
        public string EncryptionPart { get; set; }
        public string ExternalUserId { get; set; }
        public string ApiKey { get; set; }
        public string ApiSecret { get; set; }
    }
}
