using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoilerplatePro.nuDirect
{
    public class FolderEntities
    {
        public int FolderId { get; set; }

        public string FolderKey { get; set; }

        public Guid UniqueRef { get; set; }

        public string Description { get; set; }

        public string SourceFolder { get; set; }

        public string DestinationFolder { get; set; }

        public bool Archive { get; set; }

        public bool Timestamp { get; set; }

        public string Direction { get; set; }

        public string Version { get; set; }

        public int EndpointId { get; set; }

        public bool Remove { get; set; }

        public bool SubFolders { get; set; }

        public int? RuleId { get; set; }

        public int? DestFolderId { get; set; }

        public int? BusinessUnitId { get; set; }

        public int? Active { get; set; }

        public int? IsStaged { get; set; }

        public string SFTPoptions { get; set; }

        public string TimestampFormat { get; set; }

        public bool? Enabled { get; set; }

        public DateTime? DateCreated { get; set; }

        public string ArchiveFolder { get; set; }

        public string Filter { get; set; }

        public string ArchiveTimestampFmt { get; set; }

        public string PreTransferJob { get; set; }

        public string PostTransferJob { get; set; }

        public int? Compression { get; set; }

        public bool PGPCompression { get; set; }

        public bool PGPSign { get; set; }

        public bool PGPEnable { get; set; }

        public int SFTPRetries { get; set; }

        public int SFTPRetryInterval { get; set; }

        public string PGPFileExtension { get; set; }

        public string CostCentreCode { get; set; }

    }
}
