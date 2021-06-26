using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.IO;
using System.Net;
using System.Web;
using Gma.QrCodeNet.Encoding;
using Gma.QrCodeNet.Encoding.Windows.Render;
using BarcodeNETWorkShop;
using System.Configuration;
using System.ComponentModel;

namespace Sport.Models
{
    public class GenBarra
    {
        public static string GenerarAFIP(string v_BarCode)
        {
            // Retrieve the parameters from the QueryString
            var codeParams = BarCodeDescriptor.InitAfip(v_BarCode);

            // Encode the content
            if (codeParams == null)
            {

            }

            // Render the Barcpde as an image (Optional Save with path and name)
            using (var ms = new MemoryStream())
            {

                if (codeParams.Barcode == "INT2OF5")
                    codeParams.GenerarBarCodeINT2OF5(ms);
                else codeParams.GenerarBarCodeCODE39(ms);


                string salid = "data:image/png;base64," + Convert.ToBase64String(ms.GetBuffer());

                return Convert.ToBase64String(ms.GetBuffer());

                //if (codeParams.Name != "no" && codeParams.Path != "no")
                //{
                //    System.Drawing.Image returnImage = System.Drawing.Image.FromStream(ms);
                //    returnImage.Save(codeParams.Path + codeParams.Name + ".png");
                //}
            }
        }
        public static string Generar(string v_BarCode)
        {
            // Retrieve the parameters from the QueryString
            var codeParams = BarCodeDescriptor.Init(v_BarCode);

            // Encode the content
            if (codeParams == null)
            {

            }

            // Render the Barcpde as an image (Optional Save with path and name)
            using (var ms = new MemoryStream())
            {

                if (codeParams.Barcode == "INT2OF5")
                    codeParams.GenerarBarCodeINT2OF5(ms);
                else codeParams.GenerarBarCodeCODE39(ms);


                string salid = "data:image/png;base64," + Convert.ToBase64String(ms.GetBuffer());

                return Convert.ToBase64String(ms.GetBuffer());

                //if (codeParams.Name != "no" && codeParams.Path != "no")
                //{
                //    System.Drawing.Image returnImage = System.Drawing.Image.FromStream(ms);
                //    returnImage.Save(codeParams.Path + codeParams.Name + ".png");
                //}
            }
        }
        internal class BarCodeDescriptor
        {
            public string Barcode;
            public string Content;
            public string ContentType;
            public string Formato;
            public string Name;
            public string Path;

            /// <summary>
            /// Parse QueryString that define the QR code properties
            /// </summary>
            /// <param name="request">HttpRequest containing HTTP GET data</param>
            /// <returns>A QR code descriptor object</returns>
            public static BarCodeDescriptor Init(string t)
            {
                var cp = new BarCodeDescriptor();
                // Type Barcode
                cp.Barcode = "INT2OF5";    //request.QueryString["b"];

                // Code content to encode
                cp.Content = t;

                // Format Code
                cp.Formato = "0|38|180|95";//request.QueryString["f"];

                // name to encode
                cp.Name = "no"; //request.QueryString["n"];

                // path 
                cp.Path = "no";//request.QueryString["p"];

                cp.ContentType = "image/png";
                return cp;
            }
            public static BarCodeDescriptor InitAfip(string t)
            {
                var cp = new BarCodeDescriptor();
                // Type Barcode
                cp.Barcode = "INT2OF5";    //request.QueryString["b"];

                // Code content to encode
                cp.Content = t;

                // Format Code
                cp.Formato = "0|38|400|95";//request.QueryString["f"];

                // name to encode
                cp.Name = "no"; //request.QueryString["n"];

                // path 
                cp.Path = "no";//request.QueryString["p"];

                cp.ContentType = "image/png";
                return cp;
            }
            public void GenerarBarCodeINT2OF5(MemoryStream ms)
            {
                try
                {
                    string newToken = Guid.NewGuid().ToString();

                    BarcodeNETWindows barcodeNETWindows1 = new BarcodeNETWindows();
                    barcodeNETWindows1.BarcodeType = (BARCODE_TYPE)TypeDescriptor.GetConverter(
                            typeof(BARCODE_TYPE)).ConvertFromString("INT2OF5");

                    // hay que cambiar el path web.xml  

                    barcodeNETWindows1.BarcodeText = Content;
                    barcodeNETWindows1.SaveToFile(
                       ConfigurationManager.AppSettings.Get("tempBarCode").ToString() +
                       newToken + ".png", FILE_FORMAT.PNG);

                    crup(ms, newToken, Formato);
                }
                catch (Exception)
                {

                    throw;
                }
               
            }


            public void GenerarBarCodeCODE39(MemoryStream ms)
            {
                string newToken = Guid.NewGuid().ToString();

                BarcodeNETWindows barcodeNETWindows1 = new BarcodeNETWindows();
                barcodeNETWindows1.BarcodeType = (BARCODE_TYPE)TypeDescriptor.GetConverter(
                        typeof(BARCODE_TYPE)).ConvertFromString("CODE39");

                barcodeNETWindows1.BarcodeText = Content;
                barcodeNETWindows1.SaveToFile(

                    ConfigurationManager.AppSettings.Get("tempBarCode").ToString() +
                    newToken + ".png", FILE_FORMAT.PNG);
               
                crup(ms, newToken, Formato);

            }


            private void crup(MemoryStream ms, string token, string formato)
            {

                string[] coordenadas =
                    formato.ToString().Split(new char[] { '|' });

                int topX = int.Parse(coordenadas[0]);
                int topY = int.Parse(coordenadas[1]);
                int bottomX = int.Parse(coordenadas[2]);
                int bottomY = int.Parse(coordenadas[3]);

                Bitmap bitMap = BitmapManipulator.CropBitmap(new Bitmap(
                    ConfigurationManager.AppSettings.Get("tempBarCode").ToString() + token + ".png"),
                    new Rectangle(topX, topY, bottomX - topX, bottomY - topY));

                MemoryStream memoryStream = new MemoryStream();
                bitMap.Save(ms, System.Drawing.Imaging.ImageFormat.Png);

            }
        }
    }

}