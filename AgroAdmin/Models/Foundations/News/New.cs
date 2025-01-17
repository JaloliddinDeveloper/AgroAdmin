using System;

namespace AgroAdmin.Models.Foundations.News
{
    public class New
    {
        public int Id { get; set; }
        public string TitleUz { get; set; }
        public string TitleRu { get; set; }
        public string DesUz { get; set; }
        public string DesRu { get; set; }
        public string DescribtionUz { get; set; }
        public string DescribtionRu { get; set; }
        public string NewPicture { get; set; }  
        public DateTimeOffset Date { get; set; }
    }
}
