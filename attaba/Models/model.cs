namespace attaba.Models
{

    public class Employee
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string SectionId { get; set; }

        public List<string> DegreeIds { get; set; }
    }

    public class Section
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string DepartmentId { get; set; }
    }

    public class Department
    {
        public string Id { get; set; }
        public string Name { get; set; }
    }

    public class Degree
    {
        public string Id { get; set; }
        public string Name { get; set; }
    }

    public class User
    {
        public string Id { get; set; }
        public string Username { get; set; }
        public string PasswordHash { get; set; }
        public List<string> Roles { get; set; }
    }

}
