namespace Backgroud.Services.Entities;

public class Email
{
    public Guid Id { get; private set; }
    // ReSharper disable once EntityFramework.ModelValidation.UnlimitedStringLength
    public string? EmailAddress { get; private set; }

    public Email() {}

    public Email(Guid id, string? emailAddress)
    {
        Id = id;
        EmailAddress = emailAddress;
    }
}