using AutoMapper;
using LoginPractice.DTOs;
using LoginPractice.Entity;

namespace LoginPractice.Utilidades
{
    public class AutorMapperProfile : Profile
    {

        public AutorMapperProfile()
        {
            CreateMap<AutorCreacionDTO, Autor>();

            CreateMap<Autor, AutorCreacionDTO>();

            CreateMap<Autor, AutorDTO>();


            //libro
            CreateMap<LibroCreacionDTO, Libro>()
                    .ForMember(libro => libro.AutoresLibros, opciones => opciones.MapFrom(MapAutoresLibros));


            CreateMap<Libro, LibroDTO>();

            CreateMap<Libro, LibroDTOConAutores>()
                .ForMember(libroDTO => libroDTO.Autores, opciones => opciones.MapFrom(MapAutoresConLibros));
                ;


            //Comentarios

            CreateMap<ComentarioCreacionDTO, Comentario>();
        }

        private List<AutorDTO> MapAutoresConLibros(Libro libro, LibroDTOConAutores libroDTOConAutores)
        {
            var resultado = new List<AutorDTO>();   
            if(resultado == null)
            {
                return resultado;
            }

            foreach(var autorLibro in libro.AutoresLibros)
            {
                resultado.Add(new AutorDTO()
                {
                    Id = autorLibro.AutorId,
                    Nombre = autorLibro.Autor.Nombre
                });
            }
            return resultado;
        }

        private List<AutorLibro> MapAutoresLibros(LibroCreacionDTO libroCreacionDTO, Libro libro)
        {
            var resultado = new List<AutorLibro>();
            if (libroCreacionDTO == null)
            {
                return resultado;
            }

            foreach(var autorId in libroCreacionDTO.AutoresIds)
            {
                resultado.Add(new AutorLibro { AutorId = autorId} );
            }

            return resultado;

        }


    }
}
