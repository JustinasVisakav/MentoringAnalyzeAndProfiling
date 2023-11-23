using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ProfileSample.DAL;
using ProfileSample.Models;

namespace ProfileSample.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            List<int> sources = new List<int>();
            using (var context = new ProfileSampleEntities())
            {
                sources = context.ImgSources.Take(20).Select(x => x.Id).ToList();
            }
            
            var model = new List<ImageModel>();

            using (var context = new ProfileSampleEntities())
            {
                foreach (var id in sources)
                {
                    var item = context.ImgSources.Find(id);

                    var obj = new ImageModel()
                    {
                        Name = item.Name,
                        Data = item.Data
                    };

                    model.Add(obj);
                }
            }

            return View(model);
        }

        public ActionResult Convert()
        {
            var files = Directory.GetFiles(Server.MapPath("~/Content/Img"), "*.jpg");

            using (var context = new ProfileSampleEntities())
            {
                foreach (var file in files)
                {
                    using (var stream = new FileStream(file, FileMode.Open))
                    {
                        byte[] buff = new byte[stream.Length];

                        stream.Read(buff, 0, (int) stream.Length);

                        var entity = new ImgSource()
                        {
                            Name = Path.GetFileName(file),
                            Data = buff,
                        };

                        context.ImgSources.Add(entity);
                        context.SaveChanges();
                    }
                } 
            }

            return RedirectToAction("Index");
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}