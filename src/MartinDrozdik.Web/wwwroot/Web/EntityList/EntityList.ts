import { EntityListConfig } from "./EntityListConfig";
import { IdIndexedEntity } from "./IdIndexedEntity";
import { ListEntity } from "./ListEntity";


export abstract class EntityList<TEntity extends ListEntity, TConfig extends EntityListConfig<TEntity>>
{

    private listElement: HTMLElement;
    private entityListElement: HTMLElement;

    private config: TConfig;

    private loadMoreButtonElement: HTMLElement;
    private loadMoreButtonVisible: boolean = true;

    private from: number = 0;
    private loaded: number;

    private entities: TEntity[] = []
    private filteredEntities: TEntity[] = []
    private entityIndexes: IdIndexedEntity = {};

    constructor(listElement: HTMLElement, config: TConfig)
    {
        //Handle initial values
        this.listElement = listElement;
        this.config = config;

        this.loaded = config.initialSize;
        this.SetEntities(config.entities);

        //Initialize this object
        this.Initialize();
    }

    /**
     * Initialized this component with DOM-related operations
     * */
    protected Initialize(): void
    {
        var object = this;

        //Bind "load more" button
        let loadMoreButton: HTMLElement = this.listElement.querySelector(".loadMoreProjectsButton");
        this.loadMoreButtonElement = loadMoreButton;
        if (loadMoreButton == null)
        {
            console.error(`No button with class ".loadMoreProjectsButton" has been found`);
            return;
        }
        loadMoreButton.addEventListener("click", function ()
        {
            object.LoadMore();
        });

        //Search for list entity
        this.entityListElement = this.listElement.querySelector(".entityList");
        if (this.entityListElement == null)
        {
            console.error(`No element with class ".entityList" has been found`);
            return;
        }
    }

    /**
     * Sets new entitis to the list
     * @param entitiy
     */
    public SetEntities(entities: TEntity[]): void
    {
        //Set new entities
        this.entities = entities;

        //Reset the filter
        this.filteredEntities = this.entities;

        //Sort items by their ID
        this.entities = this.config.entities;
        for (let i = 0; i < this.entities.length; i++)
        {
            let entity = this.entities[i];
            this.entityIndexes[entity.id] = i;
        }
    }

    /**
     * Returns the ending index of target load entity
     * */
    protected To(): number
    {
        return this.from + this.loaded;
    }

    /**
     * Returns entity instance based on its ID -- O(1)
     * @param id
     */
    protected GetEntityById(id: number): TEntity
    {
        var index = this.entityIndexes[id];
        return this.entities[index];
    }

    /**
     * Appends entities to the HTML entity list
     * @param entities
     */
    protected AppendEntities(entities: TEntity[]): void
    {
        var result = "";
        for (let i = 0; i < entities.length; i++)
            result += `<li>${entities[i].html}</li>`;

        this.entityListElement.insertAdjacentHTML("beforeend", result);
    }

    /**
     * Resets and corrects the state of the list
     * */
    public Reset(): void
    {
        this.from = 0;
        this.loaded = 0;
        this.filteredEntities = this.entities;
        this.listElement.innerHTML = "";
    }

    /**
     * Load new items into the current list (one whole page)
     * */
    public LoadMore(): void
    {
        var newEntities: TEntity[] = [];
        var newEntitiesCount = this.config.pageSize;
        var start = this.from + this.loaded;

        console.log(this.filteredEntities.length);

        //Load new entities
        for (let i = start; i < start + newEntitiesCount && i < this.filteredEntities.length; i++)
        {
            let toAdd = this.filteredEntities[i];
            console.log("Add entity " + toAdd.id);
            newEntities.push(toAdd);
        }

        //Append new entities
        this.AppendEntities(newEntities);

        //Move the counter
        this.loaded += newEntitiesCount;

        //Check the load more button visibility
        this.CheckLoadMoreButtonVisibility();
    }

    /**
     * Checks if the load more button should be visible or not and corrects if needed
     * */
    protected CheckLoadMoreButtonVisibility()
    {
        if (!this.config.loadMoreButton)
            return;

        if (this.loaded >= this.filteredEntities.length && this.loadMoreButtonVisible)
        {
            this.loadMoreButtonElement.style.display = "none";
            this.loadMoreButtonVisible = false;
        }
        else if (this.loaded < this.filteredEntities.length && !this.loadMoreButtonVisible)
        {
            this.loadMoreButtonElement.style.display = "inline-block";
            this.loadMoreButtonVisible = true;
        }

        //this.loadMoreButtonElement
        //this.loadMoreButtonVisible
    }
}