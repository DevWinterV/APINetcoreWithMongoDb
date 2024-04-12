namespace APINetcoreWithSqLServer.Request
{
    public class AccountRequest : DTORequest
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public bool Status { get; set; }
        public int AccountId { get; set; }
    }
}
