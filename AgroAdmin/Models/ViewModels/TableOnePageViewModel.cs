using AgroAdmin.Models.Foundations.ProductOnes;

namespace AgroAdmin.Models.ViewModels
{
    public class TableOnePageViewModel
    {
        public int ProuctOneId { get; set; }
        public string ProductOnename { get; set; }
        public IEnumerable<TableOne> TableOnes { get; set; }
    }
}
