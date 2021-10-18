import { Menu } from "@drozdik.m/menu";
import { WindowEvents } from "@drozdik.m/window-events";
import { DimensionsHelper } from "@drozdik.m/dimensions-helper"


WindowEvents.OnDOMReady.Add(function ()
{
    //Get menu element
    let menuElement = document.getElementById("menu");
    if (!menuElement)
    {
        console.error("Element #menu not found");
        return;
    }

    //Get nav element
    let navElement = menuElement.querySelector("nav");
    if (!menuElement)
    {
        console.error("Element \"#menu nav\" not found");
        return;
    }
    let navDimHelper = new DimensionsHelper(navElement);

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
    let ulElement = menuElement.querySelector("ul")
    if (!ulElement)
    {
        console.error("Element \"#menu ul\" not found");
        return;
    }
    let ulDimHelper = new DimensionsHelper(ulElement)

    //Get menu logo
    let menuLogoElement = document.getElementById("menuLogo")
    if (!menuLogoElement)
    {
        console.error("Element #menuLogoElement not found");
        return;
    }
    let menuLogoDimHelper = new DimensionsHelper(menuLogoElement)

    //Get menu items
    let liElements = ulElement.querySelectorAll("li a");

    //Close mobile menu on item click
    for (let i = 0; i < liElements.length; i++)
    {
        let element = liElements[i];
        element.addEventListener("click", function ()
        {
            menu.Close();
        });
    }

    /**
     * Recalculates margin of the ul menu list
     * */
    function RecalculateUlMargin()
    {
        let screenHeight = WindowEvents.Height();
        let logoHeight = menuLogoDimHelper.HeightWithMargin();
        let ulHeight = ulDimHelper.HeightWithBorder();

        let freeSpace = screenHeight - logoHeight - ulHeight;
        let marginTop = freeSpace / 3;

        ulElement.style.marginTop = marginTop + "px";
    }

    /**
     * Recalculates position of mobile menu when hidden
     * */
    function RecaltulateMenuRight()
    {
        let menuWidth = navDimHelper.WidthWithMargin();
        navElement.style.right = (-menuWidth).toString() + "px";
    }

    WindowEvents.OnResize.Add(function ()
    {
        if (menu.IsOpen() && menu.IsMobile())
            RecalculateUlMargin();

        if (!menu.IsOpen() && menu.IsMobile())
            RecaltulateMenuRight();
    });

    RecaltulateMenuRight();

    menu.OnMenuOpen.Add(function ()
    {
        RecalculateUlMargin();
    });

    menu.OnDesktopMenuOnce.Add(function ()
    {
        ulElement.style.marginTop = "0";
    });
});