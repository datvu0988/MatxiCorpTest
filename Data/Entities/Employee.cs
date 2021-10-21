using System;

namespace matxicorp.Data.Entities
{
    public class Employee
    {
        public int Id { get; set; }
        public string EmployeeCode { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Mobile { get; set; }
        public string Email { get; set; }
        public string ParentCode { get; set; }
        public string LeaderCode { get; set; }
        public int Level { get; set; }
        public int? EntryUserID { get; set; }
        public DateTime? EntryDateTime { get; set; }
        public int? UpdateUserID { get; set; }
        public DateTime? UpdateDateTime { get; set; }
    }
}
