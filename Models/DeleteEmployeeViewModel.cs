namespace matxicorp.Models
{
    public class DeleteEmployeeViewModel
    {
        public int ID { get; set; }
        public string EmployeeCode { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string LevelName { get; set; }
        public string DirectManagerName { get; set; }
        public string TeamLeaderName { get; set; }
    }
}
