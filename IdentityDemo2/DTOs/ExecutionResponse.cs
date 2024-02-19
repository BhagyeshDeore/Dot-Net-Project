namespace IdentityDemo2.DTOs
{
    public class ExecutionResponse
    {
        public string Status { get; set; }
        public string Exception { get; set; }
        public string Stdout { get; set; }
        public string Stderr { get; set; }
        public int ExecutionTime { get; set; }
        public int LimitRemaining { get; set; }
        public string Stdin { get; set; }


    }
}
