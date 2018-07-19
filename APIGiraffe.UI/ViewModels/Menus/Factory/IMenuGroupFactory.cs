using System.Collections.Generic;
using APIGiraffe.UI.ViewModels.Menus;

namespace APIGiraffe.UI.ViewModels.Menus.Factory
{
    public interface IMenuGroupFactory
    {
        MenuGroup Create(string name, int id, List<RequestMenuItem> items);
    }
}
