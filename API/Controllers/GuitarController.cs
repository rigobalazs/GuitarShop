using Data.DbContext;
using Data.Dto;
using Data.Models;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace API.Controllers
{
    [Route("api/Guitar")]
    [ApiController]
    public class GuitarController : ControllerBase
    {
        private readonly ApplicationDbContext _db;
        private ApiResponse _response;
        public GuitarController(ApplicationDbContext db)
        {
            _db = db;
            _response = new ApiResponse();
        }

        [HttpGet]
        public async Task<IActionResult> GetGuitars()
        {
            _response.Result = _db.Guitars;
            _response.StatusCode = HttpStatusCode.OK;
            return Ok(_response);
        }

        [HttpGet("{id:int}", Name = "GetGuitar")]
        public async Task<IActionResult> GetGuitar(int id)
        {
            if (id == 0)
            {
                _response.StatusCode = HttpStatusCode.BadRequest;
                _response.IsSuccess = false;
                return BadRequest(_response);
            }
            Guitar guitarItem = _db.Guitars.FirstOrDefault(u => u.Id == id);
            if (guitarItem == null)
            {
                _response.StatusCode = HttpStatusCode.NotFound;
                _response.IsSuccess = false;
                return NotFound(_response);
            }
            _response.Result = guitarItem;
            _response.StatusCode = HttpStatusCode.OK;
            return Ok(_response);
        }

        [HttpPost]
        public async Task<ActionResult<ApiResponse>> CreateGuitar([FromForm] GuitarCreateDto guitarCreateDto)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    Guitar guitarItemToCreate = new()
                    {
                        Name = guitarCreateDto.Name,
                        Price = guitarCreateDto.Price,
                        Category = guitarCreateDto.Category,
                        Description = guitarCreateDto.Description,
                    };
                    _db.Guitars.Add(guitarItemToCreate);
                    _db.SaveChanges();
                    _response.Result = guitarItemToCreate;
                    _response.StatusCode = HttpStatusCode.Created;
                    return CreatedAtRoute("GetGuitar", new { id = guitarItemToCreate.Id }, _response);

                }
                else
                {
                    _response.IsSuccess = false;
                }
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages
                     = new List<string>() { ex.ToString() };
            }

            return _response;
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult<ApiResponse>> UpdateGuitar(int id, [FromForm] GuitarUpdateDto guitarUpdateDto)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (guitarUpdateDto == null || id != guitarUpdateDto.Id)
                    {
                        _response.StatusCode = HttpStatusCode.BadRequest;
                        _response.IsSuccess = false;
                        return BadRequest();
                    }

                    Guitar guitarItemFromDb = await _db.Guitars.FindAsync(id);
                    if (guitarItemFromDb == null)
                    {
                        _response.StatusCode = HttpStatusCode.BadRequest;
                        _response.IsSuccess = false;
                        return BadRequest();
                    }

                    guitarItemFromDb.Name = guitarUpdateDto.Name;
                    guitarItemFromDb.Price = guitarUpdateDto.Price;
                    guitarItemFromDb.Category = guitarUpdateDto.Category;
                    guitarItemFromDb.Description = guitarUpdateDto.Description;

                    _db.Guitars.Update(guitarItemFromDb);
                    _db.SaveChanges();
                    _response.StatusCode = HttpStatusCode.NoContent;
                    return Ok(_response);
                }
                else
                {
                    _response.IsSuccess = false;
                }
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages
                     = new List<string>() { ex.ToString() };
            }

            return _response;
        }


        [HttpDelete("{id:int}")]
        public async Task<ActionResult<ApiResponse>> DeleteGuitar(int id)
        {
            try
            {
                if (id == 0)
                {
                    _response.StatusCode = HttpStatusCode.BadRequest;
                    _response.IsSuccess = false;
                    return BadRequest();
                }

                Guitar guitarItemFromDb = await _db.Guitars.FindAsync(id);
                if (guitarItemFromDb == null)
                {
                    _response.StatusCode = HttpStatusCode.BadRequest;
                    _response.IsSuccess = false;
                    return BadRequest();
                }

                _db.Guitars.Remove(guitarItemFromDb);
                _db.SaveChanges();
                _response.StatusCode = HttpStatusCode.NoContent;
                return Ok(_response);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages
                     = new List<string>() { ex.ToString() };
            }

            return _response;
        }
    }
}
