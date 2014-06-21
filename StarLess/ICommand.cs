namespace StarLess
{
    public interface ICommand
    {
        string Keyword { get; }
        string Description { get; }

        void Run(string[] args);

        string CompleteDescription();
    }
}