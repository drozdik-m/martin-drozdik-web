import { Menu } from "@drozdik.m/menu";
import { WindowEvents } from "@drozdik.m/window-events";
import { DimensionsHelper } from "@drozdik.m/dimensions-helper"


WindowEvents.OnDOMReady.Add(function ()
{
    //Get menu element
    let menuElement = document.getElementById("menu")
    if (!menuElement)
    {
        console.error("Element #menu not found");
        return;
    }

    //Get menu toggle element
    let menuToggleElement = document.getElementById("menuHamburger");
    if (!menuToggleElement)
    {
        console.error("Element #menuHamburger not found");
        return;
    }

    //Create menu
    let menu = new Menu(menuElement)
        .AddToggleButton(menuToggleElement)
        .SetWidthBreaking(1300);

    //Get list of items
    var ulElement = menuElement.querySelector("ul")
    if (!ulElement)
    {
        console.error("Element \"#menu ul\" not found");
        return;
    }
    var ulDimHelper = new DimensionsHelper(ulElement)

    //Get menu logo
    var menuLogoElement = document.getElementById("menuLogo")
    if (!menuLogoElement)
    {
        console.error("Element #menuLogoElement not found");
        return;
    }
    var menuLogoDimHelper = new DimensionsHelper(menuLogoElement)

    /**
     * Recalculates margin of the ul menu list
     * */
    function RecalculateUlMargin()
    {
        var screenHeight = WindowEvents.Height();
        var logoHeight = menuLogoDimHelper.HeightWithMargin();
        var ulHeight = ulDimHelper.HeightWithBorder();

        var freeSpace = screenHeight - logoHeight - ulHeight;
        var marginTop = freeSpace / 3;

        ulElement.style.marginTop = marginTop + "px";
    }

    WindowEvents.OnResize.Add(function ()
    {
        if (menu.IsOpen() && menu.IsMobile())
            RecalculateUlMargin();
    });

    menu.OnMenuOpen.Add(function ()
    {
        RecalculateUlMargin();
    });

    menu.OnDesktopMenuOnce.Add(function ()
    {
        ulElement.style.marginTop = "0";
    });

    /*menu.OnMenuOpen.Add(function ()
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
    });*/

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