using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ToDoApp.AppServices.Dtos;
using ToDoApp.AppServices.Interfaces;
using ToDoApp.Extensions;
using ToDoApp.Validators;

namespace ToDoApp.Controllers
{
    [Route("api/[controller]")]
    public class TodoController : Controller
    {
        private readonly ITodoAppService appService;
        private readonly TodoValidator validator;

        public TodoController(ITodoAppService appService, TodoValidator validator)
        {
            this.appService = appService;
            this.validator = validator;
        }
        // GET: api/todo
        [HttpGet]
        public Results.GenericResult<IEnumerable<TodoDto>> Get([FromQuery]TodoFilterDto filter)
        {
            var result = new Results.GenericResult<IEnumerable<TodoDto>>();

            try
            {
                result.Result = appService.List(filter);
                result.Success = true;
            }
            catch (Exception ex)
            {
                result.Errors = new string[] { ex.Message };
            }

            return result;
        }

        // GET api/todo/5
        [HttpGet("{id}")]
        public Results.GenericResult<TodoDto> Get(int id)
        {
            var result = new Results.GenericResult<TodoDto>();

            try
            {
                result.Result = appService.GetById(id);
                result.Success = true;
            }
            catch (Exception ex)
            {
                result.Errors = new string[] { ex.Message };
            }

            return result;
        }

        // POST api/todo
        [HttpPost]
        public Results.GenericResult<TodoDto> Post([FromBody]TodoDto model)
        {
            var result = new Results.GenericResult<TodoDto>();

            var validatorResult = validator.Validate(model);
            if (validatorResult.IsValid)
            {
                try
                {
                    result.Result = appService.Create(model);
                    result.Success = true;
                }
                catch (Exception ex)
                {
                    result.Errors = new string[] { ex.Message };
                }
            }
            else
                result.Errors = validatorResult.GetErrors();


            return result;
        }

        // PUT api/todo/5
        [HttpPut("{id}")]
        public Results.GenericResult Put(int id, [FromBody]TodoDto model)
        {
            var result = new Results.GenericResult();

            var validatorResult = validator.Validate(model);
            if (validatorResult.IsValid)
            {
                try
                {
                    result.Success = appService.Update(model);
                    if (!result.Success)
                        throw new Exception($"Todo {id} can't be updated");
                }
                catch (Exception ex)
                {
                    result.Errors = new string[] { ex.Message };
                }
            }
            else
                result.Errors = validatorResult.GetErrors();


            return result;

        }

        // DELETE api/todo/5
        [HttpDelete("{id}")]
        public Results.GenericResult Delete(int id)
        {
            var result = new Results.GenericResult();

            try
            {
                result.Success = appService.Delete(id);
                if (!result.Success)
                    throw new Exception($"Todo {id} can't be deleted");
            }
            catch (Exception ex)
            {
                result.Errors = new string[] { ex.Message };
            }

            return result;
        }
    }
}
