const path = require('path');
const webpack = require('webpack');

//development/production
const mode = "development"
const outPath = path.resolve(__dirname, "./wwwroot/_out")

module.exports = [
    //FRONT-END WEB SCRIPTS
    {
        //Entry points
        entry: {

            //Scripts
            Global: "./wwwroot/_dist/Web/Global/Global.js",
            ErrorPage: "./wwwroot/_dist/Web/ErrorPage/ErrorPage.js",
            OfflinePage: "./wwwroot/_dist/Web/OfflinePage/OfflinePage.js",
        },

        //Output
        output: {
            filename: "js/[name].js",
            path: outPath
        },

        //Mode
        mode: mode,

        //Optimalization
        optimization: {

            //Split chunks
            splitChunks: {
                chunks: "all",
                automaticNameDelimiter: ".",
                name: "common"
            }
        },

        //Watcher
        watch: false,
        watchOptions: watchOptions = {
            ignored: /node_modules/
        },

        //Target
        target: ["web", "es5"],
    },

    //FRONT-END WEB STYLES
    {
        //Entry points
        entry: {
            Global: "./wwwroot/Web/Global/Global.scss",
            IndexPage: "./wwwroot/Web/ErrorPage/ErrorPage.scss",
            SitemapPage: "./wwwroot/Web/OfflinePage/OfflinePage.scss",
        },

        //Output
        output: {
            filename: "css/[name].js",
            path: outPath
        },

        //Mode
        mode: mode,

        //Watcher
        watch: false,
        watchOptions: watchOptions = {
            ignored: /node_modules/
        },

        //Module
        module: {
            rules: [
                {
                    //SCSS -> CSS compilation
                    test: /\.s[ac]ss$/i,
                    exclude: /node_modules/,
                    use: [
                        {
                            loader: "file-loader",
                            options: { outputPath: 'css/', name: '[name].css' }
                        },
                        {
                            loader: "sass-loader",
                            options: {
                                // Prefer `dart-sass`
                                implementation: require("sass"),
                                sourceMap: true,
                                sassOptions: {
                                    outputStyle: "compressed",
                                },
                            },
                        },
                    ]

                },
            ],
        }
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