using System.ComponentModel.DataAnnotations;

namespace matxicorp.Models
{
    public class AddEmployeeViewModel
    {
        public int ID { get; set; }
        [Required(ErrorMessage = "Employee Code is required")]
        public string EmployeeCode { get; set; }
        [Required(ErrorMessage = "First Name is required")]
        public string FirstName { get; set; }
        [Required(ErrorMessage = "Last Name is required")]
        public string LastName { get; set; }
        [RegularExpression("^(?!0+$)(\\+\\d{1,3}[- ]?)?(?!0+$)\\d{10,15}$", ErrorMessage = "Phone Number is not valid")]
        public string Mobile { get; set; }
        [RegularExpression("^[a-zA-Z0-9_\\.-]+@([a-zA-Z0-9-]+\\.)+[a-zA-Z]{2,6}$", ErrorMessage = "E-mail is not valid")]
        public string Email { get; set; }
        public string LeaderName { get; set; }
        public string ParentCode { get; set; }
        public string LeaderCode { get; set; }
        public string ParentName { get; set; }
        public int Level { get; set; }
        public string JsonTeamLeaders { get; set; }
        public string JsonDirectManagers { get; set; }
    }
}
