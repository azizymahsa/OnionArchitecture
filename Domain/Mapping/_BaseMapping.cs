namespace Domain.Mapping
{
    public static class BaseMapping
    {
        #region DateTime

        public static DateTime? ToNullableDateTime(this string data)
        {
            return DateTime.TryParse(data, out var result) ? result : null;
        }

        public static DateTime ToDateTime(this string data)
        {
            return DateTime.Parse(data);
        }

        #endregion

        #region Int

        public static int ToInt32(this string data)
        {
            return Convert.ToInt32(data);
        }

        public static int? ToNullableInt32(this string data)
        {
            return int.TryParse(data, out var result) ? result : null;
        }

        public static short ToInt16(this string data)
        {
            return Convert.ToInt16(data);
        }

        public static short? ToNullableInt16(this string data)
        {
            return short.TryParse(data, out var result) ? result : null;
        }

        #endregion

        #region Bool

        public static bool ToBoolean(this string data)
        {
            return Convert.ToBoolean(data);
        }

        public static bool? ToNullableBoolean(this string data)
        {
            return bool.TryParse(data, out var result) ? result : null;
        }

        #endregion

        #region Decimal

        public static decimal ToDecimal(this string data)
        {
            return Convert.ToDecimal(data);
        }

        public static decimal? ToNullableDecimal(this string data)
        {
            return decimal.TryParse(data, out var result) ? result : null;
        }

        #endregion

        #region Enum

        public static T ToEnum<T>(this string data) where T : struct
        {
            return Enum.Parse<T>(data, true);
        }

        #endregion
    }
}
