namespace Ecom.Client.Core.Common
{
    public abstract class Result
    {
        public List<Errors> Errors { get; set; }

        public bool IsError => Errors != null && Errors.Any();
    }

    public class Result<T> : Result
    {
        public T Response { get; set; }

        public string WarningMessage { get; set; }
    }
}
