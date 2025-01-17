using AgroAdmin.Models.Foundations.ProductTwos;
using System.Collections.Generic;

namespace AgroAdmin.Models.Orchestrations.ProductTwos
{
    public class ProductTwoTableTwo
    {
        public ProductTwo ProductTwo { get; set; }
        public List<TableTwo> TableTwo { get; set; }
    }
}
