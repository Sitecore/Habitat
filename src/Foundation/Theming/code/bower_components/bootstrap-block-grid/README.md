# bootstrap-block-grid
**block grid** library (css & sass) for [twitter bootstrap](https://github.com/twbs/bootstrap) (version 3 & 4), based on the native [zurb foundation feature](http://foundation.zurb.com/sites/docs/v/5.5.3/components/block_grid.html)

[![npm version](https://badge.fury.io/js/bootstrap-block-grid.png)](https://badge.fury.io/js/bootstrap-block-grid)
[![Bower version](https://badge.fury.io/bo/bootstrap-block-grid.png)](https://badge.fury.io/bo/bootstrap-block-grid)

## demo
[demo on plnkr](http://plnkr.co/qkyOlC)

## usage

1. Install via either [bower](http://bower.io/), [npm](https://www.npmjs.com/), CND or downloaded files:
    1. `bower install --save bootstrap-block-grid`
    2. `npm install --save bootstrap-block-grid`
    3. use CDN files from [jsdelivr](http://www.jsdelivr.com/projects)
    4. download [bootstrap-block-grid.zip](https://github.com/JohnnyTheTank/bootstrap-block-grid/zipball/master)
2. Add files to your html
    1. when using bower
    ```html
    <!-- bootstrap 3 -->
    <link rel="stylesheet" href="bower_components/bootstrap-block-grid/dist/bootstrap3-block-grid.min.css">
    <!-- bootstrap 4 -->
    <link rel="stylesheet" href="bower_components/bootstrap-block-grid/dist/bootstrap4-block-grid.min.css">
    ```

    2. when using npm
    ```html
    <!-- bootstrap 3 -->
    <link rel="stylesheet" href="node_modules/bootstrap-block-grid/dist/bootstrap3-block-grid.min.css">
    <!-- bootstrap 4 -->
    <link rel="stylesheet" href="node_modules/bootstrap-block-grid/dist/bootstrap4-block-grid.min.css">
    ```

    3. when using CDN files from jsdelivr
    ```html
    <!-- bootstrap 3 -->
    <link rel="stylesheet" href="//cdn.jsdelivr.net/bootstrap.block-grid/latest/bootstrap3-block-grid.min.css">
    <!-- bootstrap 4 -->
    <link rel="stylesheet" href="//cdn.jsdelivr.net/bootstrap.block-grid/latest/bootstrap4-block-grid.min.css">
    ```

    4. when using downloaded files
    ```html
    <!-- bootstrap 3 -->
    <link rel="stylesheet" href="bootstrap3-block-grid.min.css">
    <!-- bootstrap 4 -->
    <link rel="stylesheet" href="bootstrap4-block-grid.min.css">
    ```

3. Sample Markup

    ```html
    <div class="block-grid-xs-2 block-grid-sm-3 block-grid-md-4">
        <div>
            Content 1
        </div>
        <div>
            Content 2
        </div>
        <div>
            Content 3
        </div>
        <div>
            Content 4
        </div>
        <div>
            Content 5
        </div>
        <div>
            Content 6
        </div>
    </div>
    ```

## available classes

- *xs*
    - `block-grid-xs-1`
    - `block-grid-xs-2`
    - ...
    - `block-grid-xs-12`
- *sm*
    - `block-grid-sm-1`
    - `block-grid-sm-2`
    - ...
    - `block-grid-sm-12`
- *md*
    - `block-grid-md-1`
    - `block-grid-md-2`
    - ...
    - `block-grid-md-12`
- *lg*
    - `block-grid-lg-1`
    - `block-grid-lg-2`
    - ...
    - `block-grid-lg-12`


# license
MIT
