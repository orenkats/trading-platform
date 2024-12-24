namespace UserService.Common.Events
{
    public class UserRegisteredEvent
    {
        public Guid UserId { get; set; }
        public string Email { get; set; }
        public string Name { get; set; }
    }
}
