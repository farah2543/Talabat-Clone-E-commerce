using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.OrderEntities
{
    public class ProductInOrderItem
    {
        private int id;
        private string name;

        public ProductInOrderItem(int id, string name, string pictureUrl)
        {
            this.id = id;
            this.name = name;
            PictureUrl = pictureUrl;
        }

        public int ProductId { get; set; }

        public string ProductName { get; set; }
        public string PictureUrl { get; set; }
    }
}
