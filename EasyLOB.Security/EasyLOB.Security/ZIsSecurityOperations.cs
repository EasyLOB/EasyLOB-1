namespace EasyLOB.Security
{
    public class ZIsSecurityOperations
    {
        #region Properties

        public string Activity { get; set; }

        public bool IsSearch { get; set; }
        public bool IsCreate { get; set; }
        public bool IsRead { get; set; }
        public bool IsUpdate { get; set; }
        public bool IsDelete { get; set; }
        public bool IsExport { get; set; }
        public bool IsImport { get; set; }
        public bool IsExecute { get; set; }

        #endregion Properties
    }
}
