const path = require('path');
const webpack = require('webpack');

//development/production
const mode = "development"

module.exports = [
    //FRONT-END WEB 
    {
        //Entry points
        entry: {
            Global: "./wwwroot/_dist/Web/Global/Global.js",
            Index: "./wwwroot/_dist/Web/_Pages/Index/Index.js",
            //Sitemap: "./wwwroot/_dist/Sitemap/Sitemap.js"
        },

        //Output
        output: {
            filename: "[name].js",
            path: path.resolve(__dirname, "./wwwroot/_dist/_pack")
        },

        //Mode
        mode: mode,

        //Optimalization
        optimization: {

            //Split chunks
            splitChunks: {
                chunks: "all",
                automaticNameDelimiter: '.',
                name: "common"
            }
        },

        //Watcher
        watch: false,
        watchOptions: watchOptions = {
            ignored: /node_modules/
        },

        target: ["web", "es5"]
    },

    //SERVICE WORKER
    /*{
        //Entry points
        entry: {
            ServiceWorker: "./wwwroot/_dist/ServiceWorker/ServiceWorkerLink.js"
        },

        //Output
        output: {
            filename: "[name].js",
            path: path.resolve(__dirname, "./wwwroot")
        },

        //Mode
        mode: mode,

        //Watcher
        watch: false,
        watchOptions: watchOptions = {
            ignored: /node_modules/
        }
    }*/
];