namespace IdentityDemo2.Models
{
    public enum DifficultyLevel
    {
        EASY, MEDIUM, HARD
    }
    public class Problem
    {
        public int Id { get; set; }

   
        public string Title { get; set; }


        public string Description { get; set; }

        public string ProblemStatement { get; set; }
        public string Explanation { get; set; }

         public int Marks { get; set; }

        public DifficultyLevel DifficultyLevel { get; set; }
        public string SampleInput { get; set; }
        public string SampleOutput { get; set; }

        public string Testcase { get; set; }

        public string ResultOfTestCase { get; set; }
        public string SolutionCode { get; set; }


        //Foreign key with contest table
        public int ContestId { get; set; }
        public Contest Contest { get; set; } = null!;


        //Foreign Key for attempt tables
        public ICollection<Attempt> attempt { get; } = new List<Attempt>();




    }
}
