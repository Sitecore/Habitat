/*!
 * Shuffle.js by @Vestride
 * Categorize, sort, and filter a responsive grid of items.
 * Dependencies: jQuery 1.9+, Modernizr 2.6.2+
 * @license MIT license
 * @version 3.1.1
 */
(function (factory) {
  if (typeof define === 'function' && define.amd) {
    define(['jquery', 'modernizr'], factory);
  } else if (typeof exports === 'object') {
    module.exports = factory(require('jquery'), window.Modernizr);
  } else {
    window.Shuffle = factory(window.jQuery, window.Modernizr);
  }
})(function($, Modernizr, undefined) {

'use strict';


// Validate Modernizr exists.
// Shuffle requires `csstransitions`, `csstransforms`, `csstransforms3d`,
// and `prefixed` to exist on the Modernizr object.
if (typeof Modernizr !== 'object') {
  throw new Error('Shuffle.js requires Modernizr.\n' +
      'http://vestride.github.io/Shuffle/#dependencies');
}


/**
 * Returns css prefixed properties like `-webkit-transition` or `box-sizing`
 * from `transition` or `boxSizing`, respectively.
 * @param {(string|boolean)} prop Property to be prefixed.
 * @return {string} The prefixed css property.
 */
function dashify( prop ) {
  if (!prop) {
    return '';
  }

  // Replace upper case with dash-lowercase,
  // then fix ms- prefixes because they're not capitalized.
  return prop.replace(/([A-Z])/g, function( str, m1 ) {
    return '-' + m1.toLowerCase();
  }).replace(/^ms-/,'-ms-');
}

// Constant, prefixed variables.
var TRANSITION = Modernizr.prefixed('transition');
var TRANSITION_DELAY = Modernizr.prefixed('transitionDelay');
var TRANSITION_DURATION = Modernizr.prefixed('transitionDuration');

// Note(glen): Stock Android 4.1.x browser will fail here because it wrongly
// says it supports non-prefixed transitions.
// https://github.com/Modernizr/Modernizr/issues/897
var TRANSITIONEND = {
  'WebkitTransition' : 'webkitTransitionEnd',
  'transition' : 'transitionend'
}[ TRANSITION ];

var TRANSFORM = Modernizr.prefixed('transform');
var CSS_TRANSFORM = dashify(TRANSFORM);

// Constants
var CAN_TRANSITION_TRANSFORMS = Modernizr.csstransforms && Modernizr.csstransitions;
var HAS_TRANSFORMS_3D = Modernizr.csstransforms3d;
var HAS_COMPUTED_STYLE = !!window.getComputedStyle;
var SHUFFLE = 'shuffle';

// Configurable. You can change these constants to fit your application.
// The default scale and concealed scale, however, have to be different values.
var ALL_ITEMS = 'all';
var FILTER_ATTRIBUTE_KEY = 'groups';
var DEFAULT_SCALE = 1;
var CONCEALED_SCALE = 0.001;

// Underscore's throttle function.
function throttle(func, wait, options) {
  var context, args, result;
  var timeout = null;
  var previous = 0;
  options = options || {};
  var later = function() {
    previous = options.leading === false ? 0 : $.now();
    timeout = null;
    result = func.apply(context, args);
    context = args = null;
  };
  return function() {
    var now = $.now();
    if (!previous && options.leading === false) {
      previous = now;
    }
    var remaining = wait - (now - previous);
    context = this;
    args = arguments;
    if (remaining <= 0 || remaining > wait) {
      clearTimeout(timeout);
      timeout = null;
      previous = now;
      result = func.apply(context, args);
      context = args = null;
    } else if (!timeout && options.trailing !== false) {
      timeout = setTimeout(later, remaining);
    }
    return result;
  };
}

function each(obj, iterator, context) {
  for (var i = 0, length = obj.length; i < length; i++) {
    if (iterator.call(context, obj[i], i, obj) === {}) {
      return;
    }
  }
}

function defer(fn, context, wait) {
  return setTimeout( $.proxy( fn, context ), wait );
}

function arrayMax( array ) {
  return Math.max.apply( Math, array );
}

function arrayMin( array ) {
  return Math.min.apply( Math, array );
}


/**
 * Always returns a numeric value, given a value.
 * @param {*} value Possibly numeric value.
 * @return {number} `value` or zero if `value` isn't numeric.
 * @private
 */
function getNumber(value) {
  return $.isNumeric(value) ? value : 0;
}

var getStyles = window.getComputedStyle || function() {};

/**
 * Represents a coordinate pair.
 * @param {number} [x=0] X.
 * @param {number} [y=0] Y.
 */
var Point = function(x, y) {
  this.x = getNumber( x );
  this.y = getNumber( y );
};


/**
 * Whether two points are equal.
 * @param {Point} a Point A.
 * @param {Point} b Point B.
 * @return {boolean}
 */
Point.equals = function(a, b) {
  return a.x === b.x && a.y === b.y;
};

var COMPUTED_SIZE_INCLUDES_PADDING = (function() {
  if (!HAS_COMPUTED_STYLE) {
    return false;
  }

  var parent = document.body || document.documentElement;
  var e = document.createElement('div');
  e.style.cssText = 'width:10px;padding:2px;' +
    '-webkit-box-sizing:border-box;box-sizing:border-box;';
  parent.appendChild(e);

  var width = getStyles(e, null).width;
  var ret = width === '10px';

  parent.removeChild(e);

  return ret;
}());


// Used for unique instance variables
var id = 0;
var $window = $( window );


/**
 * Categorize, sort, and filter a responsive grid of items.
 *
 * @param {Element} element An element which is the parent container for the grid items.
 * @param {Object} [options=Shuffle.options] Options object.
 * @constructor
 */
var Shuffle = function( element, options ) {
  options = options || {};
  $.extend( this, Shuffle.options, options, Shuffle.settings );

  this.$el = $(element);
  this.element = element;
  this.unique = 'shuffle_' + id++;

  this._fire( Shuffle.EventType.LOADING );
  this._init();

  // Dispatch the done event asynchronously so that people can bind to it after
  // Shuffle has been initialized.
  defer(function() {
    this.initialized = true;
    this._fire( Shuffle.EventType.DONE );
  }, this, 16);
};


/**
 * Events the container element emits with the .shuffle namespace.
 * For example, "done.shuffle".
 * @enum {string}
 */
Shuffle.EventType = {
  LOADING: 'loading',
  DONE: 'done',
  LAYOUT: 'layout',
  REMOVED: 'removed'
};


/** @enum {string} */
Shuffle.ClassName = {
  BASE: SHUFFLE,
  SHUFFLE_ITEM: 'shuffle-item',
  FILTERED: 'filtered',
  CONCEALED: 'concealed'
};


// Overrideable options
Shuffle.options = {
  group: ALL_ITEMS, // Initial filter group.
  speed: 250, // Transition/animation speed (milliseconds).
  easing: 'ease-out', // CSS easing function to use.
  itemSelector: '', // e.g. '.picture-item'.
  sizer: null, // Sizer element. Use an element to determine the size of columns and gutters.
  gutterWidth: 0, // A static number or function that tells the plugin how wide the gutters between columns are (in pixels).
  columnWidth: 0, // A static number or function that returns a number which tells the plugin how wide the columns are (in pixels).
  delimeter: null, // If your group is not json, and is comma delimeted, you could set delimeter to ','.
  buffer: 0, // Useful for percentage based heights when they might not always be exactly the same (in pixels).
  columnThreshold: HAS_COMPUTED_STYLE ? 0.01 : 0.1, // Reading the width of elements isn't precise enough and can cause columns to jump between values.
  initialSort: null, // Shuffle can be initialized with a sort object. It is the same object given to the sort method.
  throttle: throttle, // By default, shuffle will throttle resize events. This can be changed or removed.
  throttleTime: 300, // How often shuffle can be called on resize (in milliseconds).
  sequentialFadeDelay: 150, // Delay between each item that fades in when adding items.
  supported: CAN_TRANSITION_TRANSFORMS // Whether to use transforms or absolute positioning.
};


// Not overrideable
Shuffle.settings = {
  useSizer: false,
  itemCss : { // default CSS for each item
    position: 'absolute',
    top: 0,
    left: 0,
    visibility: 'visible'
  },
  revealAppendedDelay: 300,
  lastSort: {},
  lastFilter: ALL_ITEMS,
  enabled: true,
  destroyed: false,
  initialized: false,
  _animations: [],
  _transitions: [],
  _isMovementCanceled: false,
  styleQueue: []
};


// Expose for testing.
Shuffle.Point = Point;


/**
 * Static methods.
 */

/**
 * If the browser has 3d transforms available, build a string with those,
 * otherwise use 2d transforms.
 * @param {Point} point X and Y positions.
 * @param {number} scale Scale amount.
 * @return {string} A normalized string which can be used with the transform style.
 * @private
 */
Shuffle._getItemTransformString = function(point, scale) {
  if ( HAS_TRANSFORMS_3D ) {
    return 'translate3d(' + point.x + 'px, ' + point.y + 'px, 0) scale3d(' + scale + ', ' + scale + ', 1)';
  } else {
    return 'translate(' + point.x + 'px, ' + point.y + 'px) scale(' + scale + ')';
  }
};


/**
 * Retrieve the computed style for an element, parsed as a float.
 * @param {Element} element Element to get style for.
 * @param {string} style Style property.
 * @param {CSSStyleDeclaration} [styles] Optionally include clean styles to
 *     use instead of asking for them again.
 * @return {number} The parsed computed value or zero if that fails because IE
 *     will return 'auto' when the element doesn't have margins instead of
 *     the computed style.
 * @private
 */
Shuffle._getNumberStyle = function( element, style, styles ) {
  if ( HAS_COMPUTED_STYLE ) {
    styles = styles || getStyles( element, null );
    var value = Shuffle._getFloat( styles[ style ] );

    // Support IE<=11 and W3C spec.
    if ( !COMPUTED_SIZE_INCLUDES_PADDING && style === 'width' ) {
      value += Shuffle._getFloat( styles.paddingLeft ) +
        Shuffle._getFloat( styles.paddingRight ) +
        Shuffle._getFloat( styles.borderLeftWidth ) +
        Shuffle._getFloat( styles.borderRightWidth );
    } else if ( !COMPUTED_SIZE_INCLUDES_PADDING && style === 'height' ) {
      value += Shuffle._getFloat( styles.paddingTop ) +
        Shuffle._getFloat( styles.paddingBottom ) +
        Shuffle._getFloat( styles.borderTopWidth ) +
        Shuffle._getFloat( styles.borderBottomWidth );
    }

    return value;
  } else {
    return Shuffle._getFloat( $( element ).css( style )  );
  }
};


/**
 * Parse a string as an float.
 * @param {string} value String float.
 * @return {number} The string as an float or zero.
 * @private
 */
Shuffle._getFloat = function(value) {
  return getNumber( parseFloat( value ) );
};


/**
 * Returns the outer width of an element, optionally including its margins.
 *
 * There are a few different methods for getting the width of an element, none of
 * which work perfectly for all Shuffle's use cases.
 *
 * 1. getBoundingClientRect() `left` and `right` properties.
 *   - Accounts for transform scaled elements, making it useless for Shuffle
 *   elements which have shrunk.
 * 2. The `offsetWidth` property (or jQuery's CSS).
 *   - This value stays the same regardless of the elements transform property,
 *   however, it does not return subpixel values.
 * 3. getComputedStyle()
 *   - This works great Chrome, Firefox, Safari, but IE<=11 does not include
 *   padding and border when box-sizing: border-box is set, requiring a feature
 *   test and extra work to add the padding back for IE and other browsers which
 *   follow the W3C spec here.
 *
 * @param {Element} element The element.
 * @param {boolean} [includeMargins] Whether to include margins. Default is false.
 * @return {number} The width.
 */
Shuffle._getOuterWidth = function( element, includeMargins ) {
  // Store the styles so that they can be used by others without asking for it again.
  var styles = getStyles( element, null );
  var width = Shuffle._getNumberStyle( element, 'width', styles );

  // Use jQuery here because it uses getComputedStyle internally and is
  // cross-browser. Using the style property of the element will only work
  // if there are inline styles.
  if ( includeMargins ) {
    var marginLeft = Shuffle._getNumberStyle( element, 'marginLeft', styles );
    var marginRight = Shuffle._getNumberStyle( element, 'marginRight', styles );
    width += marginLeft + marginRight;
  }

  return width;
};


/**
 * Returns the outer height of an element, optionally including its margins.
 * @param {Element} element The element.
 * @param {boolean} [includeMargins] Whether to include margins. Default is false.
 * @return {number} The height.
 */
Shuffle._getOuterHeight = function( element, includeMargins ) {
  var styles = getStyles( element, null );
  var height = Shuffle._getNumberStyle( element, 'height', styles );

  if ( includeMargins ) {
    var marginTop = Shuffle._getNumberStyle( element, 'marginTop', styles );
    var marginBottom = Shuffle._getNumberStyle( element, 'marginBottom', styles );
    height += marginTop + marginBottom;
  }

  return height;
};


/**
 * Change a property or execute a function which will not have a transition
 * @param {Element} element DOM element that won't be transitioned
 * @param {Function} callback A function which will be called while transition
 *     is set to 0ms.
 * @param {Object} [context] Optional context for the callback function.
 * @private
 */
Shuffle._skipTransition = function( element, callback, context ) {
  var duration = element.style[ TRANSITION_DURATION ];

  // Set the duration to zero so it happens immediately
  element.style[ TRANSITION_DURATION ] = '0ms'; // ms needed for firefox!

  callback.call( context );

  // Force reflow
  var reflow = element.offsetWidth;
  // Avoid jshint warnings: unused variables and expressions.
  reflow = null;

  // Put the duration back
  element.style[ TRANSITION_DURATION ] = duration;
};


/**
 * Instance methods.
 */

Shuffle.prototype._init = function() {
  this.$items = this._getItems();

  this.sizer = this._getElementOption( this.sizer );

  if ( this.sizer ) {
    this.useSizer = true;
  }

  // Add class and invalidate styles
  this.$el.addClass( Shuffle.ClassName.BASE );

  // Set initial css for each item
  this._initItems();

  // Bind resize events
  // http://stackoverflow.com/questions/1852751/window-resize-event-firing-in-internet-explorer
  $window.on('resize.' + SHUFFLE + '.' + this.unique, this._getResizeFunction());

  // Get container css all in one request. Causes reflow
  var containerCSS = this.$el.css(['position', 'overflow']);
  var containerWidth = Shuffle._getOuterWidth( this.element );

  // Add styles to the container if it doesn't have them.
  this._validateStyles( containerCSS );

  // We already got the container's width above, no need to cause another reflow getting it again...
  // Calculate the number of columns there will be
  this._setColumns( containerWidth );

  // Kick off!
  this.shuffle( this.group, this.initialSort );

  // The shuffle items haven't had transitions set on them yet
  // so the user doesn't see the first layout. Set them now that the first layout is done.
  if ( this.supported ) {
    defer(function() {
      this._setTransitions();
      this.element.style[ TRANSITION ] = 'height ' + this.speed + 'ms ' + this.easing;
    }, this);
  }
};


/**
 * Returns a throttled and proxied function for the resize handler.
 * @return {Function}
 * @private
 */
Shuffle.prototype._getResizeFunction = function() {
  var resizeFunction = $.proxy( this._onResize, this );
  return this.throttle ?
      this.throttle( resizeFunction, this.throttleTime ) :
      resizeFunction;
};


/**
 * Retrieve an element from an option.
 * @param {string|jQuery|Element} option The option to check.
 * @return {?Element} The plain element or null.
 * @private
 */
Shuffle.prototype._getElementOption = function( option ) {
  // If column width is a string, treat is as a selector and search for the
  // sizer element within the outermost container
  if ( typeof option === 'string' ) {
    return this.$el.find( option )[0] || null;

  // Check for an element
  } else if ( option && option.nodeType && option.nodeType === 1 ) {
    return option;

  // Check for jQuery object
  } else if ( option && option.jquery ) {
    return option[0];
  }

  return null;
};


/**
 * Ensures the shuffle container has the css styles it needs applied to it.
 * @param {Object} styles Key value pairs for position and overflow.
 * @private
 */
Shuffle.prototype._validateStyles = function(styles) {
  // Position cannot be static.
  if ( styles.position === 'static' ) {
    this.element.style.position = 'relative';
  }

  // Overflow has to be hidden
  if ( styles.overflow !== 'hidden' ) {
    this.element.style.overflow = 'hidden';
  }
};


/**
 * Filter the elements by a category.
 * @param {string} [category] Category to filter by. If it's given, the last
 *     category will be used to filter the items.
 * @param {ArrayLike} [$collection] Optionally filter a collection. Defaults to
 *     all the items.
 * @return {jQuery} Filtered items.
 * @private
 */
Shuffle.prototype._filter = function( category, $collection ) {
  category = category || this.lastFilter;
  $collection = $collection || this.$items;

  var set = this._getFilteredSets( category, $collection );

  // Individually add/remove concealed/filtered classes
  this._toggleFilterClasses( set.filtered, set.concealed );

  // Save the last filter in case elements are appended.
  this.lastFilter = category;

  // This is saved mainly because providing a filter function (like searching)
  // will overwrite the `lastFilter` property every time its called.
  if ( typeof category === 'string' ) {
    this.group = category;
  }

  return set.filtered;
};


/**
 * Returns an object containing the filtered and concealed elements.
 * @param {string|Function} category Category or function to filter by.
 * @param {ArrayLike.<Element>} $items A collection of items to filter.
 * @return {!{filtered: jQuery, concealed: jQuery}}
 * @private
 */
Shuffle.prototype._getFilteredSets = function( category, $items ) {
  var $filtered = $();
  var $concealed = $();

  // category === 'all', add filtered class to everything
  if ( category === ALL_ITEMS ) {
    $filtered = $items;

  // Loop through each item and use provided function to determine
  // whether to hide it or not.
  } else {
    each($items, function( el ) {
      var $item = $(el);
      if ( this._doesPassFilter( category, $item ) ) {
        $filtered = $filtered.add( $item );
      } else {
        $concealed = $concealed.add( $item );
      }
    }, this);
  }

  return {
    filtered: $filtered,
    concealed: $concealed
  };
};


/**
 * Test an item to see if it passes a category.
 * @param {string|Function} category Category or function to filter by.
 * @param {jQuery} $item A single item, wrapped with jQuery.
 * @return {boolean} Whether it passes the category/filter.
 * @private
 */
Shuffle.prototype._doesPassFilter = function( category, $item ) {
  if ( $.isFunction( category ) ) {
    return category.call( $item[0], $item, this );

  // Check each element's data-groups attribute against the given category.
  } else {
    var groups = $item.data( FILTER_ATTRIBUTE_KEY );
    var keys = this.delimeter && !$.isArray( groups ) ?
        groups.split( this.delimeter ) :
        groups;
    return $.inArray(category, keys) > -1;
  }
};


/**
 * Toggles the filtered and concealed class names.
 * @param {jQuery} $filtered Filtered set.
 * @param {jQuery} $concealed Concealed set.
 * @private
 */
Shuffle.prototype._toggleFilterClasses = function( $filtered, $concealed ) {
  $filtered
    .removeClass( Shuffle.ClassName.CONCEALED )
    .addClass( Shuffle.ClassName.FILTERED );
  $concealed
    .removeClass( Shuffle.ClassName.FILTERED )
    .addClass( Shuffle.ClassName.CONCEALED );
};


/**
 * Set the initial css for each item
 * @param {jQuery} [$items] Optionally specifiy at set to initialize
 */
Shuffle.prototype._initItems = function( $items ) {
  $items = $items || this.$items;
  $items.addClass([
    Shuffle.ClassName.SHUFFLE_ITEM,
    Shuffle.ClassName.FILTERED
  ].join(' '));
  $items.css( this.itemCss ).data('point', new Point()).data('scale', DEFAULT_SCALE);
};


/**
 * Updates the filtered item count.
 * @private
 */
Shuffle.prototype._updateItemCount = function() {
  this.visibleItems = this._getFilteredItems().length;
};


/**
 * Sets css transform transition on a an element.
 * @param {Element} element Element to set transition on.
 * @private
 */
Shuffle.prototype._setTransition = function( element ) {
  element.style[ TRANSITION ] = CSS_TRANSFORM + ' ' + this.speed + 'ms ' +
    this.easing + ', opacity ' + this.speed + 'ms ' + this.easing;
};


/**
 * Sets css transform transition on a group of elements.
 * @param {ArrayLike.<Element>} $items Elements to set transitions on.
 * @private
 */
Shuffle.prototype._setTransitions = function( $items ) {
  $items = $items || this.$items;
  each($items, function( el ) {
    this._setTransition( el );
  }, this);
};


/**
 * Sets a transition delay on a collection of elements, making each delay
 * greater than the last.
 * @param {ArrayLike.<Element>} $collection Array to iterate over.
 */
Shuffle.prototype._setSequentialDelay = function( $collection ) {
  if ( !this.supported ) {
    return;
  }

  // $collection can be an array of dom elements or jquery object
  each($collection, function( el, i ) {
    // This works because the transition-property: transform, opacity;
    el.style[ TRANSITION_DELAY ] = '0ms,' + ((i + 1) * this.sequentialFadeDelay) + 'ms';
  }, this);
};


Shuffle.prototype._getItems = function() {
  return this.$el.children( this.itemSelector );
};


Shuffle.prototype._getFilteredItems = function() {
  return this.$items.filter('.' + Shuffle.ClassName.FILTERED);
};


Shuffle.prototype._getConcealedItems = function() {
  return this.$items.filter('.' + Shuffle.ClassName.CONCEALED);
};


/**
 * Returns the column size, based on column width and sizer options.
 * @param {number} containerWidth Size of the parent container.
 * @param {number} gutterSize Size of the gutters.
 * @return {number}
 * @private
 */
Shuffle.prototype._getColumnSize = function( containerWidth, gutterSize ) {
  var size;

  // If the columnWidth property is a function, then the grid is fluid
  if ( $.isFunction( this.columnWidth ) ) {
    size = this.columnWidth(containerWidth);

  // columnWidth option isn't a function, are they using a sizing element?
  } else if ( this.useSizer ) {
    size = Shuffle._getOuterWidth(this.sizer);

  // if not, how about the explicitly set option?
  } else if ( this.columnWidth ) {
    size = this.columnWidth;

  // or use the size of the first item
  } else if ( this.$items.length > 0 ) {
    size = Shuffle._getOuterWidth(this.$items[0], true);

  // if there's no items, use size of container
  } else {
    size = containerWidth;
  }

  // Don't let them set a column width of zero.
  if ( size === 0 ) {
    size = containerWidth;
  }

  return size + gutterSize;
};


/**
 * Returns the gutter size, based on gutter width and sizer options.
 * @param {number} containerWidth Size of the parent container.
 * @return {number}
 * @private
 */
Shuffle.prototype._getGutterSize = function( containerWidth ) {
  var size;
  if ( $.isFunction( this.gutterWidth ) ) {
    size = this.gutterWidth(containerWidth);
  } else if ( this.useSizer ) {
    size = Shuffle._getNumberStyle(this.sizer, 'marginLeft');
  } else {
    size = this.gutterWidth;
  }

  return size;
};


/**
 * Calculate the number of columns to be used. Gets css if using sizer element.
 * @param {number} [theContainerWidth] Optionally specify a container width if it's already available.
 */
Shuffle.prototype._setColumns = function( theContainerWidth ) {
  var containerWidth = theContainerWidth || Shuffle._getOuterWidth( this.element );
  var gutter = this._getGutterSize( containerWidth );
  var columnWidth = this._getColumnSize( containerWidth, gutter );
  var calculatedColumns = (containerWidth + gutter) / columnWidth;

  // Widths given from getStyles are not precise enough...
  if ( Math.abs(Math.round(calculatedColumns) - calculatedColumns) < this.columnThreshold ) {
    // e.g. calculatedColumns = 11.998876
    calculatedColumns = Math.round( calculatedColumns );
  }

  this.cols = Math.max( Math.floor(calculatedColumns), 1 );
  this.containerWidth = containerWidth;
  this.colWidth = columnWidth;
};

/**
 * Adjust the height of the grid
 */
Shuffle.prototype._setContainerSize = function() {
  this.$el.css( 'height', this._getContainerSize() );
};


/**
 * Based on the column heights, it returns the biggest one.
 * @return {number}
 * @private
 */
Shuffle.prototype._getContainerSize = function() {
  return arrayMax( this.positions );
};


/**
 * Fire events with .shuffle namespace
 */
Shuffle.prototype._fire = function( name, args ) {
  this.$el.trigger( name + '.' + SHUFFLE, args && args.length ? args : [ this ] );
};


/**
 * Zeros out the y columns array, which is used to determine item placement.
 * @private
 */
Shuffle.prototype._resetCols = function() {
  var i = this.cols;
  this.positions = [];
  while (i--) {
    this.positions.push( 0 );
  }
};


/**
 * Loops through each item that should be shown and calculates the x, y position.
 * @param {Array.<Element>} items Array of items that will be shown/layed out in order in their array.
 *     Because jQuery collection are always ordered in DOM order, we can't pass a jq collection.
 * @param {boolean} [isOnlyPosition=false] If true this will position the items with zero opacity.
 */
Shuffle.prototype._layout = function( items, isOnlyPosition ) {
  each(items, function( item ) {
    this._layoutItem( item, !!isOnlyPosition );
  }, this);

  // `_layout` always happens after `_shrink`, so it's safe to process the style
  // queue here with styles from the shrink method.
  this._processStyleQueue();

  // Adjust the height of the container.
  this._setContainerSize();
};


/**
 * Calculates the position of the item and pushes it onto the style queue.
 * @param {Element} item Element which is being positioned.
 * @param {boolean} isOnlyPosition Whether to position the item, but with zero
 *     opacity so that it can fade in later.
 * @private
 */
Shuffle.prototype._layoutItem = function( item, isOnlyPosition ) {
  var $item = $(item);
  var itemData = $item.data();
  var currPos = itemData.point;
  var currScale = itemData.scale;
  var itemSize = {
    width: Shuffle._getOuterWidth( item, true ),
    height: Shuffle._getOuterHeight( item, true )
  };
  var pos = this._getItemPosition( itemSize );

  // If the item will not change its position, do not add it to the render
  // queue. Transitions don't fire when setting a property to the same value.
  if ( Point.equals(currPos, pos) && currScale === DEFAULT_SCALE ) {
    return;
  }

  // Save data for shrink
  itemData.point = pos;
  itemData.scale = DEFAULT_SCALE;

  this.styleQueue.push({
    $item: $item,
    point: pos,
    scale: DEFAULT_SCALE,
    opacity: isOnlyPosition ? 0 : 1,
    // Set styles immediately if there is no transition speed.
    skipTransition: isOnlyPosition || this.speed === 0,
    callfront: function() {
      if ( !isOnlyPosition ) {
        $item.css( 'visibility', 'visible' );
      }
    },
    callback: function() {
      if ( isOnlyPosition ) {
        $item.css( 'visibility', 'hidden' );
      }
    }
  });
};


/**
 * Determine the location of the next item, based on its size.
 * @param {{width: number, height: number}} itemSize Object with width and height.
 * @return {Point}
 * @private
 */
Shuffle.prototype._getItemPosition = function( itemSize ) {
  var columnSpan = this._getColumnSpan( itemSize.width, this.colWidth, this.cols );

  var setY = this._getColumnSet( columnSpan, this.cols );

  // Finds the index of the smallest number in the set.
  var shortColumnIndex = this._getShortColumn( setY, this.buffer );

  // Position the item
  var point = new Point(
    Math.round( this.colWidth * shortColumnIndex ),
    Math.round( setY[shortColumnIndex] ));

  // Update the columns array with the new values for each column.
  // e.g. before the update the columns could be [250, 0, 0, 0] for an item
  // which spans 2 columns. After it would be [250, itemHeight, itemHeight, 0].
  var setHeight = setY[shortColumnIndex] + itemSize.height;
  var setSpan = this.cols + 1 - setY.length;
  for ( var i = 0; i < setSpan; i++ ) {
    this.positions[ shortColumnIndex + i ] = setHeight;
  }

  return point;
};


/**
 * Determine the number of columns an items spans.
 * @param {number} itemWidth Width of the item.
 * @param {number} columnWidth Width of the column (includes gutter).
 * @param {number} columns Total number of columns
 * @return {number}
 * @private
 */
Shuffle.prototype._getColumnSpan = function( itemWidth, columnWidth, columns ) {
  var columnSpan = itemWidth / columnWidth;

  // If the difference between the rounded column span number and the
  // calculated column span number is really small, round the number to
  // make it fit.
  if ( Math.abs(Math.round( columnSpan ) - columnSpan ) < this.columnThreshold ) {
    // e.g. columnSpan = 4.0089945390298745
    columnSpan = Math.round( columnSpan );
  }

  // Ensure the column span is not more than the amount of columns in the whole layout.
  return Math.min( Math.ceil( columnSpan ), columns );
};


/**
 * Retrieves the column set to use for placement.
 * @param {number} columnSpan The number of columns this current item spans.
 * @param {number} columns The total columns in the grid.
 * @return {Array.<number>} An array of numbers represeting the column set.
 * @private
 */
Shuffle.prototype._getColumnSet = function( columnSpan, columns ) {
  // The item spans only one column.
  if ( columnSpan === 1 ) {
    return this.positions;

  // The item spans more than one column, figure out how many different
  // places it could fit horizontally.
  // The group count is the number of places within the positions this block
  // could fit, ignoring the current positions of items.
  // Imagine a 2 column brick as the second item in a 4 column grid with
  // 10px height each. Find the places it would fit:
  // [10, 0, 0, 0]
  //  |   |  |
  //  *   *  *
  //
  // Then take the places which fit and get the bigger of the two:
  // max([10, 0]), max([0, 0]), max([0, 0]) = [10, 0, 0]
  //
  // Next, find the first smallest number (the short column).
  // [10, 0, 0]
  //      |
  //      *
  //
  // And that's where it should be placed!
  } else {
    var groupCount = columns + 1 - columnSpan;
    var groupY = [];

    // For how many possible positions for this item there are.
    for ( var i = 0; i < groupCount; i++ ) {
      // Find the bigger value for each place it could fit.
      groupY[i] = arrayMax( this.positions.slice( i, i + columnSpan ) );
    }

    return groupY;
  }
};


/**
 * Find index of short column, the first from the left where this item will go.
 *
 * @param {Array.<number>} positions The array to search for the smallest number.
 * @param {number} buffer Optional buffer which is very useful when the height
 *     is a percentage of the width.
 * @return {number} Index of the short column.
 * @private
 */
Shuffle.prototype._getShortColumn = function( positions, buffer ) {
  var minPosition = arrayMin( positions );
  for (var i = 0, len = positions.length; i < len; i++) {
    if ( positions[i] >= minPosition - buffer && positions[i] <= minPosition + buffer ) {
      return i;
    }
  }
  return 0;
};


/**
 * Hides the elements that don't match our filter.
 * @param {jQuery} $collection jQuery collection to shrink.
 * @private
 */
Shuffle.prototype._shrink = function( $collection ) {
  var $concealed = $collection || this._getConcealedItems();

  each($concealed, function( item ) {
    var $item = $(item);
    var itemData = $item.data();

    // Continuing would add a transitionend event listener to the element, but
    // that listener would not execute because the transform and opacity would
    // stay the same.
    if ( itemData.scale === CONCEALED_SCALE ) {
      return;
    }

    itemData.scale = CONCEALED_SCALE;

    this.styleQueue.push({
      $item: $item,
      point: itemData.point,
      scale : CONCEALED_SCALE,
      opacity: 0,
      callback: function() {
        $item.css( 'visibility', 'hidden' );
      }
    });
  }, this);
};


/**
 * Resize handler.
 * @private
 */
Shuffle.prototype._onResize = function() {
  // If shuffle is disabled, destroyed, don't do anything
  if ( !this.enabled || this.destroyed ) {
    return;
  }

  // Will need to check height in the future if it's layed out horizontaly
  var containerWidth = Shuffle._getOuterWidth( this.element );

  // containerWidth hasn't changed, don't do anything
  if ( containerWidth === this.containerWidth ) {
    return;
  }

  this.update();
};


/**
 * Returns styles for either jQuery animate or transition.
 * @param {Object} opts Transition options.
 * @return {!Object} Transforms for transitions, left/top for animate.
 * @private
 */
Shuffle.prototype._getStylesForTransition = function( opts ) {
  var styles = {
    opacity: opts.opacity
  };

  if ( this.supported ) {
    styles[ TRANSFORM ] = Shuffle._getItemTransformString( opts.point, opts.scale );
  } else {
    styles.left = opts.point.x;
    styles.top = opts.point.y;
  }

  return styles;
};


/**
 * Transitions an item in the grid
 *
 * @param {Object} opts options.
 * @param {jQuery} opts.$item jQuery object representing the current item.
 * @param {Point} opts.point A point object with the x and y coordinates.
 * @param {number} opts.scale Amount to scale the item.
 * @param {number} opts.opacity Opacity of the item.
 * @param {Function} opts.callback Complete function for the animation.
 * @param {Function} opts.callfront Function to call before transitioning.
 * @private
 */
Shuffle.prototype._transition = function( opts ) {
  var styles = this._getStylesForTransition( opts );
  this._startItemAnimation( opts.$item, styles, opts.callfront || $.noop, opts.callback || $.noop );
};


Shuffle.prototype._startItemAnimation = function( $item, styles, callfront, callback ) {
  var _this = this;
  // Transition end handler removes its listener.
  function handleTransitionEnd( evt ) {
    // Make sure this event handler has not bubbled up from a child.
    if ( evt.target === evt.currentTarget ) {
      $( evt.target ).off( TRANSITIONEND, handleTransitionEnd );
      _this._removeTransitionReference(reference);
      callback();
    }
  }

  var reference = {
    $element: $item,
    handler: handleTransitionEnd
  };

  callfront();

  // Transitions are not set until shuffle has loaded to avoid the initial transition.
  if ( !this.initialized ) {
    $item.css( styles );
    callback();
    return;
  }

  // Use CSS Transforms if we have them
  if ( this.supported ) {
    $item.css( styles );
    $item.on( TRANSITIONEND, handleTransitionEnd );
    this._transitions.push(reference);

  // Use jQuery to animate left/top
  } else {
    // Save the deferred object which jQuery returns.
    var anim = $item.stop( true ).animate( styles, this.speed, 'swing', callback );
    // Push the animation to the list of pending animations.
    this._animations.push( anim.promise() );
  }
};


/**
 * Execute the styles gathered in the style queue. This applies styles to elements,
 * triggering transitions.
 * @param {boolean} noLayout Whether to trigger a layout event.
 * @private
 */
Shuffle.prototype._processStyleQueue = function( noLayout ) {
  if ( this.isTransitioning ) {
    this._cancelMovement();
  }

  var $transitions = $();

  // Iterate over the queue and keep track of ones that use transitions.
  each(this.styleQueue, function( transitionObj ) {
    if ( transitionObj.skipTransition ) {
      this._styleImmediately( transitionObj );
    } else {
      $transitions = $transitions.add( transitionObj.$item );
      this._transition( transitionObj );
    }
  }, this);


  if ( $transitions.length > 0 && this.initialized && this.speed > 0 ) {
    // Set flag that shuffle is currently in motion.
    this.isTransitioning = true;

    if ( this.supported ) {
      this._whenCollectionDone( $transitions, TRANSITIONEND, this._movementFinished );

    // The _transition function appends a promise to the animations array.
    // When they're all complete, do things.
    } else {
      this._whenAnimationsDone( this._movementFinished );
    }

  // A call to layout happened, but none of the newly filtered items will
  // change position. Asynchronously fire the callback here.
  } else if ( !noLayout ) {
    defer( this._layoutEnd, this );
  }

  // Remove everything in the style queue
  this.styleQueue.length = 0;
};

Shuffle.prototype._cancelMovement = function() {
  if (this.supported) {
    // Remove the transition end event for each listener.
    each(this._transitions, function( transition ) {
      transition.$element.off( TRANSITIONEND, transition.handler );
    });
  } else {
    // Even when `stop` is called on the jQuery animation, its promise will
    // still be resolved. Since it cannot be determine from within that callback
    // whether the animation was stopped or not, a flag is set here to distinguish
    // between the two states.
    this._isMovementCanceled = true;
    this.$items.stop(true);
    this._isMovementCanceled = false;
  }

  // Reset the array.
  this._transitions.length = 0;

  // Show it's no longer active.
  this.isTransitioning = false;
};

Shuffle.prototype._removeTransitionReference = function(ref) {
  var indexInArray = $.inArray(ref, this._transitions);
  if (indexInArray > -1) {
    this._transitions.splice(indexInArray, 1);
  }
};


/**
 * Apply styles without a transition.
 * @param {Object} opts Transitions options object.
 * @private
 */
Shuffle.prototype._styleImmediately = function( opts ) {
  Shuffle._skipTransition(opts.$item[0], function() {
    opts.$item.css( this._getStylesForTransition( opts ) );
  }, this);
};

Shuffle.prototype._movementFinished = function() {
  this.isTransitioning = false;
  this._layoutEnd();
};

Shuffle.prototype._layoutEnd = function() {
  this._fire( Shuffle.EventType.LAYOUT );
};

Shuffle.prototype._addItems = function( $newItems, addToEnd, isSequential ) {
  // Add classes and set initial positions.
  this._initItems( $newItems );

  // Add transition to each item.
  this._setTransitions( $newItems );

  // Update the list of
  this.$items = this._getItems();

  // Shrink all items (without transitions).
  this._shrink( $newItems );
  each(this.styleQueue, function( transitionObj ) {
    transitionObj.skipTransition = true;
  });

  // Apply shrink positions, but do not cause a layout event.
  this._processStyleQueue( true );

  if ( addToEnd ) {
    this._addItemsToEnd( $newItems, isSequential );
  } else {
    this.shuffle( this.lastFilter );
  }
};


Shuffle.prototype._addItemsToEnd = function( $newItems, isSequential ) {
  // Get ones that passed the current filter
  var $passed = this._filter( null, $newItems );
  var passed = $passed.get();

  // How many filtered elements?
  this._updateItemCount();

  this._layout( passed, true );

  if ( isSequential && this.supported ) {
    this._setSequentialDelay( passed );
  }

  this._revealAppended( passed );
};


/**
 * Triggers appended elements to fade in.
 * @param {ArrayLike.<Element>} $newFilteredItems Collection of elements.
 * @private
 */
Shuffle.prototype._revealAppended = function( newFilteredItems ) {
  defer(function() {
    each(newFilteredItems, function( el ) {
      var $item = $( el );
      this._transition({
        $item: $item,
        opacity: 1,
        point: $item.data('point'),
        scale: DEFAULT_SCALE
      });
    }, this);

    this._whenCollectionDone($(newFilteredItems), TRANSITIONEND, function() {
      $(newFilteredItems).css( TRANSITION_DELAY, '0ms' );
      this._movementFinished();
    });
  }, this, this.revealAppendedDelay);
};


/**
 * Execute a function when an event has been triggered for every item in a collection.
 * @param {jQuery} $collection Collection of elements.
 * @param {string} eventName Event to listen for.
 * @param {Function} callback Callback to execute when they're done.
 * @private
 */
Shuffle.prototype._whenCollectionDone = function( $collection, eventName, callback ) {
  var done = 0;
  var items = $collection.length;
  var self = this;

  function handleEventName( evt ) {
    if ( evt.target === evt.currentTarget ) {
      $( evt.target ).off( eventName, handleEventName );
      done++;

      // Execute callback if all items have emitted the correct event.
      if ( done === items ) {
        self._removeTransitionReference(reference);
        callback.call( self );
      }
    }
  }

  var reference = {
    $element: $collection,
    handler: handleEventName
  };

  // Bind the event to all items.
  $collection.on( eventName, handleEventName );

  // Keep track of transitionend events so they can be removed.
  this._transitions.push(reference);
};


/**
 * Execute a callback after jQuery `animate` for a collection has finished.
 * @param {Function} callback Callback to execute when they're done.
 * @private
 */
Shuffle.prototype._whenAnimationsDone = function( callback ) {
  $.when.apply( null, this._animations ).always( $.proxy( function() {
    this._animations.length = 0;
    if (!this._isMovementCanceled) {
      callback.call( this );
    }
  }, this ));
};


/**
 * Public Methods
 */

/**
 * The magic. This is what makes the plugin 'shuffle'
 * @param {string|Function} [category] Category to filter by. Can be a function
 * @param {Object} [sortObj] A sort object which can sort the filtered set
 */
Shuffle.prototype.shuffle = function( category, sortObj ) {
  if ( !this.enabled ) {
    return;
  }

  if ( !category ) {
    category = ALL_ITEMS;
  }

  this._filter( category );

  // How many filtered elements?
  this._updateItemCount();

  // Shrink each concealed item
  this._shrink();

  // Update transforms on .filtered elements so they will animate to their new positions
  this.sort( sortObj );
};


/**
 * Gets the .filtered elements, sorts them, and passes them to layout.
 * @param {Object} opts the options object for the sorted plugin
 */
Shuffle.prototype.sort = function( opts ) {
  if ( this.enabled ) {
    this._resetCols();

    var sortOptions = opts || this.lastSort;
    var items = this._getFilteredItems().sorted( sortOptions );

    this._layout( items );

    this.lastSort = sortOptions;
  }
};


/**
 * Reposition everything.
 * @param {boolean} isOnlyLayout If true, column and gutter widths won't be
 *     recalculated.
 */
Shuffle.prototype.update = function( isOnlyLayout ) {
  if ( this.enabled ) {

    if ( !isOnlyLayout ) {
      // Get updated colCount
      this._setColumns();
    }

    // Layout items
    this.sort();
  }
};


/**
 * Use this instead of `update()` if you don't need the columns and gutters updated
 * Maybe an image inside `shuffle` loaded (and now has a height), which means calculations
 * could be off.
 */
Shuffle.prototype.layout = function() {
  this.update( true );
};


/**
 * New items have been appended to shuffle. Fade them in sequentially
 * @param {jQuery} $newItems jQuery collection of new items
 * @param {boolean} [addToEnd=false] If true, new items will be added to the end / bottom
 *     of the items. If not true, items will be mixed in with the current sort order.
 * @param {boolean} [isSequential=true] If false, new items won't sequentially fade in
 */
Shuffle.prototype.appended = function( $newItems, addToEnd, isSequential ) {
  this._addItems( $newItems, addToEnd === true, isSequential !== false );
};


/**
 * Disables shuffle from updating dimensions and layout on resize
 */
Shuffle.prototype.disable = function() {
  this.enabled = false;
};


/**
 * Enables shuffle again
 * @param {boolean} [isUpdateLayout=true] if undefined, shuffle will update columns and gutters
 */
Shuffle.prototype.enable = function( isUpdateLayout ) {
  this.enabled = true;
  if ( isUpdateLayout !== false ) {
    this.update();
  }
};


/**
 * Remove 1 or more shuffle items
 * @param {jQuery} $collection A jQuery object containing one or more element in shuffle
 * @return {Shuffle} The shuffle object
 */
Shuffle.prototype.remove = function( $collection ) {

  // If this isn't a jquery object, exit
  if ( !$collection.length || !$collection.jquery ) {
    return;
  }

  function handleRemoved() {
    // Remove the collection in the callback
    $collection.remove();

    // Update things now that elements have been removed.
    this.$items = this._getItems();
    this._updateItemCount();

    this._fire( Shuffle.EventType.REMOVED, [ $collection, this ] );

    // Let it get garbage collected
    $collection = null;
  }

  // Hide collection first.
  this._toggleFilterClasses( $(), $collection );
  this._shrink( $collection );

  this.sort();

  this.$el.one( Shuffle.EventType.LAYOUT + '.' + SHUFFLE, $.proxy( handleRemoved, this ) );
};


/**
 * Destroys shuffle, removes events, styles, and classes
 */
Shuffle.prototype.destroy = function() {
  // If there is more than one shuffle instance on the page,
  // removing the resize handler from the window would remove them
  // all. This is why a unique value is needed.
  $window.off('.' + this.unique);

  // Reset container styles
  this.$el
      .removeClass( SHUFFLE )
      .removeAttr('style')
      .removeData( SHUFFLE );

  // Reset individual item styles
  this.$items
      .removeAttr('style')
      .removeData('point')
      .removeData('scale')
      .removeClass([
        Shuffle.ClassName.CONCEALED,
        Shuffle.ClassName.FILTERED,
        Shuffle.ClassName.SHUFFLE_ITEM
      ].join(' '));

  // Null DOM references
  this.$items = null;
  this.$el = null;
  this.sizer = null;
  this.element = null;
  this._transitions = null;

  // Set a flag so if a debounced resize has been triggered,
  // it can first check if it is actually destroyed and not doing anything
  this.destroyed = true;
};


// Plugin definition
$.fn.shuffle = function( opts ) {
  var args = Array.prototype.slice.call( arguments, 1 );
  return this.each(function() {
    var $this = $( this );
    var shuffle = $this.data( SHUFFLE );

    // If we don't have a stored shuffle, make a new one and save it
    if ( !shuffle ) {
      shuffle = new Shuffle( this, opts );
      $this.data( SHUFFLE, shuffle );
    } else if ( typeof opts === 'string' && shuffle[ opts ] ) {
      shuffle[ opts ].apply( shuffle, args );
    }
  });
};


// http://stackoverflow.com/a/962890/373422
function randomize( array ) {
  var tmp, current;
  var top = array.length;

  if ( !top ) {
    return array;
  }

  while ( --top ) {
    current = Math.floor( Math.random() * (top + 1) );
    tmp = array[ current ];
    array[ current ] = array[ top ];
    array[ top ] = tmp;
  }

  return array;
}


// You can return `undefined` from the `by` function to revert to DOM order
// This plugin does NOT return a jQuery object. It returns a plain array because
// jQuery sorts everything in DOM order.
$.fn.sorted = function(options) {
  var opts = $.extend({}, $.fn.sorted.defaults, options);
  var arr = this.get();
  var revert = false;

  if ( !arr.length ) {
    return [];
  }

  if ( opts.randomize ) {
    return randomize( arr );
  }

  // Sort the elements by the opts.by function.
  // If we don't have opts.by, default to DOM order
  if ( $.isFunction( opts.by ) ) {
    arr.sort(function(a, b) {

      // Exit early if we already know we want to revert
      if ( revert ) {
        return 0;
      }

      var valA = opts.by($(a));
      var valB = opts.by($(b));

      // If both values are undefined, use the DOM order
      if ( valA === undefined && valB === undefined ) {
        revert = true;
        return 0;
      }

      if ( valA < valB || valA === 'sortFirst' || valB === 'sortLast' ) {
        return -1;
      }

      if ( valA > valB || valA === 'sortLast' || valB === 'sortFirst' ) {
        return 1;
      }

      return 0;
    });
  }

  // Revert to the original array if necessary
  if ( revert ) {
    return this.get();
  }

  if ( opts.reverse ) {
    arr.reverse();
  }

  return arr;
};


$.fn.sorted.defaults = {
  reverse: false, // Use array.reverse() to reverse the results
  by: null, // Sorting function
  randomize: false // If true, this will skip the sorting and return a randomized order in the array
};

return Shuffle;

});

