<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Avatar.aspx.cs" Inherits="FileUploadProject.Avatar" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
     <link rel="stylesheet" type="text/css"  href="StyleSheet.css" />
    <title>Контрол для загрузки файлов</title>
</head>
<body>
    <div class="container">
        <header>
				<h1>Лабораторная работа ASP.NET WebForms 
                    <span>Контрол для загрузки файлов</span>
				</h1>
		</header>
    <div class="main">
        <div class ="fileupload">
        <form id="form1" runat="server">    
            <asp:PlaceHolder ID="PlaceHolder1" runat="server"></asp:PlaceHolder>
        </form>
        </div>
    </div>
     </div>
</body>
</html>
