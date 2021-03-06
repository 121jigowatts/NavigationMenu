﻿using NavigationMenu.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace NavigationMenu.Models
{
    public class MenuItem
    {
        [Key]
        public int Id { get; set; }
        public int? ParentId { get; set; }
        [Required]
        public int RoleId { get; set; }
        [Required]
        public string LinkText { get; set; }
        public string ActionName { get; set; }
        public string ControllerName { get; set; }
        public int Order { get; set; }
    }

    public class NavigationLink : MenuItem
    {
        public IEnumerable<NavigationLink> ChildMenu { get; set; }
    }

    public enum Role
    {
        User,
        PowerUser,
        Administrator,
    }

    public class Navigation
    {
        public Navigation() : this(role: Role.User)
        {

        }

        public Navigation(Role role)
        {
            this.RoleId = (int)role;
        }

        public int RoleId { get; }

        public IEnumerable<NavigationLink> Menu
        {
            get
            {
                var menu = new List<NavigationLink>();
                using (var context = new AppDbContext())
                {
                    var data = context.MenuItems
                        .Where(n => n.RoleId <= RoleId)
                        .OrderBy(n => n.Order)
                        .ToList();

                    var mainMenu = data.Where(n => n.ParentId == null);
                    foreach (var item in mainMenu)
                    {
                        NavigationLink linkItem = MappingLinkItems(item);
                        menu.Add(linkItem);
                    }
                    SetChildMenu(menu, data);
                }
                return menu;
            }
        }

        #region helper method
        private static NavigationLink MappingLinkItems(MenuItem item)
        {
            var linkItem = new NavigationLink();
            linkItem.Id = item.Id;
            linkItem.ParentId = item.ParentId;
            linkItem.LinkText = item.LinkText;
            linkItem.ActionName = item.ActionName;
            linkItem.ControllerName = item.ControllerName;
            linkItem.Order = item.Order;
            return linkItem;
        }

        private void SetChildMenu(IEnumerable<NavigationLink> menu, IEnumerable<MenuItem> data)
        {
            foreach (var item in menu)
            {
                if (item.ActionName == null && item.ChildMenu == null)
                {
                    var childMenu = GetChildMenu(data, item.Id);
                    item.ChildMenu = childMenu;

                    SetChildMenu(childMenu, data);
                }
            }
        }

        private static IEnumerable<NavigationLink> GetChildMenu(IEnumerable<MenuItem> data, int id)
        {
            var child = data.Where(n => id == n.ParentId)
                .OrderBy(n => n.Order).ToList();

            var childMenu = new List<NavigationLink>();
            foreach (var childItem in child)
            {
                NavigationLink childLinkItem = MappingLinkItems(childItem);
                childMenu.Add(childLinkItem);
            }
            return childMenu;
        }
        #endregion
    }
}