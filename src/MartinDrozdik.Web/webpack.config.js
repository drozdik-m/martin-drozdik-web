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
            IndexPage: "./wwwroot/_dist/Web/_Pages/IndexPage/IndexPage.js",
            SitemapPage: "./wwwroot/_dist/Web/_Pages/SitemapPage/SitemapPage.js",
            StyleTest: "./wwwroot/Web/Global/Block.scss"
            //https://stackoverflow.com/questions/50394789/webpack-4-compile-scss-to-separate-css-file ???
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

        //Target
        target: ["web", "es5"],

        //Module
        module: {
            rules: [
                {
                    //SCSS -> CSS compilation
                    test: /\.s[ac]ss$/i,
                    use: [
                        "style-loader",
                        "css-loader",
                        {
                            loader: "sass-loader",
                            options: {
                                // Prefer `dart-sass`
                                implementation: require("sass"),
                            },
                        },
                    ],
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