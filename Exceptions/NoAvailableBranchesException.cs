namespace Exceptions
{
    [Serializable]
    public class NoAvailableBranchesException : Exception
    {
        public NoAvailableBranchesException() { }

        public NoAvailableBranchesException(string message)
            : base(message)
        {

        }
    }
}