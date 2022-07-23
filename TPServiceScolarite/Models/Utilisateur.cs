using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace TPServiceScolarite.Models
{
    // (Nom, Prenom, Mail, Adresse)
    public class Utilisateur
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string? Nom { get; set; }
        public string? Prenom { get; set; }
        public string? Adresse { get; set; }
    }
}
