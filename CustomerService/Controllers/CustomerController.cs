using AutoMapper;
using CustomerService.Dtos;
using CustomerService.Model;
using CustomerService.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace CustomerService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly ICustomerRepo _repo;
        private readonly IMapper _mapper; 
        private readonly string db = Path.Combine(Environment.CurrentDirectory, "States");


        public CustomerController(ICustomerRepo repo, IMapper mapper )
        {
            _repo = repo;
            _mapper = mapper;
        }

        [HttpGet]
        public ActionResult<IEnumerable<CustomerReadDto>> GetCustomers()
        {
            Console.WriteLine("---> Getting Customers.....");
            var customerDetails = _repo.GetAllCustomers();
            return Ok(_mapper.Map<IEnumerable<CustomerReadDto>>(customerDetails));
        }

        [HttpGet("{id}", Name = "GetCustomerById")]
        public ActionResult<CustomerReadDto> GetCustomerById(int id)
        {
            var customerDetails = _repo.GetCustomerById(id);
            if (customerDetails != null)
            {
                return Ok(_mapper.Map<CustomerReadDto>(customerDetails));
            }
            return NotFound();

        }

        

        [HttpPost]
        public async Task<ActionResult<CustomerReadDto>> AddCustomer(CustomerCreateDto customerCreateDto)
        {
            var customerModel = _mapper.Map<Customer>(customerCreateDto);

            OTPService.OTPServiceExtensions(customerModel.PhoneNumber);

            var readText = await System.IO.File.ReadAllTextAsync(db + "nigeria-state-and-lgas.json");
            var serializer = new JsonSerializer();
            using var stringReader = new StringReader(readText);
            using (var jsonReader = new JsonTextReader(stringReader))




            _repo.CreateCustomer(customerModel);
            _repo.SaveChanges();

            var customerReadDto = _mapper.Map<CustomerReadDto>(customerModel);

            return CreatedAtRoute(nameof(GetCustomerById), new {id = customerReadDto.Id}, customerReadDto);
           
        }
    }
}
