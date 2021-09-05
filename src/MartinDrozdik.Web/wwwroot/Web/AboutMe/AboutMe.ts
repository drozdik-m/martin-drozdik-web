﻿import { WindowEvents } from "@drozdik.m/window-events";
import { RegisterStatForAnimation } from "../ImportantStats/ImportantStats";


WindowEvents.OnDOMReady.Add(function ()
{
    //Get #aboutMe
    let aboutMeElement = document.getElementById("aboutMe");
    if (!aboutMeElement)
    {
        console.error("No #aboutMe element found");
        return;
    }

    //Iterate over important stats and register them
    let importantStats = aboutMeElement.querySelectorAll(".importantStat");
    importantStats.forEach(function (item)
    {
        RegisterStatForAnimation(item as HTMLElement);
    });

    
});