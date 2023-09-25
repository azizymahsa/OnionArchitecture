using OfficeOpenXml;
using System.Reflection;
using Domain.Service;
using ExcelColumn = Domain.Attributes.ExcelColumn;

namespace Service
{
    public class EPPlusService : IEPPlusService
    {
        public EPPlusService()
        {

        }

        public IEnumerable<T> ConvertSheetToObjects<T>(ExcelWorksheet worksheet) where T : new()
        {
            var columns = typeof(T)
                .GetProperties()
                .Where(x => x.CustomAttributes.Any(q => q.AttributeType == typeof(ExcelColumn)))
                .Select(p => new
                {
                    Property = p,
                    Column = p.GetCustomAttributes<ExcelColumn>().First().ColumnIndex //safe because if where above
                }).ToList();

            var rows = worksheet.Cells
                .Select(cell => cell.Start.Row)
                .Distinct()
                .OrderBy(x => x);

            //Create the collection container
            var collection = rows.Skip(1)
                .Select(row =>
                {
                    var newModel = new T();
                    columns.ForEach(col =>
                    {
                        //This is the real wrinkle to using reflection - Excel stores all numbers as double including int
                        var val = worksheet.Cells[row, col.Column];
                        //If it is numeric it is a double since that is how excel stores all numbers
                        if (val.Value == null)
                        {
                            col.Property.SetValue(newModel, null);
                            return;
                        }

                        if (col.Property.PropertyType == typeof(int))
                        {
                            col.Property.SetValue(newModel, val.GetValue<int>());
                            return;
                        }

                        if (col.Property.PropertyType == typeof(double))
                        {
                            col.Property.SetValue(newModel, val.GetValue<double>());
                            return;
                        }

                        if (col.Property.PropertyType == typeof(DateTime))
                        {
                            col.Property.SetValue(newModel, val.GetValue<DateTime>());
                            return;
                        }

                        //Its a string
                        col.Property.SetValue(newModel, val.GetValue<string>());
                    });

                    return newModel;
                });


            //Send it back
            return collection;
        }

        //protected string Username => HttpContext.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;

        //protected IEnumerable<Permission> UserPermissions
        //{
        //    get
        //    {
        //        if (User == null)
        //            return Enumerable.Empty<Permission>();
        //        var claim = User.Claims.FirstOrDefault(c => c.Type == nameof(Permission));
        //        if (claim == null)
        //            return Enumerable.Empty<Permission>();
        //        return claim.Value.Split(',', StringSplitOptions.RemoveEmptyEntries)
        //            .Select(c => Enum.Parse(typeof(Permission), c))
        //            .Cast<Permission>();
        //    }
        //}

        //protected FileContentResult CreateExcelFile(DataTable table, string sheetName, string fileName)
        //{
        //    using var memStream = new MemoryStream();
        //    using var package = new ExcelPackage(memStream);
        //    ExcelWorksheet sheet = package.Workbook.Worksheets.Add(sheetName);
        //    sheet.Cells["A1"].LoadFromDataTable(table, true);
        //    package.Save();
        //    var file = memStream.ToArray();
        //    return File(file, "application/octet-stream", fileName);
        //}

        //protected DataTable CreateTableFromObjects<T>(IEnumerable<T> items, string dateFormat = null)
        //{
        //    return CreateTableFromObjects(items, typeof(T).GetProperties(), dateFormat);
        //}

        //protected DataTable CreateTableFromObjectsExclusive<T>(IEnumerable<T> items, IEnumerable<string> excludeProperties, string dateFormat = null)
        //{
        //    if (excludeProperties == null)
        //        excludeProperties = Array.Empty<string>();

        //    var props = typeof(T).GetProperties().Where(p => !excludeProperties.Contains(p.Name));
        //    return CreateTableFromObjects(items, props, dateFormat);
        //}

        //protected DataTable CreateTableFromObjects<T>(IEnumerable<T> items, IEnumerable<string> properties, string dateFormat = null)
        //{
        //    var props = properties.Select(p => typeof(T).GetProperty(p));
        //    return CreateTableFromObjects(items, props, dateFormat);
        //}

        //protected DataTable CreateTableFromObjects<T>(IEnumerable<T> items, IEnumerable<PropertyInfo> properties, string dateFormat = null)
        //{
        //    var table = new DataTable();
        //    var props = properties.ToDictionary(p => DisplayUtils.DisplayName(p));
        //    foreach (var prop in props)
        //    {
        //        table.Columns.Add(prop.Key);
        //    }
        //    foreach (var item in items)
        //    {
        //        var row = table.NewRow();
        //        foreach (var prop in props)
        //        {
        //            var value = prop.Value.GetValue(item);
        //            if (prop.Value.PropertyType.IsEnum)
        //                value = DisplayUtils.DisplayName(prop.Value.PropertyType, value.ToString());
        //            else if (prop.Value.PropertyType == typeof(DateTime) && dateFormat != null)
        //            {
        //                value = ((DateTime)value).ToString(dateFormat);
        //            }
        //            else if (prop.Value.PropertyType == typeof(DateTime?) && value != null && dateFormat != null)
        //            {
        //                value = ((DateTime?)value).Value.ToString(dateFormat);
        //            }
        //            row[prop.Key] = value;
        //        }
        //        table.Rows.Add(row);
        //    }
        //    return table;
        //}
    }
}
