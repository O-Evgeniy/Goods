namespace GoodsManagerWeb.Models
{
    public class User
    {
        public string Login { get; set; }
        public string Password { get; set; }
        public Role Role { get; set; }

        public User(string login,string password,Role role)
        {
            Login = login;
            Password = password;
            Role = role;
        }
    }

    public class Role
    {
        public string Name { get; set; }
        public Role(string name)
        {
            Name = name;
        }
    }
}
