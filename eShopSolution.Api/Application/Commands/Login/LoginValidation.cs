using FluentValidation;

namespace eShopSolution.Api.Application.Commands.Login.Create
{
    public class LoginValidation : AbstractValidator<CreateLoginCommand>
    {
        public LoginValidation()
        {
            this.RuleFor(x => x.UserName).NotEmpty().WithMessage("{PropertyName} không được để trống");
            this.RuleFor(x => x.PassWord).NotEmpty().WithMessage("{PropertyName} không được để trống")
                .MinimumLength(8).WithMessage("Độ dài tối thiểu của {PropertyName} là {MinLength} kí tự bạn đã nhập {TotalLength}")
                .MaximumLength(20).WithMessage("Độ dài tối đa của {PropertyName} là {MaxLength} kí tự bạn đã nhập {TotalLength}");
        }
    }
}