namespace WpfCrud.ViewModels
{
    public class CookViewModel : ViewModelBase
    {
        public CookViewModel()
        {
            ProductListingViewModel = new ProductListingViewModel();
            DishListingViewModel = new DishListingViewModel();
            DishCookingListingViewModel = new DishCookingListingViewModel();
        }

        public CookViewModel(ProductListingViewModel productListingViewModel, DishListingViewModel dishListingViewModel, DishCookingListingViewModel dishCookingListingViewModel)
        {
            ProductListingViewModel = productListingViewModel ?? throw new System.ArgumentNullException(nameof(productListingViewModel));
            DishListingViewModel = dishListingViewModel ?? throw new System.ArgumentNullException(nameof(dishListingViewModel));
            DishCookingListingViewModel = dishCookingListingViewModel ?? throw new System.ArgumentNullException(nameof(dishCookingListingViewModel));
        }

        public ProductListingViewModel ProductListingViewModel { get; }
        public DishListingViewModel DishListingViewModel { get; }
        public DishCookingListingViewModel DishCookingListingViewModel { get; }
    }
}
