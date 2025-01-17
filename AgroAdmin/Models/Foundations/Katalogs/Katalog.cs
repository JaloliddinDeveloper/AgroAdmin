// ---------------------------------------------------------------------------------- 
// Copyright (c) The Standard Organization, a coalition of the Good-Hearted Engineers 
// ----------------------------------------------------------------------------------
using System;

namespace AgroAdmin.Models.Foundations.Katalogs
{
    public class Katalog
    {
        public int Id { get; set; }          
        public string FileName { get; set; } 
        public string FilePicture { get; set; }
        public string FilePath { get; set; } 
        public long FileSize { get; set; }  
    }
}
