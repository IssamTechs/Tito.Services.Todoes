namespace Tito.Services.Todoes.Core.Exceptions
{
    public class InvalidDescriptionException : DomainException
    {
        public InvalidDescriptionException(string description) : base($"Invalid todo description: {description}")
        {
            this.Description = description;
        }

        // localization purposes
        public override string Code => "invalid_todo_description";
        public string Description { get; set; }
    }
}
