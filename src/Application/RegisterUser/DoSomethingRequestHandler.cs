using System.Threading;
using System.Threading.Tasks;
using Application.Interfaces;
using MediatR;

namespace Application.Some
{
    public class RegisterUserRequestHandler : IRequestHandler<DoSomethingRequest>
    {
        private readonly IBrowserService _browserService;

        public RegisterUserRequestHandler(IBrowserService browserService)
        {
            _browserService = browserService;
        }

        public async Task<Unit> Handle(DoSomethingRequest request, CancellationToken cancellationToken)
        {
            var result = await _browserService.Launch();

            var page = result.Data;

            await page.GoToAsync("https://www.google.com/");
            
            await page.WaitForTimeoutAsync(1000);
            await _browserService.Close();
            
            return Unit.Value;
        }
    }
}