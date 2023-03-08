using WpfCrud.Models;

namespace WpfCrud.ViewModels
{
    public class AdminViewModel : ViewModelBase
    {
        private readonly CurrentUser _currentUser;
        private readonly UserAccountListingCrudViewModel _userAccountListingCrudViewModel;
        private readonly ProductListingCrudViewModel _productListingCrudViewModel;
        private readonly DishTypeListingCrudViewModel _dishTypeListingCrudViewModel;
        private readonly DishCookingListingCrudViewModel _dishCookingListingCrudViewModel;
        private readonly DishIngredientListingCrudViewModel _dishIngredientListingCrudViewModel;
        private readonly DishListingCrudViewModel _dishListingCrudViewModel;

        public AdminViewModel(CurrentUser currentUser)
        {
            _currentUser = currentUser ?? throw new System.ArgumentNullException(nameof(currentUser));
            _userAccountListingCrudViewModel = new UserAccountListingCrudViewModel(currentUser);
            _productListingCrudViewModel = new ProductListingCrudViewModel();
            _dishTypeListingCrudViewModel = new DishTypeListingCrudViewModel();
            _dishCookingListingCrudViewModel = new DishCookingListingCrudViewModel();
            _dishIngredientListingCrudViewModel = new DishIngredientListingCrudViewModel();
            _dishListingCrudViewModel = new DishListingCrudViewModel();
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
