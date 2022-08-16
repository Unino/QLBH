using QLBH.Common.BLL;
using QLBH.Common.Req;
using QLBH.Common.Rsp;
using QLBH.DAL;
using QLBH.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace QLBH.BLL
{
    public class ProductSvc : GenericSvc<ProductRep, Product>
    {
        ProductRep req = new ProductRep();

        #region -- Overrides --

        public override SingleRsp Read(int id)
        {
            var res = new SingleRsp();

            var m = _rep.Read(id);
            res.Data = m;

            return res;
        }

        public override SingleRsp Update(Product m)
        {
            var res = new SingleRsp();

            var m1 = m.CategoryId > 0 ? _rep.Read(m.ProductId) : _rep.Read(m.ProductId);
            if (m1 == null)
            {
                res.SetError("EZ103", "No data.");
            }
            else
            {
                res = base.Update(m);
                res.Data = m;
            }

            return res;
        }
        #endregion

        #region -- Methods --

       
        public ProductSvc() { }



        public object SearchProduct(SearchProductReq searchProductReq)
        {
            var products = All.Where(x => x.ProductName.Contains(searchProductReq.Keyword));
            var offset = (searchProductReq.Page - 1) * searchProductReq.Size;
            var total = products.Count();
            int totalPage = (total % searchProductReq.Size) == 0 ? (int)(total / searchProductReq.Size) :
                (int)(1+(total / searchProductReq.Size));
            var data = products.OrderBy(x => x.ProductName).Skip(offset).Take(searchProductReq.Size).ToList() ;
            var res = new
            {
                Data = data,
                TotalRecord = total,
                TotalPages=totalPage,
                Page=searchProductReq.Page,
                Size = searchProductReq.Size
            
            };
        
            return res;
        }

        public SingleRsp CreateProduct(ProductReq productReq)
        {
            var res = new SingleRsp();
            Product product = new Product();
            product.ProductId = productReq.ProductId;
            product.ProductName = productReq.ProductName;
            product.UnitPrice = productReq.UnitPrice;
            product.UnitsInStock = productReq.UnitsInStock;
            res = req.CreateProduct(product);
            return res;
        }
        public SingleRsp UpdateProduct(ProductReq productReq)
        {
            var res = new SingleRsp();
            Product product = new Product();
            product.ProductId = productReq.ProductId;
            product.ProductName = productReq.ProductName;
            product.UnitPrice = productReq.UnitPrice;
            product.UnitsInStock = productReq.UnitsInStock;
            res = req.UpdateProduct(product);
            return res;
        }

        #endregion
    }
}
