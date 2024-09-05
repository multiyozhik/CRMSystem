using CRMSystem.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CRMSystem.Api
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class ApiServicesController : ControllerBase
    {
        private readonly ServicesModel model;
        public ApiServicesController(ServicesModel servicesModel)
        {
            model = servicesModel;
        }

        [AllowAnonymous]
        [HttpGet]
        public async Task<List<Service>> ServicesList()
            => await model.GetServicesList();

        [HttpPost]
        public async Task Add(ServiceDataFromRequest serviceData)
        => await model.Add(serviceData.Name, serviceData.Description);

        [HttpGet]
        public async Task<Service> GetServiceById(Guid id)
        => await model.GetServiceById(id);

        [HttpPost]
        public async Task Update(Service service)
        => await model.Update(service);

        [HttpPost]
        public async Task Delete(Guid id)
        => await model.Delete(id);
    }
    public record ServiceDataFromRequest(string Name, string Description);
}
