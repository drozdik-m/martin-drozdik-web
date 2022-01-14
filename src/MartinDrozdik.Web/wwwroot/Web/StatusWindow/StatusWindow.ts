


export class StatusWindow
{
    private nameId: string;

    private heading: string = null;
    private text: string = null;
    private closeButtonText: string = null;
    private dialogClass: string = null;

    private instantiated: boolean = false;
    private reinstantiate: boolean = true;

    constructor(nameId: string)
    {
        this.nameId = nameId;
    }

    SetHeading(heading: string): void
    {
        this.heading = heading;
    }

    SetText(text: string): void
    {
        this.text = text;
    }

    SetCloseButtonText(closeButtonText: string): void
    {
        this.closeButtonText = closeButtonText;
    }

    SetClass(dialogClass: string): void
    {
        this.dialogClass = dialogClass;
    }


    private Reinstantiate(): void
    {

    }

    private static GetHTML(id: string, headingContent: string, text: string, activeButtonText: string, closeButtonText: string, dialogClass: string, buttonClass: string): string
    {
        return `<div id="${id}" class="statusWindow"> \
                    ${headingContent == null ? "" : `<span class="statusWindowHeading">${headingContent}</span>`} \
                    ${text == null ? "" : `<p class="statusWindowText">${text}</p>`} \
                    <button class="commonButton ${buttonClass} dialogClose"></button> \
                </div>`;
    }
}