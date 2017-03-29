using System;


namespace ASOL.HireThings.Model
{
    public interface IBaseModel
    {
        long UserId { get; set; }
        int CPage { get; set; }
        int TPage { get; set; }
        long TRows { get; set; }
        string PSize { get; set; }
        string ColumnName { get; set; }
        int SortType { get; set; }
        IPageInfo GetPageInfo(int cpage, int tpage, long trows,string psize,long userid, string columnname = "", int sorttype = -1);
        
        
    }
}
