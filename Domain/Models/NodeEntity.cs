namespace ILS_BE.Domain.Models
{
    public class NodeEntity<Tkey> : BaseEntity<Tkey>
    {
        public string Path { get; set; } = ".";
    }
}
