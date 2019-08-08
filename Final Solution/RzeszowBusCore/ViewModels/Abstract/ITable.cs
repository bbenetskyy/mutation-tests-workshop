namespace RzeszowBusCore.ViewModels.Abstract
{
    public interface ITable
    {
        string[] GetColumns();
        string[] GetRow();
        bool HaveInnerTable();
    }
}