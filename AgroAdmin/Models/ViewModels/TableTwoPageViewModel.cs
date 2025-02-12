using AgroAdmin.Models.Foundations.ProductTwos;

namespace AgroAdmin.Models.ViewModels
{
    public class TableTwoPageViewModel
    {
        public int ProuctTwoId { get; set; }
        public string ProductTwoName { get; set; }
        public IEnumerable<TableTwo> TableTwoes { get; set; }
    }
}
