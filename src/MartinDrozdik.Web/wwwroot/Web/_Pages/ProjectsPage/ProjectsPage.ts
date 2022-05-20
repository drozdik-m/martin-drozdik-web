/*
import { WindowEvents } from "@drozdik.m/window-events";
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
    let listElementParent: HTMLElement = document.getElementById("projects");
    if (listElementParent == null)
    {
        console.error(`#projects element not found`);
        return;
    }
    
    let projectListElement: HTMLElement = listElementParent.querySelector(".projectList")
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

    EntityList.InitiateViaAPI<ProjectListEntity, ProjectListConfig>(projectList, "/api/ProjectList");
});*/