using FluentValidation;

public static class EmailValidator
{
    public static IRuleBuilderOptions<T, string> EmailRule<T>(
        this IRuleBuilder<T, string> rule)
    {
        return rule
            .NotEmpty()
            .EmailAddress()
            .MaximumLength(255);
    }
}