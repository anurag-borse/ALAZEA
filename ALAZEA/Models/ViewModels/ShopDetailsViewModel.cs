namespace ALAZEA.Models.ViewModels
{
    public class ShopDetailsViewModel : BaseViewModel
    {
        public Plant SelectedPlant { get; set; } // Single Plant
        public List<Plant> RelatedProducts { get; set; } // List of Related Plants


    }
}
