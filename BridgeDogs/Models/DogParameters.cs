using Microsoft.AspNetCore.Mvc;

namespace BridgeDogs.Models
{
    public class DogParameters
    {
        private readonly string[] DOG_FIELDS = { "name", "color", "tail_length", "weight" };
        private readonly string[] ORDER_VALUES = { "asc", "desc" };

        private const int maxPageSize = 50;
        private int _pageSize = 10;

        [FromQuery(Name = "attribute")]
        public string Attribute { get; set; } = "name";

        [FromQuery(Name = "order")]
        public string OrderBy { get; set; } = "asc";

        public bool ValidAttributeAndOrder
        {
            get
            {
                return DOG_FIELDS.Contains(Attribute.ToLower())
                    && ORDER_VALUES.Contains(OrderBy.ToLower());
            }
        }

        [FromQuery(Name = "pageNumber")]
        public int PageNumber { get; set; } = 1;

        [FromQuery(Name = "pageSize")]
        public int PageSize
        {
            get
            {
                return _pageSize;
            }
            set
            {
                _pageSize = (value > maxPageSize) ? maxPageSize : value;
            }
        }
    }
}
