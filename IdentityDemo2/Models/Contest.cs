using System.ComponentModel.DataAnnotations;

namespace IdentityDemo2.Models
{
    public enum Topics
    {
        DSA, String, ExceptionHandling, Loops, OOps
    }
    public class Contest
    {
        public int Id { get; set; }
        
        public string Title { get; set; }

        public string Description { get; set; }

        public Topics Topic { get; set; }

        [Timestamp]
        public DateTime CreatedAt { get; set; }




        //Foreign keys to show who created it
        public string ApplicationUserId { get; set; }
        public virtual ApplicationUser ApplicationUser { get; set; }



        //Foreign Key for Problem Table
        public ICollection<Problem> problems { get; } = new List<Problem>();


        //Foreign Key for attempt tables
        public ICollection<Attempt> attempt { get; } = new List<Attempt>();


    }
}
