namespace NavigationMenu.Migrations
{
    using Models;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<NavigationMenu.Data.AppDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(NavigationMenu.Data.AppDbContext context)
        {
            if (context == null)
            {
                throw new ArgumentNullException("context", "context is null.");
            }
            // Unicode(UTF-8) - コードページ65001で保存
            context.MenuItems.AddOrUpdate(
                p => p.LinkText,
                new MenuItem { ParentId = null, LinkText = "ホーム", ActionName = "Index", ControllerName = "Home", Order = 1, RoleId = 0 },
                new MenuItem { ParentId = null, LinkText = "詳細", ActionName = "About", ControllerName = "Home", Order = 2, RoleId = 0 },
                new MenuItem { ParentId = null, LinkText = "連絡先", ActionName = "Contact", ControllerName = "Home", Order = 3, RoleId = 0 },
                new MenuItem { ParentId = null, LinkText = "サンプル", ActionName = null, ControllerName = null, Order = 4, RoleId = 1 },
                new MenuItem { ParentId = 4, LinkText = "Upload", ActionName = "Upload", ControllerName = "Home", Order = 1, RoleId = 1 },
                new MenuItem { ParentId = 4, LinkText = "Autocomplete", ActionName = "Create", ControllerName = "Home", Order = 2, RoleId = 1 },
                new MenuItem { ParentId = 4, LinkText = "Sample", ActionName = null, ControllerName = null, Order = 3, RoleId = 1 },
                new MenuItem { ParentId = 7, LinkText = "Sample01", ActionName = "Index", ControllerName = "Home", Order = 1, RoleId = 1 },
                new MenuItem { ParentId = 7, LinkText = "Sample02", ActionName = "Index", ControllerName = "Home", Order = 2, RoleId = 1 },
                new MenuItem { ParentId = 7, LinkText = "More Options", ActionName = null, ControllerName = null, Order = 3, RoleId = 2 },
                new MenuItem { ParentId = 10, LinkText = "Google", ActionName = "Index", ControllerName = "Home", Order = 1, RoleId = 2 },
                new MenuItem { ParentId = 10, LinkText = "Yahoo", ActionName = "Index", ControllerName = "Home", Order = 2, RoleId = 2 },
                new MenuItem { ParentId = 4, LinkText = "LogViewer", ActionName = "Index", ControllerName = "Logging", Order = 4, RoleId = 2 },
                new MenuItem { ParentId = 4, LinkText = "Todo", ActionName = "Index", ControllerName = "Todo", Order = 5, RoleId = 2 }
                );
        }
    }
}
