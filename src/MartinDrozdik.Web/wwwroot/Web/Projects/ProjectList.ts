import { ProjectListConfig } from "./ProjectListConfig";



export class ProjectList
{
    private config: ProjectListConfig;
    private listElement: HTMLElement;
    private currentPageCount: number;



    constructor(listElement: HTMLElement, config: ProjectListConfig)
    {
        this.listElement = listElement;
        this.config = config;

        //Do not use "load more button" by defalt
        if (typeof config.loadMoreButton == "undefined")
            config.loadMoreButton = false;

        //Default page size is 3
        if (typeof config.initialPageSize == "undefined")
            config.initialPageSize = 3;
        this.currentPageCount = config.initialPageSize;
        
        //Initialize this object
        this.Initialize();
    }

    /**
     * Initialized this component
     * */
    protected Initialize(): void
    {
        var object = this;

        //Bind "load more" button
        var loadMoreButton = this.listElement.querySelector(".loadMoreProjectsButton");
        if (loadMoreButton == null)
        {
            console.error(`No button with class ".loadMoreProjectsButton" has not been found`);
            return;
        }
        loadMoreButton.addEventListener("click", function ()
        {
            object.LoadMore();
        });

    }

    /**
     * Load new items into the current list (one whole page)
     * */
    public LoadMore(): void
    {
        this.currentPageCount += this.config.initialPageSize;

        console.warn("not implemented");
    }
}