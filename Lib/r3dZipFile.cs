namespace GameServer.YALLS.ConfigFile;

internal class r3dZipFile
{
    //struct File* f;
    FileStream f;
    string filename; //char[0x100];
    entry_s[] entries;
    int n_entries;

    struct entry_s
    {
        uint hdr_offset;
        uint data_offset;
        uint compression_method;
        uint crc32;
        uint compressed_size;
        uint uncompressed_size;
        string filename;
    };
}
