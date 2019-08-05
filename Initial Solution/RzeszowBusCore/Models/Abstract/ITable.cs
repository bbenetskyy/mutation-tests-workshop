namespace RzeszowBusCore.Models.Abstract
{
    public interface ITable
    {
        string[] GetColumns();
        string[] GetRow();
        bool HaveInnerTable();
    }
}