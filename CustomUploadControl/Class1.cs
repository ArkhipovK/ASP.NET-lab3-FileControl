using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI.WebControls;
using System.Drawing;
using System.Web;
using System.Web.UI;
using System.Threading;
using System.IO;

namespace FileUploadControl
{
    public class CustomFileUpload : WebControl
    {
        bool FileExist = false;
        string MyFilePath = "D:\\temp\\uploads\\";
        string FileName;
        string UserFileName;
        FileUpload FUl;
        Button Upload;
        System.Web.UI.WebControls.Image ImageControl;
        Button Save;
        Button Delete;
        public event EventHandler Uploading;
        public CustomFileUpload()
        {

            FileExist = false;
            FileUpload FUl = new FileUpload();
            FUl.ID = "FileUpload";
            Button Upload = new Button();
            Upload.ID = "UploadButton";
            Upload.Text = "Upload";
            Upload.Click += new EventHandler(HandleUploading);
            ImageControl = new System.Web.UI.WebControls.Image();
            ImageControl.ImageUrl = "images" + @"/" + UserFileName;
            Save = new Button();
            Save.ID = "Save";
            Save.Text = "Скачать";
            Delete = new Button();
            Delete.ID = "Delete";
            Delete.Text = "Удалить";
        }
        protected override void CreateChildControls()
        {
            Controls.Clear();
            if (!FileExist)
            {
                this.Controls.Add(FUl);
                this.Controls.Add(Upload);
            }
            else
            {
                this.Controls.Add(ImageControl);
                this.Controls.Add(Save);
                this.Controls.Add(Delete);
            }


            base.CreateChildControls();
        }

        protected override void OnInit(System.EventArgs e)
        {
            base.OnInit(e);
            Page.RegisterRequiresControlState(this);
        }
        protected override object SaveControlState()
        {
            //object baseSate = base.SaveControlState();
            object baseState = base.SaveControlState();

            //create an array to hold the base control’s state 
            //and this control’s state.
            object thisState = new object[] { baseState, this.FileExist, this.FileName, this.UserFileName, this.MyFilePath };
            return thisState;
        }
        protected override void LoadControlState(object savedState)
        {
            object[] stateLastRequest = (object[])savedState;

            //Grab the state for the base class 
            //and give it to it.
            object baseState = stateLastRequest[0];
            base.LoadControlState(baseState);

            //Now load this control’s state.
            FileExist = (bool)stateLastRequest[1];
            FileName = (string)stateLastRequest[2];
            UserFileName = (string)stateLastRequest[3];
            MyFilePath = (string)stateLastRequest[4];

        }
        protected void HandleUploading(object obj, EventArgs e)
        {
            UserFileName = FUl.FileName;
            string ext = Path.GetExtension(UserFileName);
            Random rnd = new Random();
            Char[] pwdChars = new Char[36] { 'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i', 'j', 'k', 'l', 'm', 'n', 'o', 'p', 'q', 'r', 's', 't', 'u', 'v', 'w', 'x', 'y', 'z', '0', '1', '2', '3', '4', '5', '6', '7', '8', '9' };
            for (int i = 0; i < 20; i++)
            {
                Thread.Sleep(3);
                FileName += pwdChars[rnd.Next(0, 35)];
            }
            FileName += ext;
            string pathToCheck = MyFilePath + FileName;
            //string tempfileName = "";
            FUl.SaveAs(pathToCheck);

            FileExist = true;

        }
    }
}