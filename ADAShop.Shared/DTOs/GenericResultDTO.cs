namespace ADAShop.Shared.DTOs
{
    public class GenericResultDTO
    {
        public bool IsSuccesfull { get; set; }
        public string? Message { get; set; }
        public List<dynamic>? ResultList { get; set; }
        public dynamic? Id { get; set; }
    }
}