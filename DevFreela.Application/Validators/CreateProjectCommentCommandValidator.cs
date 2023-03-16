using DevFreela.Application.Commands.CreateProjectComment;
using FluentValidation;

namespace DevFreela.Application.Validators;

public class CreateProjectCommentCommandValidator : AbstractValidator<CreateProjectCommentCommand>
{
    public CreateProjectCommentCommandValidator()
    {
        RuleFor(p => p.Content)
            .MaximumLength(255)
            .WithMessage("Tamanho máximo de Texto de Comentário é de 255 caracteres.");
    }
}
