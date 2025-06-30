namespace Application.IUseCase;

public interface IRegisterUser
{
    Task<bool> Execute(RegisterRequestDto request);
}