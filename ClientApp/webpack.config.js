const webpack = require('webpack')
const path = require('path')
const ProgressBar = require('progress-bar-webpack-plugin')
const HtmlWebpackPlugin = require('html-webpack-plugin')
const CleanWebpackPlugin = require('clean-webpack-plugin')

module.exports = {
  entry: './src/App.jsx',
  module: {
    rules: [
      {
        test: /\.(js|jsx)$/,
        exclude: /node_modules/,
        use: ['babel-loader']
      }, {
        test: /\.css$/,
        use: ['style-loader', 'css-loader']
      }, {
        test: /\.(eot|svg|ttf|woff|woff2)$/,
        use: 'file-loader?name=public/fonts/[name].[ext]'
      }
    ]
  },
  resolve: {
    extensions: ['*', '.js', '.jsx', '.ttf']
  },
  output: {
    path: path.join(__dirname, '..', 'Api', 'wwwroot'),
    publicPath: '/',
    filename: `bundle${(new Date()).getTime()}.js`
  },
  plugins: [
    new webpack.HotModuleReplacementPlugin(),
    new ProgressBar(),
    new HtmlWebpackPlugin({
      template: path.join(__dirname, 'public', 'index.html'),
      title: 'Cashflow'
    }),
    new CleanWebpackPlugin()
  ],
  devServer: {
    contentBase: './public',
    hot: true,
    proxy: {
      '/api': 'http://localhost:5000'
    }
  }
}