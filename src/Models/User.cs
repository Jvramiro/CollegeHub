namespace CollegeHub.Models {
    public enum Role { Student, Teacher, Adm }
    public class User : Entity {
        public string Name { get; set; }
        public string Email { get; private set; }
        public string Password { get; private set; }
        public string CPF { get; private set; }
        public string Phone { get; private set; }
        public Role Role { get; private set; }

        public User(string Name, string Email, string Password, string CPF, string Phone, Role Role) {
            this.Name = Name;
            this.Email = Email;
            this.Password = Password;
            this.CPF = CPF;
            this.Phone = Phone;
            this.Role = Role;

            CreatedOn = DateTime.Now;
            EditedOn = DateTime.Now;
            Active = true;
        }

        public void Update(string? Name = null, string? Password = null, string? Phone = null, bool? Active = null) {
            this.Name = Name ?? this.Name;
            this.Password = Password ?? this.Password;
            this.Phone = Phone ?? this.Phone;
            this.Active = Active ?? this.Active;

            EditedOn = DateTime.Now;
        }
    }
}
