using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;

namespace FileUploadProject
{
    public partial class Avatar : System.Web.UI.Page
    {
        string saveFile2;
        bool Condition = true;
        CustomFileUpload load_cont;
        //protected override void OnInit(EventArgs e)
        //{


        //}
        protected void Page_Init(object sender, EventArgs e)
        {
            //PlaceHolder1.Controls.Add(new CustomFileUpload() { ID = "CustomFileUpload" });
            //base.OnPreRender(e);
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                load_cont = new CustomFileUpload();
                load_cont.Saving += new EventHandler(HandleOuter);
                load_cont.Deleting += new EventHandler(HandleOuter);
                PlaceHolder1.Controls.Add(load_cont);
                base.OnPreRender(e);
                
            }
            else
            {
              //  load_cont.Load
                load_cont = new CustomFileUpload();
                load_cont.Saving += new EventHandler(HandleOuter);
                load_cont.Deleting += new EventHandler(HandleOuter);
                PlaceHolder1.Controls.Add(load_cont);
                base.OnPreRender(e);
            }
        }
        public void HandleOuter(object obj, EventArgs e)
        {
            object[] newObj = (object[])obj;
            string FilePath = (string)newObj[0];
            string FileName = (string)newObj[1]; 
            string action = (string)newObj[2];
            FileInfo file = new FileInfo(FilePath+FileName);
            switch(action)
            {
            case "save":
                Response.Clear();
                Response.ClearHeaders();
                Response.ClearContent();
                Response.AddHeader("Content-Disposition", "attachment; filename=" + FileName);
                Response.AddHeader("Content-Length", file.Length.ToString());
                Response.ContentType = "text/plain";
                Response.Flush();
                Response.TransmitFile(file.FullName);
                Response.End();
            break;
            case "delete":
                File.Delete(FilePath + FileName);
                load_cont.FileExist = false;
                load_cont.UserFileName = null;
                break;
            }
        }

    }
}