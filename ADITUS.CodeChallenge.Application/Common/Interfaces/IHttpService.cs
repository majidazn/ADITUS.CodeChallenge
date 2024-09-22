namespace ADITUS.CodeChallenge.Application.Common.Interfaces;

public interface IHttpService<in TRequest, TResponse>
  where TRequest : class
  where TResponse : class
{
  Task<TResponse> Send(TRequest request = null);
}