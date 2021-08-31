//import "../../ServiceWorker/ServiceWorkerRegistrator";
//import "../CookiesBar/CookiesBar";
//import "../TopInfoBar/TopInfoBar";
//import "../Menu/Menu";
//import "../Footer/Footer";
//import "./ArticleBase";
exports.__esModule = true;
var window_events_1 = require("@drozdik.m/window-events");
var slow_scroll_1 = require("@drozdik.m/slow-scroll");
var google_analytics_1 = require("@drozdik.m/google-analytics");
window_events_1.WindowEvents.OnDOMReady.Add(function () {
    //Add slow scrolling
    slow_scroll_1.SlowScroll.AnchorScroll("slowScroll");
    //Add google analytics
    var googleAnalytics = new google_analytics_1.GoogleAnalytics("G-597EYKW04M", google_analytics_1.StorageType.Local, true);
    googleAnalytics.StartTracking();
});
