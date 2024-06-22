using NPOI.SS.UserModel;

namespace GoodsLib
{
    public static class CellExtension
    {
        public static string Parse(this ICell cell)
        {
            return cell.CellType switch
            {
                CellType.Boolean => cell.BooleanCellValue.ToString(),
                CellType.Numeric => cell.NumericCellValue.ToString(),
                CellType.Error => string.Empty,
                _ => cell.StringCellValue
            };
        }
    }
}