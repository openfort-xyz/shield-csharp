using Shield.SDK.Enums;

namespace Shield.SDK.Models
{
    public class CustomAuthOptions : ShieldAuthOptions
    {
        public ShieldAuthProvider AuthProvider { get; } = ShieldAuthProvider.CUSTOM;
        public string CustomToken { get; set; }
    }
}
