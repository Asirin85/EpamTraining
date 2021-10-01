using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp
{
    public record ArgsStructure
    {
        private double _minMark = 0;
        private double _maxMark = 5;
        private DateTime _dateFrom = DateTime.MinValue;
        private DateTime _dateTo = DateTime.MaxValue;
        private string _test = "";
        private string _name = "";
        private string _sortBy = "Name";
        private string _sortOrder = "asc";
        public bool SortAsc { get { return SortOrder.Equals("asc"); } }
        public double MinMark { get { return _minMark; } init { if (value >= 0 && value <= 5) _minMark = value; } }
        public double MaxMark { get { return _maxMark; } init { if (value >= 0 && value <= 5) _maxMark = value; else _maxMark = 5; } }
        public string DateFrom
        {
            get { return _dateFrom.ToString(); }
            init
            {
                DateTime dateFromTime = DateTime.TryParseExact(value, "dd/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out dateFromTime) ? dateFromTime : DateTime.MinValue;
                if (dateFromTime.CompareTo(DateTime.Now) <= 0) _dateFrom = dateFromTime;
            }
        }
        public string DateTo
        {
            get { return _dateTo.ToString(); }
            init
            {
                DateTime dateToTime = DateTime.TryParseExact(value, "dd/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out dateToTime) ? dateToTime : DateTime.MaxValue;
                if (dateToTime.CompareTo(DateTime.Now) <= 0) _dateTo = dateToTime; else _dateTo = DateTime.MaxValue;
            }
        }
        public string Test { get { return _test; } init { if (value is not null) _test = value; } }
        public string Name { get { return _name; } init { if (value is not null) _name = value; } }
        public string SortBy
        {
            get { return _sortBy ?? "name"; }
            init
            {
                if (value is not null && new[] { nameof(Student.Mark), nameof(Student.Name), nameof(Student.Test), nameof(Student.Date) }.Contains(value, StringComparer.InvariantCultureIgnoreCase)) _sortBy = $"{value[0].ToString().ToUpperInvariant()}{value.ToLowerInvariant()[1..value.Length]}";
                else _sortBy = nameof(Student.Name);
            }
        }
        public string SortOrder
        {
            get { return _sortOrder ?? "asc"; }
            init
            {
                if (value is not null && (value.Equals("asc", StringComparison.InvariantCultureIgnoreCase) || value.Equals("desc", StringComparison.InvariantCultureIgnoreCase))) _sortOrder = value.ToLowerInvariant();
                else _sortOrder = "asc";
            }
        }
    }
}
