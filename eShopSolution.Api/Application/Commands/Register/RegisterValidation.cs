using FluentValidation;

namespace eShopSolution.Api.Application.Commands.Register.Create
{
    public class RegisterValidation : AbstractValidator<CreateRegisterCommand>
    {
        public RegisterValidation()
        {
            this.RuleFor(x => x.FirstName).NotEmpty().WithMessage("{PropertyName} không được để trống")
                .MaximumLength(50).WithMessage("Độ dài tối đa của {PropertyName} là {MaxLength} kí tự bạn đã nhập {TotalLength}");
            this.RuleFor(x => x.LastName).NotEmpty().WithMessage("{PropertyName} không được để trống")
                .MaximumLength(50).WithMessage("Độ dài tối đa của {PropertyName} là {MaxLength} kí tự bạn đã nhập {TotalLength}");
            this.RuleFor(x => x.Address).NotEmpty().WithMessage("{PropertyName} không được để trống")
                .MaximumLength(200).WithMessage("Độ dài tối đa của {PropertyName} là {MaxLength} kí tự bạn đã nhập {TotalLength}");
            this.RuleFor(x => x.Email).NotEmpty().WithMessage("{PropertyName} không được để trống")
                .EmailAddress().WithMessage("Địa chỉ nhập vào phải là địa chỉ email, vd: abc@abc.com");
            this.RuleFor(x => x.Dob).NotNull().WithMessage("{PropertyName} không được để trống");
            this.RuleFor(x => x.UserName).NotNull().WithMessage("{PropertyName} không được để trống");
            this.RuleFor(x => x.PassWord).NotNull().WithMessage("{PropertyName} không được để trống")
                .MinimumLength(8).WithMessage("{PropertyName} có độ dài tối thiểu là {MinLength}, bạn đã nhập {TotalLength}")
                .MaximumLength(20).WithMessage("{PropertyName} có độ dài tối đa là {MaxLength}, bạn đã nhập {TotalLength}");
            this.RuleFor(x => x.ConfirmPass).NotNull().WithMessage("{PropertyName} không được để trống")
                .Equal(x => x.PassWord).WithMessage("ConfirmPass phải trùng với Pass word");
        }
    }
}