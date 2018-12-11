namespace WebApplication1.dto
{
    public class UserProfileDTO
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public int Role { get; set; }       //0 - admin, 1 - user
        public long UserId { get; set; }
    }
}