(function() {
  var MutationObserver, Util, WeakMap, getComputedStyle, getComputedStyleRX,
    bind = function(fn, me){ return function(){ return fn.apply(me, arguments); }; },
    indexOf = [].indexOf || function(item) { for (var i = 0, l = this.length; i < l; i++) { if (i in this && this[i] === item) return i; } return -1; };

  Util = (function() {
    function Util() {}

    Util.prototype.extend = function(custom, defaults) {
      var key, value;
      for (key in defaults) {
        value = defaults[key];
        if (custom[key] == null) {
          custom[key] = value;
        }
      }
      return custom;
    };

    Util.prototype.isMobile = function(agent) {
      return /Android|webOS|iPhone|iPad|iPod|BlackBerry|IEMobile|Opera Mini/i.test(agent);
    };

    Util.prototype.createEvent = function(event, bubble, cancel, detail) {
      var customEvent;
      if (bubble == null) {
        bubble = false;
      }
      if (cancel == null) {
        cancel = false;
      }
      if (detail == null) {
        detail = null;
      }
      if (document.createEvent != null) {
        customEvent = document.createEvent('CustomEvent');
        customEvent.initCustomEvent(event, bubble, cancel, detail);
      } else if (document.createEventObject != null) {
        customEvent = document.createEventObject();
        customEvent.eventType = event;
      } else {
        customEvent.eventName = event;
      }
      return customEvent;
    };

    Util.prototype.emitEvent = function(elem, event) {
      if (elem.dispatchEvent != null) {
        return elem.dispatchEvent(event);
      } else if (event in (elem != null)) {
        return elem[event]();
      } else if (("on" + event) in (elem != null)) {
        return elem["on" + event]();
      }
    };

    Util.prototype.addEvent = function(elem, event, fn) {
      if (elem.addEventListener != null) {
        return elem.addEventListener(event, fn, false);
      } else if (elem.attachEvent != null) {
        return elem.attachEvent("on" + event, fn);
      } else {
        return elem[event] = fn;
      }
    };

    Util.prototype.removeEvent = function(elem, event, fn) {
      if (elem.removeEventListener != null) {
        return elem.removeEventListener(event, fn, false);
      } else if (elem.detachEvent != null) {
        return elem.detachEvent("on" + event, fn);
      } else {
        return delete elem[event];
      }
    };

    Util.prototype.innerHeight = function() {
      if ('innerHeight' in window) {
        return window.innerHeight;
      } else {
        return document.documentElement.clientHeight;
      }
    };

    return Util;

  })();

  WeakMap = this.WeakMap || this.MozWeakMap || (WeakMap = (function() {
    function WeakMap() {
      this.keys = [];
      this.values = [];
    }

    WeakMap.prototype.get = function(key) {
      var i, item, j, len, ref;
      ref = this.keys;
      for (i = j = 0, len = ref.length; j < len; i = ++j) {
        item = ref[i];
        if (item === key) {
          return this.values[i];
        }
      }
    };

    WeakMap.prototype.set = function(key, value) {
      var i, item, j, len, ref;
      ref = this.keys;
      for (i = j = 0, len = ref.length; j < len; i = ++j) {
        item = ref[i];
        if (item === key) {
          this.values[i] = value;
          return;
        }
      }
      this.keys.push(key);
      return this.values.push(value);
    };

    return WeakMap;

  })());

  MutationObserver = this.MutationObserver || this.WebkitMutationObserver || this.MozMutationObserver || (MutationObserver = (function() {
    function MutationObserver() {
      if (typeof console !== "undefined" && console !== null) {
        console.warn('MutationObserver is not supported by your browser.');
      }
      if (typeof console !== "undefined" && console !== null) {
        console.warn('WOW.js cannot detect dom mutations, please call .sync() after loading new content.');
      }
    }

    MutationObserver.notSupported = true;

    MutationObserver.prototype.observe = function() {};

    return MutationObserver;

  })());

  getComputedStyle = this.getComputedStyle || function(el, pseudo) {
    this.getPropertyValue = function(prop) {
      var ref;
      if (prop === 'float') {
        prop = 'styleFloat';
      }
      if (getComputedStyleRX.test(prop)) {
        prop.replace(getComputedStyleRX, function(_, _char) {
          return _char.toUpperCase();
        });
      }
      return ((ref = el.currentStyle) != null ? ref[prop] : void 0) || null;
    };
    return this;
  };

  getComputedStyleRX = /(\-([a-z]){1})/g;

  this.WOW = (function() {
    WOW.prototype.defaults = {
      boxClass: 'wow',
      animateClass: 'animated',
      offset: 0,
      mobile: true,
      live: true,
      callback: null
    };

    function WOW(options) {
      if (options == null) {
        options = {};
      }
      this.scrollCallback = bind(this.scrollCallback, this);
      this.scrollHandler = bind(this.scrollHandler, this);
      this.resetAnimation = bind(this.resetAnimation, this);
      this.start = bind(this.start, this);
      this.scrolled = true;
      this.config = this.util().extend(options, this.defaults);
      this.animationNameCache = new WeakMap();
      this.wowEvent = this.util().createEvent(this.config.boxClass);
    }

    WOW.prototype.init = function() {
      var ref;
      this.element = window.document.documentElement;
      if ((ref = document.readyState) === "interactive" || ref === "complete") {
        this.start();
      } else {
        this.util().addEvent(document, 'DOMContentLoaded', this.start);
      }
      return this.finished = [];
    };

    WOW.prototype.start = function() {
      var box, j, len, ref;
      this.stopped = false;
      this.boxes = (function() {
        var j, len, ref, results;
        ref = this.element.querySelectorAll("." + this.config.boxClass);
        results = [];
        for (j = 0, len = ref.length; j < len; j++) {
          box = ref[j];
          results.push(box);
        }
        return results;
      }).call(this);
      this.all = (function() {
        var j, len, ref, results;
        ref = this.boxes;
        results = [];
        for (j = 0, len = ref.length; j < len; j++) {
          box = ref[j];
          results.push(box);
        }
        return results;
      }).call(this);
      if (this.boxes.length) {
        if (this.disabled()) {
          this.resetStyle();
        } else {
          ref = this.boxes;
          for (j = 0, len = ref.length; j < len; j++) {
            box = ref[j];
            this.applyStyle(box, true);
          }
        }
      }
      if (!this.disabled()) {
        this.util().addEvent(window, 'scroll', this.scrollHandler);
        this.util().addEvent(window, 'resize', this.scrollHandler);
        this.interval = setInterval(this.scrollCallback, 50);
      }
      if (this.config.live) {
        return new MutationObserver((function(_this) {
          return function(records) {
            var k, len1, node, record, results;
            results = [];
            for (k = 0, len1 = records.length; k < len1; k++) {
              record = records[k];
              results.push((function() {
                var l, len2, ref1, results1;
                ref1 = record.addedNodes || [];
                results1 = [];
                for (l = 0, len2 = ref1.length; l < len2; l++) {
                  node = ref1[l];
                  results1.push(this.doSync(node));
                }
                return results1;
              }).call(_this));
            }
            return results;
          };
        })(this)).observe(document.body, {
          childList: true,
          subtree: true
        });
      }
    };

    WOW.prototype.stop = function() {
      this.stopped = true;
      this.util().removeEvent(window, 'scroll', this.scrollHandler);
      this.util().removeEvent(window, 'resize', this.scrollHandler);
      if (this.interval != null) {
        return clearInterval(this.interval);
      }
    };

    WOW.prototype.sync = function(element) {
      if (MutationObserver.notSupported) {
        return this.doSync(this.element);
      }
    };

    WOW.prototype.doSync = function(element) {
      var box, j, len, ref, results;
      if (element == null) {
        element = this.element;
      }
      if (element.nodeType !== 1) {
        return;
      }
      element = element.parentNode || element;
      ref = element.querySelectorAll("." + this.config.boxClass);
      results = [];
      for (j = 0, len = ref.length; j < len; j++) {
        box = ref[j];
        if (indexOf.call(this.all, box) < 0) {
          this.boxes.push(box);
          this.all.push(box);
          if (this.stopped || this.disabled()) {
            this.resetStyle();
          } else {
            this.applyStyle(box, true);
          }
          results.push(this.scrolled = true);
        } else {
          results.push(void 0);
        }
      }
      return results;
    };

    WOW.prototype.show = function(box) {
      this.applyStyle(box);
      box.className = box.className + " " + this.config.animateClass;
      if (this.config.callback != null) {
        this.config.callback(box);
      }
      this.util().emitEvent(box, this.wowEvent);
      this.util().addEvent(box, 'animationend', this.resetAnimation);
      this.util().addEvent(box, 'oanimationend', this.resetAnimation);
      this.util().addEvent(box, 'webkitAnimationEnd', this.resetAnimation);
      this.util().addEvent(box, 'MSAnimationEnd', this.resetAnimation);
      return box;
    };

    WOW.prototype.applyStyle = function(box, hidden) {
      var delay, duration, iteration;
      duration = box.getAttribute('data-wow-duration');
      delay = box.getAttribute('data-wow-delay');
      iteration = box.getAttribute('data-wow-iteration');
      return this.animate((function(_this) {
        return function() {
          return _this.customStyle(box, hidden, duration, delay, iteration);
        };
      })(this));
    };

    WOW.prototype.animate = (function() {
      if ('requestAnimationFrame' in window) {
        return function(callback) {
          return window.requestAnimationFrame(callback);
        };
      } else {
        return function(callback) {
          return callback();
        };
      }
    })();

    WOW.prototype.resetStyle = function() {
      var box, j, len, ref, results;
      ref = this.boxes;
      results = [];
      for (j = 0, len = ref.length; j < len; j++) {
        box = ref[j];
        results.push(box.style.visibility = 'visible');
      }
      return results;
    };

    WOW.prototype.resetAnimation = function(event) {
      var target;
      if (event.type.toLowerCase().indexOf('animationend') >= 0) {
        target = event.target || event.srcElement;
        return target.className = target.className.replace(this.config.animateClass, '').trim();
      }
    };

    WOW.prototype.customStyle = function(box, hidden, duration, delay, iteration) {
      if (hidden) {
        this.cacheAnimationName(box);
      }
      box.style.visibility = hidden ? 'hidden' : 'visible';
      if (duration) {
        this.vendorSet(box.style, {
          animationDuration: duration
        });
      }
      if (delay) {
        this.vendorSet(box.style, {
          animationDelay: delay
        });
      }
      if (iteration) {
        this.vendorSet(box.style, {
          animationIterationCount: iteration
        });
      }
      this.vendorSet(box.style, {
        animationName: hidden ? 'none' : this.cachedAnimationName(box)
      });
      return box;
    };

    WOW.prototype.vendors = ["moz", "webkit"];

    WOW.prototype.vendorSet = function(elem, properties) {
      var name, results, value, vendor;
      results = [];
      for (name in properties) {
        value = properties[name];
        elem["" + name] = value;
        results.push((function() {
          var j, len, ref, results1;
          ref = this.vendors;
          results1 = [];
          for (j = 0, len = ref.length; j < len; j++) {
            vendor = ref[j];
            results1.push(elem["" + vendor + (name.charAt(0).toUpperCase()) + (name.substr(1))] = value);
          }
          return results1;
        }).call(this));
      }
      return results;
    };

    WOW.prototype.vendorCSS = function(elem, property) {
      var j, len, ref, result, style, vendor;
      style = getComputedStyle(elem);
      result = style.getPropertyCSSValue(property);
      ref = this.vendors;
      for (j = 0, len = ref.length; j < len; j++) {
        vendor = ref[j];
        result = result || style.getPropertyCSSValue("-" + vendor + "-" + property);
      }
      return result;
    };

    WOW.prototype.animationName = function(box) {
      var animationName;
      try {
        animationName = this.vendorCSS(box, 'animation-name').cssText;
      } catch (_error) {
        animationName = getComputedStyle(box).getPropertyValue('animation-name');
      }
      if (animationName === 'none') {
        return '';
      } else {
        return animationName;
      }
    };

    WOW.prototype.cacheAnimationName = function(box) {
      return this.animationNameCache.set(box, this.animationName(box));
    };

    WOW.prototype.cachedAnimationName = function(box) {
      return this.animationNameCache.get(box);
    };

    WOW.prototype.scrollHandler = function() {
      return this.scrolled = true;
    };

    WOW.prototype.scrollCallback = function() {
      var box;
      if (this.scrolled) {
        this.scrolled = false;
        this.boxes = (function() {
          var j, len, ref, results;
          ref = this.boxes;
          results = [];
          for (j = 0, len = ref.length; j < len; j++) {
            box = ref[j];
            if (!(box)) {
              continue;
            }
            if (this.isVisible(box)) {
              this.show(box);
              continue;
            }
            results.push(box);
          }
          return results;
        }).call(this);
        if (!(this.boxes.length || this.config.live)) {
          return this.stop();
        }
      }
    };

    WOW.prototype.offsetTop = function(element) {
      var top;
      while (element.offsetTop === void 0) {
        element = element.parentNode;
      }
      top = element.offsetTop;
      while (element = element.offsetParent) {
        top += element.offsetTop;
      }
      return top;
    };

    WOW.prototype.isVisible = function(box) {
      var bottom, offset, top, viewBottom, viewTop;
      offset = box.getAttribute('data-wow-offset') || this.config.offset;
      viewTop = window.pageYOffset;
      viewBottom = viewTop + Math.min(this.element.clientHeight, this.util().innerHeight()) - offset;
      top = this.offsetTop(box);
      bottom = top + box.clientHeight;
      return top <= viewBottom && bottom >= viewTop;
    };

    WOW.prototype.util = function() {
      return this._util != null ? this._util : this._util = new Util();
    };

    WOW.prototype.disabled = function() {
      return !this.config.mobile && this.util().isMobile(navigator.userAgent);
    };

    return WOW;

  })();

}).call(this);

