namespace StarLess
{
    public interface ICommand
    {
        string Keyword { get; }
        string Description { get; }

        void Run(params string[] args);

        string CompleteDescription();
    }
}