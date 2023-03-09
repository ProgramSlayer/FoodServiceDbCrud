using WpfCrud.ViewModels;

namespace WpfCrud.Services
{
    public class EditEntityWindowDialogService
    {
        public bool? ShowDialog(ViewModelBase editableEntityViewModel)
        {
            var editEntityViewModel = new EditEntityViewModel(editableEntityViewModel);
            var editEntityWindow = new EditEntityWindow(editEntityViewModel);
            var dialogResult = editEntityWindow.ShowDialog();
            return dialogResult;
        }
    }
}
