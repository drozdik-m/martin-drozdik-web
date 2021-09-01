import { Menu } from "@drozdik.m/menu";
import { WindowEvents } from "@drozdik.m/window-events";

WindowEvents.OnDOMReady.Add(function ()
{
    let menuElement = document.getElementById("menu")
    if (!menuElement)
    {
        console.error("Element #menu not found");
        return;
    }

    let menuToggleElement = document.getElementById("menuToggle");
    if (!menuToggleElement)
    {
        console.error("Element #menuToggle not found");
        return;
    }

    let menu = new Menu(menuElement)
        .AddToggleButton(menuToggleElement)
        .SetWidthBreaking(1000);

    menu.OnMenuOpen.Add(function ()
    {
        function HeightWithMargin(el: HTMLElement)
        {
            var height = el.offsetHeight;
            var style = getComputedStyle(el);

            height += parseInt(style.marginTop) + parseInt(style.marginBottom);
            return height;
        }

        let ulElement = document.getElementById("menu").querySelector("ul")
        let liElements = ulElement.querySelectorAll("li")
        var liHeight = HeightWithMargin(liElements.item(0))
        var liCount = liElements.length;
        ulElement.style.setProperty("max-height", liCount * liHeight + "px")

    });

    menu.OnMenuClose.Add(function ()
    {
        let ulElement = document.getElementById("menu").querySelector("ul")
        ulElement.style.setProperty("max-height", "0")
    });

    /*menu.OnDesktopMenu.Add(function ()
    {
        console.log("Desktop menu")
    })

    menu.OnMobileMenu.Add(function ()
    {
        console.log("Mobile menu")
    })

    menu.OnDesktopMenuOnce.Add(function ()
    {
        console.log("Desktop menu once")
    })

    menu.OnMobileMenuOnce.Add(function ()
    {
        console.log("Mobile menu once")
    })

    menu.OnMenuOpen.Add(function ()
    {
        console.log("Menu open")
    })

    menu.OnMenuClose.Add(function ()
    {
        console.log("Menu close")
    })*/
});