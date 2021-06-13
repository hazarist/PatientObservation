using System;

namespace Amatis.PatientObservation.Common.Entities
{
    public class BaseEntity
    {
        public long Id { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime CreatedDate { get; set; }
#nullable enable
        public DateTime? LastUpdatedDate { get; set; }
#nullable disable
    }
}
