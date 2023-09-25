namespace Domain.Attributes
{
    [AttributeUsage(AttributeTargets.All)]
    public class ExcelColumn : Attribute
    {
        public int ColumnIndex { get; set; }


        public ExcelColumn(int column)
        {
            ColumnIndex = column;
        }
    }
}
