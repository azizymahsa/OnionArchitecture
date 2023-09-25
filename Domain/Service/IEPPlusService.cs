using OfficeOpenXml;

namespace Domain.Service
{
    public interface IEPPlusService
    {
        IEnumerable<T> ConvertSheetToObjects<T>(ExcelWorksheet worksheet) where T : new();
    }
}
