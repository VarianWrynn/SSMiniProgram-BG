require('webpack');

var path = require('path');
var MinifyPlugin = require("babel-minify-webpack-plugin");

var config = {
    entry: './src/BootstrapCookieConsent.js',
     // entry: {
      //  index: './src/BootstrapCookieConsent.js',
        // cookieconsentapi: './node_modules/cookie-consent-api/src/index.js'
     // },
    output: {
        library: 'BootstrapCookieConsent',
        libraryTarget: 'umd',
        libraryExport: 'default',
        path: path.resolve(__dirname, 'dist'),
        filename: 'bootstrap-cookie-consent.js'
    },

    module: {
        rules: [
            {
                test: /\.js$/,
                exclude: /node_modules\/(?![js\-cookie])/,
                use: ['babel-loader']
            }
        ]
    },

};

var devConfig = Object.assign({}, config, {
    mode: 'development',
    output: Object.assign({}, config.output, {
        filename: 'bootstrap-cookie-consent.dev.js'
    }),
    devServer: {
        contentBase: path.join(__dirname, 'dist'),
        open: true,
        openPage: 'demo.html',
        port: 3000
    },
});

// Return Array of Configurations
module.exports = [
    devConfig
];
