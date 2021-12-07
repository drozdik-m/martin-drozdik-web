import { WindowEvents } from "@drozdik.m/window-events";
import { OnScrollAtBottomWatcherOnce } from "@drozdik.m/on-scroll-to";
import { Animation } from "@drozdik.m/animation";
import { EaseOutCubic } from "@drozdik.m/lerp";

/**
 * Registeres an element for animation event on scroll
 * @param statElement
 */
export function RegisterStatForAnimation(statElement: HTMLElement)
{
    let statNumber = statElement.querySelector(".statNumber");
    if (!statNumber)
    {
        console.error("No .statNumber found")
        return;
    }


    let watcher = new OnScrollAtBottomWatcherOnce(statElement);
    watcher.OnScroll.Add(function ()
    {
        //Get numbers
        let startNumber = 0;
        let endNumber = parseInt(statNumber.textContent);

        //Setup animation
        let animation = new Animation(2000, EaseOutCubic);
        animation.OnFrame.Add(function (caller, args)
        {
            statNumber.textContent = (Math.round(args.Value())).toString();
        });
        animation.OnEnd.Add(function ()
        {
            statNumber.textContent = endNumber.toString();
        });

        //Start the animation
        animation.Start(startNumber, endNumber);
    });
}

/*WindowEvents.OnDOMReady.Add(function ()
{
    
});
*/