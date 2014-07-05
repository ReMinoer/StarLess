namespace StarLess
{
    public interface ICommand
    {
        string Keyword { get; }
        string Description { get; }
        AbstractCommand.ArgumentsList Arguments { get; }
        AbstractCommand.OptionsList Options { get; }

        void Run(params string[] args);

        string CompleteDescription();
    }
}