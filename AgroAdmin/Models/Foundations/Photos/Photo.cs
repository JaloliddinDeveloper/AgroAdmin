// ---------------------------------------------------------------------------------- 
// Copyright (c) The Standard Organization, a coalition of the Good-Hearted Engineers 
// ----------------------------------------------------------------------------------
namespace AgroAdmin.Models.Foundations.Photos
{
    public class Photo
    {
        public int Id { get; set; }
        public string NameUz { get; set; }
        public string NameRu { get; set; }
        public string PictureUrl { get; set; }
        public DateTimeOffset CreateDate { get; set; }
    }
}
