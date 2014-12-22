using System;
using System.Web;
using System.IO;
using System.Web.UI.WebControls;
using System.Drawing;
using System.Web;
using System.Web.UI;
using System.Threading;
namespace FileUploadProject
{
    public class ImageHandler : IHttpHandler
    {
        
        #region IHttpHandler Members

        public bool IsReusable
        {
            // Верните значение false в том случае, если ваш управляемый обработчик не может быть повторно использован для другого запроса.
            // Обычно значение false соответствует случаю, когда некоторые данные о состоянии сохранены по запросу.
            get { return false; }
        }

        public void ProcessRequest(HttpContext context)
        {
            string FilePath = "D:\\temp\\uploads\\";
            string ParamValue = Path.GetFileName(context.Request.AppRelativeCurrentExecutionFilePath);
            System.Drawing.Image image = System.Drawing.Image.FromFile(FilePath + ParamValue);
            System.Drawing.Image thumb = image.GetThumbnailImage(80, 150, () => false, IntPtr.Zero);
            thumb.Save(Path.ChangeExtension(FilePath + "tumb", "thumb"));
            
           // context.Response.ContentType = "text/image";
           //context.Response.WriteFile(FilePath + ParamValue);
            using (var ms = new MemoryStream())
            {
                thumb.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
                ms.Close();
                context.Response.BinaryWrite(ms.ToArray());
                thumb.Dispose();
                image.Dispose();
                context.Response.End();
            }
            
             
                 
            
           
        }

        #endregion
    }
}
