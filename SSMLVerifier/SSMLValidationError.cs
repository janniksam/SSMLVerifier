namespace SSMLVerifier
{
    public class SSMLValidationError
    {
        public SSMLValidationError(VerificationState state, string error = null)
        {
            State = state;
            Error = error;
        }

        public VerificationState State { get; }

        public string Error { get; }
    }
}