namespace TPServiceScolarite.Models
{

    public class Module
    {
        //un module: logo, id, nom, resume, infos
        public int Id { get; set; }
        public string Nom { get; set; }
        public string? Resume { get; set; }
        public string? Infos { get; set; }
        public string? Logo { get; set; }
    }
}
