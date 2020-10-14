namespace Models.DTO.Grid
{
    using System.Collections.Generic;

    public abstract class Grid<T>
    {
        public int Count { get; set; }
        public IEnumerable<T> List { get; set; }
    }
}
