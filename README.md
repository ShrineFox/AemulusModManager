# AemulusModManager
## Introduction
The wait is finally over!  No longer will you have to manually merge conflicting bin files found in different mods.  This is the latest and greatest mod package manager, made specifically for Persona 4 Golden on PC.

## How to Use
[Make sure you've set up Reloaded-II and the P4G mod loader first and foremost.](https://gamebanana.com/tuts/13379)

### Pointing to the the mods Folder
After unzipping the download, just double-click AemulusPackageManager.exe to launch the program.

The first thing you'll want to do is click the Config button on the top left.  From there click the Select Output Folder button on top.  Select the mods folder found in your Persona 4 Golden Steam game directory.  This is where your merged mod loadout will be output.

If you're a super duper nerd and want to build your loadout in some other folder, Aemulus will allow it, but be careful! Whatever location you choose will have all of its contents deleted when you click Build.

The image below is what picking the mods folder should look like.

# <img src="https://i.imgur.com/cAnt58c.png">

### Unpacking Vanilla bin Files

This feature, introduced in Aemulus 1.2, unpacks data00004.pac locally on your system. This way, Aemulus can grab the unchanged assets in files like init_free.bin immediately, which saves a lot of time when building and downloading mods.

You only need to do this once (unless P4G updates data00004.pac in the future).  If you download another update for Aemulus Package Manager, you can just move the contents of the Original folder to the new update's one without having to unpack again.

Open the Config menu and click Unpack data00004.pac, then select your P4G game directory if the prompt comes up (it will be named either Persona 4 Golden or Day). You'll find the unpacked files for Aemulus (just under 300MB) in your Original folder.

### Adding Packages
Once you've set up Aemulus, drop your mods/packages into the Packages folder found in the same folder as AemulusPackageManager.exe.

# <img src="https://i.imgur.com/63zWgb5.png">

Once you click Refresh or relaunch the program, you'll see all of your packages in the middle of the Aemulus window.

As of version 1.3, you can now click the New button on the top right to create a directory along with metadata and a preview.  The directory will pop up when you click confirm and you can drop the contents of the mod inside.

### Setting Up Your Loadout/Package Priority
Next, you'll need to set up your package loadout. Packages are disabled by default, so enable the ones you want by checking the box to the left of each package.

You can drag and drop mods to move them up and down in order of priority. A higher priority mod has its files merged later, meaning it will overwrite more packages and fewer packages will overwrite it.

Remember, any P4G mod will work with Aemulus, but the mod creator has to provide a mods.aem file for bin merging to be supported. Without that file, a package with a bin file will overwrite the file completely, so it's recommended that you put non-Aemulus mods at the bottom of your loadout.

### Final Step - Merging and Building Your Loadout
Please note that Aemulus will completely erase the previous contents of your output folder when creating a loadout. Back up your current folder if you aren't sure about the changes you're making, and make sure not to use a location like Desktop for your output.

Finally, to merge all supported bin files and build your loadout (as well as patch tables if you have it enabled), just click the Build button at the top.  The console at the bottom will print what Aemulus manager is currently doing. 

Don't worry if it seems like the console is stuck on "unpacking" something. Some files take longer than others to unpack.

A window will pop up once everything is complete. Congratulations, you're all done!
Now when you run P4G through Reloaded-II, the game will utilize your brand new loadout.

## How Bin Merging Works
### The mods.aem File
Aemulus now supports loose file merging in data00004, but this section may still be useful for mods that edit bin files in other folders or if you want to add Aemulus support to a legacy mod.

In order to support merging bin files, each mod/package that edits the bin file needs a mods.aem file in its folder.  This is just a text file with a changed extension that you can open with Notepad or any other text editor.

Inside is a list of all the files that the package edits. Follow these instructions when typing out the file paths:
One file path per line.
In the path, make sure to take out any .bin, .arc, .pak, and .spr extensions (for example, "init_free.bin" becomes "init_free"). 
Make sure to use '\' and not '/' between directory levels. 
If the file being addressed is a Texture within a .spr file, give it the .tmx extension.

You can find these exact paths using a tool like [Amicitia](https://github.com/TGEnigma/Amicitia/releases).  For example, SeaGuardian's Bearable Fast Forward mod's mods.aem would look like this:

<img src="https://i.imgur.com/rxmHvbw.png">

And here's an example what the path looks like in Amicitia:

<img src="https://i.imgur.com/NhacV7i.png">

Notice that instead of data00004\init_free.bin\init\camp.arc\event_skip.spr\sankaku, the file path is is data00004\init_free\init\camp\event_skip\sankaku.tmx, which follows all of the rules above.

Do note that mods.aem files are unnecessary for packages that don't edit bin files.  Also note that if a bin file doesn't have a path indicated inside mods.aem, it will overwrite the entire bin instead of merging.

As of v1.2, mods.aem is no longer required to merge bin files, although it is still supported for everyone who has converted.  You can now just include the loose files in the same folder paths that you listed in the mods.aem file and it'll merge them over the Original bin files you unpacked on setup.

### The Actual Merging Process
If you're curious how the program actually works, I'll run you through it here.

Creates a list of enabled packages in the order indicated in the UI.
Deletes the current mod loadout found in your Steam game directory.
Goes through each packages' contents and copies and overwrites all contents (excluding mods.aem and .tblpatch files) it over to the mods directory you had to select.
If there's a conflict with a bin file, it unpacks the entire bin and refers to mods.aem to copy over the loose files to the mods directory, then deletes the unpacked files.
Merges all the loose files with the bins then deletes all the loose files.

## How Table Patching Works
New Feature Added in v1.1!

Table patching is now an option that can be enabled/disabled in the checkbox found in the config menu.  This feature was carried over from Inaba Exe Patcher (formerly known as Aemulus Patcher/Exe Patcher).  It takes .tblpatch files from the top layer of your Package folders to modify .tbl files found in data00004/init_free.bin.  

### Structure of .tblpatch Files (for modders)

First three bytes of the .tblpatch file is the tbl id, or 3 letters that indicate which tbl file it is.
- SKILL.TBL - SKL
- UNIT.TBL - UNT
- MSG.TBL - MSG
- PERSONA.TBL - PSA
- ENCOUNT.TBL - ENC
- EFFECT.TBL - EFF
- MODEL.TBL - MDL
- AICALC.TBL - AIC

<img src="https://i.imgur.com/69l2DEW.png">
In this example, we can see that MSG is the tbl id.  So this .tblpatch will be editing MSG.TBL.

Next 8 bytes indicates the offset at which you want to start overwriting the hex.  This will also be in hex rather than decimal.  The offsets will be found in the individual .TBL file you identified in the first three bytes.  You can use a decimal to hex converter like [this one](https://www.rapidtables.com/convert/number/decimal-to-hex.html) if you only know the decimal offset.

<img src="https://i.imgur.com/9AqwrPw.png">
Here we can see that the offset is 00 00 00 00 00 00 02 50 (or 592 in decimal).

Finally, the rest of the bytes will be used to overwrite the hex starting at the specified contents.

<img src="https://i.imgur.com/yoCYwAJ.png">
In this case we'll just be using 57 65 65 62 00 00 00 00 (Weeb 00 00 00 00) to overwrite the hex found at the previously stated offset of 592 in MSG.TBL.

I recommend for you to use [010 Editor](https://www.sweetscape.com/010editor/) and [these templates](https://github.com/TGEnigma/010-Editor-Templates) if you want to mess with .tbl files to create .tblpatch's.  Do note that my examples are really small to easily fit in this description but you can overwrite as much bytes as you want so that you don't need to create too many .tblpatch files.  You can also utilize T-Pose Ratkechi's [Aemulus TBL Patcher](https://gamebanana.com/tools/6876), to easily convert your edited tbl's to .tblpatch's.

### The Actual Table Patching Process
Extracts all .tbl files from data00004/init_free.bin.
Goes through each package folder from the bottom up (again after the merging process) and applies each tblpatch file it finds to the .tbl file it specifies.
Repacks the edited .tbl files into init_free.bin.
Deletes the temporary extracted/edited tbl files.

## NEW - Metadata
As of version 1.3, metadata has been added to provide more info for each package.  Along with name, you can now display author, version, link, description, and even a thumbnail.

### Package.xml
Below is an example of the contents of a Package.xml for one of mods, Persona 5 Overhaul:
```
<?xml version="1.0"?>
<Metadata xmlns:xsd="http://www.w3.org/2001/XMLSchema" xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance">
  <name>Persona 5 UI Overhaul</name>
  <id>tekka.persona5uioverhaul</id>
  <author>Tekka</author>
  <version>2.19</version>
  <link>https://gamebanana.com/gamefiles/12278</link>
  <description>This mod tries to replace every single aspect of the game that I can in a Persona 5 style.</description>
</Metadata>
```
- Name is displayed in the package grid and can be different from the folder name
- ID is a unique identifier for the package, it's unused for now but there are plans to check versioning based on this same ID
- Author is the creator of the mod/package also displayed on the package grid
- Version is the version of the mod/package displayed on the package grid.  Try to keep it in x.x format for future update compatibility. (Can go up to x.x.x.x)
- Link is the url to the mod/package page and is displayed as a clickable hyperlink on the grid.  If the domain is gamebanana.com, it'll show up as GameBanana, ShrineFox for shrinefox.com, Nexus for nexusmods.com, and Other for anything else
- Description is to describe the package in the box under the thumbnail.  It could be however long you want and include newlines and tabs if you so desire.

### Creating/Editing Metadata
There are multiple ways to create/edit Package.xml files:

If you place a mod folder in the Packages directory and refresh, it will automatically create a 
Package.xml with the name as the name of the folder with the rest of the metadata blank.  You can 
then edit the Name, Author, and Version by double clicking on their cells in the Package grid.  You
can modify all parts of the metadata by right clicking the row and selecting Edit Metadata.  A window
with the current metadata will pop up and you can edit whatever you like here.

I also added a New button on the top right that brings up a window for you to type in the metadata 
and have the Package.xml be created for you.  When you type in the name and author it autofills a
suggested ID but you can feel free to change it if you want.  Also included in the window is a file
selector for the preview.  Choose a png that you want to display as the preview when using this
option.  When you create the package, it'll create and open a folder with the mod name and version as well as the Package.xml and preview chosen named as Preview.png inside.  You can now put the contents of the mod/package inside this folder to be used in your Aemulus loadout and/or to distribute.

## Compatibility with P4G Music Manager
Since Aemulus Package Manager deletes the entire mods directory everytime you rebuild, it also deletes the mods/SND folder which [P4G Music Manager](https://gamebanana.com/tools/6835) utilizes.  To add compatibility I added a checkbox in the Config menu to Empty SND Folder.  By default, it leaves the SND folder in tact.  Enabling it will delete the SND Folder.

## Launching the Game from the Manager
A new QoL feature added in v1.2 is the Launch button.  This is used to be able to launch your modded game straight from the package manager after building your loadout.  You can setup the paths for this to work in the config menu.  Under P4G Launch Shortcut click browse to select P4G.exe and Reloaded-II.exe in their proper spots.  Once you picked valid exe's, the Launch button on the main window will now start the game for you.

## For Mod Creators - Aemulus Logo Overlay
If you'd like to include an Aemulus overlay in your mod thumbnails to indicate that it supports Aemulus (which just means having a named folder and a mods.aem if necessary), you can use the file aemulus_overlay.png included in the download.  Thanks to Pixelguin for designing it.

The overlay looks best on thumbnails that are 1920 x 1080.

## FAQ
### What makes a mod Aemulus Compatible?
All mods are compatible with Aemulus, some just might need a simple directory change.  The contents of the directory should be in the root folder of the Package.  The folders located in the root directory may include: data00000, data00001, data00002, data00003, data00004, data00005, data00006, movie00000, movie00001, movie00002, SND, and patches.

Also check to see if some of the modded files are located in your unpacked Original folder.  If so, extract the loose files or create a mods.aem to make them mergeable.  Otherwise, it'll just overwrite rather than merge them.

### What about mods that are made for Mod Compendium?
These mods just need to have a simple directory change to make them work in Aemulus.  You can tell that a mod was prepackaged for Mod Compendium if the structure if the root folder is as follows:
```
Data
Mod.xml
prebuild.bat
```
Simply relocate the contents in the Data folder up to the root folder.  Then delete the empty Data folder, Mod.xml, and prebuild.bat.

### Why is my antivirus is acting up?
For some reason the latest update triggered some of my testers' antivirus programs.  Simply make AemulusPackageManager.exe an exception in order to use it.  The code base is now open source so feel free to look through it and even build it yourself if you're still worried about the antivirus notification.

## Future Plans
I have a lot of ideas in mind to keep on improving Aemulus.  These include the following: 

- Improve my code and algorithms to optimize the merging process
- Add separators between mods (requested by Pixelguin to use for his modpack)
- Drag and drop mod/package folders onto interface to easily add to manager
- Merging bf and pm1 files
- Cpk building support for other Persona games
- Tabs for managing multiple Persona games in the same application
