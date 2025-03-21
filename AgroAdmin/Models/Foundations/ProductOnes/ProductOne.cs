﻿// ---------------------------------------------------------------------------------- 
// Copyright (c) The Standard Organization, a coalition of the Good-Hearted Engineers 
// ----------------------------------------------------------------------------------
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace AgroAdmin.Models.Foundations.ProductOnes
{
    public class ProductOne
    {
        public int Id { get; set; }
        public string TitleUz { get; set; }
        public string TitleRu { get; set; }
        public string DesUz { get; set; }
        public string DesRu { get; set; }
        public string DescriptionUz { get; set; }
        public string DescriptionRu { get; set; }
        public string TasirModdaUz { get; set; }
        public string TasirModdaRu { get; set; }
        public string KimyoviySinfiUz { get; set; }
        public string KimyoviySinfiRu { get; set; }
        public string PreparatShakliUz { get; set; }
        public string PreparatShakliRu { get; set; }
        public string QadogiUz { get; set; }
        public string QadogiRu { get; set; }
        public string IconUrl { get; set; } 
        public string ProductPicture { get; set; }
        public string ProductOneName { get; set; }
        public ProductType ProductType { get; set; }
        public int JadvalType { get; set; }
        [JsonIgnore]
        public List<TableOne> TableOnes { get; set; }  
    }
}
