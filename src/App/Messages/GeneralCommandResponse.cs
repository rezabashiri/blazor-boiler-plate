namespace Messages
{
    public class GeneralCommandResponse
    {
        public string Message { get; }
        public string ErrorMessage { get; }

        public GeneralCommandResponse(string message, string errorMessage)
        {
            Message = message;
            ErrorMessage = errorMessage;
        }
    }
}