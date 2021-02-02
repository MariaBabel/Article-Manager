using System.ComponentModel;
using article_manager.Models;
using frontendlib.Models;
using Microsoft.AspNetCore.Components;
using article_manager.Services;

namespace article_manager.Pages
{
    public class ArticlesBase : ComponentBase
    {
        [Inject]
        private ICRUDService<ArticleListItem, ArticleItem> service { get; set; }

        protected string error;

        protected ItemDetailsModel<ArticleItem> articleItem = new ItemDetailsModel<ArticleItem>()
        {
            ItemName = "Article"
        };

        protected ItemListModel articlesModel = new ItemListModel()
        {
            ItemName = "Article",
            Headers = new string[] { "", "" },
            Items = new ArticleListItem[0]
        };
    }
}