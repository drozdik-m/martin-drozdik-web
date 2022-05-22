/*


WindowEvents.OnDOMReady.Add(function ()
{
    let listElementParent: HTMLElement = document.getElementById("projects");
    if (listElementParent == null)
    {
        console.error(`#projects element not found`);
        return;
    }
    
    
});*/

import { WindowEvents } from "@drozdik.m/window-events";
import { RegisterStatForAnimation } from "../../ImportantStats/ImportantStats";
import { Ajax, AjaxResponse } from "@drozdik.m/ajax";
import { Pipeline } from "@drozdik.m/pipeline";
import { LoadingAnimation } from "@drozdik.m/loading-animation";
import { StatusWindowError } from "../../StatusWindow/StatusWindowError";
import { ProjectListEntity } from "../../Projects/ProjectListEntity";
import { ProjectList } from "../../Projects/ProjectList";
import { ProjectListConfig } from "../../Projects/ProjectListConfig";
import { EntityList } from "../../EntityList/EntityList";


WindowEvents.OnDOMReady.Add(function ()
{
    //Get #projects
    let projectsElement = document.getElementById("projects");
    if (!projectsElement)
    {
        console.error("No #projects element found");
        return;
    }

    //Get #projectsStats
    let projectsStatsElement = document.getElementById("projectsStats");
    if (!projectsStatsElement)
    {
        console.error("No #projectsStats element found");
        return;
    }

    //Iterate over important stats and register them
    let importantStats = projectsStatsElement.querySelectorAll(".importantStat");
    for (let i = 0; i < importantStats.length; i++)
        RegisterStatForAnimation(importantStats[i] as HTMLElement);

    //Project list
    let projectListElement: HTMLElement = projectsElement.querySelector(".projectList")
    if (projectListElement == null)
    {
        console.error(`#projects .projectList element not found`);
        return;
    }
    let projectList = new ProjectList(projectListElement, {
        entities: [],
        loadMoreButton: true,
        tagsFilter: true,
        pageSize: 3,
        initialSize: 0,
        noResultsMessage: "Žádné projekty nenalezeny"
    });

    EntityList.InitiateViaAPI(projectList, "/api/ProjectList?scheme=light");
});