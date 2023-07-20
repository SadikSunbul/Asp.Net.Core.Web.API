using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.DTO_DataTransferObject_
{
    public record BookDTOForUpdate : BookDtoForManipulation
    { //record clasa benzer DTO dan bahsedıyorsak ozellıklerı onemli
        //Readonly olmalıdır immutable 
        //Lınq 
        //ref type
        //ctor(dto)
        [Required]
        public int Id { get; init; } //init olmalıdır sonra deger degısmez
        //public string Title { get; init; }
        //public decimal Price { get; init; }
    }
    // public record BookDTOForUpdate1(int id,string Title,decimal Price); //boylede kullanılır

    // [Serializable] //kapalı yazdıgımız ıcın ceviremedi o yuzden yazıldı burası xml aldık ama okunamıycak sekılde cevirdi
    //public record BookDto(int id, string Title, decimal Price); //ileride değişebilir
    //Altakı gıbı yazsa hata almaz hata lamdadıı ıcınde [Serializable] e gerek yok 
    //public record BookDto
    //{
    //    public int Id { get; init; } //init olmalıdır sonra deger degısmez
    //    public string Title { get; init; }
    //    public decimal Price { get; init; }
    //}

    //böyle yazar isek karısık yazılardan kurtulur serilazer yazmaayda gerek yoktur
    public record BookDto : BookDtoForManipulation
    {
        public int Id { get; init; }

    }

    /*
     Person person1 = new Person { FirstName = "John", LastName = "Doe", Age = 30 };
Person person2 = person1 with { Age = 31 }; // person1'in değerlerini kopyalayarak yeni bir nesne oluşturuldu.
    recorda ozel bu 
     */
}
