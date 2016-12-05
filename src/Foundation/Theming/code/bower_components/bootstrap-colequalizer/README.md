# Bootstrap ColEqualizer

A jQuery plugin that dynamically equalizes the height of uneven column elements in Bootstrap 3 grids.

## Requirements

- jQuery 1.11.0 or higher
- Bootstrap 3.x.x

## Usage

Just load the script before the end of `body` tag and call the script using a selector on your Bootstrap row.

```javascript
	$(selector).colequalizer();
```

NOTE: You may want to avoid selecting the class `row` as it may affect grids you may not want cleared. I suggest adding a class specifically for clearing. Like `col-eq` for example.

## Example

Here's a typical Bootstrap grid:
```html
<div class="row col-eq">
	<div class="col-sm-3"></div>
	<div class="col-sm-3"></div>
	<div class="col-sm-3"></div>
	<div class="col-sm-3"></div>
	<div class="col-sm-3"></div>
</div>
```

To clear these rows on a typical 12 column grid, just run this script:
```html
<script src="bootstrap-colequalizer.js"></script>
<script>
	$('.col-eq').rowequalizer();
</script>
```

Which will output this in the browser:
```html
<div class="row row-eq">
	<div class="col-sm-3" style="height: 120px;"></div>
	<div class="col-sm-3" style="height: 120px;"></div>
	<div class="col-sm-3" style="height: 120px;"></div>
	<div class="col-sm-3" style="height: 120px;"></div>
	<div class="col-sm-3" style="height: 120px;"></div>
</div>
```

## Notes

- Heights will update based on window resize to account for responsive styles. Yay!
- Currently heights reset to auto when the viewport is within Bootstrap’s `xs` breakpoint. Future versions may make this more flexible.
- If you don’t need the column heights to be equal and you just want elements to clear each other, checkout [Bootstrap RowEqualizer](https://github.com/gsmke/bootstrap-rowequalizer). It will clear each "row" without matching ALL the heights. Plus front-end performance fares better as well.

## Known Issues

- This will only work with Bootstrap’s default class names. If you are making your own class names via LESS or SASS, you’ll need to find other means of clearing rows. Alternatively, you can fake it by adding a non-Bootstrap class that starts with "col-". For example, `col-item` would work.
- I'm assuming Bootstrap 4’s Flexbox support may make this script obsolete. At the time of writing this, I haven't had a chance to work with Bootstrap 4 to see if this is the case. Given that there are new breakpoints in Bootstrap 4, I can say this script likely won't work at this time.
- May support older versions of jQuery. Untested.
