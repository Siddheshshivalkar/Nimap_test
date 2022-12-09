using Nimap_test.Context;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Nimap_test.Controllers
{
    public class CategoryController : Controller
    {
        //
        // GET: /Category/
        productdbEntities dbObj = new productdbEntities();
        public ActionResult Category(tbl_category obj)
        {
           
                return View(obj);
            
          
            
        }
        [HttpPost]

        public ActionResult AddCategory(tbl_category model)
        {
            if (ModelState.IsValid)
            {
                tbl_category obj = new tbl_category();
                obj.CategoryID = model.CategoryID;
                obj.CategoryName = model.CategoryName;
                if(model.CategoryID==0)
                {
                    dbObj.tbl_category.Add(obj);
                    dbObj.SaveChanges();
                }
                else
                {
                    dbObj.Entry(obj).State = EntityState.Modified;
                    dbObj.SaveChanges();

                }
               
            }
            ModelState.Clear();
            return View("Category");
        }

        public ActionResult CategoryList()
        {
            var res = dbObj.tbl_category.ToList();
            return View(res);
        }

        public ActionResult Delete(tbl_category cat)
        {
           
                var res = dbObj.tbl_category.Where(x => x.CategoryID == cat.CategoryID).First();
                dbObj.tbl_category.Remove(res);
                dbObj.SaveChanges();
                var list = dbObj.tbl_category.ToList();
                return View("CategoryList", list);
          
           
        }

	}
}