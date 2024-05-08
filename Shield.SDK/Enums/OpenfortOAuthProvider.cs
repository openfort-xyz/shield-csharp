using System.ComponentModel;

namespace Shield.SDK.Enums
{
    public enum OpenfortOAuthProvider
    {
        [Description("accelbyte")]
        ACCELBYE,

        [Description("firebase")]
        FIREBASE,

        [Description("lootlocker")]
        LOOTLOCKER,

        [Description("supabase")]
        SUPABASE,

        [Description("playfab")]
        PLAYFAB,

        [Description("custom")]
        CUSTOM,

        [Description("oidc")]
        OIDC,
    }
}
