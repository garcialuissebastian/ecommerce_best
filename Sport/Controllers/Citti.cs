using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Web;

namespace Sport.Controllers
{
    public class Citti
    {


        public   void Wscttiventas(string v_tipoCbate, string v_tipo, string v_valor1, string v_valor2)
        {
            try
            {

                List<Be.kx_cbtes> list = new List<Be.kx_cbtes>();
                string user = "";
                string conf = "";
                Dictionary<string, List<string>> Results;
                Results = Bll.BllBest.DameInstancia().Wscttiventas(v_tipoCbate, v_tipo, v_valor1.ToUpper(), v_valor2.ToUpper(), user, conf);

                //foreach (KeyValuePair<string, string> result in Results)
                //{
                //    Console.WriteLine(string.Format("Key-{0}:Value-{1}", result.Key, result.Value));
                //}

                string dirFullPath = System.Web.HttpContext.Current.Server.MapPath("~/");


                // Write the string array to a new file named "WriteLines.txt".


                using (StreamWriter outputFile = new StreamWriter(Path.Combine(dirFullPath, "alicutas.txt")))
                {
                    foreach (KeyValuePair<string, List<string>> result in Results)
                    {
                        //  Console.WriteLine(string.Format("Key-{0}:Value-{1}", result.Key, result.Value));
                        if (result.Key == "alic")
                        {
                            foreach (var item in result.Value)
                            {
                                outputFile.WriteLine(item);
                            }

                        }
                    }

                }



                using (StreamWriter outputFile = new StreamWriter(Path.Combine(dirFullPath, "ventas.txt")))
                {
                    foreach (KeyValuePair<string, List<string>> result in Results)
                    {
                        //  Console.WriteLine(string.Format("Key-{0}:Value-{1}", result.Key, result.Value));
                        if (result.Key == "vtas")
                        {
                            foreach (var item in result.Value)
                            {
                                outputFile.WriteLine(item);
                            }

                        }
                    }

                }






                File.Delete(Path.Combine(dirFullPath, "VENTAS.zip"));

                //provide the path and name for the zip file to create
                string zipFile = Path.Combine(dirFullPath, "VENTAS.zip");


                using (var zip = ZipFile.Open(zipFile, ZipArchiveMode.Create))
                {
                    var entry = zip.CreateEntry("alicutas.txt");
                    entry.LastWriteTime = DateTimeOffset.Now;

                    using (var stream = File.OpenRead(Path.Combine(dirFullPath, "alicutas.txt")))
                    using (var entryStream = entry.Open())
                        stream.CopyTo(entryStream);


                    var entry1 = zip.CreateEntry("ventas.txt");
                    entry1.LastWriteTime = DateTimeOffset.Now;

                    using (var stream1 = File.OpenRead(Path.Combine(dirFullPath, "ventas.txt")))
                    using (var entryStream1 = entry1.Open())
                        stream1.CopyTo(entryStream1);
                }




            }
            catch (Exception)
            {

                throw;
            }


        }

    }
}