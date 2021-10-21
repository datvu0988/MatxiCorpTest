namespace matxicorp.Models
{
    public class BasicEmployeeInfo
    {
        public int ID { get; set; }
        public string EmployeeCode { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int Level { get; set; }
        public string LevelName { get; set; }
        public string DirectManagerName { get; set; }
        public string TeamLeaderName { get; set; }
    }
}
