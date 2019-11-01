Public Enum PackageType
    Unchecked
    bin
    Folder
    HSPC
    EPK8
    EPAC
    PACH
    SHDC
    PachDirectory_4
    PachDirectory_8
    ZLIB
    BPE
    OODL
    TextureLibrary
    TextureReference
    YANMPack
    YOBJ
    StringFile
    DDS
    ArenaInfo
    ShowInfo
    FileRefBin
    NIBJ
    bk2
    CostumeFile
    MuscleFile
    MaskFile
    YOBJArray
    OFOP
    YANM
    VMUM
    TitleFile
    SoundReference
    WeaponPosition
    LSD
    LSD_BIN
    Arc

    'here are additional ufc type files that we will have no handling for.  However we want to properly cover them for adding in future.
    UFC_MAP

    UFC_HKX
    UFC_PAC
    UFC_TXT
    UFC_CVX
    UFC_BIN

    'EA UFC files
    big

    '2K20
    Cak
    CakFolder 'This is a virtual folder that shouldn't ever have an actual file output.
    CakBaked 'This is a temp state where the file name has been parsed but bytes have not been retrieved.
    OODL7 'These are labeled if the Cak part file has a OODL part
    tex 'Here is the Main Tex container which will normally have a Crunched Texture inside.
    Crn 'This is the signal for the file still being crunched.  when bytes are no longer crunched it will be a dds file.
    mskinfo
    mtls
    mdl
    mprms
    jsfb
    other_ywa
    ywa
    sdb
    TronFile
    EntrData
    acts
    evd

    'Day of Reckoning GV File Types added as needed
    TPL

    DUMY

    'Sending the text editor notice that we are editing a file name
    EditingFileName

End Enum