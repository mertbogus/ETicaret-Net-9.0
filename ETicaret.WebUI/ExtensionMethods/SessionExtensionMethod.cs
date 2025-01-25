using Newtonsoft.Json;
namespace ETicaret.WebUI.ExtensionMethods
{
    public static class SessionExtensionMethod
    {
        //static newlemeye gerek kalmadan metot oluşturuyoruz. 
        public static void SetJson(this ISession Session, string key, object value)
        {
            // Burada Sessiondan gelen keyi alıyoruz, daha sonra obje türünde olan value değerini serialize ediyoruz.
            Session.SetString(key,JsonConvert.SerializeObject(value));


        }

        //yeni fonksiyon oluşturuyoruz T değeri boş geçilebilir, diyoruz. Daha getson'un t alabileceğini beliyoruz. 
        // Ayrıca T'nin bir sınıf olacağını belirtiyoruz.
        public static T? GetJson<T>(this ISession Session, string key) where T : class
        {
            //Burada da yukarıdan gelen key get ile alıyoruz. 
            var data= Session.GetString(key);

            // eğer ki bu data boş ise fdefault olara t dönder diyoruz değil de data içerisinde yer aşan jsonu nesneye
            //çeviriyoruz.
            return data==null ? default(T): JsonConvert.DeserializeObject<T>(data);
        }
    }
}
