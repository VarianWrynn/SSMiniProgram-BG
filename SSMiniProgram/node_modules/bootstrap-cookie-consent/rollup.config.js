const path    = require('path')
const babel   = require('rollup-plugin-babel')
const resolve = require('rollup-plugin-node-resolve')
const commonjs = require('rollup-plugin-commonjs')
const nodeResolve = require('rollup-plugin-node-resolve');

const plugins = [
  commonjs({
    include: [
        "src/BootstrapCookieConsent.js",
        "node_modules/cookie-consent-api/**" ,
        "node_modules/events/**" ,
        "node_modules/js-cookie/**"
    ],
    ignoreGlobal: false
  }),
  nodeResolve({
      jsnext: true,
      main: false
    }),
  babel({
    exclude: 'node_modules/**'
  })
]

module.exports = {
  input: path.resolve(__dirname, 'src/BootstrapCookieConsent.js'),
  output: {
    file: path.resolve(__dirname, 'dist/bootstrap-cookie-consent.js'),
    format: 'umd',
    name: 'BootstrapCookieConsent'
  },
  plugins
}
