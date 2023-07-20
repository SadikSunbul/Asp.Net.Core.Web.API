using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Entities.ErorModel
{
    public class ErrorDetails
    {
        public int StatusCode { get; set; }
        public string? Message { get; set; } //null olabilir demek ? işareti
        public override string ToString()
        {
            return JsonSerializer.Serialize(this);
            // Yukarıdaki kod parçası, "this" ifadesini mevcut bağlamdaki sınıf nesnesini temsil eder.Bu, mevcut sınıfın özelliklerini ve değerlerini içeren bir nesneyi JSON biçimine dönüştürmek için kullanılır.Bu dönüştürme işlemi, nesne verilerini metin tabanlı bir JSON dizgesine dönüştürür ve ardından bu dizgeyi işlemek, kaydetmek veya başka bir sistemle iletişim kurmak için kullanılabilir

            /*
             {
            Message: "...",
            StatusCode: 200
             }
            gibi bir jsona döner
             */
        }
    }
}
