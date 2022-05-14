import { EntityListConfig } from "./EntityListConfig";
import { IdIndexedEntity } from "./IdIndexedEntity";
import { IIdentifiable } from "./IIdentifiable";


export abstract class EntityList<TEntity extends IIdentifiable<number>, TConfig extends EntityListConfig<number, TEntity>>
{

    private listElement: HTMLElement;
    private entityListElement: HTMLElement;

    private config: EntityListConfig<number, TEntity>;

    private from: number = 0;
    private loaded: number;

    private entities: TEntity[] = []
    private filteredEntities: TEntity[] = []
    private entityIndexes: IdIndexedEntity = {};

    constructor(listElement: HTMLElement, config: EntityListConfig<number, TEntity>)
    {
        this.listElement = listElement;
        this.config = config;

        //Check entities
        if (typeof config.entities == "undefined")
            throw new Error("Entities are not defined");

        //Do not use "load more button" by defalt
        if (typeof config.loadMoreButton == "undefined")
            config.loadMoreButton = false;

        //Default page size is 3
        if (typeof config.pageSize == "undefined")
            config.pageSize = 3;
        this.loaded = config.pageSize;

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
        var loadMoreButton = this.listElement.querySelector(".loadMoreProjectsButton");
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

        //Sort items by their ID
        this.entities = this.config.entities;
        for (let i = 0; i < this.entities.length; i++)
        {
            let entity = this.entities[i];
            this.entityIndexes[entity.Id] = i;
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
            result += this.GetHTMLFor(entities[i]);

        this.entityListElement.insertAdjacentHTML("beforeend", result);
    }

    /**
     * Returns HTML representation of an entity
     * @param entity
     */
    protected abstract GetHTMLFor(entity: TEntity): string;

    /**
     * Load new items into the current list (one whole page)
     * */
    public LoadMore(): void
    {
        var newEntities: TEntity[] = [];
        var newEntitiesCount = this.config.pageSize;
        var start = this.from + this.loaded;

        //Load new entities
        for (let i = start; i < start + newEntitiesCount && i < this.filteredEntities.length; i++)
            newEntities.push(this.filteredEntities[i]);

        //Append new entities
        this.AppendEntities(newEntities);

        //Move the counter
        this.loaded += newEntitiesCount;
    }
}