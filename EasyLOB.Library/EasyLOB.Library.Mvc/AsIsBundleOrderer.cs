using System.Collections.Generic;
using System.Web.Optimization;

namespace EasyLOB.Library.Mvc
{
    /// <summary>
    /// Ordered Bundle
    /// Install-Package Microsoft.AspNet.Web.Optimization
    /// </summary>
    public class AsIsBundleOrderer : IBundleOrderer
    {
        #region Methods

        public virtual IEnumerable<BundleFile> OrderFiles(BundleContext context, IEnumerable<BundleFile> files)
        {
            return files;
        }

        #endregion Methods
    }
}