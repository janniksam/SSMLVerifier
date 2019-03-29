namespace SSMLVerifier
{
    public class VerificationResult
    {
        public VerificationResult(VerificationState state, string error = null)
        {
            State = state;
            Error = error;
        }

        public VerificationState State { get; }

        public string Error { get; }

        public static VerificationResult Sucess => new VerificationResult(VerificationState.Valid);
    }
}