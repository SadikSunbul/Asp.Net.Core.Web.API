using AutoMapper;
using Entities.DTO_DataTransferObject_;
using Entities.Models;

namespace _01_WebApi.Utilities.Automaper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<BookDTOForUpdate, Book>(); //tek yönlü bu 
            CreateMap<Book, BookDto>();
        }
    }
}
