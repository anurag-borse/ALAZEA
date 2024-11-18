namespace ALAZEA.Models.ViewModels
{

    public class ShopViewModel : BaseViewModel
    {
        public IEnumerable<Plant> Plants { get; set; }
        public User User { get; set; }  // or any other type of User model you are using
    }
}
