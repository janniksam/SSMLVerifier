namespace SSMLVerifier
{
    public enum VerificationState
    {
        Valid = 0,
        InvalidTag = -1,
        MissingAttribute = -2,
        InvalidAttributeValue = -3,
        ContainerContainsInvalidChilds = -4,
        InvalidAttribute = -5
    }
}