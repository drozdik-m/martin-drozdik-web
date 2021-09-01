//import "../../ServiceWorker/ServiceWorkerRegistrator";
//import "../CookiesBar/CookiesBar";
//import "../TopInfoBar/TopInfoBar";
import "../Menu/Menu";
//import "../Footer/Footer";
//import "./ArticleBase";

import { WindowEvents } from "@drozdik.m/window-events";
import { SlowScroll } from "@drozdik.m/slow-scroll";
import { GoogleAnalytics, StorageType } from "@drozdik.m/google-analytics";

WindowEvents.OnDOMReady.Add(function ()
{
    //Add slow scrolling
    SlowScroll.AnchorScroll("slowScroll");

    //Add google analytics
    const googleAnalytics = new GoogleAnalytics("G-597EYKW04M", StorageType.Local, true);
    googleAnalytics.StartTracking();
})