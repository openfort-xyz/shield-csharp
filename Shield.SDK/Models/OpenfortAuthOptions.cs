using Shield.SDK.Enums;

namespace Shield.SDK.Models
{
    public class OpenfortAuthOptions : ShieldAuthOptions
    {
        public ShieldAuthProvider AuthProvider { get; } = ShieldAuthProvider.OPENFORT;
        public OpenfortOAuthProvider? OpenfortOAuthProvider { get; set; } // Nullable for optional property
        public string OpenfortOAuthToken { get; set; }
        public OpenfortOAuthTokenType? OpenfortOAuthTokenType { get; set; } // Nullable for optional property
    }
}
