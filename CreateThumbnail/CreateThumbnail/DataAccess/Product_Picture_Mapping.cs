namespace CreateThumbnail.DataAccess
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Product_Picture_Mapping
    {
        public int Id { get; set; }

        public int ProductId { get; set; }

        public int PictureId { get; set; }

        public int DisplayOrder { get; set; }

        public virtual Picture Picture { get; set; }

        public virtual Product Product { get; set; }
    }
}
