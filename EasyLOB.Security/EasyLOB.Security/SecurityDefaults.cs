namespace EasyLOB.Security
{
    public static partial class SecurityDefaults
    {
        #region Properties

        public static string[] SecurityOperationsAcronyms
        {
            get
            {
                return new string[] {
                    "S", // Search
                    "C", // Create
                    "R", // Read
                    "U", // Update
                    "D", // Delete
                    "E", // Export
                    "I", // Import
                    "X"  // Execute
                };
            }
        }

        public static string[] SecurityOperationsNames
        {
            get
            {
                return new string[] {
                    "Search", // Search
                    "Create", // Create
                    "Read",   // Read
                    "Update", // Update
                    "Delete", // Delete
                    "Export", // Export
                    "Import", // Import
                    "Execute" // Execute
                };
            }
        }

        #endregion Properties
    }
}