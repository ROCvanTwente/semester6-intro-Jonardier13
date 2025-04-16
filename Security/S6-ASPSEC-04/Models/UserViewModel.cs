namespace S6_ASPSEC_04.Models
{
    public class UserViewModel
    {
        public string Id { get; set; }  // Gebruiker ID
        public string Email { get; set; }  // Gebruiker e-mail
        public string Roles { get; set; }  // Gebruiker rollen als string
        public string SelectedRole { get; set; }  // De huidige geselecteerde rol
        public List<string> AllRoles { get; set; }  // Alle rollen die beschikbaar zijn
    }
}
