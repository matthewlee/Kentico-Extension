using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Data;
using System.Web;

using CMS.SettingsProvider;
using CMS.MediaLibrary;
using CMS.CMSHelper;
using CMS.TreeEngine;
using TreeNode = CMS.TreeEngine.TreeNode;

namespace VML.KenticoExtensionLib.MediaLibrary
{
    public class KenticoMediaManager
    {
        //absolute folder path
        private string _image_root_folder = null;
        private string _sitename = null;
        private int _libraryId = -1;
        private int _siteId = -1;
        private int _userid = -1;

        public KenticoMediaManager(string imagefolder, int libraryId, int userId, int siteId, string sitename)
        {
            _image_root_folder = imagefolder;
            _libraryId = libraryId;
            _siteId = siteId;
            _userid = userId;
            _sitename = sitename;
        }

        /*
         * Import all files in the image folder to the Kentico CMS Media library.
         */
        public void Import(bool forceUpdate, string folder)
        {
            try
            {
                //Import files in current folder
                string imagefolderpath = _image_root_folder + folder;
                FileInfo[] fileInfos = (new DirectoryInfo(imagefolderpath)).GetFiles();

                foreach (FileInfo fileInfo in fileInfos)
                {
                    //Check if file exist　
                    string relativefilepath = folder + fileInfo.Name;
                    MediaFileInfo existinginfo = MediaFileInfoProvider.GetMediaFileInfo(_libraryId, relativefilepath.TrimStart('\\'));

                    if (existinginfo == null)
                    {
                        //Import file
                        MediaFileInfo mediafileinfo = new MediaFileInfo(_image_root_folder + relativefilepath, _libraryId, "");
                        mediafileinfo.FilePath = relativefilepath.TrimStart('\\').Replace('\\', '/');
                        mediafileinfo.FileMimeType = string.Format("image/{0}", fileInfo.Extension.TrimStart('.'));
                        mediafileinfo.FileSiteID = _siteId;
                        MediaFileInfoProvider.ImportMediaFileInfo(mediafileinfo, _userid);
                    }
                    else
                    {
                        //Update existing file entry
                        if (forceUpdate) MediaFileInfoProvider.UpdateFilesPath(existinginfo.FilePath, relativefilepath, _libraryId);
                    }
                }

                //Import subfolders
                DirectoryInfo[] dirInfos = (new DirectoryInfo(imagefolderpath)).GetDirectories();

                foreach (DirectoryInfo dirInfo in dirInfos) 
                {
                    //import subfolder but exclude system folder i.e. __thumbnails
                    if (!dirInfo.Name.StartsWith("__")) Import(forceUpdate, folder + dirInfo.Name + "\\");     
                }
            }
            catch (Exception ex)
            {
                throw new Exception(string.Format("Media Library Error : Import - {0}", ex.Message));
            }
        }

        /* 
         * Check existing entries in the Kentico CMS. 
         * If file is not found according to the file path in the system, the entry will be removed.
         */
        public int RemoveOrphanEntry(int libraryId)
        {
            try 
            {
                int count = 0;

                DataSet ds = MediaFileInfoProvider.GetMediaFiles("FileLibraryID=" + libraryId, "");
                if (ds != null && ds.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow dr in ds.Tables[0].Rows)
                    {
                        MediaFileInfo info = new MediaFileInfo(dr);
                        string filefullpath = _image_root_folder + "\\" + info.FilePath.Replace('/', '\\');

                        if (!File.Exists(filefullpath))
                        {
                            info.Delete();
                            count++;
                        }
                    }
                }

                return count;
            }
            catch (Exception ex) 
            {
                throw new Exception(string.Format("Media Library Error : Remove Orphan Entry - {0}", ex.Message)); 
            }
        }
    }
}