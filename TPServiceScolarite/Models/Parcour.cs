namespace TPServiceScolarite.Models
{
    public class Parcour
    {
        public int Id { get; set; }
        public string? Nom { get; set; }
        public string? Resume { get; set; }
        public string? Infos { get; set; }
        public string? Logo { get; set; }
        public IEnumerable<Module>? Modules { get; set; }
    }
}
