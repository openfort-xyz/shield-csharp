using Shield.SDK.Enums;
using System.ComponentModel;

namespace Shield.SDK.Models
{
    public enum Entropy
    {
        [Description("none")]
        NONE,

        [Description("user")]
        USER,

        [Description("project")]
        PROJECT,
    }

    public class Share
    {
        public string Secret { get; set; }
        public Entropy Entropy { get; set; }
        public EncryptionParameters EncryptionParameters { get; set; }
    }

    public class EncryptionParameters
    {
        public string Salt { get; set; }
        public int Iterations { get; set; }
        public int Length { get; set; }
        public string Digest { get; set; }
    }

}
