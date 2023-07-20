using Entities.DTO_DataTransferObject_;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.Net.Http.Headers;
using System.Text;

namespace _01_WebApi.Utilities.Formates
{
    public class CsvOutputFormatter:TextOutputFormatter
    {
        public CsvOutputFormatter()
        {
            // MediaTypeHeaderValue mıcrosoftan coz
            SupportedMediaTypes.Add(MediaTypeHeaderValue.Parse("text/csv"));
            SupportedEncodings.Add(Encoding.UTF8);
            SupportedEncodings.Add(Encoding.Unicode);
        }
        protected override bool CanWriteType(Type? type)
        {
            if(typeof(BookDto).IsAssignableFrom(type) || typeof(IEnumerable<BookDto>).IsAssignableFrom(type)) //bu ıkısınden bırısı gelırse
            {
                return base.CanWriteType(type);
            }
            return false; //başka tur ıse cevrılmez
        }
        private static void FormatCsv(StringBuilder buffer,BookDto book)
        {
            buffer.AppendLine($"{book.Id},{book.Title},{book.Price}"); //bu alanlar bufura alındı
        }

        public override async Task WriteResponseBodyAsync(OutputFormatterWriteContext context, Encoding selectedEncoding)
        {
            var respons = context.HttpContext.Response;
            var bufer = new StringBuilder();

            if(context.Object is IEnumerable<BookDto>) //liste
            {
                foreach (var book in (IEnumerable<BookDto>)context.Object )
                {
                    FormatCsv(bufer, book);
                }
            }
            else //tekil 
            {
                FormatCsv(bufer, (BookDto)context.Object);
            }
            await respons.WriteAsync(bufer.ToString());
        }
    }
}
