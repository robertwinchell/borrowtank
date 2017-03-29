using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASOL.HireThings.Model
{
    public interface IPageInfo
    {
        int CPage { get; set; }
        int TPage { get; set; }
        long TRows { get; set; }
        double TCount { get; set; }
        string PSize {  get; set; }
        long UserId { get; set; }
        string ColumnName { get; set; }
        int SortType { get; set; }
    }

    public class PageInfo : IPageInfo
    {
        public int CPage { get; set; }
        public int TPage { get; set; }
        public long TRows { get; set; }
        public double TCount { get; set; }
        public string PSize { get; set; }
        public long UserId { get; set; }
        public string ColumnName { get; set; }
        public int SortType { get; set; }
    }
}
