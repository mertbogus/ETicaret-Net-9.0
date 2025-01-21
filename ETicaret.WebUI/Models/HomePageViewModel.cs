using ETicaret.Core.Entities;

namespace ETicaret.WebUI.Models
{
    public class HomePageViewModel
    {
        //Anasayfa da birden fazla model geleceği için oluşturuldu. 
        public List<Slider>? Sliders { get; set; }
        public List<Product>? Product { get; set; }
        public List<News>? News { get; set; }
    }

}