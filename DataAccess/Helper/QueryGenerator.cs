using System.Reflection;
using Dapper.FastCrud;
using DataAccess.Configuration.Register;
using Domain.DataModel.Contracts;

namespace DataAccess.Helper
{
    public static class QueryGenerator
    {
        public static string CreateInsertQuery<T>()
        {
            var query = $"INSERT INTO {Sql.Table<T>().ToTableName()} ([COLUMNS]) VALUES ([VALUES])";

            var props = GetProperties<T>(out var keyInfo);
            var columns = string.Empty;
            var values = string.Empty;
            var last = props.Last();

            foreach (var prop in props)
            {
                if (prop.Equals(last))
                    columns += prop.Name;
                else columns += prop.Name + ", ";

                if (prop.Equals(last))
                    values += $":{prop.Name}";
                else values += $":{prop.Name}, ";
            }

            query = query.Replace("[COLUMNS]", columns);
            query = query.Replace("[VALUES]", values);

            return query;
        }

        public static string CreateUpdateQuery<T>()
        {
            var query = $"UPDATE {Sql.Table<T>().ToTableName()} SET [STATEMENTS] [WHERE]";

            var props = GetProperties<T>(out var keyInfo);
            var statements = string.Empty;
            var last = props.Last();

            foreach (var prop in props)
            {
                if (prop.Equals(last))
                    statements += $"{prop.Name} = :{prop.Name}";
                else statements += $"{prop.Name} = :{prop.Name}" + ", ";
            }

            query = query.Replace("[STATEMENTS]", statements);
            query = query.Replace("[WHERE]", $"WHERE {keyInfo.Name} = :{keyInfo.Name}");

            return query;
        }

        public static string CreateSelectByPrimaryKeyQuery<T>()
        {
            var query = $"SELECT * FROM {Sql.Table<T>().ToTableName()} [WHERE]";

            GetProperties<T>(out var keyInfo);
            query = query.Replace("[WHERE]", $"WHERE {keyInfo.Name} = :{keyInfo.Name}");

            return query;
        }

        public static string CreateDeleteQuery<T>()
        {
            var query = $"DELETE FROM {Sql.Table<T>().ToTableName()} [WHERE]";

            GetProperties<T>(out var keyInfo);
            query = query.Replace("[WHERE]", $"WHERE {keyInfo.Name} = :{keyInfo.Name}");

            return query;
        }

        private static PropertyInfo[] GetProperties<T>(out PropertyInfo key)
        {
            var type = typeof(T);
            var props = type.GetProperties();
            var interfaces = type.GetInterfaces();

            if (interfaces.Any(q => q.Name == nameof(IIntegerBaseEntity)))
            {
                key = props.First(q => q.Name == nameof(IIntegerBaseEntity.Id));
                props = props.Where(q => q.Name != nameof(IIntegerBaseEntity.Id)).ToArray();
            }
            else
            {
                key = props.First();
            }

            return props;
        }
    }
}
