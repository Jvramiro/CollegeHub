namespace CollegeHub.Models {
    public enum Role { Student, Teacher, Adm }
    public class User : Entity {
        public string Name { get; set; }
        public string Email { get; private set; }
        public string Password { get; private set; }
        public string CPF { get; private set; }
        public string Phone { get; private set; }
        public Role Role { get; private set; }

        public User(string Name, string Email, string Password, string CPF, string Phone, Role Role, Guid CreatedBy) {
            this.Name = Name;
            this.Email = Email;
            this.Password = Password;
            this.CPF = CPF;
            this.Phone = Phone;
            this.Role = Role;

            this.CreatedBy = CreatedBy;
            this.EditedBy = CreatedBy;
            CreatedOn = DateTime.Now;
            EditedOn = DateTime.Now;
            Active = true;
        }

        public void Update(string? Name = null, string? Password = null, string? Phone = null, bool? Active = null, Guid? EditedBy = null) {
            this.Name = Name ?? this.Name;
            this.Password = Password ?? this.Password;
            this.Phone = Phone ?? this.Phone;
            this.Active = Active ?? this.Active;

            this.EditedBy = EditedBy ?? this.EditedBy;
            EditedOn = DateTime.Now;
        }
    }
}
