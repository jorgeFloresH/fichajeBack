namespace apiServices.Data
{
    public class AgenciaPagination
    {
        private const int _maxItemsPerPage = 50;
        private int pageSize;


        public int PageNumber { get; set; } = 1;
    
        public int PageSize
        {
            get => pageSize;
            set => pageSize = value > _maxItemsPerPage ? _maxItemsPerPage : value;
        }
        public string sort { get; set; } = "null";
    }

}
