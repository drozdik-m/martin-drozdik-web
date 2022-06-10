import { WindowEvents } from "@drozdik.m/window-events";
import { EntityList } from "../../EntityList/EntityList";
import { BlogListConfig } from "../../Blog/BlogListConfig";
import { BlogList } from "../../Blog/BlogList";


WindowEvents.OnDOMReady.Add(function ()
{
    //Get #blog
    let blogElement = document.getElementById("blog");
    if (!blogElement)
    {
        console.error("No #blog element found");
        return;
    }

    //Article list
    let articleListElement: HTMLElement = blogElement.querySelector(".articleList")
    if (articleListElement == null)
    {
        console.error(`#blog .articleList element not found`);
        return;
    }
    var config: BlogListConfig = {
        entities: [],
        loadMoreButton: true,
        tagsFilter: true,
        pageSize: 9,
        initialSize: 0,
        noResultsMessage: "Žádné články nenalezeny"
    }
    let blogList = new BlogList(articleListElement, config);

    EntityList.InitiateViaAPI(blogList, config.pageSize, "/api/ArticleList");
});