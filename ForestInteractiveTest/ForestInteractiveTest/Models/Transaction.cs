using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ForestInteractiveTest.Models
{
    public class Transaction
    {
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public DateTime DateCreated { get; set; }
        public string UserCreated { get; set; }
        [MaxLength(160)]
        public string Message { get; set; }

        public static List<Transaction> GetSubmitedMessage()
        {
            using (UsersContext dbContext = new UsersContext())
            {
                return dbContext.Transaction.OrderBy(p => p.Id).ToList();
            }
        }

        public static bool CheckMessage(string message)
        {
            bool result = false;
            using (UsersContext dbContext = new UsersContext())
            {
                var now = DateTime.Now;
                var start = new DateTime(now.Year, now.Month, now.Day, 0, 0, 0);
                var end = new DateTime(now.Year, now.Month, now.Day, 23, 59, 59);
                if (dbContext.Transaction.Where(x => x.DateCreated > start && x.DateCreated < end).Any(x => x.Message == message))
                {
                    result = true;
                }
            }

            return result;
        }
    }
}