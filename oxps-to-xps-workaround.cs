void oxpsToXps(string oxpsFile, string xpsFile, string tmpPath)
{
    var fName = System.IO.Path.GetFileName(oxpsFile);
    var tmpDir = tmpPath + "\\" + fName;
    tmpDir = tmpDir.Replace("\\\\", "\\");

    try
    {
        System.IO.Compression.ZipFile.ExtractToDirectory(oxpsFile, tmpDir);
        var files = System.IO.Directory.GetFiles(tmpDir, "*.*", System.IO.SearchOption.AllDirectories);
        foreach (var file in files)
        {
            var content = System.IO.File.ReadAllText(file);
            var content_new = content.Replace("http://schemas.openxps.org/oxps/v1.0", "http://schemas.microsoft.com/xps/2005/06");
            if (content.Length != content_new.Length) System.IO.File.WriteAllText(file, content_new);
        }
        System.IO.Compression.ZipFile.CreateFromDirectory(tmpDir, xpsFile);
        System.IO.Directory.Delete(tmpDir, true);
    }
    catch
    {
        
    }
}