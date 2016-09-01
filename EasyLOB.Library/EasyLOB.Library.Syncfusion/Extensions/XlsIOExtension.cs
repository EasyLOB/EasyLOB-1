using Syncfusion.XlsIO;

namespace EasyLOB.Library.Syncfusion
{
    public static class XlsIOExtensions
    {
        public static void AutoAlign(this IWorksheet worksheet, int startColumn, int columns)
        {
            for (int column = startColumn; column <= startColumn + columns - 1; column++)
            {
                worksheet.AutofitColumn(column);
            }
        }
    }
}