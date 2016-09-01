namespace EasyLOB.Library.Mvc
{
    /// <summary>
    /// Operation Result Model.
    /// </summary>
    public class OperationResultModel
    {
        /// <summary>
        /// Operation Result.
        /// </summary>
        public ZOperationResult OperationResult { get; set; }

        public OperationResultModel()
        {
            OperationResult = new ZOperationResult();
        }

        public OperationResultModel(ZOperationResult operationResult)
        {
            OperationResult = operationResult;
        }
    }
}