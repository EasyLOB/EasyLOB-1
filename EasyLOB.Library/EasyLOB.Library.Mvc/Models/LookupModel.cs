using EasyLOB.Security;
using System.Collections.Generic;

namespace EasyLOB.Library.Mvc
{
    /// <summary>
    /// Lookup Model
    /// </summary>
    public class LookupModel
    {
        /// <summary>
        /// Operation Result.
        /// </summary>
        public ZOperationResult OperationResult { get; set; }

        /// <summary>
        /// Security Operations.
        /// </summary>
        public ZIsSecurityOperations IsSecurityOperations { get; set; }

        /// <summary>
        /// Default lookup text.
        /// </summary>
        public string Text { get; set; }

        /// <summary>
        /// Value Element HTML Id.
        /// </summary>
        public string ValueId { get; set; }

        /// <summary>
        /// HTML Elements to be uptaded after lookup selection.
        /// </summary>
        public List<LookupModelElement> Elements { get; set; }

        public LookupModel()
        {
            OperationResult = new ZOperationResult();
            Elements = new List<LookupModelElement>();
        }
    }

    /// <summary>
    /// Lookup HTML elements
    /// </summary>
    public class LookupModelElement
    {
        /// <summary>
        /// Element Id.
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// Element associated property.
        /// </summary>
        public string Property { get; set; }

        public LookupModelElement()
        {
        }

        public LookupModelElement(string id, string property)
        {
            Id = id;
            Property = property;
        }
    }
}