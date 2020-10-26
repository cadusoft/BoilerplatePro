using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoilerplatePro.nuDirect
{
    public class TransferEntities
    {
        public int TransferId { get; set; }

        public DateTime Started { get; set; }

        public DateTime? Finished { get; set; }

        public int? Size { get; set; }

        public int SourceEpId { get; set; }

        public int? DestinationEpId { get; set; }

        public int CurrentStatus { get; set; }

        public int SourceFolderId { get; set; }

        public int? DestinationFolderId { get; set; }

        public int? RuleId { get; set; }

        public string Version { get; set; }

        public string Hash { get; set; }

        public string FileName { get; set; }

        public int? Retries { get; set; }

        public string EngineKey { get; set; }

        public string NewFileName { get; set; }

        public long? LSize { get; set; }

    }

}
