using NavigationMenu.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Html;

namespace NavigationMenu.Helper
{
    public static class NavigationMenuHelper
    {
        public static IHtmlString NavigationListFor(this HtmlHelper helper, IEnumerable<NavigationLink> menu)
        {
            if (menu == null)
            {
                throw new ArgumentNullException("menu", "menu is null.");
            }

            var sb = new StringBuilder();
            foreach (var item in menu)
            {
                var li = new TagBuilder("li");
                li.InnerHtml = helper.ActionLink(item.LinkText, item.ActionName, item.ControllerName).ToString();
                sb.Append(li);
            }
            return MvcHtmlString.Create(sb.ToString());
        }

        public static IHtmlString NavigationSubmenuListFor(this HtmlHelper helper, IEnumerable<NavigationLink> submenu)
        {
            if (submenu == null)
            {
                throw new ArgumentNullException("submenu", "submenu is null.");
            }
            var sb = new StringBuilder();
            foreach (var item in submenu)
            {
                if (item.ChildMenu == null)
                {
                    var li = new TagBuilder("li");
                    li.InnerHtml = helper.ActionLink(item.LinkText, item.ActionName, item.ControllerName).ToString();
                    sb.Append(li.ToString(TagRenderMode.Normal));
                }
                else
                {
                    sb.Append(CreateSubMenu(helper, item.LinkText, item.ChildMenu));
                }
            }

            return MvcHtmlString.Create(sb.ToString());
        }

        private static string CreateSubMenu(HtmlHelper helper, string linkText, IEnumerable<NavigationLink> subMenu)
        {
            var a = new TagBuilder("a");
            var attributes = new Dictionary<string, string>
            {
                ["tableindex"] = "-1",
                ["href"] = "#",
            };
            a.MergeAttributes(attributes);
            a.InnerHtml = linkText;

            var top_li = new TagBuilder("li");
            top_li.MergeAttribute("class", "dropdown-submenu");
            top_li.InnerHtml = a.ToString();

            var ul = new TagBuilder("ul");
            ul.MergeAttribute("class", "dropdown-menu");

            foreach (var item in subMenu)
            {
                if (item.ChildMenu == null)
                {
                    var li = new TagBuilder("li");
                    li.InnerHtml = helper.ActionLink(item.LinkText, item.ActionName, item.ControllerName).ToString();
                    ul.InnerHtml += li.ToString();
                }
                else
                {
                    ul.InnerHtml += CreateSubMenu(helper, item.LinkText, item.ChildMenu);
                }
            }

            top_li.InnerHtml += ul.ToString();
            return top_li.ToString();
        }

    }
}