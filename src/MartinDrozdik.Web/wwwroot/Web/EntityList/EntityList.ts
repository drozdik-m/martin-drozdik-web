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

    
    private filteredEntities: TEntity[] = []
    private selectedTags: number[] = []
    private entitiesByTags: IdIndexedEntity<TEntity[]> = {}

    private entities: TEntity[] = []
    private entityIndexes: IdIndexedEntity<number> = {};

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

        //Bind "tags"
        let listTagsElement: HTMLElement = this.listElement.querySelector(".listTags");
        if (listTagsElement == null)
        {
            console.error(`No element with class ".listTagsElement" has been found`);
            return;
        }
        let tags = listTagsElement.querySelectorAll("button");
        for (let i = 0; i < tags.length; i++)
        {
            tags[i].addEventListener("click", function ()
            {
                //Get basic info
                let id = this.getAttribute("data-id");
                let idInt = parseInt(id);
                let parent = this.parentElement;
                let isSelected = parent.classList.contains("selected");

                //Toggle selection
                parent.classList.toggle("selected");

                //Handle filter
                if (isSelected)
                    object.RemoveTagFromFilter(idInt);
                else
                    object.AddTagToFilter(idInt);

                //Reset and redraw
                object.ResetPaging();
            });
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
        for (let i = 0; i < this.entities.length; i++)
        {
            let entity = this.entities[i];
            this.entityIndexes[entity.id] = i;
        }

        //Sort items by tags
        if (this.config.tagsFilter)
        {
            this.entitiesByTags = {};
            for (let i = 0; i < this.entities.length; i++)
            {
                let entity = this.entities[i];
                for (let t = 0; t < entity.tags.length; t++)
                {
                    let tagId = entity.tags[t];

                    if (typeof this.entitiesByTags[tagId] == "undefined")
                        this.entitiesByTags[tagId] = [];
                    this.entitiesByTags[tagId].push(entity);
                }
            }
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
    public ResetPaging(): void
    {
        this.from = 0;
        this.loaded = 0;
        this.entityListElement.innerHTML = "";
        this.LoadMore();
    }

    /**
     * Updates filtered entities based on the new tag
     * @param tagId
     */
    protected AddTagToFilter(tagId: number)
    {
        //Add the id to selected tags
        this.selectedTags.push(tagId);

        //Update filtered entities
        this.filteredEntities = this.filteredEntities.filter(function (e) { return e.tags.includes(tagId); });
    }

    /**
     * Updates filtered entities based on the removed tag
     * @param tagId
     */
    protected RemoveTagFromFilter(tagId: number)
    {   
        //Remove the id from selected tags
        this.selectedTags = this.selectedTags.filter(function (e) { return e != tagId });
        
        //Update filtered entities
        if (this.selectedTags.length == 0)
            this.filteredEntities = this.entities;
        else if (this.selectedTags.length == 1)
            this.filteredEntities = this.entitiesByTags[this.selectedTags[0]];
        else
        {
            for (let i = 0; i < this.entitiesByTags[tagId].length; i++)
            {
                let candidateEntity = this.entitiesByTags[tagId][i];

                //Check if the candidate entity has all selected tags
                var suitableCandidate = true;
                for (let t = 0; this.selectedTags.length; t++)
                {
                    let selectedTag = this.selectedTags[t];
                    if (!candidateEntity.tags.includes(selectedTag))
                    {
                        suitableCandidate = false;
                        break;
                    }
                        
                }

                //Add the candidate entity if not already there
                if (suitableCandidate && !this.filteredEntities.includes(candidateEntity))
                    this.filteredEntities.push(candidateEntity);
            }
        }
    }

    /**
     * Load new items into the current list (one whole page)
     * */
    public LoadMore(): void
    {
        this.LoadMoreCount(this.config.pageSize);
    }

    /**
     * Load new items into the current list (defined count)
     * @param count
     */
    public LoadMoreCount(count: number): void
    {
        var newEntities: TEntity[] = [];
        var start = this.from + this.loaded;

        //console.log(this.filteredEntities.length);

        //Load new entities
        for (let i = start; i < start + count && i < this.filteredEntities.length; i++)
        {
            let toAdd = this.filteredEntities[i];
            //console.log("Add entity " + toAdd.id);
            newEntities.push(toAdd);
        }

        //Append new entities
        this.AppendEntities(newEntities);

        //Move the counter
        this.loaded += count;

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