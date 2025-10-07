using System.Numerics;

namespace LoLINI;

public class Cache
{
    public static Cache Instance { get; } = new();
    private readonly Dictionary<string, RFile> FileNameToFile = [];
    private RFile LastAccessedFile;
    private string LastAccessedFileName;

    private Cache()
    {
        FileNameToFile = [];
        LastAccessedFile = null!;
        LastAccessedFileName = "";
    }

    //Check
    private string CreateFullPath(string filename)
    {
        string path = "";
        if (FileNameToFile is not null) //?
        {
            path = Directory.GetCurrentDirectory();
            FileSysHelper.RelativeToAbsolutePath(path, out path);
        }
        FileSysHelper.FormatPath(ref path);
        return $"{path}/{filename}";
    }
    internal void PreloadFile(string fileName)
    {
        GetFile(fileName, false);
    }
    public RFile? GetFile(string fileName, bool skipCache /*unused*/ = false)
    {
        if (string.IsNullOrEmpty(fileName))
        {
            return null;
        }

        RFile? file;

        if (!skipCache) //Custom? Maybe I overlooked this check in the disassembly
        {
            if (fileName == LastAccessedFileName)
            {
                file = LastAccessedFile;
            }
            else if (FileNameToFile.TryGetValue(fileName, out file))
            {
                LastAccessedFile = file;
                LastAccessedFileName = fileName;
            }

            if (file is not null)
            {
                if (!file.FileHasBeenModified)
                {
                    return file;
                }
            }
        }

        string fullPath = Path.IsPathRooted(fileName) ? fileName : CreateFullPath(fileName);
        FileSysHelper.ParseFileSpecification(fullPath, out string folder, out string name, out string extension);
        string keyVal = $"{folder}/defaults/{name}.{extension}";
        file = new(Path.GetFileName(fileName), fullPath, keyVal);
        FileNameToFile[fileName] = file;

        LastAccessedFile = file;
        LastAccessedFileName = fileName;
        return file;
    }
    public void GetValue(out string returnValue, string pFileName, string pSection, string pName, string pDefault, bool skipCache)
    {
        RFile? eax = GetFile(pFileName, skipCache);
        if (eax is null)
        {
            returnValue = pDefault;
            return;
        }
        eax.GetValue(out returnValue, pFileName, pName, HashFunctions.HashString_SDBM(pSection, pName), pDefault);
    }
    public void GetValue(out bool returnValue, string pFileName, string pSection, string pName, bool pDefault, bool skipCache)
    {
        RFile? eax = GetFile(pFileName, skipCache);
        if (eax is null)
        {
            returnValue = pDefault;
            return;
        }
        eax.GetValue(out returnValue, pFileName, pName, HashFunctions.HashString_SDBM(pSection, pName), pDefault);
    }
    public void GetValue(out Vector4 returnValue, string pFileName, string pSection, string pName, Vector4 pDefault, bool skipCache)
    {
        RFile? eax = GetFile(pFileName, skipCache);
        if (eax is null)
        {
            returnValue = pDefault;
            return;
        }
        eax.GetValue(out returnValue, pFileName, pName, HashFunctions.HashString_SDBM(pSection, pName), pDefault);
    }
    public void GetValue(out Vector3 returnValue, string pFileName, string pSection, string pName, Vector3 pDefault, bool skipCache)
    {
        RFile? eax = GetFile(pFileName, skipCache);
        if (eax is null)
        {
            returnValue = pDefault;
            return;
        }
        eax.GetValue(out returnValue, pFileName, pName, HashFunctions.HashString_SDBM(pSection, pName), pDefault);
    }
    public void GetValue(out Vector2 returnValue, string pFileName, string pSection, string pName, Vector2 pDefault, bool skipCache)
    {
        RFile? eax = GetFile(pFileName, skipCache);
        if (eax is null)
        {
            returnValue = pDefault;
            return;
        }
        eax.GetValue(out returnValue, pFileName, pName, HashFunctions.HashString_SDBM(pSection, pName), pDefault);
    }
    public void GetValue(out float returnValue, string pFileName, string pSection, string pName, float pDefault, bool skipCache)
    {
        RFile? eax = GetFile(pFileName, skipCache);
        if (eax is null)
        {
            returnValue = pDefault;
            return;
        }
        eax.GetValue(out returnValue, pFileName, pName, HashFunctions.HashString_SDBM(pSection, pName), pDefault);
    }
    public void GetValue(out int returnValue, string pFileName, string pSection, string pName, int pDefault, bool skipCache)
    {
        RFile? eax = GetFile(pFileName, skipCache);
        if (eax is null)
        {
            returnValue = pDefault;
            return;
        }
        eax.GetValue(out returnValue, pFileName, pName, HashFunctions.HashString_SDBM(pSection, pName), pDefault);
    }
}
