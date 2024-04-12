namespace Common.Repository
{
    public static class Errors
    {
        public const string ALREADY_INICIATED_TRANSACTION = "You Have Already one trasaction opened to this scoped UnitOfWork.";
        public const string NO_INICIATED_TRANSACTION = "No Inicialized transaction opened to this scoped UnitOfWork.";
    }
}