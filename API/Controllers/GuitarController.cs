using Services.Core.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Domain.Models;

namespace API.Controllers
{
    [Route("api/Guitar")]
    [ApiController]
    public class GuitarController : ControllerBase
    {
        private readonly IGuitarService _guitarService;
        public GuitarController(IGuitarService guitarService)
        {
            _guitarService = guitarService;
        }

        [HttpGet]
        public async Task<IActionResult> GetGuitars()
        {
            var response = new ApiResponse();
            try
            {
                var guitars = await _guitarService.GetGuitars();
                response.IsSuccess = true;
                response.Result = guitars;
                return Ok(response);

            } catch (Exception ex)
            {
                response.IsSuccess = false;
                return BadRequest(response);
            }
        }

        [HttpGet("{id:int}", Name = "GetGuitar")]
        public async Task<IActionResult> GetGuitar(int id)
        {
            var response = new ApiResponse();
            if (id == 0)
            {
                response.IsSuccess = false;
                return BadRequest(response);
            }

            Guitar guitarItem = await _guitarService.GetGuitar(id);
            if (guitarItem == null)
            {
                response.IsSuccess = false;
                return NotFound(response);
            }
            response.Result = guitarItem;

            return Ok(response);
        }

        [HttpPost]
        public async Task<IActionResult> CreateGuitar([FromForm] Guitar guitar)
        {
            var response = new ApiResponse();
            try
            {
                if (ModelState.IsValid)
                {
                    await _guitarService.CreateGuitar(guitar);
                    response.Result = guitar;
                    return CreatedAtRoute("GetGuitar", new { id = guitar.Id }, response);
                }
                else
                {
                    response.IsSuccess = false;
                    return BadRequest(response);
                }
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.ErrorMessages = new List<string>() { ex.ToString() };
                return BadRequest(response);
            }

        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> UpdateGuitar(int id, [FromForm] Guitar guitar)
        {
            var response = new ApiResponse();
            try
            {
                if (ModelState.IsValid)
                {
                    if (guitar == null || id != guitar.Id)
                    {
                        response.IsSuccess = false;
                        return BadRequest();
                    }

                    await _guitarService.UpdateGuitar(id, guitar);
                    return Ok(response);
                }
                else
                {
                    response.IsSuccess = false;
                    return BadRequest(response);
                }
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.ErrorMessages = new List<string>() { ex.ToString() };
                return BadRequest(response);
            }
        }


        [HttpDelete("{id:int}")]
        public async Task<ActionResult> DeleteGuitar(int id)
        {
            var response = new ApiResponse();
            try
            {
                await _guitarService.DeleteGuitar(id);
                response.IsSuccess = false;
                return Ok(response);
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.ErrorMessages = new List<string>() { ex.ToString() };
                return BadRequest(response);
            }

        }
    }
}
