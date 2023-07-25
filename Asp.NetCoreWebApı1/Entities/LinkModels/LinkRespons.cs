using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.LinkModels
{
    public class LinkRespons
    {
        public bool HasLinks { get; set; }
        public List<Entity> ShapedEntites { get; set; }
        public LinkCollectionWrapper<Entity> LinkedEntities { get; set; }
        public LinkRespons() //serılıze edılcekse default ctor eklenmelıdır
        {
            ShapedEntites = new();
            LinkedEntities= new();
        }
    }
}
