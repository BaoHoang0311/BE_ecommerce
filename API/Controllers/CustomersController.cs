﻿using API.Dtos;
using API.Entites;
using API.Helpers;
using API.Repository;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomersController : ControllerBase
    {
        private ICustomerRepository _customerRepository;
        private readonly IMapper _mapper;
        public CustomersController(ICustomerRepository services, IMapper mapper)
        {
            _customerRepository = services;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetCustomers(string sortBy, int? pageNumber , int pageSize)
        {
            var dulieu = await _customerRepository.GetAllAsyncSortByIdAndPaging(sortBy, pageNumber , pageSize );
            var AllCus = await _customerRepository.GetAllAsync();
            var totalCus = AllCus.ToList().Count();
            if (dulieu == null) return NotFound();

            // return list with special
            return Ok(new
            {
                message = "GetCustomers thanh cong",
                total = totalCus,
                data = dulieu
            });
        }
        [HttpGet("/api/all-cus")]
        public async Task<IActionResult> GetCustomers()
        {
            var AllCus = await _customerRepository.GetAllAsync();

            // return list with special
            return Ok(new
            {
                message = "GetCustomers thanh cong",
                data = AllCus
            });
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetCustomesrsById(int id)
        {
            var dulieu = await _customerRepository.GetByIdAsync(id);

            if (dulieu == null) return NotFound();

            return Ok(new
            {
                message = "GetCustomesrsById thanh cong",
                data = dulieu
            });
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteCustomrer(int id)
        {
            await _customerRepository.DeleteAsync(id);

            var results = new results()
            {
                statusCode = 200,
                message = "DeleteCustomrer thanh cong",
            };

            return Ok(results);
        }
        [HttpPost]
        public async Task<IActionResult> CreateCustomrer(CustomerDtos customerdtos)
        {
            var data = await _customerRepository.GetQuery().FirstOrDefaultAsync(m => m.FullName == customerdtos.FullName);
            if(data== null)
            {
                var cus = _mapper.Map<Customer>(customerdtos);
                await _customerRepository.AddAsync(cus);
                var results = new results()
                {
                    statusCode = 200,
                    message = "CreateCustomrer thanh cong",
                };

                return Ok(results);
            }
            return BadRequest();
        }
        [HttpPut]
        //https://localhost:44381/api/Customers?id=21862dcd-b42c-4468-9b8d-f86d9f5fcc6f
        public async Task<IActionResult> UpdateCustomer(Customer customer)
        {
            var data = await _customerRepository.GetQuery().AsNoTracking().FirstOrDefaultAsync(m => m.Id == customer.Id);
            if (data != null)
            {
                await _customerRepository.UpdateAsync(customer);
                var results = new results()
                {
                    statusCode = 200,
                    message = "UpdateCustomer thanh cong",
                };
                return Ok(results);
            }
            return BadRequest();
        }
    }
}
