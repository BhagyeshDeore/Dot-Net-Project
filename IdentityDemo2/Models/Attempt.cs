namespace IdentityDemo2.Models
{
    public enum CodingLanguage
    {
        JAVA
    }
    
    public enum SolvedStatus {
        CompileTime_Error, RunTime_Exception, TimeExceeded_Error, Partially_Solved, Solved
    }
    public class Attempt
    {
        public int Id { get; set; }
        public string Code { get; set; }
        public CodingLanguage Language { get; set; }
        public SolvedStatus SolvedStatus { get; set; }
        public string Result {  get; set; }
        public int ObtainedMarks { get; set; }


        //Foreign keys to show who created it
        public string ApplicationUserId { get; set; }
        public virtual ApplicationUser ApplicationUser { get; set; }



        //Foreign key with contest table
        public int ContestId { get; set; }
        public Contest Contest { get; set; } = null!;



        //Foreign key with Problem table
        public int ProblemId { get; set; }
        public Problem Problem { get; set; } = null!;





    }

}
    