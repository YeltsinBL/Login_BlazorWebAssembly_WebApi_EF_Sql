namespace LoginApplication.Shared
{
    public class RegisteResult
	{
        public bool Successful { get; set; }
        public IEnumerable<string>? Errors { get; set; }
    }
}

