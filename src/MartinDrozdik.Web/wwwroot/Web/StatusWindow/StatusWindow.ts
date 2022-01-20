import { IDisposable } from "@drozdik.m/common-interfaces/IDisposable";
import { DialogWindow } from "@drozdik.m/dialog-window";
import { StatusWindowContent } from "./StatusWindowContent";


export class StatusWindow implements IDisposable
{
    private nameId: string;
    private dialogTypeClass: string;
    private content: StatusWindowContent

    private instantiated: boolean = false;


    constructor(nameId: string, dialogTypeClass: string, content: StatusWindowContent)
    {
        this.nameId = nameId;
        this.content = content;
        this.dialogTypeClass = dialogTypeClass;
    }

    Dispose(): void
    {
        if (!this.instantiated)
            return;

        let element = document.getElementById(this.nameId);
        element.parentNode.removeChild(element);
        this.instantiated = false;
    }

    Display(): void
    {
        document.body.insertAdjacentHTML("beforeend", this.GetHTML());
        let dialogWindow = new DialogWindow(this.nameId);
        let object = this;
        dialogWindow.OnClose.Add(function ()
        {
            setTimeout(function ()
            {
                object.Dispose();
            }, 1000);
        });
        dialogWindow.Open();
    }

    protected GetHTML(): string
    {
        let dialogClass = this.content.dialogClass == null ? "" : this.content.dialogClass
        return `<div id="${this.nameId}" class="statusWindow ${this.dialogTypeClass} ${dialogClass}"> \
                    ${this.content.heading == null ? "" : `<span class="statusWindowHeading">${this.content.heading}</span>`} \
                    ${this.content.message == null ? "" : `<span class="statusWindowText">${this.content.message}</span>`} \
                    ${this.content.closeButtonText == null ? "" : `<button class="commonButton commonButtonSlim commonButtonBlack dialogClose">${this.content.closeButtonText}</button>`} \
                     \
                </div>`;
    }
}