//import "../../ServiceWorker/ServiceWorkerRegistrator";
//import "../CookiesBar/CookiesBar";
//import "../TopInfoBar/TopInfoBar";
import "../Menu/Menu";
import "../Footer/Footer";
//import "./ArticleBase";

import "../Contact/ContactValuesFiller";

import { WindowEvents } from "@drozdik.m/window-events";
import { SlowScroll } from "@drozdik.m/slow-scroll";
import { GoogleAnalytics, StorageType } from "@drozdik.m/google-analytics";


WindowEvents.OnDOMReady.Add(function ()
{
    //Add slow scrolling
    SlowScroll.AnchorScroll("slowScroll");

    //Add google analytics
    const googleAnalytics = new GoogleAnalytics("UA-206361583-1", StorageType.Local, true);
    googleAnalytics.StartTracking();
})