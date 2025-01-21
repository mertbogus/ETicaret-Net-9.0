using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace ETicaret.WebUI.Utils
{
    public class FileHelper
    {
        public static async Task<string> FileLoaderAsync(IFormFile formFile, string FilePath = "/Img/")
        //dosya yolunu belirtiyoruz değişkene atıyoruz

        {
            //dosya ismi için değişken oluşturuyoz.
            var fileName = "";

            //eğer ki dosya boş değil ve lenght'i 0'dan büyükse 
            if (formFile!=null && formFile.Length>0)
            {
                // Dosya ismini alıyoruz ve dosya ismini küçüğe dönüştürüyoruz.
                fileName = formFile.FileName.ToLower();
                //Dosya yolunu alıyoruz. Projenin çalıştığı yerde wwwrootu bul file path'den gelen dosya adını bul.
                string directory = Directory.GetCurrentDirectory() + "/wwwroot" + FilePath + fileName;
                //Dosyayı ilgili klasöre ekliyoruz. 
                using var stream = new FileStream(directory,FileMode.Create);
                await formFile.CopyToAsync(stream);
            }
            return fileName;
        }

        public static bool FileRemover(string fileName, string FilePath= "/Img/")
        {
            //Dosya yolunu alıyoruz. Projenin çalıştığı yerde wwwrootu bul file path'den gelen dosya adını bul.
            string directory = Directory.GetCurrentDirectory() + "/wwwroot" + FilePath + fileName;

            //Böyle bir dosya var mı yok mu kontrolü
            if (File.Exists(directory))
            {
                //dosyayı sildiriyoruz.
                File.Delete(directory);
                //başarılı ise true dön
                return true;
            }
            //değilse false dön.
            return false;
        }
    }


}
