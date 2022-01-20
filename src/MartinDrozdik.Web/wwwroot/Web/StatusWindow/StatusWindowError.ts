import { StatusWindow } from "./StatusWindow";
import { StatusWindowContent } from "./StatusWindowContent";


export class StatusWindowError extends StatusWindow
{
    constructor(nameId: string, content: StatusWindowContent)
    {
        super(nameId, "statusWindowError", content);
    }
}