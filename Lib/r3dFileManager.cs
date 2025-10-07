using System.IO.Compression;

namespace LoLINI;

internal class r3dFileManager
{
    public static r3dFileManager Instance { get; } = new();

    private r3dFileManager() { }

    internal r3dFileImpl? Open(string? fName) //Extremelly stripped version of the actual function
    {
        if (File.Exists(fName))
        {
            return Open_fOpen(fName);
        }
        return null;
    }

    internal static r3dFileImpl Open_fOpen(string fName)
    {
        r3dFileImpl toReturn = new(0)
        {
            stream = null,
            data = null,
            bFileOwned = false,
            size = 0,
            pos = 0,
            Location =
            {
                FileName = "",
                Where = 0,
                Offset = 0
            }
        };

        Stream stream = File.OpenRead(fName);
        toReturn.stream = stream;
        toReturn.bFileOwned = true;
        toReturn.Location.Where = 0;
        toReturn.Location.Offset = (int)stream.Position;
        stream.Seek(0, SeekOrigin.End); //_fseek(edi, 0, 2);
        toReturn.size = (int)stream.Position;
        stream.Seek(0, SeekOrigin.Begin);
        toReturn.Location.FileName = fName;
        return toReturn;
    }

    internal static r3dFileImpl? TryOpen(string fname)
    {
        //if (r3dFileManager::SearchInZipsConvert((const char*)fname, (r3dFileManager*)&v10, v8) )
        //  return r3dFileManager::OpenZipFile(v10, fname, v9);

        if(!File.Exists(fname))
        {
            //("r3dFileImpl: Can't open %s (from file %s)\n", (const char *)fname, mode)
            Console.WriteLine($"r3dFileImpl: Can't open {fname}!");
            return null;
        }

        r3dFileImpl toReturn = Open_fOpen(fname);
        if (toReturn is null)
        {
            //("r3dFileImpl: Can't open %s (from file %s)\n", (const char *)fname, mode)
            Console.WriteLine($"r3dFileImpl: Can't open {fname}!");
        }
        return toReturn;
    }
}
