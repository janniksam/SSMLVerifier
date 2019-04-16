namespace SSMLVerifier
{
    public enum VerificationState
    {
        InvalidTag = -1,
        MissingAttribute = -2,
        InvalidAttributeValue = -3,
        ContainerContainsInvalidChilds = -4,
        InvalidAttribute = -5,
        InvalidParent = -6,
    }
}