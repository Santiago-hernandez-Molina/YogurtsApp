using System.Net;
using FluentFTP;

namespace NutryDairyASPApplication.Utils
{
  class SaveFiles
  {
    private string FtpUser;
    private string FtpPasswd;
    private string[] allowedContentTypes;
    private string ruta;

    public SaveFiles()
    {
      this.FtpUser = Environment.GetEnvironmentVariable("FTP_USER");
      this.FtpPasswd = Environment.GetEnvironmentVariable("FTP_PASSWD");
      this.allowedContentTypes = new string[]{ "image/jpeg", "image/png", "image/gif" };
      this.ruta = "://localhost/nutredairy/";
    }

    public string SaveImage(IFormFile file)
    {
      if (this.allowedContentTypes.Contains(file.ContentType))
      {
        var fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
        var rutaDestino = "ftp" + ruta + fileName;
        var credenciales = new NetworkCredential(this.FtpUser, this.FtpPasswd);

        using (var client = new FtpClient("localhost", credenciales.UserName, credenciales.Password))
        {
          client.AutoConnect();
          client.UploadStream(file.OpenReadStream(), "/nutredairy/"+fileName);
        }
        var rutaAcceso = "http" + ruta + fileName;
        return rutaAcceso;
      }
      return "";
    }

    public string SaveImageToBase64(IFormFile file)
    {
      if (this.allowedContentTypes.Contains(file.ContentType))
      {
        using (var ms = new MemoryStream())
        {
          file.CopyTo(ms);
          var fileBytes = ms.ToArray();
          return "data:jpeg;base64," + Convert.ToBase64String(fileBytes);
        }
      }
      else
      {
        return "";
      }
    }
  }
}
