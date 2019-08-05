using RzeszowBusCore.Models.Abstract;

namespace RzeszowBusCore.Models
{
    public class SimpleBusStop : ITable
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Name { get; set; }
        public int Id2 { get; set; }

        public string[] GetColumns()
            => new[] { "Id", "Title", "Name" };

        public string[] GetRow()
            => new[] { Id.ToString(), Title, Name };

        public bool HaveInnerTable() => false;
    }
}