namespace WpfCrud.ViewModels
{
    public class AdminViewModel : ViewModelBase
    {
        private readonly ProductListingCrudViewModel _productListingCrudViewModel = new ProductListingCrudViewModel();
        private readonly DishTypeListingCrudViewModel _dishTypeListingCrudViewModel = new DishTypeListingCrudViewModel();
        private readonly DishCookingListingCrudViewModel _dishCookingListingCrudViewModel = new DishCookingListingCrudViewModel();
        private readonly DishIngredientListingCrudViewModel _dishIngredientListingCrudViewModel = new DishIngredientListingCrudViewModel();
        private readonly DishListingCrudViewModel _dishListingCrudViewModel = new DishListingCrudViewModel();
        private readonly UserAccountListingCrudViewModel _userAccountListingCrudViewModel = new UserAccountListingCrudViewModel();

        public AdminViewModel()
        {
        }

        public AdminViewModel(
            ProductListingCrudViewModel productListingCrudViewModel,
            DishTypeListingCrudViewModel dishTypeListingCrudViewModel,
            DishCookingListingCrudViewModel dishCookingListingCrudViewModel,
            DishIngredientListingCrudViewModel dishIngredientListingCrudViewModel,
            DishListingCrudViewModel dishListingCrudViewModel,
            UserAccountListingCrudViewModel userAccountListingCrudViewModel)
        {
            _productListingCrudViewModel = productListingCrudViewModel ?? throw new System.ArgumentNullException(nameof(productListingCrudViewModel));
            _dishTypeListingCrudViewModel = dishTypeListingCrudViewModel ?? throw new System.ArgumentNullException(nameof(dishTypeListingCrudViewModel));
            _dishCookingListingCrudViewModel = dishCookingListingCrudViewModel ?? throw new System.ArgumentNullException(nameof(dishCookingListingCrudViewModel));
            _dishIngredientListingCrudViewModel = dishIngredientListingCrudViewModel ?? throw new System.ArgumentNullException(nameof(dishIngredientListingCrudViewModel));
            _dishListingCrudViewModel = dishListingCrudViewModel ?? throw new System.ArgumentNullException(nameof(dishListingCrudViewModel));
            _userAccountListingCrudViewModel = userAccountListingCrudViewModel ?? throw new System.ArgumentNullException(nameof(userAccountListingCrudViewModel));
        }

        public ProductListingCrudViewModel ProductListingCrudViewModel => _productListingCrudViewModel;

        public DishTypeListingCrudViewModel DishTypeListingCrudViewModel => _dishTypeListingCrudViewModel;

        public DishCookingListingCrudViewModel DishCookingListingCrudViewModel => _dishCookingListingCrudViewModel;

        public DishIngredientListingCrudViewModel DishIngredientListingCrudViewModel => _dishIngredientListingCrudViewModel;

        public DishListingCrudViewModel DishListingCrudViewModel => _dishListingCrudViewModel;

        public UserAccountListingCrudViewModel UserAccountListingCrudViewModel => _userAccountListingCrudViewModel;
    }
}
