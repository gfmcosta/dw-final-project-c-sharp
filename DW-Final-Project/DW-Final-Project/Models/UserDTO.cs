namespace DW_Final_Project.Models
{
    public class UserDTO
    {
        public string email { get; set; }
        
        public string password { get; set; }

        public string name{ get; set; }

        public string phoneNumber { get; set; }

        public string address { get; set; }

        public string postalCode { get; set; }

        public DateTime dataNasc { get; set; }

        public string gender { get; set; }
        
        public string? imagePath { get; set; }
    }
}
