import { StatusWindow } from "./StatusWindow";
import { StatusWindowContent } from "./StatusWindowContent";


export class StatusWindowSuccess extends StatusWindow
{
    constructor(nameId: string, content: StatusWindowContent)
    {
        super(nameId, "statusWindowSuccess", content);
    }
}