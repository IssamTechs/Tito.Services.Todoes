namespace Tito.Services.Todoes.Core.Exceptions
{
    public class InvalidTitleException : DomainException
    {
        public InvalidTitleException(string title): base($"Invalid todo title: {title}")
        {
            this.Title = title;
        }

        // localization purposes
        public override string Code => "invalid_todo_title";
        public string Title { get; set; }
    }
}
