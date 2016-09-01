using LinqToDB;
using LinqToDB.Data;
using EasyLOB.AuditTrail.Data;

namespace EasyLOB.AuditTrail.Persistence
{
    public class AuditTrailLINQ2DB : DataConnection
    {
        public AuditTrailLINQ2DB()
            : base("AuditTrail")
        {
            AuditTrailLINQ2DBMap.AuditTrailConfigurationMap(MappingSchema);
            AuditTrailLINQ2DBMap.AuditTrailLogMap(MappingSchema);
        }

        public ITable<AuditTrailConfiguration> AuditTrailConfiguration
        {
            get { return GetTable<AuditTrailConfiguration>(); }
        }

        public ITable<AuditTrailLog> AuditTrailLog
        {
            get { return GetTable<AuditTrailLog>(); }
        }
    }
}