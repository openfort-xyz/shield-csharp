using System.ComponentModel;

namespace Shield.SDK.Enums
{
    public enum OpenfortOAuthTokenType
    {
        [Description("idToken")]
        ID_TOKEN,

        [Description("customToken")]
        CUSTOM_TOKEN,
    }
}
