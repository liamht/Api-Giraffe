using APIGiraffe.UI.ViewModels.Menus;

namespace APIGiraffe.UI.ViewModels.Menus.Factory
{
    public interface IRequestMenuItemFactory
    {
        RequestMenuItem Create(string text, int id);
    }
}
