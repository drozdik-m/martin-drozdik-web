import "../../HeroHeader/HeroHeader";
import "../../AboutMe/AboutMe";
import "../../MySkills/MySkills";
import "../../Biography/Biography";
import "../../Contact/Contact";

import { WindowEvents } from "@drozdik.m/window-events";
import { Ajax, AjaxResponse } from "@drozdik.m/ajax";
import { Pipeline } from "@drozdik.m/pipeline";
import { LoadingAnimation } from "@drozdik.m/loading-animation";
import { StatusWindowError } from "../../StatusWindow/StatusWindowError";
import { ProjectListEntity } from "../../Projects/ProjectListEntity";
import { ProjectList } from "../../Projects/ProjectList";

WindowEvents.OnDOMReady.Add(function ()
{
    //Project list

    let projectsElement: HTMLElement = document.getElementById("projects");
    if (projectsElement == null)
    {
        console.error(`#projects element not found`);
        return;
    }

    let projectListElement: HTMLElement = projectsElement.querySelector(".projectList")
    if (projectListElement == null)
    {
        console.error(`#projects .projectList element not found`);
        return;
    }

    let projectAjax = new Ajax();
    let projectLoading = new LoadingAnimation(projectListElement);
    let projectList = new ProjectList(projectListElement, {
        entities: [],
        loadMoreButton: true,
        tagsFilter: true,
        pageSize: 3,
        initialSize: 0
    });

    projectLoading.Show();
    projectAjax
        .Get("/api/ProjectList")
        .Then(function (response: AjaxResponse)
        {
            //Check for response error
            if (response.IsError())
                return Pipeline.Reject(new Error(response.StatusText() + ": " + response.Response()))

            return response.Response();
        })
        .Then(function (listProjects: string)
        {
            projectLoading.Hide();
            let projects: ProjectListEntity[] = JSON.parse(listProjects);
            //projects = projects.slice(0, 8);
            //console.log(projects);
            projectList.SetEntities(projects);
            projectList.LoadMoreCount(3);
        })
        .Catch(function (error: Error)
        {
            console.error(error);
            projectLoading.Hide();
            let errorDialog = new StatusWindowError("contactFormErrorDialog",
                {
                    heading: "Načítání selhalo",
                    message: "Něco se moc pokazilo :( <br /> " + error.message,
                    closeButtonText: "Zavřít"
                });
            errorDialog.Display();
        });

});