/*
 *  jQuery OwlCarousel v1.3.2
 *
 *  Copyright (c) 2013 Bartosz Wojciechowski
 *  http://www.owlgraphic.com/owlcarousel/
 *
 *  Licensed under MIT
 *
 */

/*JS Lint helpers: */
/*global dragMove: false, dragEnd: false, $, jQuery, alert, window, document */
/*jslint nomen: true, continue:true */

if (typeof Object.create !== "function") {
    Object.create = function (obj) {
        function F() {}
        F.prototype = obj;
        return new F();
    };
}
(function ($, window, document) {

    var Carousel = {
        init : function (options, el) {
            var base = this;

            base.$elem = $(el);
            base.options = $.extend({}, $.fn.owlCarousel.options, base.$elem.data(), options);

            base.userOptions = options;
            base.loadContent();
        },

        loadContent : function () {
            var base = this, url;

            function getData(data) {
                var i, content = "";
                if (typeof base.options.jsonSuccess === "function") {
                    base.options.jsonSuccess.apply(this, [data]);
                } else {
                    for (i in data.owl) {
                        if (data.owl.hasOwnProperty(i)) {
                            content += data.owl[i].item;
                        }
                    }
                    base.$elem.html(content);
                }
                base.logIn();
            }

            if (typeof base.options.beforeInit === "function") {
                base.options.beforeInit.apply(this, [base.$elem]);
            }

            if (typeof base.options.jsonPath === "string") {
                url = base.options.jsonPath;
                $.getJSON(url, getData);
            } else {
                base.logIn();
            }
        },

        logIn : function () {
            var base = this;

            base.$elem.data("owl-originalStyles", base.$elem.attr("style"))
                      .data("owl-originalClasses", base.$elem.attr("class"));

            base.$elem.css({opacity: 0});
            base.orignalItems = base.options.items;
            base.checkBrowser();
            base.wrapperWidth = 0;
            base.checkVisible = null;
            base.setVars();
        },

        setVars : function () {
            var base = this;
            if (base.$elem.children().length === 0) {return false; }
            base.baseClass();
            base.eventTypes();
            base.$userItems = base.$elem.children();
            base.itemsAmount = base.$userItems.length;
            base.wrapItems();
            base.$owlItems = base.$elem.find(".owl-item");
            base.$owlWrapper = base.$elem.find(".owl-wrapper");
            base.playDirection = "next";
            base.prevItem = 0;
            base.prevArr = [0];
            base.currentItem = 0;
            base.customEvents();
            base.onStartup();
        },

        onStartup : function () {
            var base = this;
            base.updateItems();
            base.calculateAll();
            base.buildControls();
            base.updateControls();
            base.response();
            base.moveEvents();
            base.stopOnHover();
            base.owlStatus();

            if (base.options.transitionStyle !== false) {
                base.transitionTypes(base.options.transitionStyle);
            }
            if (base.options.autoPlay === true) {
                base.options.autoPlay = 5000;
            }
            base.play();

            base.$elem.find(".owl-wrapper").css("display", "block");

            if (!base.$elem.is(":visible")) {
                base.watchVisibility();
            } else {
                base.$elem.css("opacity", 1);
            }
            base.onstartup = false;
            base.eachMoveUpdate();
            if (typeof base.options.afterInit === "function") {
                base.options.afterInit.apply(this, [base.$elem]);
            }
        },

        eachMoveUpdate : function () {
            var base = this;

            if (base.options.lazyLoad === true) {
                base.lazyLoad();
            }
            if (base.options.autoHeight === true) {
                base.autoHeight();
            }
            base.onVisibleItems();

            if (typeof base.options.afterAction === "function") {
                base.options.afterAction.apply(this, [base.$elem]);
            }
        },

        updateVars : function () {
            var base = this;
            if (typeof base.options.beforeUpdate === "function") {
                base.options.beforeUpdate.apply(this, [base.$elem]);
            }
            base.watchVisibility();
            base.updateItems();
            base.calculateAll();
            base.updatePosition();
            base.updateControls();
            base.eachMoveUpdate();
            if (typeof base.options.afterUpdate === "function") {
                base.options.afterUpdate.apply(this, [base.$elem]);
            }
        },

        reload : function () {
            var base = this;
            window.setTimeout(function () {
                base.updateVars();
            }, 0);
        },

        watchVisibility : function () {
            var base = this;

            if (base.$elem.is(":visible") === false) {
                base.$elem.css({opacity: 0});
                window.clearInterval(base.autoPlayInterval);
                window.clearInterval(base.checkVisible);
            } else {
                return false;
            }
            base.checkVisible = window.setInterval(function () {
                if (base.$elem.is(":visible")) {
                    base.reload();
                    base.$elem.animate({opacity: 1}, 200);
                    window.clearInterval(base.checkVisible);
                }
            }, 500);
        },

        wrapItems : function () {
            var base = this;
            base.$userItems.wrapAll("<div class=\"owl-wrapper\">").wrap("<div class=\"owl-item\"></div>");
            base.$elem.find(".owl-wrapper").wrap("<div class=\"owl-wrapper-outer\">");
            base.wrapperOuter = base.$elem.find(".owl-wrapper-outer");
            base.$elem.css("display", "block");
        },

        baseClass : function () {
            var base = this,
                hasBaseClass = base.$elem.hasClass(base.options.baseClass),
                hasThemeClass = base.$elem.hasClass(base.options.theme);

            if (!hasBaseClass) {
                base.$elem.addClass(base.options.baseClass);
            }

            if (!hasThemeClass) {
                base.$elem.addClass(base.options.theme);
            }
        },

        updateItems : function () {
            var base = this, width, i;

            if (base.options.responsive === false) {
                return false;
            }
            if (base.options.singleItem === true) {
                base.options.items = base.orignalItems = 1;
                base.options.itemsCustom = false;
                base.options.itemsDesktop = false;
                base.options.itemsDesktopSmall = false;
                base.options.itemsTablet = false;
                base.options.itemsTabletSmall = false;
                base.options.itemsMobile = false;
                return false;
            }

            width = $(base.options.responsiveBaseWidth).width();

            if (width > (base.options.itemsDesktop[0] || base.orignalItems)) {
                base.options.items = base.orignalItems;
            }
            if (base.options.itemsCustom !== false) {
                //Reorder array by screen size
                base.options.itemsCustom.sort(function (a, b) {return a[0] - b[0]; });

                for (i = 0; i < base.options.itemsCustom.length; i += 1) {
                    if (base.options.itemsCustom[i][0] <= width) {
                        base.options.items = base.options.itemsCustom[i][1];
                    }
                }

            } else {

                if (width <= base.options.itemsDesktop[0] && base.options.itemsDesktop !== false) {
                    base.options.items = base.options.itemsDesktop[1];
                }

                if (width <= base.options.itemsDesktopSmall[0] && base.options.itemsDesktopSmall !== false) {
                    base.options.items = base.options.itemsDesktopSmall[1];
                }

                if (width <= base.options.itemsTablet[0] && base.options.itemsTablet !== false) {
                    base.options.items = base.options.itemsTablet[1];
                }

                if (width <= base.options.itemsTabletSmall[0] && base.options.itemsTabletSmall !== false) {
                    base.options.items = base.options.itemsTabletSmall[1];
                }

                if (width <= base.options.itemsMobile[0] && base.options.itemsMobile !== false) {
                    base.options.items = base.options.itemsMobile[1];
                }
            }

            //if number of items is less than declared
            if (base.options.items > base.itemsAmount && base.options.itemsScaleUp === true) {
                base.options.items = base.itemsAmount;
            }
        },

        response : function () {
            var base = this,
                smallDelay,
                lastWindowWidth;

            if (base.options.responsive !== true) {
                return false;
            }
            lastWindowWidth = $(window).width();

            base.resizer = function () {
                if ($(window).width() !== lastWindowWidth) {
                    if (base.options.autoPlay !== false) {
                        window.clearInterval(base.autoPlayInterval);
                    }
                    window.clearTimeout(smallDelay);
                    smallDelay = window.setTimeout(function () {
                        lastWindowWidth = $(window).width();
                        base.updateVars();
                    }, base.options.responsiveRefreshRate);
                }
            };
            $(window).resize(base.resizer);
        },

        updatePosition : function () {
            var base = this;
            base.jumpTo(base.currentItem);
            if (base.options.autoPlay !== false) {
                base.checkAp();
            }
        },

        appendItemsSizes : function () {
            var base = this,
                roundPages = 0,
                lastItem = base.itemsAmount - base.options.items;

            base.$owlItems.each(function (index) {
                var $this = $(this);
                $this
                    .css({"width": base.itemWidth})
                    .data("owl-item", Number(index));

                if (index % base.options.items === 0 || index === lastItem) {
                    if (!(index > lastItem)) {
                        roundPages += 1;
                    }
                }
                $this.data("owl-roundPages", roundPages);
            });
        },

        appendWrapperSizes : function () {
            var base = this,
                width = base.$owlItems.length * base.itemWidth;

            base.$owlWrapper.css({
                "width": width * 2,
                "left": 0
            });
            base.appendItemsSizes();
        },

        calculateAll : function () {
            var base = this;
            base.calculateWidth();
            base.appendWrapperSizes();
            base.loops();
            base.max();
        },

        calculateWidth : function () {
            var base = this;
            base.itemWidth = Math.round(base.$elem.width() / base.options.items);
        },

        max : function () {
            var base = this,
                maximum = ((base.itemsAmount * base.itemWidth) - base.options.items * base.itemWidth) * -1;
            if (base.options.items > base.itemsAmount) {
                base.maximumItem = 0;
                maximum = 0;
                base.maximumPixels = 0;
            } else {
                base.maximumItem = base.itemsAmount - base.options.items;
                base.maximumPixels = maximum;
            }
            return maximum;
        },

        min : function () {
            return 0;
        },

        loops : function () {
            var base = this,
                prev = 0,
                elWidth = 0,
                i,
                item,
                roundPageNum;

            base.positionsInArray = [0];
            base.pagesInArray = [];

            for (i = 0; i < base.itemsAmount; i += 1) {
                elWidth += base.itemWidth;
                base.positionsInArray.push(-elWidth);

                if (base.options.scrollPerPage === true) {
                    item = $(base.$owlItems[i]);
                    roundPageNum = item.data("owl-roundPages");
                    if (roundPageNum !== prev) {
                        base.pagesInArray[prev] = base.positionsInArray[i];
                        prev = roundPageNum;
                    }
                }
            }
        },

        buildControls : function () {
            var base = this;
            if (base.options.navigation === true || base.options.pagination === true) {
                base.owlControls = $("<div class=\"owl-controls\"/>").toggleClass("clickable", !base.browser.isTouch).appendTo(base.$elem);
            }
            if (base.options.pagination === true) {
                base.buildPagination();
            }
            if (base.options.navigation === true) {
                base.buildButtons();
            }
        },

        buildButtons : function () {
            var base = this,
                buttonsWrapper = $("<div class=\"owl-buttons\"/>");
            base.owlControls.append(buttonsWrapper);

            base.buttonPrev = $("<div/>", {
                "class" : "owl-prev",
                "html" : base.options.navigationText[0] || ""
            });

            base.buttonNext = $("<div/>", {
                "class" : "owl-next",
                "html" : base.options.navigationText[1] || ""
            });

            buttonsWrapper
                .append(base.buttonPrev)
                .append(base.buttonNext);

            buttonsWrapper.on("touchstart.owlControls mousedown.owlControls", "div[class^=\"owl\"]", function (event) {
                event.preventDefault();
            });

            buttonsWrapper.on("touchend.owlControls mouseup.owlControls", "div[class^=\"owl\"]", function (event) {
                event.preventDefault();
                if ($(this).hasClass("owl-next")) {
                    base.next();
                } else {
                    base.prev();
                }
            });
        },

        buildPagination : function () {
            var base = this;

            base.paginationWrapper = $("<div class=\"owl-pagination\"/>");
            base.owlControls.append(base.paginationWrapper);

            base.paginationWrapper.on("touchend.owlControls mouseup.owlControls", ".owl-page", function (event) {
                event.preventDefault();
                if (Number($(this).data("owl-page")) !== base.currentItem) {
                    base.goTo(Number($(this).data("owl-page")), true);
                }
            });
        },

        updatePagination : function () {
            var base = this,
                counter,
                lastPage,
                lastItem,
                i,
                paginationButton,
                paginationButtonInner;

            if (base.options.pagination === false) {
                return false;
            }

            base.paginationWrapper.html("");

            counter = 0;
            lastPage = base.itemsAmount - base.itemsAmount % base.options.items;

            for (i = 0; i < base.itemsAmount; i += 1) {
                if (i % base.options.items === 0) {
                    counter += 1;
                    if (lastPage === i) {
                        lastItem = base.itemsAmount - base.options.items;
                    }
                    paginationButton = $("<div/>", {
                        "class" : "owl-page"
                    });
                    paginationButtonInner = $("<span></span>", {
                        "text": base.options.paginationNumbers === true ? counter : "",
                        "class": base.options.paginationNumbers === true ? "owl-numbers" : ""
                    });
                    paginationButton.append(paginationButtonInner);

                    paginationButton.data("owl-page", lastPage === i ? lastItem : i);
                    paginationButton.data("owl-roundPages", counter);

                    base.paginationWrapper.append(paginationButton);
                }
            }
            base.checkPagination();
        },
        checkPagination : function () {
            var base = this;
            if (base.options.pagination === false) {
                return false;
            }
            base.paginationWrapper.find(".owl-page").each(function () {
                if ($(this).data("owl-roundPages") === $(base.$owlItems[base.currentItem]).data("owl-roundPages")) {
                    base.paginationWrapper
                        .find(".owl-page")
                        .removeClass("active");
                    $(this).addClass("active");
                }
            });
        },

        checkNavigation : function () {
            var base = this;

            if (base.options.navigation === false) {
                return false;
            }
            if (base.options.rewindNav === false) {
                if (base.currentItem === 0 && base.maximumItem === 0) {
                    base.buttonPrev.addClass("disabled");
                    base.buttonNext.addClass("disabled");
                } else if (base.currentItem === 0 && base.maximumItem !== 0) {
                    base.buttonPrev.addClass("disabled");
                    base.buttonNext.removeClass("disabled");
                } else if (base.currentItem === base.maximumItem) {
                    base.buttonPrev.removeClass("disabled");
                    base.buttonNext.addClass("disabled");
                } else if (base.currentItem !== 0 && base.currentItem !== base.maximumItem) {
                    base.buttonPrev.removeClass("disabled");
                    base.buttonNext.removeClass("disabled");
                }
            }
        },

        updateControls : function () {
            var base = this;
            base.updatePagination();
            base.checkNavigation();
            if (base.owlControls) {
                if (base.options.items >= base.itemsAmount) {
                    base.owlControls.hide();
                } else {
                    base.owlControls.show();
                }
            }
        },

        destroyControls : function () {
            var base = this;
            if (base.owlControls) {
                base.owlControls.remove();
            }
        },

        next : function (speed) {
            var base = this;

            if (base.isTransition) {
                return false;
            }

            base.currentItem += base.options.scrollPerPage === true ? base.options.items : 1;
            if (base.currentItem > base.maximumItem + (base.options.scrollPerPage === true ? (base.options.items - 1) : 0)) {
                if (base.options.rewindNav === true) {
                    base.currentItem = 0;
                    speed = "rewind";
                } else {
                    base.currentItem = base.maximumItem;
                    return false;
                }
            }
            base.goTo(base.currentItem, speed);
        },

        prev : function (speed) {
            var base = this;

            if (base.isTransition) {
                return false;
            }

            if (base.options.scrollPerPage === true && base.currentItem > 0 && base.currentItem < base.options.items) {
                base.currentItem = 0;
            } else {
                base.currentItem -= base.options.scrollPerPage === true ? base.options.items : 1;
            }
            if (base.currentItem < 0) {
                if (base.options.rewindNav === true) {
                    base.currentItem = base.maximumItem;
                    speed = "rewind";
                } else {
                    base.currentItem = 0;
                    return false;
                }
            }
            base.goTo(base.currentItem, speed);
        },

        goTo : function (position, speed, drag) {
            var base = this,
                goToPixel;

            if (base.isTransition) {
                return false;
            }
            if (typeof base.options.beforeMove === "function") {
                base.options.beforeMove.apply(this, [base.$elem]);
            }
            if (position >= base.maximumItem) {
                position = base.maximumItem;
            } else if (position <= 0) {
                position = 0;
            }

            base.currentItem = base.owl.currentItem = position;
            if (base.options.transitionStyle !== false && drag !== "drag" && base.options.items === 1 && base.browser.support3d === true) {
                base.swapSpeed(0);
                if (base.browser.support3d === true) {
                    base.transition3d(base.positionsInArray[position]);
                } else {
                    base.css2slide(base.positionsInArray[position], 1);
                }
                base.afterGo();
                base.singleItemTransition();
                return false;
            }
            goToPixel = base.positionsInArray[position];

            if (base.browser.support3d === true) {
                base.isCss3Finish = false;

                if (speed === true) {
                    base.swapSpeed("paginationSpeed");
                    window.setTimeout(function () {
                        base.isCss3Finish = true;
                    }, base.options.paginationSpeed);

                } else if (speed === "rewind") {
                    base.swapSpeed(base.options.rewindSpeed);
                    window.setTimeout(function () {
                        base.isCss3Finish = true;
                    }, base.options.rewindSpeed);

                } else {
                    base.swapSpeed("slideSpeed");
                    window.setTimeout(function () {
                        base.isCss3Finish = true;
                    }, base.options.slideSpeed);
                }
                base.transition3d(goToPixel);
            } else {
                if (speed === true) {
                    base.css2slide(goToPixel, base.options.paginationSpeed);
                } else if (speed === "rewind") {
                    base.css2slide(goToPixel, base.options.rewindSpeed);
                } else {
                    base.css2slide(goToPixel, base.options.slideSpeed);
                }
            }
            base.afterGo();
        },

        jumpTo : function (position) {
            var base = this;
            if (typeof base.options.beforeMove === "function") {
                base.options.beforeMove.apply(this, [base.$elem]);
            }
            if (position >= base.maximumItem || position === -1) {
                position = base.maximumItem;
            } else if (position <= 0) {
                position = 0;
            }
            base.swapSpeed(0);
            if (base.browser.support3d === true) {
                base.transition3d(base.positionsInArray[position]);
            } else {
                base.css2slide(base.positionsInArray[position], 1);
            }
            base.currentItem = base.owl.currentItem = position;
            base.afterGo();
        },

        afterGo : function () {
            var base = this;

            base.prevArr.push(base.currentItem);
            base.prevItem = base.owl.prevItem = base.prevArr[base.prevArr.length - 2];
            base.prevArr.shift(0);

            if (base.prevItem !== base.currentItem) {
                base.checkPagination();
                base.checkNavigation();
                base.eachMoveUpdate();

                if (base.options.autoPlay !== false) {
                    base.checkAp();
                }
            }
            if (typeof base.options.afterMove === "function" && base.prevItem !== base.currentItem) {
                base.options.afterMove.apply(this, [base.$elem]);
            }
        },

        stop : function () {
            var base = this;
            base.apStatus = "stop";
            window.clearInterval(base.autoPlayInterval);
        },

        checkAp : function () {
            var base = this;
            if (base.apStatus !== "stop") {
                base.play();
            }
        },

        play : function () {
            var base = this;
            base.apStatus = "play";
            if (base.options.autoPlay === false) {
                return false;
            }
            window.clearInterval(base.autoPlayInterval);
            base.autoPlayInterval = window.setInterval(function () {
                base.next(true);
            }, base.options.autoPlay);
        },

        swapSpeed : function (action) {
            var base = this;
            if (action === "slideSpeed") {
                base.$owlWrapper.css(base.addCssSpeed(base.options.slideSpeed));
            } else if (action === "paginationSpeed") {
                base.$owlWrapper.css(base.addCssSpeed(base.options.paginationSpeed));
            } else if (typeof action !== "string") {
                base.$owlWrapper.css(base.addCssSpeed(action));
            }
        },

        addCssSpeed : function (speed) {
            return {
                "-webkit-transition": "all " + speed + "ms ease",
                "-moz-transition": "all " + speed + "ms ease",
                "-o-transition": "all " + speed + "ms ease",
                "transition": "all " + speed + "ms ease"
            };
        },

        removeTransition : function () {
            return {
                "-webkit-transition": "",
                "-moz-transition": "",
                "-o-transition": "",
                "transition": ""
            };
        },

        doTranslate : function (pixels) {
            return {
                "-webkit-transform": "translate3d(" + pixels + "px, 0px, 0px)",
                "-moz-transform": "translate3d(" + pixels + "px, 0px, 0px)",
                "-o-transform": "translate3d(" + pixels + "px, 0px, 0px)",
                "-ms-transform": "translate3d(" + pixels + "px, 0px, 0px)",
                "transform": "translate3d(" + pixels + "px, 0px,0px)"
            };
        },

        transition3d : function (value) {
            var base = this;
            base.$owlWrapper.css(base.doTranslate(value));
        },

        css2move : function (value) {
            var base = this;
            base.$owlWrapper.css({"left" : value});
        },

        css2slide : function (value, speed) {
            var base = this;

            base.isCssFinish = false;
            base.$owlWrapper.stop(true, true).animate({
                "left" : value
            }, {
                duration : speed || base.options.slideSpeed,
                complete : function () {
                    base.isCssFinish = true;
                }
            });
        },

        checkBrowser : function () {
            var base = this,
                translate3D = "translate3d(0px, 0px, 0px)",
                tempElem = document.createElement("div"),
                regex,
                asSupport,
                support3d,
                isTouch;

            tempElem.style.cssText = "  -moz-transform:" + translate3D +
                                  "; -ms-transform:"     + translate3D +
                                  "; -o-transform:"      + translate3D +
                                  "; -webkit-transform:" + translate3D +
                                  "; transform:"         + translate3D;
            regex = /translate3d\(0px, 0px, 0px\)/g;
            asSupport = tempElem.style.cssText.match(regex);
            support3d = (asSupport !== null && asSupport.length === 1);

            isTouch = "ontouchstart" in window || window.navigator.msMaxTouchPoints;

            base.browser = {
                "support3d" : support3d,
                "isTouch" : isTouch
            };
        },

        moveEvents : function () {
            var base = this;
            if (base.options.mouseDrag !== false || base.options.touchDrag !== false) {
                base.gestures();
                base.disabledEvents();
            }
        },

        eventTypes : function () {
            var base = this,
                types = ["s", "e", "x"];

            base.ev_types = {};

            if (base.options.mouseDrag === true && base.options.touchDrag === true) {
                types = [
                    "touchstart.owl mousedown.owl",
                    "touchmove.owl mousemove.owl",
                    "touchend.owl touchcancel.owl mouseup.owl"
                ];
            } else if (base.options.mouseDrag === false && base.options.touchDrag === true) {
                types = [
                    "touchstart.owl",
                    "touchmove.owl",
                    "touchend.owl touchcancel.owl"
                ];
            } else if (base.options.mouseDrag === true && base.options.touchDrag === false) {
                types = [
                    "mousedown.owl",
                    "mousemove.owl",
                    "mouseup.owl"
                ];
            }

            base.ev_types.start = types[0];
            base.ev_types.move = types[1];
            base.ev_types.end = types[2];
        },

        disabledEvents :  function () {
            var base = this;
            base.$elem.on("dragstart.owl", function (event) { event.preventDefault(); });
            base.$elem.on("mousedown.disableTextSelect", function (e) {
                return $(e.target).is('input, textarea, select, option');
            });
        },

        gestures : function () {
            /*jslint unparam: true*/
            var base = this,
                locals = {
                    offsetX : 0,
                    offsetY : 0,
                    baseElWidth : 0,
                    relativePos : 0,
                    position: null,
                    minSwipe : null,
                    maxSwipe: null,
                    sliding : null,
                    dargging: null,
                    targetElement : null
                };

            base.isCssFinish = true;

            function getTouches(event) {
                if (event.touches !== undefined) {
                    return {
                        x : event.touches[0].pageX,
                        y : event.touches[0].pageY
                    };
                }

                if (event.touches === undefined) {
                    if (event.pageX !== undefined) {
                        return {
                            x : event.pageX,
                            y : event.pageY
                        };
                    }
                    if (event.pageX === undefined) {
                        return {
                            x : event.clientX,
                            y : event.clientY
                        };
                    }
                }
            }

            function swapEvents(type) {
                if (type === "on") {
                    $(document).on(base.ev_types.move, dragMove);
                    $(document).on(base.ev_types.end, dragEnd);
                } else if (type === "off") {
                    $(document).off(base.ev_types.move);
                    $(document).off(base.ev_types.end);
                }
            }

            function dragStart(event) {
                var ev = event.originalEvent || event || window.event,
                    position;

                if (ev.which === 3) {
                    return false;
                }
                if (base.itemsAmount <= base.options.items) {
                    return;
                }
                if (base.isCssFinish === false && !base.options.dragBeforeAnimFinish) {
                    return false;
                }
                if (base.isCss3Finish === false && !base.options.dragBeforeAnimFinish) {
                    return false;
                }

                if (base.options.autoPlay !== false) {
                    window.clearInterval(base.autoPlayInterval);
                }

                if (base.browser.isTouch !== true && !base.$owlWrapper.hasClass("grabbing")) {
                    base.$owlWrapper.addClass("grabbing");
                }

                base.newPosX = 0;
                base.newRelativeX = 0;

                $(this).css(base.removeTransition());

                position = $(this).position();
                locals.relativePos = position.left;

                locals.offsetX = getTouches(ev).x - position.left;
                locals.offsetY = getTouches(ev).y - position.top;

                swapEvents("on");

                locals.sliding = false;
                locals.targetElement = ev.target || ev.srcElement;
            }

            function dragMove(event) {
                var ev = event.originalEvent || event || window.event,
                    minSwipe,
                    maxSwipe;

                base.newPosX = getTouches(ev).x - locals.offsetX;
                base.newPosY = getTouches(ev).y - locals.offsetY;
                base.newRelativeX = base.newPosX - locals.relativePos;

                if (typeof base.options.startDragging === "function" && locals.dragging !== true && base.newRelativeX !== 0) {
                    locals.dragging = true;
                    base.options.startDragging.apply(base, [base.$elem]);
                }

                if ((base.newRelativeX > 8 || base.newRelativeX < -8) && (base.browser.isTouch === true)) {
                    if (ev.preventDefault !== undefined) {
                        ev.preventDefault();
                    } else {
                        ev.returnValue = false;
                    }
                    locals.sliding = true;
                }

                if ((base.newPosY > 10 || base.newPosY < -10) && locals.sliding === false) {
                    $(document).off("touchmove.owl");
                }

                minSwipe = function () {
                    return base.newRelativeX / 5;
                };

                maxSwipe = function () {
                    return base.maximumPixels + base.newRelativeX / 5;
                };

                base.newPosX = Math.max(Math.min(base.newPosX, minSwipe()), maxSwipe());
                if (base.browser.support3d === true) {
                    base.transition3d(base.newPosX);
                } else {
                    base.css2move(base.newPosX);
                }
            }

            function dragEnd(event) {
                var ev = event.originalEvent || event || window.event,
                    newPosition,
                    handlers,
                    owlStopEvent;

                ev.target = ev.target || ev.srcElement;

                locals.dragging = false;

                if (base.browser.isTouch !== true) {
                    base.$owlWrapper.removeClass("grabbing");
                }

                if (base.newRelativeX < 0) {
                    base.dragDirection = base.owl.dragDirection = "left";
                } else {
                    base.dragDirection = base.owl.dragDirection = "right";
                }

                if (base.newRelativeX !== 0) {
                    newPosition = base.getNewPosition();
                    base.goTo(newPosition, false, "drag");
                    if (locals.targetElement === ev.target && base.browser.isTouch !== true) {
                        $(ev.target).on("click.disable", function (ev) {
                            ev.stopImmediatePropagation();
                            ev.stopPropagation();
                            ev.preventDefault();
                            $(ev.target).off("click.disable");
                        });
                        handlers = $._data(ev.target, "events").click;
                        owlStopEvent = handlers.pop();
                        handlers.splice(0, 0, owlStopEvent);
                    }
                }
                swapEvents("off");
            }
            base.$elem.on(base.ev_types.start, ".owl-wrapper", dragStart);
        },

        getNewPosition : function () {
            var base = this,
                newPosition = base.closestItem();

            if (newPosition > base.maximumItem) {
                base.currentItem = base.maximumItem;
                newPosition  = base.maximumItem;
            } else if (base.newPosX >= 0) {
                newPosition = 0;
                base.currentItem = 0;
            }
            return newPosition;
        },
        closestItem : function () {
            var base = this,
                array = base.options.scrollPerPage === true ? base.pagesInArray : base.positionsInArray,
                goal = base.newPosX,
                closest = null;

            $.each(array, function (i, v) {
                if (goal - (base.itemWidth / 20) > array[i + 1] && goal - (base.itemWidth / 20) < v && base.moveDirection() === "left") {
                    closest = v;
                    if (base.options.scrollPerPage === true) {
                        base.currentItem = $.inArray(closest, base.positionsInArray);
                    } else {
                        base.currentItem = i;
                    }
                } else if (goal + (base.itemWidth / 20) < v && goal + (base.itemWidth / 20) > (array[i + 1] || array[i] - base.itemWidth) && base.moveDirection() === "right") {
                    if (base.options.scrollPerPage === true) {
                        closest = array[i + 1] || array[array.length - 1];
                        base.currentItem = $.inArray(closest, base.positionsInArray);
                    } else {
                        closest = array[i + 1];
                        base.currentItem = i + 1;
                    }
                }
            });
            return base.currentItem;
        },

        moveDirection : function () {
            var base = this,
                direction;
            if (base.newRelativeX < 0) {
                direction = "right";
                base.playDirection = "next";
            } else {
                direction = "left";
                base.playDirection = "prev";
            }
            return direction;
        },

        customEvents : function () {
            /*jslint unparam: true*/
            var base = this;
            base.$elem.on("owl.next", function () {
                base.next();
            });
            base.$elem.on("owl.prev", function () {
                base.prev();
            });
            base.$elem.on("owl.play", function (event, speed) {
                base.options.autoPlay = speed;
                base.play();
                base.hoverStatus = "play";
            });
            base.$elem.on("owl.stop", function () {
                base.stop();
                base.hoverStatus = "stop";
            });
            base.$elem.on("owl.goTo", function (event, item) {
                base.goTo(item);
            });
            base.$elem.on("owl.jumpTo", function (event, item) {
                base.jumpTo(item);
            });
        },

        stopOnHover : function () {
            var base = this;
            if (base.options.stopOnHover === true && base.browser.isTouch !== true && base.options.autoPlay !== false) {
                base.$elem.on("mouseover", function () {
                    base.stop();
                });
                base.$elem.on("mouseout", function () {
                    if (base.hoverStatus !== "stop") {
                        base.play();
                    }
                });
            }
        },

        lazyLoad : function () {
            var base = this,
                i,
                $item,
                itemNumber,
                $lazyImg,
                follow;

            if (base.options.lazyLoad === false) {
                return false;
            }
            for (i = 0; i < base.itemsAmount; i += 1) {
                $item = $(base.$owlItems[i]);

                if ($item.data("owl-loaded") === "loaded") {
                    continue;
                }

                itemNumber = $item.data("owl-item");
                $lazyImg = $item.find(".lazyOwl");

                if (typeof $lazyImg.data("src") !== "string") {
                    $item.data("owl-loaded", "loaded");
                    continue;
                }
                if ($item.data("owl-loaded") === undefined) {
                    $lazyImg.hide();
                    $item.addClass("loading").data("owl-loaded", "checked");
                }
                if (base.options.lazyFollow === true) {
                    follow = itemNumber >= base.currentItem;
                } else {
                    follow = true;
                }
                if (follow && itemNumber < base.currentItem + base.options.items && $lazyImg.length) {
                    base.lazyPreload($item, $lazyImg);
                }
            }
        },

        lazyPreload : function ($item, $lazyImg) {
            var base = this,
                iterations = 0,
                isBackgroundImg;

            if ($lazyImg.prop("tagName") === "DIV") {
                $lazyImg.css("background-image", "url(" + $lazyImg.data("src") + ")");
                isBackgroundImg = true;
            } else {
                $lazyImg[0].src = $lazyImg.data("src");
            }

            function showImage() {
                $item.data("owl-loaded", "loaded").removeClass("loading");
                $lazyImg.removeAttr("data-src");
                if (base.options.lazyEffect === "fade") {
                    $lazyImg.fadeIn(400);
                } else {
                    $lazyImg.show();
                }
                if (typeof base.options.afterLazyLoad === "function") {
                    base.options.afterLazyLoad.apply(this, [base.$elem]);
                }
            }

            function checkLazyImage() {
                iterations += 1;
                if (base.completeImg($lazyImg.get(0)) || isBackgroundImg === true) {
                    showImage();
                } else if (iterations <= 100) {//if image loads in less than 10 seconds 
                    window.setTimeout(checkLazyImage, 100);
                } else {
                    showImage();
                }
            }

            checkLazyImage();
        },

        autoHeight : function () {
            var base = this,
                $currentimg = $(base.$owlItems[base.currentItem]).find("img"),
                iterations;

            function addHeight() {
                var $currentItem = $(base.$owlItems[base.currentItem]).height();
                base.wrapperOuter.css("height", $currentItem + "px");
                if (!base.wrapperOuter.hasClass("autoHeight")) {
                    window.setTimeout(function () {
                        base.wrapperOuter.addClass("autoHeight");
                    }, 0);
                }
            }

            function checkImage() {
                iterations += 1;
                if (base.completeImg($currentimg.get(0))) {
                    addHeight();
                } else if (iterations <= 100) { //if image loads in less than 10 seconds 
                    window.setTimeout(checkImage, 100);
                } else {
                    base.wrapperOuter.css("height", ""); //Else remove height attribute
                }
            }

            if ($currentimg.get(0) !== undefined) {
                iterations = 0;
                checkImage();
            } else {
                addHeight();
            }
        },

        completeImg : function (img) {
            var naturalWidthType;

            if (!img.complete) {
                return false;
            }
            naturalWidthType = typeof img.naturalWidth;
            if (naturalWidthType !== "undefined" && img.naturalWidth === 0) {
                return false;
            }
            return true;
        },

        onVisibleItems : function () {
            var base = this,
                i;

            if (base.options.addClassActive === true) {
                base.$owlItems.removeClass("active");
            }
            base.visibleItems = [];
            for (i = base.currentItem; i < base.currentItem + base.options.items; i += 1) {
                base.visibleItems.push(i);

                if (base.options.addClassActive === true) {
                    $(base.$owlItems[i]).addClass("active");
                }
            }
            base.owl.visibleItems = base.visibleItems;
        },

        transitionTypes : function (className) {
            var base = this;
            //Currently available: "fade", "backSlide", "goDown", "fadeUp"
            base.outClass = "owl-" + className + "-out";
            base.inClass = "owl-" + className + "-in";
        },

        singleItemTransition : function () {
            var base = this,
                outClass = base.outClass,
                inClass = base.inClass,
                $currentItem = base.$owlItems.eq(base.currentItem),
                $prevItem = base.$owlItems.eq(base.prevItem),
                prevPos = Math.abs(base.positionsInArray[base.currentItem]) + base.positionsInArray[base.prevItem],
                origin = Math.abs(base.positionsInArray[base.currentItem]) + base.itemWidth / 2,
                animEnd = 'webkitAnimationEnd oAnimationEnd MSAnimationEnd animationend';

            base.isTransition = true;

            base.$owlWrapper
                .addClass('owl-origin')
                .css({
                    "-webkit-transform-origin" : origin + "px",
                    "-moz-perspective-origin" : origin + "px",
                    "perspective-origin" : origin + "px"
                });
            function transStyles(prevPos) {
                return {
                    "position" : "relative",
                    "left" : prevPos + "px"
                };
            }

            $prevItem
                .css(transStyles(prevPos, 10))
                .addClass(outClass)
                .on(animEnd, function () {
                    base.endPrev = true;
                    $prevItem.off(animEnd);
                    base.clearTransStyle($prevItem, outClass);
                });

            $currentItem
                .addClass(inClass)
                .on(animEnd, function () {
                    base.endCurrent = true;
                    $currentItem.off(animEnd);
                    base.clearTransStyle($currentItem, inClass);
                });
        },

        clearTransStyle : function (item, classToRemove) {
            var base = this;
            item.css({
                "position" : "",
                "left" : ""
            }).removeClass(classToRemove);

            if (base.endPrev && base.endCurrent) {
                base.$owlWrapper.removeClass('owl-origin');
                base.endPrev = false;
                base.endCurrent = false;
                base.isTransition = false;
            }
        },

        owlStatus : function () {
            var base = this;
            base.owl = {
                "userOptions"   : base.userOptions,
                "baseElement"   : base.$elem,
                "userItems"     : base.$userItems,
                "owlItems"      : base.$owlItems,
                "currentItem"   : base.currentItem,
                "prevItem"      : base.prevItem,
                "visibleItems"  : base.visibleItems,
                "isTouch"       : base.browser.isTouch,
                "browser"       : base.browser,
                "dragDirection" : base.dragDirection
            };
        },

        clearEvents : function () {
            var base = this;
            base.$elem.off(".owl owl mousedown.disableTextSelect");
            $(document).off(".owl owl");
            $(window).off("resize", base.resizer);
        },

        unWrap : function () {
            var base = this;
            if (base.$elem.children().length !== 0) {
                base.$owlWrapper.unwrap();
                base.$userItems.unwrap().unwrap();
                if (base.owlControls) {
                    base.owlControls.remove();
                }
            }
            base.clearEvents();
            base.$elem
                .attr("style", base.$elem.data("owl-originalStyles") || "")
                .attr("class", base.$elem.data("owl-originalClasses"));
        },

        destroy : function () {
            var base = this;
            base.stop();
            window.clearInterval(base.checkVisible);
            base.unWrap();
            base.$elem.removeData();
        },

        reinit : function (newOptions) {
            var base = this,
                options = $.extend({}, base.userOptions, newOptions);
            base.unWrap();
            base.init(options, base.$elem);
        },

        addItem : function (htmlString, targetPosition) {
            var base = this,
                position;

            if (!htmlString) {return false; }

            if (base.$elem.children().length === 0) {
                base.$elem.append(htmlString);
                base.setVars();
                return false;
            }
            base.unWrap();
            if (targetPosition === undefined || targetPosition === -1) {
                position = -1;
            } else {
                position = targetPosition;
            }
            if (position >= base.$userItems.length || position === -1) {
                base.$userItems.eq(-1).after(htmlString);
            } else {
                base.$userItems.eq(position).before(htmlString);
            }

            base.setVars();
        },

        removeItem : function (targetPosition) {
            var base = this,
                position;

            if (base.$elem.children().length === 0) {
                return false;
            }
            if (targetPosition === undefined || targetPosition === -1) {
                position = -1;
            } else {
                position = targetPosition;
            }

            base.unWrap();
            base.$userItems.eq(position).remove();
            base.setVars();
        }

    };

    $.fn.owlCarousel = function (options) {
        return this.each(function () {
            if ($(this).data("owl-init") === true) {
                return false;
            }
            $(this).data("owl-init", true);
            var carousel = Object.create(Carousel);
            carousel.init(options, this);
            $.data(this, "owlCarousel", carousel);
        });
    };

    $.fn.owlCarousel.options = {

        items : 5,
        itemsCustom : false,
        itemsDesktop : [1199, 4],
        itemsDesktopSmall : [979, 3],
        itemsTablet : [768, 2],
        itemsTabletSmall : false,
        itemsMobile : [479, 1],
        singleItem : false,
        itemsScaleUp : false,

        slideSpeed : 200,
        paginationSpeed : 800,
        rewindSpeed : 1000,

        autoPlay : false,
        stopOnHover : false,

        navigation : false,
        navigationText : ["prev", "next"],
        rewindNav : true,
        scrollPerPage : false,

        pagination : true,
        paginationNumbers : false,

        responsive : true,
        responsiveRefreshRate : 200,
        responsiveBaseWidth : window,

        baseClass : "owl-carousel",
        theme : "owl-theme",

        lazyLoad : false,
        lazyFollow : true,
        lazyEffect : "fade",

        autoHeight : false,

        jsonPath : false,
        jsonSuccess : false,

        dragBeforeAnimFinish : true,
        mouseDrag : true,
        touchDrag : true,

        addClassActive : false,
        transitionStyle : false,

        beforeUpdate : false,
        afterUpdate : false,
        beforeInit : false,
        afterInit : false,
        beforeMove : false,
        afterMove : false,
        afterAction : false,
        startDragging : false,
        afterLazyLoad: false
    };
}(jQuery, window, document));

