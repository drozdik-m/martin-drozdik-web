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
            Global: "./wwwroot/_dist/Web/Global/Global.js",
            Login: "./wwwroot/_dist/Web/Login/Login.js",
            Register: "./wwwroot/_dist/Web/Register/Register.js",
            Denied: "./wwwroot/_dist/Web/Denied/Denied.js",
            ChangePassword: "./wwwroot/_dist/Web/ChangePassword/ChangePassword.js",
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
            Login: "./wwwroot/Web/Login/Login.scss",
            Register: "./wwwroot/Web/Register/Register.scss",
            Denied: "./wwwroot/Web/Denied/Denied.scss",
            ChangePassword: "./wwwroot/Web/ChangePassword/ChangePassword.scss",
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
];