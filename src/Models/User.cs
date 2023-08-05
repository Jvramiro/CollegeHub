namespace CollegeHub.Models {
    public enum Role { Student, Teacher, Adm }
    public class User : Entity {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string CPF { get; set; }
        public string Phone { get; set; }
        public Role Role { get; set; }
    }
}
