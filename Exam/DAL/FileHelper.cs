namespace DAL;

public class FileHelper
{
    public static readonly string BasePath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData)
                                             + System.IO.Path.DirectorySeparatorChar + "ExamApp" + System.IO.Path.DirectorySeparatorChar;
}