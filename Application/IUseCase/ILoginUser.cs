namespace Application.IUseCase;

public interface ILoginUser
{
    Task<string?> Execute(LoginRequestDto request);
    
}