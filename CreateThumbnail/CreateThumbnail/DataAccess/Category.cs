namespace CreateThumbnail.DataAccess
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Category")]
    public partial class Category
    {
        public int Id { get; set; }

        [Required]
        [StringLength(400)]
        public string Name { get; set; }

        public string Description { get; set; }

        public int CategoryTemplateId { get; set; }

        [StringLength(400)]
        public string MetaKeywords { get; set; }

        public string MetaDescription { get; set; }

        [StringLength(400)]
        public string MetaTitle { get; set; }

        public int ParentCategoryId { get; set; }

        public int PictureId { get; set; }

        public int PageSize { get; set; }

        public bool AllowCustomersToSelectPageSize { get; set; }

        [StringLength(200)]
        public string PageSizeOptions { get; set; }

        [StringLength(400)]
        public string PriceRanges { get; set; }

        public bool ShowOnHomePage { get; set; }

        public bool IncludeInTopMenu { get; set; }

        public bool SubjectToAcl { get; set; }

        public bool LimitedToStores { get; set; }

        public bool Published { get; set; }

        public bool Deleted { get; set; }

        public int DisplayOrder { get; set; }

        public DateTime CreatedOnUtc { get; set; }

        public DateTime UpdatedOnUtc { get; set; }
    }
}
