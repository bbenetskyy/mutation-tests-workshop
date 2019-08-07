namespace RzeszowBusCore.ViewModels.Abstract
{
    public interface IModel<T> where T : class
    {
        T Model { get; set; }
    }
}