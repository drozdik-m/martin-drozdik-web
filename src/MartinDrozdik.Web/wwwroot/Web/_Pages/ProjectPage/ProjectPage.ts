import { WindowEvents } from "@drozdik.m/window-events";
import { MakeGallery } from "../../Gallery/Gallery";
import "../../ContactMeStripe/ContactMeStripe";
import "../../Team/Team";
import "../../Article/Article";
import "../../Technologies/Technologies";


WindowEvents.OnDOMReady.Add(function ()
{
    /*let listElementParent: HTMLElement = document.getElementById("projects");
    if (listElementParent == null)
    {
        console.error(`#projects element not found`);
        return;
    }*/

    //Create the main project gallery
    MakeGallery("#project .gallery");
    console.log(document.querySelectorAll("#project .gallery li a"));
});



