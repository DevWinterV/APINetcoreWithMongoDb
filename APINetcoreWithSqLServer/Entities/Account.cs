
using APINetcoreWithMongoDb.Entities;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace APINetcoreWithSqLServer.Entities
{
    [Table("Account")]
    public class Account : BaseEntity
    {
        [Key]
        public int Id { get; set; }

        public int UserId { get; set; }

        public string Username { get; set; }

        public string Password { get; set; }

        public bool Status { get; set; }

        public int AccountId { get; set; }
    }
}
