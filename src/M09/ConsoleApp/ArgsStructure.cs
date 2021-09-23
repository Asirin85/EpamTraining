using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp
{
    public class ArgsStructure
    {
        private double _minMark = 0;
        private double _maxMark = 5;
        private DateTime _dateFrom = DateTime.MinValue;
        private DateTime _dateTo = DateTime.MaxValue;
        private string _test = "";
        private string _name = "";
        private string _sortBy = "";
        private string _sortOrder = "";
        public double MinMark { get { return _minMark; } init { if (value >= 0 && value <= 5) _minMark = value; } }
        public double MaxMark { get { return _maxMark; } init { if (value >= 0 && value <= 5) _maxMark = value; else _maxMark = 5; } }
        public DateTime DateFrom { get { return _dateFrom; } init { if (value.CompareTo(DateTime.Now) <= 0) _dateFrom = value; } }
        public DateTime DateTo { get { return _dateTo; } init { if (value.CompareTo(DateTime.Now) <= 0) _dateTo = value; else _dateTo = DateTime.MaxValue; } }
        public string Test { get { return _test ?? ""; } init { _test = value; } }
        public string Name { get { return _name ?? ""; } init { _name = value; } }
        public string SortBy { get { return _sortBy ?? ""; } init { _sortBy = value; } }
        public string SortOrder { get { return _sortOrder ?? ""; } init { _sortOrder = value; } }
    }
}
