# bootstrap-block-grid
**block grid** library (css & sass) for [twitter bootstrap](https://github.com/twbs/bootstrap) (version 3 & 4), based on the native [zurb foundation feature](http://foundation.zurb.com/sites/docs/v/5.5.3/components/block_grid.html)

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
    <script src="bower_components/bootstrap-block-grid/dist/bootstrap3-block-grid.min.css"></script>
    <!-- bootstrap 4 -->
    <script src="bower_components/bootstrap-block-grid/dist/bootstrap4-block-grid.min.css"></script>
    ```

    2. when using npm
    ```html
    <!-- bootstrap 3 -->
    <script src="node_modules/bootstrap-block-grid/dist/bootstrap3-block-grid.min.css"></script>
    <!-- bootstrap 4 -->
    <script src="node_modules/bootstrap-block-grid/dist/bootstrap4-block-grid.min.css"></script>
    ```

    3. when using CDN files from jsdelivr
    ```html
    <!-- bootstrap 3 -->
    <script src="//cdn.jsdelivr.net/bootstrap.block-grid/1.1.4/bootstrap3-block-grid.min.css"></script>
    <!-- bootstrap 4 -->
    <script src="//cdn.jsdelivr.net/bootstrap.block-grid/1.1.4/bootstrap4-block-grid.min.css"></script>
    ```

    4. when using downloaded files
    ```html
    <!-- bootstrap 3 -->
    <script src="bootstrap3-block-grid.min.css"></script>
    <!-- bootstrap 4 -->
    <script src="bootstrap4-block-grid.min.css"></script>
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


# licence
MIT
