﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using QLBH.BLL;
using QLBH.Common.Req;
using QLBH.Common.Rsp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QLBH.Web.Controllers
{
    
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly CategorySvc categorySvc;
        public CategoryController()
        {
            categorySvc = new CategorySvc();
        }
        [HttpPost("get-by-id")]
        public IActionResult getCategoryById([FromBody] SimpleReq req)
        {
            var res = new SingleRsp();
            res = categorySvc.Read(req.Id);
            return Ok(res);
        }
        [HttpPost("get-all")]
        public IActionResult getAllCategories()
        {
            var res = new SingleRsp();
            res.Data = categorySvc.All;
            return Ok(res);
        }
    }
}
