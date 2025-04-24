using Newtonsoft.Json;
using System.Collections.Generic;

namespace WixSharp
{
    public class ProductWix : WixObject
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("slug")]
        public string Slug { get; set; }

        [JsonProperty("visible")]
        public bool? Visible { get; set; }

        [JsonProperty("productType")]
        public string ProductType { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("sku")]
        public string Sku { get; set; }

        [JsonProperty("weight")]
        public int? Weight { get; set; }

        [JsonProperty("weightRange")]
        public WeightRange WeightRange { get; set; }

        [JsonProperty("stock")]
        public Stock Stock { get; set; }

        [JsonProperty("price")]
        public PriceMember Price { get; set; }

        [JsonProperty("priceData")]
        public PriceMember PriceData { get; set; }

        [JsonProperty("convertedPriceData")]
        public PriceMember ConvertedPriceData { get; set; }

        [JsonProperty("priceRange")]
        public WeightRange PriceRange { get; set; }

        [JsonProperty("costAndProfitData")]
        public CostAndProfitData CostAndProfitData { get; set; }

        [JsonProperty("costRange")]
        public WeightRange CostRange { get; set; }

        [JsonProperty("pricePerUnitData")]
        public PricePerUnitData PricePerUnitData { get; set; }

        [JsonProperty("additionalInfoSections")]
        public IEnumerable<AdditionalInfoSections> AdditionalInfoSections { get; set; }

        [JsonProperty("ribbons")]
        public IEnumerable<Ribbons> Ribbons { get; set; }

        [JsonProperty("media")]
        public Media Media { get; set; }

        [JsonProperty("customTextFields")]
        public IEnumerable<CustomTextFieldForProduct> CustomTextFields { get; set; }

        [JsonProperty("manageVariants")]
        public bool ManageVariants { get; set; }

        [JsonProperty("productOptions")]
        public IEnumerable<ProductOptions> ProductOptions { get; set; }

        [JsonProperty("productPageUrl")]
        public ProductPageUrl ProductPageUrl { get; set; }

        [JsonProperty("numericId")]
        public string NumericId { get; set; }

        [JsonProperty("inventoryItemId")]
        public string InventoryItemId { get; set; }

        [JsonProperty("discount")]
        public ProductDiscount Discount { get; set; }

        [JsonProperty("collectionIds")]
        public List<string> CollectionIds { get; set; }

        [JsonProperty("variants")]
        public List<Variants> Variants { get; set; }

        [JsonProperty("lastUpdated")]
        public string LastUpdated { get; set; }

        [JsonProperty("createdDate")]
        public string CreatedDate { get; set; }

        [JsonProperty("seoData")]
        public SeoData SeoData { get; set; }

        [JsonProperty("ribbon")]
        public string Ribbon { get; set; }

        [JsonProperty("brand")]
        public string Brand { get; set; }
    }

    public class SeoData
    {
        [JsonProperty("tags")]
        public IEnumerable<Tags> Tags { get; set; }

        [JsonProperty("settings")]
        public Settings Settings { get; set; }
    }

    public class Settings
    {
        [JsonProperty("preventAutoRedirect")]
        public string PreventAutoRedirect { get; set; }
    }
    public class Tags
    {

        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("props")]
        public object Props { get; set; }

        [JsonProperty("meta")]
        public object Meta { get; set; }

        [JsonProperty("children")]
        public string Children { get; set; }

        [JsonProperty("custom")]
        public bool Custom { get; set; }

        [JsonProperty("disabled")]
        public bool Disabled { get; set; }
    }
    public class Variants
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("choices")]
        public object Choices { get; set; }

        [JsonProperty("variant")]
        public Variant Variant { get; set; }

        public VariantWithInventory VariantInventory { get; set; }
    }
    public class VariantPriceData
    {
        [JsonProperty("currency")]
        public string Currency { get; set; }

        [JsonProperty("pricePerUnit")]
        public string PricePerUnit { get; set; }

        [JsonProperty("price")]
        public string ProductPrice { get; set; }

        [JsonProperty("discountedPrice")]
        public string DiscountedPrice { get; set; }

        [JsonProperty("formatted")]
        public Formatted Formatted { get; set; }
    }
    public class Variant
    {
        [JsonProperty("priceData")]
        public VariantPriceData VariantPriceData { get; set; }

        [JsonProperty("weight")]
        public double Weight { get; set; }

        [JsonProperty("sku")]
        public string Sku { get; set; }

        [JsonProperty("visible")]
        public bool Visible { get; set; }

        [JsonProperty("convertedPriceData")]
        public ConvertedPriceData ConvertedPriceData { get; set; }
    }
    public class ConvertedPriceData
    {
        [JsonProperty("discountedPrice")]
        public string DiscountedPrice { get; set; }

        [JsonProperty("price")]
        public string Price { get; set; }

        [JsonProperty("formatted")]
        public Formatted Formatted { get; set; }

        [JsonProperty("pricePerUnit")]
        public string PricePerUnit { get; set; }

        [JsonProperty("currency")]
        public string Currency { get; set; }
    }
    public class Formatted
    {
        [JsonProperty("price")]
        public string Price { get; set; }

        [JsonProperty("discountedPrice")]
        public string DiscountedPrice { get; set; }

        [JsonProperty("pricePerUnit")]
        public string PricePerUnit { get; set; }

    }
    public partial class VariantWithInventory
    {
        [JsonProperty("variantId")]
        public string VariantId { get; set; }

        [JsonProperty("inStock")]
        public bool InStock { get; set; }

        [JsonProperty("quantity")]
        public long Quantity { get; set; }
    }
    public class ProductDiscount
    {
        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("value")]
        public double Value { get; set; }
    }
    public class ProductPageUrl
    {
        [JsonProperty("base")]
        public string Base { get; set; }

        [JsonProperty("path")]
        public string Path { get; set; }
    }
    public class Choice
    {
        [JsonProperty("inStock")]
        public bool InStock { get; set; }

        [JsonProperty("visible")]
        public bool Visible { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("value")]
        public string Value { get; set; }
        
        [JsonProperty("media")]
        public Media Media { get; set; }

    }
    public class ProductOptions
    {
        [JsonProperty("optionType")]
        public string OptionType { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("choices")]
        public IEnumerable<Choice> Choices { get; set; }
    }
    public class CustomTextFieldForProduct
    {
        [JsonProperty("title")]
        public string Title { get; set; }

        [JsonProperty("maxLength")]
        public int MaxLength { get; set; }

        [JsonProperty("mandatory")]
        public bool Mandatory { get; set; }
    }
    public class WeightRange
    {
        [JsonProperty("minValue")]
        public int MinValue { get; set; }
        [JsonProperty("maxValue")]
        public int MaxValue { get; set; }
    }

    public class Stock
    {
        [JsonProperty("trackInventory")]
        public bool TrackInventory { get; set; }

        [JsonProperty("quantity")]
        public int Quantity { get; set; }

        [JsonProperty("inStock")]
        public bool InStock { get; set; }

        [JsonProperty("inventoryStatus")]
        public string InventoryStatus { get; set; }
    }

    public class PriceMember
    {
        [JsonProperty("currency")]
        public string Currency { get; set; }

        [JsonProperty("price")]
        public int Price { get; set; }

        [JsonProperty("discountedPrice")]
        public int DiscountedPrice { get; set; }

        [JsonProperty("pricePerUnit")]
        public int PricePerUnit { get; set; }

        [JsonProperty("formatted")]
        public PriceFormatted Formatted { get; set; }
    }

    public class PriceFormatted
    {
        [JsonProperty("price")]
        public string Price { get; set; }

        [JsonProperty("discountedPrice")]
        public string DiscountedPrice { get; set; }

        [JsonProperty("pricePerUnit")]
        public string PricePerUnit { get; set; }
    }

    public class CostAndProfitData
    {
        [JsonProperty("itemCost")]
        public int ItemCost { get; set; }

        [JsonProperty("formattedItemCost")]
        public string FormattedItemCost { get; set; }

        [JsonProperty("profit")]
        public int profit { get; set; }

        [JsonProperty("formattedProfit")]
        public string FormattedProfit { get; set; }

        [JsonProperty("profitMargin")]
        public int ProfitMargin { get; set; }
    }

    public class PricePerUnitData
    {
        [JsonProperty("totalQuantity")]
        public int TotalQuantity { get; set; }

        [JsonProperty("totalMeasurementUnit")]
        public string TotalMeasurementUnit { get; set; }

        [JsonProperty("baseQuantity")]
        public int BaseQuantity { get; set; }

        [JsonProperty("baseMeasurementUnit")]
        public string BaseMeasurementUnit { get; set; }
    }
    public class AdditionalInfoSections
    {
        [JsonProperty("title")]
        public string Title { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }
    }

    public class Ribbons
    {
        [JsonProperty("text")]
        public string Text { get; set; }
    }

    public class Media
    {
        [JsonProperty("mainMedia")]
        public MainMedia MainMedia { get; set; }

        [JsonProperty("items")]
        public IEnumerable<MainMedia> Items { get; set; }
    }

    public class MainMedia
    {
        [JsonProperty("mediaType")]
        public string MediaType { get; set; }

        [JsonProperty("image")]
        public ImageMedia Image { get; set; }

        [JsonProperty("thumbnail")]
        public ThumbnailMedia Thumbnail { get; set; }

        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("video")]
        public VideoMedia Video { get; set; }

        [JsonProperty("title")]
        public string Title { get; set; }
    }

    public class ThumbnailMedia
    {
        [JsonProperty("url")]
        public string Url { get; set; }

        [JsonProperty("width")]
        public int Width { get; set; }

        [JsonProperty("height")]
        public int Height { get; set; }
    }

    public class ImageMedia
    {
        [JsonProperty("url")]
        public string Url { get; set; }

        [JsonProperty("width")]
        public int Width { get; set; }

        [JsonProperty("height")]
        public int Height { get; set; }
    }

    public class VideoMedia
    {
        [JsonProperty("files")]
        public IEnumerable<ImageMedia> Files { get; set; }
    }
}
