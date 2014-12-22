
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

namespace FileUploadProject
{
    public class CustomFileUpload : WebControl
    {
        public bool FileExist ;
        public string MyFilePath = "D:\\temp\\uploads\\";
        public string FileName;
        public string UserFileName;
        public FileUpload full;
        public Button Upload;
        public System.Web.UI.WebControls.Image ImageControl;
        public Button Save;
        public Button Delete;
        public event EventHandler Saving;
        public event EventHandler Deleting;
        
        public CustomFileUpload()
        {
            
               FileExist = false;               
               full = new FileUpload();
               full.ID = "FileUpload";
               Upload = new Button();
                Upload.ID = "UploadButton";
                Upload.Text = "Upload";
                Upload.Click += new EventHandler(HandleUploading);           
                ImageControl = new System.Web.UI.WebControls.Image();
                ImageControl.ID = "Image"; 
                ImageControl.ImageUrl = "images"+@"/" + UserFileName;                
                Save = new Button();
                Save.ID = "Save";
                Save.Text = "Скачать";
                Save.Click += new EventHandler(HandleSaving);
                Delete = new Button();
                Delete.Click += new EventHandler(HandleDeleting);
                Delete.ID = "Delete";
                Delete.Text = "Удалить";
               
                    this.Controls.Add(full);
                    this.Controls.Add(Upload);
                
                
                    this.Controls.Add(ImageControl);
                    this.Controls.Add(Save);
                    this.Controls.Add(Delete);
                
        }
        protected override void CreateChildControls()
        {
            Controls.Clear();
            if (!FileExist)
            {
                this.Controls.Add(full);
                this.Controls.Add(Upload);
            }
            else {
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
            object thisState = new object[] { baseState, this.FileExist,this.FileName,this.UserFileName,this.MyFilePath};
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
        protected  void HandleUploading(object obj, EventArgs e)
        {
            
            //if (this.Uploading != null)
            //{
            //    object cont = (object)this.full;
            //    object thisState = new object[] { this, cont };
            //    this.Uploading(cont, e);
            //}
            UserFileName = full.FileName;
            //string ext = Path.GetExtension(UserFileName);
            //Random rnd = new Random();
            //Char[] pwdChars = new Char[36] { 'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i', 'j', 'k', 'l', 'm', 'n', 'o', 'p', 'q', 'r', 's', 't', 'u', 'v', 'w', 'x', 'y', 'z', '0', '1', '2', '3', '4', '5', '6', '7', '8', '9' };
            //for (int i = 0; i < 20; i++)
            //{
            //    Thread.Sleep(3);
            //    FileName += pwdChars[rnd.Next(0, 35)];
            //}
            //FileName += ext;
            string pathToCheck = MyFilePath + UserFileName;
            ImageControl.ImageUrl = "images" + @"/" + UserFileName;   
            //string tempfileName = "";
            full.SaveAs(pathToCheck);

            FileExist = true;
        }
        protected void HandleSaving(object obj, EventArgs e)
        {
            string save = "save";
            if (this.Saving != null)
            {
                object cont = (object)this.full;
                object thisState = new object[] { MyFilePath, UserFileName, save};
                this.Saving(thisState, e);
            }
            
        }
        protected void HandleDeleting(object obj, EventArgs e)
        {
            string delete = "delete";
            if (this.Saving != null)
            {
                object cont = (object)this.full;
                object thisState = new object[] { MyFilePath, UserFileName, delete };
                this.Saving(thisState, e);
            }

        }
    }
}
