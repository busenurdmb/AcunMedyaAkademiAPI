namespace AcunMedyaAkademiWebAPI.DTOs.ProductsDto
{
    public class ProductsCreateDto
    {
       
        public string ProductName { get; set; }
        public decimal Price { get; set; }
        public string ImageUrl { get; set; }


        public int CategoryId { get; set; }
    }
}
