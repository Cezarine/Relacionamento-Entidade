using AutoMapper;
using FilmesAPI.Data;
using FilmesAPI.DTOs;
using FilmesAPI.Models;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace FilmesAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class MovieController : ControllerBase
    {
        private MovieContext _context;
        private IMapper _mapper;

        public MovieController(MovieContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
    
        /// <summary>
        /// Adiciona um filme ao banco de dados
        /// </summary>
        /// <param name="MovieDTO"> Objeto com os campos necessários para criação de um filme</param>
        /// <returns>IActionResult</returns>
        /// <response code="201">Caso inserção seja feita com sucesso</response>
        [HttpPost]
        public IActionResult CreateMovie([FromBody] CreateMovieDTO MovieDTO)
        {
            MovieVM movie = _mapper.Map<MovieVM>(MovieDTO);
            _context.Movies.Add(movie);
            _context.SaveChanges();
            return CreatedAtAction(nameof(GetMovieFromID), new { id = movie.ID }, movie); //Esse "id" é referente ao Modelo, então tem que ser igual, ignorando letras maiusculas e minusculas
        }

        [HttpGet]
        public IEnumerable<ReadMovieDTO> GetAllMovies([FromQuery] int skip = 0, [FromQuery] int take = 50)
        {
            return _mapper.Map<List<ReadMovieDTO>>(_context.Movies.Skip(skip).Take(take));
        }

        [HttpGet("{id}")]
        public IActionResult GetMovieFromID(int id )
        {
            var filme = _context.Movies.FirstOrDefault(Movie => Movie.ID == id);
            if (filme == null)
                return NotFound();

            var movioDTO = _mapper.Map<ReadMovieDTO>(filme);
            return Ok(movioDTO);
        }

        [HttpPut("{id}")]
        public IActionResult UpdateMovie(int id, [FromBody] UpdateMovieDTO MovieDTO)
        {
            var filme = _context.Movies.FirstOrDefault(Movie => Movie.ID == id);
            if(filme == null)
                return NotFound();

            _mapper.Map(MovieDTO, filme);
            _context.SaveChanges();
            return NoContent();
        }

        [HttpPatch("{id}")]
        public IActionResult UpdateMovie(int id, [FromBody] JsonPatchDocument<UpdateMovieDTO> patch)
        {
            var filme = _context.Movies.FirstOrDefault(Movie => Movie.ID == id);
            if (filme == null)
                return NotFound();
            
            var filmeAtualizar = _mapper.Map<UpdateMovieDTO>(filme);
            patch.ApplyTo(filmeAtualizar, ModelState);
            if (!TryValidateModel(filmeAtualizar))
                return ValidationProblem(ModelState);


            _mapper.Map(filmeAtualizar, filme);
            _context.SaveChanges();
            return NoContent();

        }

        [HttpDelete("{id}")]
        public IActionResult DeleteMovie(int id)
        {
            var filme = _context.Movies.FirstOrDefault(Movie => Movie.ID == id);
            if (filme == null)
                return NotFound();
            _context.Movies.Remove(filme);
            _context.SaveChanges();
            return NoContent();
        }

    }
}

