using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoilerplatePro.nuDirect
{
    public class EndpointEntities
    {
        public int EndpointId { get; set; }

        public string Description { get; set; }

        public Guid epGUID { get; set; }

        public string epKey { get; set; }

        public DateTime RegisteredDate { get; set; }

        public DateTime LastSeenDate { get; set; }

        public string epType { get; set; }

        public string Version { get; set; }

        public int CustomerId { get; set; }

        public string epIV { get; set; }

        public bool Enabled { get; set; }

        public string epVersion { get; set; }

        public string KnownIP { get; set; }

        public string FoundIP { get; set; }

        public string Hostname { get; set; }

        public string VirtSFTPUser { get; set; }

        public string VirtSFTPPass { get; set; }

        public string VirtSFTPServer { get; set; }

        public bool? Configured { get; set; }

        public string LocalSFTPHome { get; set; }

        public string RemoteSFTPHome { get; set; }

        public string LocalUser { get; set; }

        public string LocalPass { get; set; }

        public string RemoteUser { get; set; }

        public string RemotePass { get; set; }

        public string PublicKey { get; set; }

        public string PrivateKey { get; set; }

        public string SSHPrivateKey { get; set; }

        public string SSHPublicKey { get; set; }

        public string PPublicKey { get; set; }

        public string PPrivateKey { get; set; }

        public string PSSHPrivateKey { get; set; }

        public string PSSHPublicKey { get; set; }

        public bool? Activated { get; set; }

        public bool? DualChannel { get; set; }

        public bool? oMaxConcurrent { get; set; }

        public bool? oCompression { get; set; }

        public bool? HeartBeatCheck { get; set; }

        public int? MaxConcurrent { get; set; }

        public int? Compression { get; set; }

        public string AlternateIP1 { get; set; }

        public string SFTPJobType { get; set; }

        public bool? SFTPJobDebug { get; set; }

        public int? SFTPBufferSize { get; set; }

        public int? TCPBufferSize { get; set; }

    }
}
