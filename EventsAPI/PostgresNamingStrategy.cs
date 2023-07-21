using NHibernate.Cfg;
using NHibernate.Util;

internal class PostgresNamingStrategy : INamingStrategy {
    public string ClassToTableName(string className) {
        return className;
    }

    public string ColumnName(string columnName) {
        return DoubleQuote(columnName);
    }

    public string LogicalColumnName(string columnName, string propertyName) {
        return StringHelper.IsNotEmpty(columnName) ? columnName : StringHelper.Unqualify(propertyName);
    }

    public string PropertyToColumnName(string propertyName) {
        return propertyName;
    }

    public string PropertyToTableName(string className, string propertyName) {
        return propertyName;
    }

    public string TableName(string tableName) {
        return DoubleQuote(tableName);
    }

    private string DoubleQuote(string raw) {
        return $"\"{raw}\"";
    }
}