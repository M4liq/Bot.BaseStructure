using System.Threading.Tasks;
using Infrastructure.Dtos;
using PuppeteerSharp;

namespace Application.Interfaces;

public interface IBrowserService
{ 
    Task<ResultDto<IPage>> Launch();
    Task<ResultDto<bool>>  Close();
}