/*
Lightbox for Bootstrap 3 by @ashleydw
https://github.com/ashleydw/lightbox

License: https://github.com/ashleydw/lightbox/blob/master/LICENSE
 */

(function() {
  "use strict";
  var $, EkkoLightbox;

  $ = jQuery;

  EkkoLightbox = function(element, options) {
    var content, footer, header;
    this.options = $.extend({
      title: null,
      footer: null,
      remote: null
    }, $.fn.ekkoLightbox.defaults, options || {});
    this.$element = $(element);
    content = '';
    this.modal_id = this.options.modal_id ? this.options.modal_id : 'ekkoLightbox-' + Math.floor((Math.random() * 1000) + 1);
    header = '<div class="modal-header"' + (this.options.title || this.options.always_show_close ? '' : ' style="display:none"') + '><button type="button" class="close" data-dismiss="modal" aria-hidden="true">&times;</button><h4 class="modal-title">' + (this.options.title || "&nbsp;") + '</h4></div>';
    footer = '<div class="modal-footer"' + (this.options.footer ? '' : ' style="display:none"') + '>' + this.options.footer + '</div>';
    $(document.body).append('<div id="' + this.modal_id + '" class="ekko-lightbox modal fade" tabindex="-1"><div class="modal-dialog"><div class="modal-content">' + header + '<div class="modal-body"><div class="ekko-lightbox-container"><div></div></div></div>' + footer + '</div></div></div>');
    this.modal = $('#' + this.modal_id);
    this.modal_dialog = this.modal.find('.modal-dialog').first();
    this.modal_content = this.modal.find('.modal-content').first();
    this.modal_body = this.modal.find('.modal-body').first();
    this.modal_header = this.modal.find('.modal-header').first();
    this.modal_footer = this.modal.find('.modal-footer').first();
    this.lightbox_container = this.modal_body.find('.ekko-lightbox-container').first();
    this.lightbox_body = this.lightbox_container.find('> div:first-child').first();
    this.showLoading();
    this.modal_arrows = null;
    this.border = {
      top: parseFloat(this.modal_dialog.css('border-top-width')) + parseFloat(this.modal_content.css('border-top-width')) + parseFloat(this.modal_body.css('border-top-width')),
      right: parseFloat(this.modal_dialog.css('border-right-width')) + parseFloat(this.modal_content.css('border-right-width')) + parseFloat(this.modal_body.css('border-right-width')),
      bottom: parseFloat(this.modal_dialog.css('border-bottom-width')) + parseFloat(this.modal_content.css('border-bottom-width')) + parseFloat(this.modal_body.css('border-bottom-width')),
      left: parseFloat(this.modal_dialog.css('border-left-width')) + parseFloat(this.modal_content.css('border-left-width')) + parseFloat(this.modal_body.css('border-left-width'))
    };
    this.padding = {
      top: parseFloat(this.modal_dialog.css('padding-top')) + parseFloat(this.modal_content.css('padding-top')) + parseFloat(this.modal_body.css('padding-top')),
      right: parseFloat(this.modal_dialog.css('padding-right')) + parseFloat(this.modal_content.css('padding-right')) + parseFloat(this.modal_body.css('padding-right')),
      bottom: parseFloat(this.modal_dialog.css('padding-bottom')) + parseFloat(this.modal_content.css('padding-bottom')) + parseFloat(this.modal_body.css('padding-bottom')),
      left: parseFloat(this.modal_dialog.css('padding-left')) + parseFloat(this.modal_content.css('padding-left')) + parseFloat(this.modal_body.css('padding-left'))
    };
    this.modal.on('show.bs.modal', this.options.onShow.bind(this)).on('shown.bs.modal', (function(_this) {
      return function() {
        _this.modal_shown();
        return _this.options.onShown.call(_this);
      };
    })(this)).on('hide.bs.modal', this.options.onHide.bind(this)).on('hidden.bs.modal', (function(_this) {
      return function() {
        if (_this.gallery) {
          $(document).off('keydown.ekkoLightbox');
        }
        _this.modal.remove();
        return _this.options.onHidden.call(_this);
      };
    })(this)).modal('show', options);
    return this.modal;
  };

  EkkoLightbox.prototype = {
    modal_shown: function() {
      var video_id;
      if (!this.options.remote) {
        return this.error('No remote target given');
      } else {
        this.gallery = this.$element.data('gallery');
        if (this.gallery) {
          if (this.options.gallery_parent_selector === 'document.body' || this.options.gallery_parent_selector === '') {
            this.gallery_items = $(document.body).find('*[data-gallery="' + this.gallery + '"]');
          } else {
            this.gallery_items = this.$element.parents(this.options.gallery_parent_selector).first().find('*[data-gallery="' + this.gallery + '"]');
          }
          this.gallery_index = this.gallery_items.index(this.$element);
          $(document).on('keydown.ekkoLightbox', this.navigate.bind(this));
          if (this.options.directional_arrows && this.gallery_items.length > 1) {
            this.lightbox_container.append('<div class="ekko-lightbox-nav-overlay"><a href="#" class="' + this.strip_stops(this.options.left_arrow_class) + '"></a><a href="#" class="' + this.strip_stops(this.options.right_arrow_class) + '"></a></div>');
            this.modal_arrows = this.lightbox_container.find('div.ekko-lightbox-nav-overlay').first();
            this.lightbox_container.find('a' + this.strip_spaces(this.options.left_arrow_class)).on('click', (function(_this) {
              return function(event) {
                event.preventDefault();
                return _this.navigate_left();
              };
            })(this));
            this.lightbox_container.find('a' + this.strip_spaces(this.options.right_arrow_class)).on('click', (function(_this) {
              return function(event) {
                event.preventDefault();
                return _this.navigate_right();
              };
            })(this));
          }
        }
        if (this.options.type) {
          if (this.options.type === 'image') {
            return this.preloadImage(this.options.remote, true);
          } else if (this.options.type === 'youtube' && (video_id = this.getYoutubeId(this.options.remote))) {
            return this.showYoutubeVideo(video_id);
          } else if (this.options.type === 'vimeo') {
            return this.showVimeoVideo(this.options.remote);
          } else if (this.options.type === 'instagram') {
            return this.showInstagramVideo(this.options.remote);
          } else if (this.options.type === 'url') {
            return this.loadRemoteContent(this.options.remote);
          } else if (this.options.type === 'video') {
            return this.showVideoIframe(this.options.remote);
          } else {
            return this.error("Could not detect remote target type. Force the type using data-type=\"image|youtube|vimeo|instagram|url|video\"");
          }
        } else {
          return this.detectRemoteType(this.options.remote);
        }
      }
    },
    strip_stops: function(str) {
      return str.replace(/\./g, '');
    },
    strip_spaces: function(str) {
      return str.replace(/\s/g, '');
    },
    isImage: function(str) {
      return str.match(/(^data:image\/.*,)|(\.(jp(e|g|eg)|gif|png|bmp|webp|svg)((\?|#).*)?$)/i);
    },
    isSwf: function(str) {
      return str.match(/\.(swf)((\?|#).*)?$/i);
    },
    getYoutubeId: function(str) {
      var match;
      match = str.match(/^.*(youtu.be\/|v\/|u\/\w\/|embed\/|watch\?v=|\&v=)([^#\&\?]*).*/);
      if (match && match[2].length === 11) {
        return match[2];
      } else {
        return false;
      }
    },
    getVimeoId: function(str) {
      if (str.indexOf('vimeo') > 0) {
        return str;
      } else {
        return false;
      }
    },
    getInstagramId: function(str) {
      if (str.indexOf('instagram') > 0) {
        return str;
      } else {
        return false;
      }
    },
    navigate: function(event) {
      event = event || window.event;
      if (event.keyCode === 39 || event.keyCode === 37) {
        if (event.keyCode === 39) {
          return this.navigate_right();
        } else if (event.keyCode === 37) {
          return this.navigate_left();
        }
      }
    },
    navigateTo: function(index) {
      var next, src;
      if (index < 0 || index > this.gallery_items.length - 1) {
        return this;
      }
      this.showLoading();
      this.gallery_index = index;
      this.$element = $(this.gallery_items.get(this.gallery_index));
      this.updateTitleAndFooter();
      src = this.$element.attr('data-remote') || this.$element.attr('href');
      this.detectRemoteType(src, this.$element.attr('data-type') || false);
      if (this.gallery_index + 1 < this.gallery_items.length) {
        next = $(this.gallery_items.get(this.gallery_index + 1), false);
        src = next.attr('data-remote') || next.attr('href');
        if (next.attr('data-type') === 'image' || this.isImage(src)) {
          return this.preloadImage(src, false);
        }
      }
    },
    navigate_left: function() {
      if (this.gallery_items.length === 1) {
        return;
      }
      if (this.gallery_index === 0) {
        this.gallery_index = this.gallery_items.length - 1;
      } else {
        this.gallery_index--;
      }
      this.options.onNavigate.call(this, 'left', this.gallery_index);
      return this.navigateTo(this.gallery_index);
    },
    navigate_right: function() {
      if (this.gallery_items.length === 1) {
        return;
      }
      if (this.gallery_index === this.gallery_items.length - 1) {
        this.gallery_index = 0;
      } else {
        this.gallery_index++;
      }
      this.options.onNavigate.call(this, 'right', this.gallery_index);
      return this.navigateTo(this.gallery_index);
    },
    detectRemoteType: function(src, type) {
      var video_id;
      type = type || false;
      if (type === 'image' || this.isImage(src)) {
        this.options.type = 'image';
        return this.preloadImage(src, true);
      } else if (type === 'youtube' || (video_id = this.getYoutubeId(src))) {
        this.options.type = 'youtube';
        return this.showYoutubeVideo(video_id);
      } else if (type === 'vimeo' || (video_id = this.getVimeoId(src))) {
        this.options.type = 'vimeo';
        return this.showVimeoVideo(video_id);
      } else if (type === 'instagram' || (video_id = this.getInstagramId(src))) {
        this.options.type = 'instagram';
        return this.showInstagramVideo(video_id);
      } else if (type === 'video') {
        this.options.type = 'video';
        return this.showVideoIframe(src);
      } else {
        this.options.type = 'url';
        return this.loadRemoteContent(src);
      }
    },
    updateTitleAndFooter: function() {
      var caption, footer, header, title;
      header = this.modal_content.find('.modal-header');
      footer = this.modal_content.find('.modal-footer');
      title = this.$element.data('title') || "";
      caption = this.$element.data('footer') || "";
      if (title || this.options.always_show_close) {
        header.css('display', '').find('.modal-title').html(title || "&nbsp;");
      } else {
        header.css('display', 'none');
      }
      if (caption) {
        footer.css('display', '').html(caption);
      } else {
        footer.css('display', 'none');
      }
      return this;
    },
    showLoading: function() {
      this.lightbox_body.html('<div class="modal-loading">' + this.options.loadingMessage + '</div>');
      return this;
    },
    showYoutubeVideo: function(id) {
      var height, rel, width;
      if ((this.$element.attr('data-norelated') != null) || this.options.no_related) {
        rel = "&rel=0";
      } else {
        rel = "";
      }
      width = this.checkDimensions(this.$element.data('width') || 560);
      height = width / (560 / 315);
      return this.showVideoIframe('//www.youtube.com/embed/' + id + '?badge=0&autoplay=1&html5=1' + rel, width, height);
    },
    showVimeoVideo: function(id) {
      var height, width;
      width = this.checkDimensions(this.$element.data('width') || 560);
      height = width / (500 / 281);
      return this.showVideoIframe(id + '?autoplay=1', width, height);
    },
    showInstagramVideo: function(id) {
      var height, width;
      width = this.checkDimensions(this.$element.data('width') || 612);
      this.resize(width);
      height = width + 80;
      this.lightbox_body.html('<iframe width="' + width + '" height="' + height + '" src="' + this.addTrailingSlash(id) + 'embed/" frameborder="0" allowfullscreen></iframe>');
      this.options.onContentLoaded.call(this);
      if (this.modal_arrows) {
        return this.modal_arrows.css('display', 'none');
      }
    },
    showVideoIframe: function(url, width, height) {
      height = height || width;
      this.resize(width);
      this.lightbox_body.html('<div class="embed-responsive embed-responsive-16by9"><iframe width="' + width + '" height="' + height + '" src="' + url + '" frameborder="0" allowfullscreen class="embed-responsive-item"></iframe></div>');
      this.options.onContentLoaded.call(this);
      if (this.modal_arrows) {
        this.modal_arrows.css('display', 'none');
      }
      return this;
    },
    loadRemoteContent: function(url) {
      var disableExternalCheck, width;
      width = this.$element.data('width') || 560;
      this.resize(width);
      disableExternalCheck = this.$element.data('disableExternalCheck') || false;
      if (!disableExternalCheck && !this.isExternal(url)) {
        this.lightbox_body.load(url, $.proxy((function(_this) {
          return function() {
            return _this.$element.trigger('loaded.bs.modal');
          };
        })(this)));
      } else {
        this.lightbox_body.html('<iframe width="' + width + '" height="' + width + '" src="' + url + '" frameborder="0" allowfullscreen></iframe>');
        this.options.onContentLoaded.call(this);
      }
      if (this.modal_arrows) {
        this.modal_arrows.css('display', 'none');
      }
      return this;
    },
    isExternal: function(url) {
      var match;
      match = url.match(/^([^:\/?#]+:)?(?:\/\/([^\/?#]*))?([^?#]+)?(\?[^#]*)?(#.*)?/);
      if (typeof match[1] === "string" && match[1].length > 0 && match[1].toLowerCase() !== location.protocol) {
        return true;
      }
      if (typeof match[2] === "string" && match[2].length > 0 && match[2].replace(new RegExp(":(" + {
        "http:": 80,
        "https:": 443
      }[location.protocol] + ")?$"), "") !== location.host) {
        return true;
      }
      return false;
    },
    error: function(message) {
      this.lightbox_body.html(message);
      return this;
    },
    preloadImage: function(src, onLoadShowImage) {
      var img;
      img = new Image();
      if ((onLoadShowImage == null) || onLoadShowImage === true) {
        img.onload = (function(_this) {
          return function() {
            var image;
            image = $('<img />');
            image.attr('src', img.src);
            image.addClass('img-responsive');
            _this.lightbox_body.html(image);
            if (_this.modal_arrows) {
              _this.modal_arrows.css('display', 'block');
            }
            return image.load(function() {
              if (_this.options.scale_height) {
                _this.scaleHeight(img.height, img.width);
              } else {
                _this.resize(img.width);
              }
              return _this.options.onContentLoaded.call(_this);
            });
          };
        })(this);
        img.onerror = (function(_this) {
          return function() {
            return _this.error('Failed to load image: ' + src);
          };
        })(this);
      }
      img.src = src;
      return img;
    },
    scaleHeight: function(height, width) {
      var border_padding, factor, footer_height, header_height, margins, max_height;
      header_height = this.modal_header.outerHeight(true) || 0;
      footer_height = this.modal_footer.outerHeight(true) || 0;
      if (!this.modal_footer.is(':visible')) {
        footer_height = 0;
      }
      if (!this.modal_header.is(':visible')) {
        header_height = 0;
      }
      border_padding = this.border.top + this.border.bottom + this.padding.top + this.padding.bottom;
      margins = parseFloat(this.modal_dialog.css('margin-top')) + parseFloat(this.modal_dialog.css('margin-bottom'));
      max_height = $(window).height() - border_padding - margins - header_height - footer_height;
      factor = Math.min(max_height / height, 1);
      this.modal_dialog.css('height', 'auto').css('max-height', max_height);
      return this.resize(factor * width);
    },
    resize: function(width) {
      var width_total;
      width_total = width + this.border.left + this.padding.left + this.padding.right + this.border.right;
      this.modal_dialog.css('width', 'auto').css('max-width', width_total);
      this.lightbox_container.find('a').css('line-height', function() {
        return $(this).parent().height() + 'px';
      });
      return this;
    },
    checkDimensions: function(width) {
      var body_width, width_total;
      width_total = width + this.border.left + this.padding.left + this.padding.right + this.border.right;
      body_width = document.body.clientWidth;
      if (width_total > body_width) {
        width = this.modal_body.width();
      }
      return width;
    },
    close: function() {
      return this.modal.modal('hide');
    },
    addTrailingSlash: function(url) {
      if (url.substr(-1) !== '/') {
        url += '/';
      }
      return url;
    }
  };

  $.fn.ekkoLightbox = function(options) {
    return this.each(function() {
      var $this;
      $this = $(this);
      options = $.extend({
        remote: $this.attr('data-remote') || $this.attr('href'),
        gallery_parent_selector: $this.attr('data-parent'),
        type: $this.attr('data-type')
      }, options, $this.data());
      new EkkoLightbox(this, options);
      return this;
    });
  };

  $.fn.ekkoLightbox.defaults = {
    gallery_parent_selector: 'document.body',
    left_arrow_class: '.glyphicon .glyphicon-chevron-left',
    right_arrow_class: '.glyphicon .glyphicon-chevron-right',
    directional_arrows: true,
    type: null,
    always_show_close: true,
    no_related: false,
    scale_height: true,
    loadingMessage: 'Loading...',
    onShow: function() {},
    onShown: function() {},
    onHide: function() {},
    onHidden: function() {},
    onNavigate: function() {},
    onContentLoaded: function() {}
  };

}).call(this);

/**
 * EvEmitter v1.0.3
 * Lil' event emitter
 * MIT License
 */

/* jshint unused: true, undef: true, strict: true */

( function( global, factory ) {
  // universal module definition
  /* jshint strict: false */ /* globals define, module, window */
  if ( typeof define == 'function' && define.amd ) {
    // AMD - RequireJS
    define( factory );
  } else if ( typeof module == 'object' && module.exports ) {
    // CommonJS - Browserify, Webpack
    module.exports = factory();
  } else {
    // Browser globals
    global.EvEmitter = factory();
  }

}( typeof window != 'undefined' ? window : this, function() {

"use strict";

function EvEmitter() {}

var proto = EvEmitter.prototype;

proto.on = function( eventName, listener ) {
  if ( !eventName || !listener ) {
    return;
  }
  // set events hash
  var events = this._events = this._events || {};
  // set listeners array
  var listeners = events[ eventName ] = events[ eventName ] || [];
  // only add once
  if ( listeners.indexOf( listener ) == -1 ) {
    listeners.push( listener );
  }

  return this;
};

proto.once = function( eventName, listener ) {
  if ( !eventName || !listener ) {
    return;
  }
  // add event
  this.on( eventName, listener );
  // set once flag
  // set onceEvents hash
  var onceEvents = this._onceEvents = this._onceEvents || {};
  // set onceListeners object
  var onceListeners = onceEvents[ eventName ] = onceEvents[ eventName ] || {};
  // set flag
  onceListeners[ listener ] = true;

  return this;
};

proto.off = function( eventName, listener ) {
  var listeners = this._events && this._events[ eventName ];
  if ( !listeners || !listeners.length ) {
    return;
  }
  var index = listeners.indexOf( listener );
  if ( index != -1 ) {
    listeners.splice( index, 1 );
  }

  return this;
};

proto.emitEvent = function( eventName, args ) {
  var listeners = this._events && this._events[ eventName ];
  if ( !listeners || !listeners.length ) {
    return;
  }
  var i = 0;
  var listener = listeners[i];
  args = args || [];
  // once stuff
  var onceListeners = this._onceEvents && this._onceEvents[ eventName ];

  while ( listener ) {
    var isOnce = onceListeners && onceListeners[ listener ];
    if ( isOnce ) {
      // remove listener
      // remove before trigger to prevent recursion
      this.off( eventName, listener );
      // unset once flag
      delete onceListeners[ listener ];
    }
    // trigger listener
    listener.apply( this, args );
    // get next listener
    i += isOnce ? 0 : 1;
    listener = listeners[i];
  }

  return this;
};

return EvEmitter;

}));

/*!
 * imagesLoaded v4.1.2
 * JavaScript is all like "You images are done yet or what?"
 * MIT License
 */

( function( window, factory ) { 'use strict';
  // universal module definition

  /*global define: false, module: false, require: false */

  if ( typeof define == 'function' && define.amd ) {
    // AMD
    define( [
      'ev-emitter/ev-emitter'
    ], function( EvEmitter ) {
      return factory( window, EvEmitter );
    });
  } else if ( typeof module == 'object' && module.exports ) {
    // CommonJS
    module.exports = factory(
      window,
      require('ev-emitter')
    );
  } else {
    // browser global
    window.imagesLoaded = factory(
      window,
      window.EvEmitter
    );
  }

})( typeof window !== 'undefined' ? window : this,

// --------------------------  factory -------------------------- //

function factory( window, EvEmitter ) {

'use strict';

var $ = window.jQuery;
var console = window.console;

// -------------------------- helpers -------------------------- //

// extend objects
function extend( a, b ) {
  for ( var prop in b ) {
    a[ prop ] = b[ prop ];
  }
  return a;
}

// turn element or nodeList into an array
function makeArray( obj ) {
  var ary = [];
  if ( Array.isArray( obj ) ) {
    // use object if already an array
    ary = obj;
  } else if ( typeof obj.length == 'number' ) {
    // convert nodeList to array
    for ( var i=0; i < obj.length; i++ ) {
      ary.push( obj[i] );
    }
  } else {
    // array of single index
    ary.push( obj );
  }
  return ary;
}

// -------------------------- imagesLoaded -------------------------- //

/**
 * @param {Array, Element, NodeList, String} elem
 * @param {Object or Function} options - if function, use as callback
 * @param {Function} onAlways - callback function
 */
function ImagesLoaded( elem, options, onAlways ) {
  // coerce ImagesLoaded() without new, to be new ImagesLoaded()
  if ( !( this instanceof ImagesLoaded ) ) {
    return new ImagesLoaded( elem, options, onAlways );
  }
  // use elem as selector string
  if ( typeof elem == 'string' ) {
    elem = document.querySelectorAll( elem );
  }

  this.elements = makeArray( elem );
  this.options = extend( {}, this.options );

  if ( typeof options == 'function' ) {
    onAlways = options;
  } else {
    extend( this.options, options );
  }

  if ( onAlways ) {
    this.on( 'always', onAlways );
  }

  this.getImages();

  if ( $ ) {
    // add jQuery Deferred object
    this.jqDeferred = new $.Deferred();
  }

  // HACK check async to allow time to bind listeners
  setTimeout( function() {
    this.check();
  }.bind( this ));
}

ImagesLoaded.prototype = Object.create( EvEmitter.prototype );

ImagesLoaded.prototype.options = {};

ImagesLoaded.prototype.getImages = function() {
  this.images = [];

  // filter & find items if we have an item selector
  this.elements.forEach( this.addElementImages, this );
};

/**
 * @param {Node} element
 */
ImagesLoaded.prototype.addElementImages = function( elem ) {
  // filter siblings
  if ( elem.nodeName == 'IMG' ) {
    this.addImage( elem );
  }
  // get background image on element
  if ( this.options.background === true ) {
    this.addElementBackgroundImages( elem );
  }

  // find children
  // no non-element nodes, #143
  var nodeType = elem.nodeType;
  if ( !nodeType || !elementNodeTypes[ nodeType ] ) {
    return;
  }
  var childImgs = elem.querySelectorAll('img');
  // concat childElems to filterFound array
  for ( var i=0; i < childImgs.length; i++ ) {
    var img = childImgs[i];
    this.addImage( img );
  }

  // get child background images
  if ( typeof this.options.background == 'string' ) {
    var children = elem.querySelectorAll( this.options.background );
    for ( i=0; i < children.length; i++ ) {
      var child = children[i];
      this.addElementBackgroundImages( child );
    }
  }
};

var elementNodeTypes = {
  1: true,
  9: true,
  11: true
};

ImagesLoaded.prototype.addElementBackgroundImages = function( elem ) {
  var style = getComputedStyle( elem );
  if ( !style ) {
    // Firefox returns null if in a hidden iframe https://bugzil.la/548397
    return;
  }
  // get url inside url("...")
  var reURL = /url\((['"])?(.*?)\1\)/gi;
  var matches = reURL.exec( style.backgroundImage );
  while ( matches !== null ) {
    var url = matches && matches[2];
    if ( url ) {
      this.addBackground( url, elem );
    }
    matches = reURL.exec( style.backgroundImage );
  }
};

/**
 * @param {Image} img
 */
ImagesLoaded.prototype.addImage = function( img ) {
  var loadingImage = new LoadingImage( img );
  this.images.push( loadingImage );
};

ImagesLoaded.prototype.addBackground = function( url, elem ) {
  var background = new Background( url, elem );
  this.images.push( background );
};

ImagesLoaded.prototype.check = function() {
  var _this = this;
  this.progressedCount = 0;
  this.hasAnyBroken = false;
  // complete if no images
  if ( !this.images.length ) {
    this.complete();
    return;
  }

  function onProgress( image, elem, message ) {
    // HACK - Chrome triggers event before object properties have changed. #83
    setTimeout( function() {
      _this.progress( image, elem, message );
    });
  }

  this.images.forEach( function( loadingImage ) {
    loadingImage.once( 'progress', onProgress );
    loadingImage.check();
  });
};

ImagesLoaded.prototype.progress = function( image, elem, message ) {
  this.progressedCount++;
  this.hasAnyBroken = this.hasAnyBroken || !image.isLoaded;
  // progress event
  this.emitEvent( 'progress', [ this, image, elem ] );
  if ( this.jqDeferred && this.jqDeferred.notify ) {
    this.jqDeferred.notify( this, image );
  }
  // check if completed
  if ( this.progressedCount == this.images.length ) {
    this.complete();
  }

  if ( this.options.debug && console ) {
    console.log( 'progress: ' + message, image, elem );
  }
};

ImagesLoaded.prototype.complete = function() {
  var eventName = this.hasAnyBroken ? 'fail' : 'done';
  this.isComplete = true;
  this.emitEvent( eventName, [ this ] );
  this.emitEvent( 'always', [ this ] );
  if ( this.jqDeferred ) {
    var jqMethod = this.hasAnyBroken ? 'reject' : 'resolve';
    this.jqDeferred[ jqMethod ]( this );
  }
};

// --------------------------  -------------------------- //

function LoadingImage( img ) {
  this.img = img;
}

LoadingImage.prototype = Object.create( EvEmitter.prototype );

LoadingImage.prototype.check = function() {
  // If complete is true and browser supports natural sizes,
  // try to check for image status manually.
  var isComplete = this.getIsImageComplete();
  if ( isComplete ) {
    // report based on naturalWidth
    this.confirm( this.img.naturalWidth !== 0, 'naturalWidth' );
    return;
  }

  // If none of the checks above matched, simulate loading on detached element.
  this.proxyImage = new Image();
  this.proxyImage.addEventListener( 'load', this );
  this.proxyImage.addEventListener( 'error', this );
  // bind to image as well for Firefox. #191
  this.img.addEventListener( 'load', this );
  this.img.addEventListener( 'error', this );
  this.proxyImage.src = this.img.src;
};

LoadingImage.prototype.getIsImageComplete = function() {
  return this.img.complete && this.img.naturalWidth !== undefined;
};

LoadingImage.prototype.confirm = function( isLoaded, message ) {
  this.isLoaded = isLoaded;
  this.emitEvent( 'progress', [ this, this.img, message ] );
};

// ----- events ----- //

// trigger specified handler for event type
LoadingImage.prototype.handleEvent = function( event ) {
  var method = 'on' + event.type;
  if ( this[ method ] ) {
    this[ method ]( event );
  }
};

LoadingImage.prototype.onload = function() {
  this.confirm( true, 'onload' );
  this.unbindEvents();
};

LoadingImage.prototype.onerror = function() {
  this.confirm( false, 'onerror' );
  this.unbindEvents();
};

LoadingImage.prototype.unbindEvents = function() {
  this.proxyImage.removeEventListener( 'load', this );
  this.proxyImage.removeEventListener( 'error', this );
  this.img.removeEventListener( 'load', this );
  this.img.removeEventListener( 'error', this );
};

// -------------------------- Background -------------------------- //

function Background( url, element ) {
  this.url = url;
  this.element = element;
  this.img = new Image();
}

// inherit LoadingImage prototype
Background.prototype = Object.create( LoadingImage.prototype );

Background.prototype.check = function() {
  this.img.addEventListener( 'load', this );
  this.img.addEventListener( 'error', this );
  this.img.src = this.url;
  // check if image is already complete
  var isComplete = this.getIsImageComplete();
  if ( isComplete ) {
    this.confirm( this.img.naturalWidth !== 0, 'naturalWidth' );
    this.unbindEvents();
  }
};

Background.prototype.unbindEvents = function() {
  this.img.removeEventListener( 'load', this );
  this.img.removeEventListener( 'error', this );
};

Background.prototype.confirm = function( isLoaded, message ) {
  this.isLoaded = isLoaded;
  this.emitEvent( 'progress', [ this, this.element, message ] );
};

// -------------------------- jQuery -------------------------- //

ImagesLoaded.makeJQueryPlugin = function( jQuery ) {
  jQuery = jQuery || window.jQuery;
  if ( !jQuery ) {
    return;
  }
  // set local variable
  $ = jQuery;
  // $().imagesLoaded()
  $.fn.imagesLoaded = function( options, callback ) {
    var instance = new ImagesLoaded( this, options, callback );
    return instance.jqDeferred.promise( $(this) );
  };
};
// try making plugin
ImagesLoaded.makeJQueryPlugin();

// --------------------------  -------------------------- //

return ImagesLoaded;

});

/* global window, module, define, jQuery, require */
/*
 * Bootstrap ColEqualizer v1.1.0
 * https://github.com/megasmack/bootstrap-colequalizer
 */

;(function(factory) {
    'use strict';

    if (typeof define === 'function' && define.amd) {
        define(['jquery'], factory);
    } else if (typeof exports !== 'undefined') {
        module.exports = factory(require('jquery'));
    } else {
        factory(jQuery);
    }

}(function($) {
    'use strict';

    var ColEqualizer = window.ColEqualizer || {};

    ColEqualizer = (function () {

        function ColEqualizer(element) {

            var _ = this;

            _.el = element;

            _.colHeight(_.el);

            _.winLoad();

        }

        return ColEqualizer;

    }());

    ColEqualizer.prototype.colHeight = function(element) {

        var _ = this;

        $(element).each(function (index,el) {
            _.colReset(el);
            var tallest = 0;
            $('> [class*=col-]',el).each(function (i,e) {
                var testHeight = $(e).height();
                if (testHeight > tallest) {
                    tallest = testHeight;
                }
            });
            $('> [class*=col-]',el).height(tallest);
        });

    };

    ColEqualizer.prototype.colReset = function(el) {

        $('> [class*=col-]',el).height('auto');

    };

    ColEqualizer.prototype.resizeWindow = function() {

        var _ = this;
        var viewWidth = Math.max(document.documentElement.clientWidth, window.innerWidth || 0);

        // If set, minWidth shows/hides the nav based on the size of the browser
        // If minWidth not set, nav will always show
        if (768 <= viewWidth) {
            _.colHeight(_.el);
        } else {
            _.colReset(_.el);
        }

    };

    ColEqualizer.prototype.winLoad = function() {

        var _ = this;
        var $win = $(window);

        $win.load(function () {
            // Run function on window resize
            $win.on('resize', function () {
                _.resizeWindow();
            });
            // Run function the first time
            _.resizeWindow();
        });

    };

    // jQuery Plugin
    $.fn.colequalizer = function () {

        var _ = this;

        return _.each(function (index, element) {
            element.navinator = new ColEqualizer(element);
        });

    };
}));

(function($) {
  $(function() {

    $('.owl-carousel').each(function(){
      var options = (typeof $(this).attr('data-options') !== "undefined") ? $.parseJSON($(this).attr('data-options')) : {};
      options.navigationText = [
        '<span class="glyphicon glyphicon-chevron-left" aria-hidden="true"></span>',
        '<span class="glyphicon glyphicon-chevron-right" aria-hidden="true"></span>'
      ];
      $(this).owlCarousel(options);
    });
    
  });
})(jQuery);

(function($) {
  $(function() {
    $('.counter').each(function() {
      var valTo = $(this).data('to');
      var increment = valTo / ($(this).data('speed')/100);
      var _this = this;
      var interval = setInterval(function() {
        var newVal = Math.ceil(parseInt($(_this).html()) + increment);
        $(_this).html(newVal);
        if (newVal >= valTo) {
          $(_this).html(valTo);
          clearInterval(interval);
        }
      }, 100);
    });
  });
})(jQuery);

(function($) {
  $(function() {
    $('input[type="file"]').on('change', function(e) {
      var fileName = $(this).val().split('\\')[$(this).val().split('\\').length-1];
      $('label[for="'+$(this).attr('id')+'"]').html(fileName).addClass('active');
    });

  });
})(jQuery);

(function ($) {
    $(function () {
        $('.grid-filter').each(function () {
            var $grid = $($(this).attr('data-grid-filter'));
            var $filters = $(this).find('[data-group]');
            $grid.shuffle({
                itemSelector: '[data-groups]'
            });
            $grid.get(0).addEventListener(Shuffle.EventType.LAYOUT, function () {
                $(this).find('img').imagesLoaded().always(function (instance) {
                    console.log('loading image!');
                    $grid.shuffle('update');
                }).done(function () {
                    $grid.shuffle('update');
                    console.log('done loading');
                    setTimeout(function () {
                        $grid.shuffle('update');
                    }, 1000);
                });
            });
            $(this).find('img').imagesLoaded().always(function (instance) {
                console.log('loading image!');
                $grid.shuffle('update');
            }).done(function () {
                $grid.shuffle('update');
                console.log('done loading');
                setTimeout(function () {
                    $grid.shuffle('update');
                }, 1000);
            });
            $filters.on('click', function (e) {
                $filters.removeClass('active');
                $(this).addClass('active');
                $grid.shuffle('shuffle', $(this).attr('data-group'));
            });
            $(this).find('.grid-filter-sort').on('change', function () {
                var reverse = $(this).data('reverse') || false;
                var sort = this.value,
                    opts = {
                        reverse: !reverse,
                        by: function ($el) {
                            return $el.attr('data-' + sort);
                        }
                    };
                $grid.shuffle('sort', opts);
            });
            $(this).find('.grid-filter-search').on('keyup change', function (e) {
                var value = this.value.toLowerCase();
                $grid.shuffle('shuffle', function ($el, shuffle) {
                    return $el.attr('data-search-term').toLowerCase().indexOf(value) !== -1;
                });
            });
        });
    });
    $('.row-col-eq').colequalizer();
})(jQuery);

(function($) {
  $(function() {
    $(document).delegate('*[data-toggle="lightbox"]', 'click', function (event) {
      event.preventDefault();
      $(this).ekkoLightbox();
    });
  });
})(jQuery);

(function($) {
  $(function() {
    $('[data-toggle="tooltip"]').tooltip();
  });
})(jQuery);

(function($) {
  $(function() {
    $('[data-toggle-class]').on('click keypress', function(e) {
      e.preventDefault();
      switch ($(this).data('toggle-class')) {
        case "is-searching":
          $('#siteNavbar').collapse('hide');
          break;
      }
      $($(this).attr('data-target')).toggleClass($(this).attr('data-toggle-class'));
      $(this).parent().find('input').focus();
    });
  });
})(jQuery);

(function($) {
  $(function() {
    $('.sidebar.sidebar-dynamic ul li a').on('click', function(e) {
      e.preventDefault();
      $(this).parent().toggleClass('open');
    });
    
    $('body').on('click', '[data-toggle="sidebar"]', function(e) {
      e.preventDefault();
      console.log(this);
      $('html').toggleClass('show-sidebar-left', $(this).attr('data-side') == 'left' && !$('html').hasClass('show-sidebar-left'));
      $('html').toggleClass('show-sidebar-right', $(this).attr('data-side') == 'right' && !$('html').hasClass('show-sidebar-right'));
      $('.sidebar-closed, .sidebar-opened').toggle();
    });
    
    !$('html').hasClass('show-sidebar-left') && !$('html').hasClass('show-sidebar-left') ? $('.sidebar-closed').show() && $('.sidebar-opened').hide() : $('.sidebar-closed').hide() && $('.sidebar-opened').show();
  });
})(jQuery);

(function($) {
  $(function() {
    $('[href="styles/white-wonder.css"]').attr('data-themetype', 'main');
    $('[href="styles/dayfrost.css"]').attr('data-themetype', 'main');
    $('[href="styles/niteflight.css"]').attr('data-themetype', 'main');

    $('[data-toggle="theme"]').on('click', function() {
      $('[data-themetype="'+$(this).attr('data-themetype')+'"]').attr('disabled', 'disabled');
      $('[href="styles/'+$(this).attr('data-theme')+'.css"]').removeAttr('disabled');
    });
  });
})(jQuery);

(function($) {
  $(function() {
    $('[data-toggle="popover"]').popover();
  });
})(jQuery);

(function($) {
  $(function() {
    var mq;

    var $mqElement = $($.parseHTML('<span id="mq-detector"><span class="visible-xs"></span><span class="visible-sm"></span><span class="visible-md"></span><span class="visible-lg"></span></span>'));
    $mqElement.css('visibility', 'hidden');

    $('body').append($mqElement);
    $mqElement.children().each(function() {
      if($(this).is(':visible')) {
        mq = $(this).attr('class').substring($(this).attr('class').length-2);
      };
    });

  });
})(jQuery);