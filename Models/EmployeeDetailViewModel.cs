using System.Collections.Generic;

namespace matxicorp.Models
{
    public class EmployeeDetailViewModel
    {
        public int ID { get; set; }
        public string EmployeeCode { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string LevelName { get; set; }
        public string DirectManagerName { get; set; }
        public string TeamLeaderName { get; set; }
        public IList<string> TeamMembers { get; set; }
    }
}
