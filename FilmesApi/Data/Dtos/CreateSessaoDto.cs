using System.ComponentModel.DataAnnotations;

namespace FilmesApi.Data.Dtos
{
  public class CreateSessaoDto
  {
    public int filmeId { get; set; }
    public int CinemaId { get; set; }
  }
}