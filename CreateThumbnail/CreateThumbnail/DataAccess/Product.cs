namespace CreateThumbnail.DataAccess
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Product")]
    public partial class Product
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Product()
        {
            Product_Picture_Mapping = new HashSet<Product_Picture_Mapping>();
        }

        public int Id { get; set; }

        public int ProductTypeId { get; set; }

        public int ParentGroupedProductId { get; set; }

        public bool VisibleIndividually { get; set; }

        [Required]
        [StringLength(400)]
        public string Name { get; set; }

        public string ShortDescription { get; set; }

        public string FullDescription { get; set; }

        public string AdminComment { get; set; }

        public int ProductTemplateId { get; set; }

        public int VendorId { get; set; }

        public bool ShowOnHomePage { get; set; }

        [StringLength(400)]
        public string MetaKeywords { get; set; }

        public string MetaDescription { get; set; }

        [StringLength(400)]
        public string MetaTitle { get; set; }

        public bool AllowCustomerReviews { get; set; }

        public int ApprovedRatingSum { get; set; }

        public int NotApprovedRatingSum { get; set; }

        public int ApprovedTotalReviews { get; set; }

        public int NotApprovedTotalReviews { get; set; }

        public bool SubjectToAcl { get; set; }

        public bool LimitedToStores { get; set; }

        [StringLength(400)]
        public string Sku { get; set; }

        [StringLength(400)]
        public string ManufacturerPartNumber { get; set; }

        [StringLength(400)]
        public string Gtin { get; set; }

        public bool IsGiftCard { get; set; }

        public int GiftCardTypeId { get; set; }

        public bool RequireOtherProducts { get; set; }

        [StringLength(1000)]
        public string RequiredProductIds { get; set; }

        public bool AutomaticallyAddRequiredProducts { get; set; }

        public bool IsDownload { get; set; }

        public int DownloadId { get; set; }

        public bool UnlimitedDownloads { get; set; }

        public int MaxNumberOfDownloads { get; set; }

        public int? DownloadExpirationDays { get; set; }

        public int DownloadActivationTypeId { get; set; }

        public bool HasSampleDownload { get; set; }

        public int SampleDownloadId { get; set; }

        public bool HasUserAgreement { get; set; }

        public string UserAgreementText { get; set; }

        public bool IsRecurring { get; set; }

        public int RecurringCycleLength { get; set; }

        public int RecurringCyclePeriodId { get; set; }

        public int RecurringTotalCycles { get; set; }

        public bool IsRental { get; set; }

        public int RentalPriceLength { get; set; }

        public int RentalPricePeriodId { get; set; }

        public bool IsShipEnabled { get; set; }

        public bool IsFreeShipping { get; set; }

        public bool ShipSeparately { get; set; }

        public decimal AdditionalShippingCharge { get; set; }

        public int DeliveryDateId { get; set; }

        public bool IsTaxExempt { get; set; }

        public int TaxCategoryId { get; set; }

        public bool IsTelecommunicationsOrBroadcastingOrElectronicServices { get; set; }

        public int ManageInventoryMethodId { get; set; }

        public bool UseMultipleWarehouses { get; set; }

        public int WarehouseId { get; set; }

        public int StockQuantity { get; set; }

        public bool DisplayStockAvailability { get; set; }

        public bool DisplayStockQuantity { get; set; }

        public int MinStockQuantity { get; set; }

        public int LowStockActivityId { get; set; }

        public int NotifyAdminForQuantityBelow { get; set; }

        public int BackorderModeId { get; set; }

        public bool AllowBackInStockSubscriptions { get; set; }

        public int OrderMinimumQuantity { get; set; }

        public int OrderMaximumQuantity { get; set; }

        [StringLength(1000)]
        public string AllowedQuantities { get; set; }

        public bool AllowAddingOnlyExistingAttributeCombinations { get; set; }

        public bool DisableBuyButton { get; set; }

        public bool DisableWishlistButton { get; set; }

        public bool AvailableForPreOrder { get; set; }

        public DateTime? PreOrderAvailabilityStartDateTimeUtc { get; set; }

        public bool CallForPrice { get; set; }

        public decimal Price { get; set; }

        public decimal OldPrice { get; set; }

        public decimal ProductCost { get; set; }

        public bool CustomerEntersPrice { get; set; }

        public decimal MinimumCustomerEnteredPrice { get; set; }

        public decimal MaximumCustomerEnteredPrice { get; set; }

        public bool BasepriceEnabled { get; set; }

        public decimal BasepriceAmount { get; set; }

        public int BasepriceUnitId { get; set; }

        public decimal BasepriceBaseAmount { get; set; }

        public int BasepriceBaseUnitId { get; set; }

        public bool HasTierPrices { get; set; }

        public bool HasDiscountsApplied { get; set; }

        public decimal Weight { get; set; }

        public decimal Length { get; set; }

        public decimal Width { get; set; }

        public decimal Height { get; set; }

        public DateTime? AvailableStartDateTimeUtc { get; set; }

        public DateTime? AvailableEndDateTimeUtc { get; set; }

        public int DisplayOrder { get; set; }

        public bool Published { get; set; }

        public bool Deleted { get; set; }

        public DateTime CreatedOnUtc { get; set; }

        public DateTime UpdatedOnUtc { get; set; }

        public decimal? OverriddenGiftCardAmount { get; set; }

        public bool MarkAsNew { get; set; }

        public DateTime? MarkAsNewStartDateTimeUtc { get; set; }

        public DateTime? MarkAsNewEndDateTimeUtc { get; set; }

        public bool NotReturnable { get; set; }

        public int ProductAvailabilityRangeId { get; set; }

        [StringLength(255)]
        public string ProductSourceId { get; set; }

        [StringLength(1000)]
        public string ProductSourceLink { get; set; }

        public int? SourceId { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Product_Picture_Mapping> Product_Picture_Mapping { get; set; }
    }
}
