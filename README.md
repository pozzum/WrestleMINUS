# WrestleMINUS
File Processing for Yukes Wrestling Games

## File Types
 
#### Primary Container Types:
These are containers that are the base file type for most files
##### HSPC:
A container primarily used in 2K17 and later.
##### EPK8:
A container primarily used in 2K15 and 2K16, used for most mods to get custom slots.
##### EPAC:
A container primarily used in older games, can sometimes appear in certain files.

#### Secondary Container Types:
these are what the primary containers have within them.
##### SHDC:
A container primarily used in 2K17 and later.
##### PACH:
A container primarily used in 2K15 and 2K16.

#### Compression Types:
These are types of compression the game uses.
##### BPE:
This is a "byte pair encoding" compression type the oldest compression type the game uses.  
The program Currently relies on a program called [Unrrbpe](http://asmodean.reverse.net/pages/unrrbpe.html)   
Future versions may incorporate [XHP-Creation's YukesBPE.dll](https://github.com/xhp-creations/YukesBPE)  
BPE Compression is achived by a program called [bpe.exe](https://www.tapatalk.com/groups/legendsofmodding/bpe-compression-tool-t3741.html#p22164) by Victhrash36
##### ZLIB:
This a microsoft file zip type.  [The source Code for the Dll required is here.](http://www.codeplex.com/DotNetZip)
##### OODL:
This is a proprietary compression type by [RADTools](http://www.radgametools.com/oodle.htm)   
This program uses the dll shipped with WWE2K19 I do not have rights to redistribute this dll.  
The game uses the Kraken compression algorithm with level 2 compression.  

#### Library Types:
These are specific file type libraries that often only contain 1 file type.
##### Texture Library:
Contains DSS Textures.
##### YANMPack:
This contains animation files.
##### YOBJ:
This contains character model files.

#### Processed File Types:
Theses are files that the tool can currently brocess into some editable menu.

## Coding Standards:
These are some notes on how the menus are coded for when additional file types are added.

The `HideTabs` command will be used for removing all tabs needed and incroperate save checks and save injection.  
`SavePending` is used for any tabs that require save injection on close.  `ReadNode` is used in conjection with this.

###### Adding Types:

* For an any new file it must be first added to the `PackageType` enum, this is a list of file types the program handles in some way.  
* Next if it is an expandable file format it must be added to the `CheckFile` function. 
  * For file contained a new `TreeNode` should be created with a `ToolTipText = HostNode.ToolTipText` This keeps the File Name in the treenodes
  * Next in the `TreeNode.Tag` there should be a `NodeProperties` created with and `.Index`(0 based),`.Length`,`.StoredData` which should be `= NodeTag.StoredData` to cary the parents data unless uncompressed.
  * This Node properties also includes a `.FileType` which unless the container will only contain 1 type of file should be `= CheckHeaderType(.Index - NodeTag.Index, FileBytes)`
  * Also make sure to add the new file type to the `Expandable` function
* No matter the format the program will need to know how to identify it.  so it must be added to the `CheckHeaderType` function. How this is done is entirely dependent on the file type.
* Now if the new file type is editable we will want to add a new `tabpage` 
  * Give the TabPage a descriptive name and first add it to the `GetTabType` 
  * This will show the tab and hide all other tabs when a filetype of that type is selected.
* Next we want to add a command to the `TabControl1_Selecting` command with information on how the tab is loaded with information.
* -
* To make any Changes in the tab saveable, you need to make a cuntion which will return a Byte Array that is the new File.  
  * To have a Save Button use `InjectIntoNode(ReadNode, YourFunction())`
  * You will want to add this to the `HideTabs` command so a save pending warning can be raised.

