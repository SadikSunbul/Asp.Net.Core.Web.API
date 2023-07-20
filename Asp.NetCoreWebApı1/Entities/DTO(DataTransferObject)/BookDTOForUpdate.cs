using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.DTO_DataTransferObject_
{
    public record BookDTOForUpdate
    { //record clasa benzer DTO dan bahsedıyorsak ozellıklerı onemli
        //Readonly olmalıdır immutable 
        //Lınq 
        //ref type
        //ctor(dto)
        public int Id { get; init; } //init olmalıdır sonra deger degısmez
        public string Title { get; init; }
        public decimal Price { get; init; }
    }
   // public record BookDTOForUpdate1(int id,string Title,decimal Price); //boylede kullanılır
}
