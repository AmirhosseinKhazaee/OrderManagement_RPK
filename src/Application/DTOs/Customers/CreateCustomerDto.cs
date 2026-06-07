using FluentValidation;
namespace Application.DTOs.Customers;

public class CreateCustomerDto
{
    public string Name { get; set; } = null!;

    public string Email { get; set; } = null!;
}


public class CreateCustomerDtoValidator : AbstractValidator<CreateCustomerDto>
{
    public CreateCustomerDtoValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty()
            .MaximumLength(100);

        RuleFor(x => x.Email)
            .EmailRule();   // 👈 اینجا reusable rule
    }
}