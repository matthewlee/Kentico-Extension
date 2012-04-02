<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="CMSModules_MediaLibExtension_Default" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Custom Media Library Extension</title>
    <link rel="stylesheet" href="../resources/css/master.css" type="text/css" media="screen" />
</head>
<body class="module">
    <form id="form1" runat="server">
    <h1 class="pagetitle">
        Custom Media Library Extension
    </h1>
    <div id="wrapper">
        <p>
            Please fill in all <strong>*</strong> mandatory fields.
        </p>
        <h2>
            Register Images</h2>
        <p>
            Register all files in the image folder to the Kentico CMS. Registration will be
            updated if it exists.</p>
        <p>
            For more information about File Registration, please check the <a href="http://devnet.kentico.com/docs/5_5R2/devguide/index.html?media_libraries_uploading_files.htm"
                target="_blank">developer guide</a>.</p>
        <p class="message" id="productimagebox" runat="server" visible="false" enableviewstate="false">
            <asp:Label ID="lb_productimage" runat="server"></asp:Label>
        </p>
        <dl>
            <dt>Gallery *:</dt>
            <dd>
                <asp:DropDownList ID="ddl_productimagelib" runat="server">
                </asp:DropDownList>
            </dd>
            <dt>&nbsp;</dt>
            <dd>
                <asp:Button ID="btn_productImageSyn" Text="Synchronise" runat="server" OnClick="ProductImageSyn_Click" /></dd>
        </dl>
        <h2>
            Remove Orphan Image Registration</h2>
        <p>
            Check existing registration in a media library (Media_File table). Remove the registration
            if file does not exist.</p>
        <p class="message" id="removeorphanimage" runat="server" visible="false" enableviewstate="false">
            <asp:Label ID="lb_removeOrphanEntry" runat="server"></asp:Label>
        </p>
        <dl>
            <dt>Gallery *:</dt>
            <dd>
                <asp:DropDownList ID="ddl_productimagelib02" runat="server">
                </asp:DropDownList>
            </dd>
            <dt>&nbsp;</dt>
            <dd>
                <asp:Button ID="btn_removeorphanimage" Text="Check" runat="server" OnClick="RemoveOrphanImage_Click" /></dd>
        </dl>
    </div>
    </form>
</body>
</html>
