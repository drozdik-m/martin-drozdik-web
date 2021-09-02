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
});