# plur [![Build Status](https://travis-ci.org/sindresorhus/plur.svg?branch=master)](https://travis-ci.org/sindresorhus/plur)

> Naively pluralize a word


## Install

```
$ npm install --save plur
```


## Usage

```js
var plur = require('plur');

plur('unicorn', 4);
//=> 'unicorns'

plur('hero', 'heroes', 4);
//=> 'heroes'
```

Words ending in *s*, *x*, *z*, *ch*, *sh* will be pluralized with *-es*, like
*foxes*. Words ending in *y* will be pluralized by replacing *y* with *-ies* as
in *puppies*. The plural suffix will match the case of the last letter in the word.

## API

### plur(word, [plural], count)

#### word

Type: `string`

Word to pluralize.

#### plural

Type: `string`  
Default: `word` + `s`

Pluralized word.

#### count

Type: `number`

Count to determine whether to use singular or plural.


## License

MIT © [Sindre Sorhus](http://sindresorhus.com)
