using System;

namespace APIGirrafe.UI.ViewModels.Menus
{
    public class RequestMenuItem
    {
        public int ItemId { get; }

        public string Text { get; }

        public Action OnSelected { get; }

        public RequestMenuItem(string text, int itemId, Action onSelected)
        {
            Text = text;
            ItemId = itemId;
            OnSelected = onSelected;
        }
    }
}
