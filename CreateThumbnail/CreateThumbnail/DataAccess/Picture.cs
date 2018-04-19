namespace CreateThumbnail.DataAccess
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Picture")]
    public partial class Picture
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Picture()
        {
            Product_Picture_Mapping = new HashSet<Product_Picture_Mapping>();
        }

        public int Id { get; set; }

        public byte[] PictureBinary { get; set; }

        [Required]
        [StringLength(40)]
        public string MimeType { get; set; }

        [StringLength(300)]
        public string SeoFilename { get; set; }

        public string AltAttribute { get; set; }

        public string TitleAttribute { get; set; }

        public bool IsNew { get; set; }

        public string SizeThumbs { get; set; }

        [StringLength(40)]
        public string UpdatedToTicks { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Product_Picture_Mapping> Product_Picture_Mapping { get; set; }
    }
}
