using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using CMS.CMSHelper;
using CMS.UIControls;
using CMS.TreeEngine;
using CMS.SiteProvider;
using CMS.MediaLibrary;
using TreeNode = CMS.TreeEngine.TreeNode;

using VML.KenticoExtensionLib.MediaLibrary;

public partial class CMSModules_MediaLibExtension_Default : CMSDeskPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack) 
        {
            // Set up Media Library
            DataSet ds_lib = MediaLibraryInfoProvider.GetMediaLibraries(string.Format("LibrarySiteID = {0}", CMSContext.CurrentSiteID), "");
            
            if (ds_lib != null) 
            {
                ddl_productimagelib.DataSource = ds_lib.Tables[0];
                ddl_productimagelib.DataTextField = "LibraryDisplayName";
                ddl_productimagelib.DataValueField = "LibraryID";
                ddl_productimagelib.DataBind();

                ddl_productimagelib02.DataSource = ds_lib.Tables[0];
                ddl_productimagelib02.DataTextField = "LibraryDisplayName";
                ddl_productimagelib02.DataValueField = "LibraryID";
                ddl_productimagelib02.DataBind();
            }
        }
    }

    protected void ProductImageSyn_Click(object sender, EventArgs e) 
    {
        try
        {
            //define ids
            int libId = Convert.ToInt32(ddl_productimagelib.SelectedItem.Value);
            string folderpath = MediaLibraryInfoProvider.GetMediaLibraryFolderPath(libId);

            //import image entries
            KenticoMediaManager mediaManager = new KenticoMediaManager(folderpath, libId, CMSContext.CurrentUser.UserID, CMSContext.CurrentSite.SiteID, CMSContext.CurrentSite.SiteName);
            mediaManager.RemoveOrphanEntry(libId);
            mediaManager.Import(true, "\\"); 

            //display success message
            productimagebox.Visible = true;
            productimagebox.Attributes.Add("class", "success");

            lb_productimage.Text = "Great! The <em>" + ddl_productimagelib.SelectedItem.Text + "</em> folder has been imported successfully.";
        }
        catch (Exception ex)
        {
            //display failure message
            productimagebox.Attributes.Add("class", "fail");
            productimagebox.Visible = true;
            lb_productimage.Text = ex.Message;     
        }
    }

    protected void RemoveOrphanImage_Click(object sender, EventArgs e) 
    {
        try
        {
            //define ids
            int libId = Convert.ToInt32(ddl_productimagelib02.SelectedItem.Value);
            string folderpath = MediaLibraryInfoProvider.GetMediaLibraryFolderPath(libId);

            //remove all entries in the library before import
            KenticoMediaManager mediaManager = new KenticoMediaManager(folderpath, libId, CMSContext.CurrentUser.UserID, CMSContext.CurrentSite.SiteID, CMSContext.CurrentSite.SiteName);
            int count = mediaManager.RemoveOrphanEntry(libId);

            //display success message
            removeorphanimage.Visible = true;
            removeorphanimage.Attributes.Add("class", "success");

            if(count > 0)
                lb_removeOrphanEntry.Text = "Great! we clear <em>" + count + "</em> entries in the <em>" + ddl_productimagelib02.SelectedItem.Text + "</em> folder.";
            else
                lb_removeOrphanEntry.Text = "Excellent! no orphan entry is found.";
        }
        catch(Exception ex)
        { 
            //display failure message
            removeorphanimage.Attributes.Add("class", "fail");
            removeorphanimage.Visible = true;
            lb_removeOrphanEntry.Text = ex.Message;     
        }
    }
}