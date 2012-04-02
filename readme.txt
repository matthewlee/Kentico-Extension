Author: VML Development Team
Date: 23/03/2012
Version: v5.5.4311 R2
-------------------------------------------------------
Installation instruction
-------------------------------------------------------

1. Copy 2 folders (Bin and CMSModules) to your Kentico folder
Note: The dll was compiled with ASP.NET Framework 4.0 and Kentico v.5.5.4311.

2. Add a new Module 
	i. Go to CMSSiteManager
	ii. Go to Development tab and Modules section
	iii. Edit Tools modules and go to the User interface section
	iv. Click on the "New element" link and enter below info.
			- Display name: "Media library extension"
			- Code name: "MediaLibraryExtension"
			- Caption: "Media Lib Extension"
			- Target Url: "~/CMSModules/CustomExtension/MediaLibrary/Default.aspx"
			- Icon Path: "~/CMSModules/CustomExtension/Resources/images/medialibicon16x16.png"
			
3. Go to CMSDesk -> Tools, you should see the new "Media Lib Extension" module

---------------------------------------------------------
For any other versions
---------------------------------------------------------

1. Includes VML.KenticoExtensionLib in your project

2. Update the Reference to the kentico dll file and compile

3. Add the new dll in your project 

4. Copy CMSModules folder to your Kentico folder

5. Add a new Module 
	i. Go to CMSSiteManager
	ii. Go to Development tab and Modules section
	iii. Edit Tools modules and go to the User interface section
	iv. Click on the "New element" link and enter below info.
			- Display name: "Media library extension"
			- Code name: "MediaLibraryExtension"
			- Caption: "Media Lib Extension"
			- Target Url: "~/CMSModules/CustomExtension/MediaLibrary/Default.aspx"
			- Icon Path: "~/CMSModules/CustomExtension/Resources/images/medialibicon16x16.png"
			
6. Go to CMSDesk -> Tools, you should see the new "Media Lib Extension" module


Note: this code has NOT been tested on any other version of Kentico CMS.