using System;
using System.IO;
using System.IO.Compression;
using Microsoft.AspNetCore.Mvc;
using Volo.Abp.AspNetCore.Mvc;

namespace FREETIME.Controllers
{
    // public class HomeController : AbpController
    // {
    //     public ActionResult Index()
    //     {
    //         return Redirect("~/swagger");
    //     }
    // }

    public class AngularHomeController : Controller
    {
        public ActionResult Index()
        {
            return File("index.html", "text/html");
        }

        public class All
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public All()
            {
                var r = new Random();
                Name = r.Next().ToString();
            }
        }

        [Route("/app/GetAll")]
        [HttpGet]
        public All GetAll()
        {
            return new All();
        }
        [Route("/downlog/{key}")]
        [HttpGet]
        public ActionResult DownloadLog([FromRoute] string key)
        {
            // TODO replace this code with public private key exchange code infrastructure 
            if (key == "50f84daf3a6dfd6a9f20c9f8ef428942")
            {
                using (var streamZip = new MemoryStream())
                {
                    var di = new DirectoryInfo("./Logs");
                    var logFiles = di.EnumerateFiles();
                    using (var zip = new ZipArchive(streamZip, ZipArchiveMode.Create, true))

                        foreach (var file in logFiles)
                        {
                            using (var stream = new FileStream(file.FullName, FileMode.Open, FileAccess.Read,
                                FileShare.Delete | FileShare.ReadWrite))
                            {
                                var zipArchiveEntry = zip.CreateEntry(Path.GetFileName(file.FullName),
                                    CompressionLevel.Optimal);
                                using (var destination1 = zipArchiveEntry.Open())
                                {
                                    stream.CopyTo(destination1);
                                }
                            }
                        }
                    streamZip.Flush();
                    streamZip.Seek(0, SeekOrigin.Begin);
                    streamZip.Dispose();
                    return File(streamZip.ToArray(), "application/zip", "log" + DateTime.Now.ToString("yyyy-MM-dd--hh-mm") + ".zip");
                }
            }
            else
            {
                return Unauthorized();
            }
        }
    }
}
