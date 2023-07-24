using Services.Contrant;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class DataShaper<T> : IDataShaper<T> where T : class
    {
        //ExpandoObject  : Run tımede dınamık olarak uretıgımız herhangıbır nesneye karsılık gelir
        public PropertyInfo[] Properties { get; set; } //propertlerı calısma zamanında yakalma
        public DataShaper()
        {
            Properties = typeof(T)
                .GetProperties(BindingFlags.Public | BindingFlags.Instance); // T tıpının propertylerı gelsın   Instance --> newlenerek elde edılenler
        }
        public IEnumerable<ExpandoObject> ShapeData(IEnumerable<T> entities, string fieldString)
        {
            var requerıdFields = GetRequiredProperties(fieldString);
            return FeatchData(entities, requerıdFields);
        }

        public ExpandoObject ShapeData(T entitie, string fieldString)
        {
            var requeridProperty = GetRequiredProperties(fieldString);
            return FetchDataForEntity(entitie, requeridProperty);
        }

        private IEnumerable<PropertyInfo> GetRequiredProperties(string fieldString)
        {
            var requeredFields = new List<PropertyInfo>();
            if (!string.IsNullOrWhiteSpace(fieldString))
            {
                var fields = fieldString.Split(',', StringSplitOptions.RemoveEmptyEntries); //hem ayır , le gore hemde bos olan verılerı alma dedık
                foreach (var field in fields)
                {
                    var property = Properties.
                        FirstOrDefault(p => p.Name.Equals(field.Trim(), StringComparison.InvariantCultureIgnoreCase));
                    if (property is null)
                    {
                        continue;
                    }
                    requeredFields.Add(property);
                }
            }
            else
            { //şekilleme ıstemıyor 
                requeredFields = Properties.ToList();//tum elemanları verdık 
            }
            return requeredFields;
        }

        //key valu seklınde yaptık 
        private ExpandoObject FetchDataForEntity(T entity, IEnumerable<PropertyInfo> requeridProperties)
        {
            var shapedObject = new ExpandoObject(); //runtımede uretılcek 
            foreach (var property in requeridProperties)
            {
                var objectPropertyValue = property.GetValue(entity); //ıd nın degerı kac ıse vb
                shapedObject.TryAdd(property.Name, objectPropertyValue);
            }
            return shapedObject;
        }

        private IEnumerable<ExpandoObject> FeatchData(IEnumerable<T> entites, IEnumerable<PropertyInfo> requeridProperty)
        {
            var shapedData = new List<ExpandoObject>();
            foreach (var entiti in entites) //kac nesne var sa ona gore seklendırılcek 
            {
                var shapedObject = FetchDataForEntity(entiti, requeridProperty); //tek tek nesnelerı parcalayıp alta eklıycez yanı Product ıcın ıd name gelsın ama order ıcın createdate gelsın gıbı 
                shapedData.Add(shapedObject);
            }
            return shapedData;
        }
    }
}
