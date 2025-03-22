using System.Collections.Generic;

namespace CochainAPI.Model.Helper
{
    public sealed class Page<T>
    {
        public required IEnumerable<T>? Items { get; set; }
        public int TotalSize { get; set; }
    }
}