

export class EntityTag
{
    constructor(id: number, element: HTMLElement)
    {
        this.id = id;
        this.element = element;
    }

    public id: number;
    public selected: boolean = false;
    public unsuitable: boolean = false;
    public element: HTMLElement;
}