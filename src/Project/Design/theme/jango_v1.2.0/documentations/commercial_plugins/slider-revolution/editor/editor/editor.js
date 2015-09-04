/*!
 * jScrollPane - v2.0.14 - 2013-05-01
 * http://jscrollpane.kelvinluck.com/
 *
 * Copyright (c) 2010 Kelvin Luck
 * Dual licensed under the MIT or GPL licenses.
 */
(function(b,a,c){b.fn.jScrollPane=function(e){function d(D,O){var ay,Q=this,Y,aj,v,al,T,Z,y,q,az,aE,au,i,I,h,j,aa,U,ap,X,t,A,aq,af,am,G,l,at,ax,x,av,aH,f,L,ai=true,P=true,aG=false,k=false,ao=D.clone(false,false).empty(),ac=b.fn.mwheelIntent?"mwheelIntent.jsp":"mousewheel.jsp";aH=D.css("paddingTop")+" "+D.css("paddingRight")+" "+D.css("paddingBottom")+" "+D.css("paddingLeft");f=(parseInt(D.css("paddingLeft"),10)||0)+(parseInt(D.css("paddingRight"),10)||0);function ar(aQ){var aL,aN,aM,aJ,aI,aP,aO=false,aK=false;ay=aQ;if(Y===c){aI=D.scrollTop();aP=D.scrollLeft();D.css({overflow:"hidden",padding:0});aj=D.innerWidth()+f;v=D.innerHeight();D.width(aj);Y=b('<div class="jspPane" />').css("padding",aH).append(D.children());al=b('<div class="jspContainer" />').css({width:aj+"px",height:v+"px"}).append(Y).appendTo(D)}else{D.css("width","");aO=ay.stickToBottom&&K();aK=ay.stickToRight&&B();aJ=D.innerWidth()+f!=aj||D.outerHeight()!=v;if(aJ){aj=D.innerWidth()+f;v=D.innerHeight();al.css({width:aj+"px",height:v+"px"})}if(!aJ&&L==T&&Y.outerHeight()==Z){D.width(aj);return}L=T;Y.css("width","");D.width(aj);al.find(">.jspVerticalBar,>.jspHorizontalBar").remove().end()}Y.css("overflow","auto");if(aQ.contentWidth){T=aQ.contentWidth}else{T=Y[0].scrollWidth}Z=Y[0].scrollHeight;Y.css("overflow","");y=T/aj;q=Z/v;az=q>1;aE=y>1;if(!(aE||az)){D.removeClass("jspScrollable");Y.css({top:0,width:al.width()-f});n();E();R();w()}else{D.addClass("jspScrollable");aL=ay.maintainPosition&&(I||aa);if(aL){aN=aC();aM=aA()}aF();z();F();if(aL){N(aK?(T-aj):aN,false);M(aO?(Z-v):aM,false)}J();ag();an();if(ay.enableKeyboardNavigation){S()}if(ay.clickOnTrack){p()}C();if(ay.hijackInternalLinks){m()}}if(ay.autoReinitialise&&!av){av=setInterval(function(){ar(ay)},ay.autoReinitialiseDelay)}else{if(!ay.autoReinitialise&&av){clearInterval(av)}}aI&&D.scrollTop(0)&&M(aI,false);aP&&D.scrollLeft(0)&&N(aP,false);D.trigger("jsp-initialised",[aE||az])}function aF(){if(az){al.append(b('<div class="jspVerticalBar" />').append(b('<div class="jspCap jspCapTop" />'),b('<div class="jspTrack" />').append(b('<div class="jspDrag" />').append(b('<div class="jspDragTop" />'),b('<div class="jspDragBottom" />'))),b('<div class="jspCap jspCapBottom" />')));U=al.find(">.jspVerticalBar");ap=U.find(">.jspTrack");au=ap.find(">.jspDrag");if(ay.showArrows){aq=b('<a class="jspArrow jspArrowUp" />').bind("mousedown.jsp",aD(0,-1)).bind("click.jsp",aB);af=b('<a class="jspArrow jspArrowDown" />').bind("mousedown.jsp",aD(0,1)).bind("click.jsp",aB);if(ay.arrowScrollOnHover){aq.bind("mouseover.jsp",aD(0,-1,aq));af.bind("mouseover.jsp",aD(0,1,af))}ak(ap,ay.verticalArrowPositions,aq,af)}t=v;al.find(">.jspVerticalBar>.jspCap:visible,>.jspVerticalBar>.jspArrow").each(function(){t-=b(this).outerHeight()});au.hover(function(){au.addClass("jspHover")},function(){au.removeClass("jspHover")}).bind("mousedown.jsp",function(aI){b("html").bind("dragstart.jsp selectstart.jsp",aB);au.addClass("jspActive");var s=aI.pageY-au.position().top;b("html").bind("mousemove.jsp",function(aJ){V(aJ.pageY-s,false)}).bind("mouseup.jsp mouseleave.jsp",aw);return false});o()}}function o(){ap.height(t+"px");I=0;X=ay.verticalGutter+ap.outerWidth();Y.width(aj-X-f);try{if(U.position().left===0){Y.css("margin-left",X+"px")}}catch(s){}}function z(){if(aE){al.append(b('<div class="jspHorizontalBar" />').append(b('<div class="jspCap jspCapLeft" />'),b('<div class="jspTrack" />').append(b('<div class="jspDrag" />').append(b('<div class="jspDragLeft" />'),b('<div class="jspDragRight" />'))),b('<div class="jspCap jspCapRight" />')));am=al.find(">.jspHorizontalBar");G=am.find(">.jspTrack");h=G.find(">.jspDrag");if(ay.showArrows){ax=b('<a class="jspArrow jspArrowLeft" />').bind("mousedown.jsp",aD(-1,0)).bind("click.jsp",aB);x=b('<a class="jspArrow jspArrowRight" />').bind("mousedown.jsp",aD(1,0)).bind("click.jsp",aB);
if(ay.arrowScrollOnHover){ax.bind("mouseover.jsp",aD(-1,0,ax));x.bind("mouseover.jsp",aD(1,0,x))}ak(G,ay.horizontalArrowPositions,ax,x)}h.hover(function(){h.addClass("jspHover")},function(){h.removeClass("jspHover")}).bind("mousedown.jsp",function(aI){b("html").bind("dragstart.jsp selectstart.jsp",aB);h.addClass("jspActive");var s=aI.pageX-h.position().left;b("html").bind("mousemove.jsp",function(aJ){W(aJ.pageX-s,false)}).bind("mouseup.jsp mouseleave.jsp",aw);return false});l=al.innerWidth();ah()}}function ah(){al.find(">.jspHorizontalBar>.jspCap:visible,>.jspHorizontalBar>.jspArrow").each(function(){l-=b(this).outerWidth()});G.width(l+"px");aa=0}function F(){if(aE&&az){var aI=G.outerHeight(),s=ap.outerWidth();t-=aI;b(am).find(">.jspCap:visible,>.jspArrow").each(function(){l+=b(this).outerWidth()});l-=s;v-=s;aj-=aI;G.parent().append(b('<div class="jspCorner" />').css("width",aI+"px"));o();ah()}if(aE){Y.width((al.outerWidth()-f)+"px")}Z=Y.outerHeight();q=Z/v;if(aE){at=Math.ceil(1/y*l);if(at>ay.horizontalDragMaxWidth){at=ay.horizontalDragMaxWidth}else{if(at<ay.horizontalDragMinWidth){at=ay.horizontalDragMinWidth}}h.width(at+"px");j=l-at;ae(aa)}if(az){A=Math.ceil(1/q*t);if(A>ay.verticalDragMaxHeight){A=ay.verticalDragMaxHeight}else{if(A<ay.verticalDragMinHeight){A=ay.verticalDragMinHeight}}au.height(A+"px");i=t-A;ad(I)}}function ak(aJ,aL,aI,s){var aN="before",aK="after",aM;if(aL=="os"){aL=/Mac/.test(navigator.platform)?"after":"split"}if(aL==aN){aK=aL}else{if(aL==aK){aN=aL;aM=aI;aI=s;s=aM}}aJ[aN](aI)[aK](s)}function aD(aI,s,aJ){return function(){H(aI,s,this,aJ);this.blur();return false}}function H(aL,aK,aO,aN){aO=b(aO).addClass("jspActive");var aM,aJ,aI=true,s=function(){if(aL!==0){Q.scrollByX(aL*ay.arrowButtonSpeed)}if(aK!==0){Q.scrollByY(aK*ay.arrowButtonSpeed)}aJ=setTimeout(s,aI?ay.initialDelay:ay.arrowRepeatFreq);aI=false};s();aM=aN?"mouseout.jsp":"mouseup.jsp";aN=aN||b("html");aN.bind(aM,function(){aO.removeClass("jspActive");aJ&&clearTimeout(aJ);aJ=null;aN.unbind(aM)})}function p(){w();if(az){ap.bind("mousedown.jsp",function(aN){if(aN.originalTarget===c||aN.originalTarget==aN.currentTarget){var aL=b(this),aO=aL.offset(),aM=aN.pageY-aO.top-I,aJ,aI=true,s=function(){var aR=aL.offset(),aS=aN.pageY-aR.top-A/2,aP=v*ay.scrollPagePercent,aQ=i*aP/(Z-v);if(aM<0){if(I-aQ>aS){Q.scrollByY(-aP)}else{V(aS)}}else{if(aM>0){if(I+aQ<aS){Q.scrollByY(aP)}else{V(aS)}}else{aK();return}}aJ=setTimeout(s,aI?ay.initialDelay:ay.trackClickRepeatFreq);aI=false},aK=function(){aJ&&clearTimeout(aJ);aJ=null;b(document).unbind("mouseup.jsp",aK)};s();b(document).bind("mouseup.jsp",aK);return false}})}if(aE){G.bind("mousedown.jsp",function(aN){if(aN.originalTarget===c||aN.originalTarget==aN.currentTarget){var aL=b(this),aO=aL.offset(),aM=aN.pageX-aO.left-aa,aJ,aI=true,s=function(){var aR=aL.offset(),aS=aN.pageX-aR.left-at/2,aP=aj*ay.scrollPagePercent,aQ=j*aP/(T-aj);if(aM<0){if(aa-aQ>aS){Q.scrollByX(-aP)}else{W(aS)}}else{if(aM>0){if(aa+aQ<aS){Q.scrollByX(aP)}else{W(aS)}}else{aK();return}}aJ=setTimeout(s,aI?ay.initialDelay:ay.trackClickRepeatFreq);aI=false},aK=function(){aJ&&clearTimeout(aJ);aJ=null;b(document).unbind("mouseup.jsp",aK)};s();b(document).bind("mouseup.jsp",aK);return false}})}}function w(){if(G){G.unbind("mousedown.jsp")}if(ap){ap.unbind("mousedown.jsp")}}function aw(){b("html").unbind("dragstart.jsp selectstart.jsp mousemove.jsp mouseup.jsp mouseleave.jsp");if(au){au.removeClass("jspActive")}if(h){h.removeClass("jspActive")}}function V(s,aI){if(!az){return}if(s<0){s=0}else{if(s>i){s=i}}if(aI===c){aI=ay.animateScroll}if(aI){Q.animate(au,"top",s,ad)}else{au.css("top",s);ad(s)}}function ad(aI){if(aI===c){aI=au.position().top}al.scrollTop(0);I=aI;var aL=I===0,aJ=I==i,aK=aI/i,s=-aK*(Z-v);if(ai!=aL||aG!=aJ){ai=aL;aG=aJ;D.trigger("jsp-arrow-change",[ai,aG,P,k])}u(aL,aJ);Y.css("top",s);D.trigger("jsp-scroll-y",[-s,aL,aJ]).trigger("scroll")}function W(aI,s){if(!aE){return}if(aI<0){aI=0}else{if(aI>j){aI=j}}if(s===c){s=ay.animateScroll}if(s){Q.animate(h,"left",aI,ae)
}else{h.css("left",aI);ae(aI)}}function ae(aI){if(aI===c){aI=h.position().left}al.scrollTop(0);aa=aI;var aL=aa===0,aK=aa==j,aJ=aI/j,s=-aJ*(T-aj);if(P!=aL||k!=aK){P=aL;k=aK;D.trigger("jsp-arrow-change",[ai,aG,P,k])}r(aL,aK);Y.css("left",s);D.trigger("jsp-scroll-x",[-s,aL,aK]).trigger("scroll")}function u(aI,s){if(ay.showArrows){aq[aI?"addClass":"removeClass"]("jspDisabled");af[s?"addClass":"removeClass"]("jspDisabled")}}function r(aI,s){if(ay.showArrows){ax[aI?"addClass":"removeClass"]("jspDisabled");x[s?"addClass":"removeClass"]("jspDisabled")}}function M(s,aI){var aJ=s/(Z-v);V(aJ*i,aI)}function N(aI,s){var aJ=aI/(T-aj);W(aJ*j,s)}function ab(aV,aQ,aJ){var aN,aK,aL,s=0,aU=0,aI,aP,aO,aS,aR,aT;try{aN=b(aV)}catch(aM){return}aK=aN.outerHeight();aL=aN.outerWidth();al.scrollTop(0);al.scrollLeft(0);while(!aN.is(".jspPane")){s+=aN.position().top;aU+=aN.position().left;aN=aN.offsetParent();if(/^body|html$/i.test(aN[0].nodeName)){return}}aI=aA();aO=aI+v;if(s<aI||aQ){aR=s-ay.verticalGutter}else{if(s+aK>aO){aR=s-v+aK+ay.verticalGutter}}if(aR){M(aR,aJ)}aP=aC();aS=aP+aj;if(aU<aP||aQ){aT=aU-ay.horizontalGutter}else{if(aU+aL>aS){aT=aU-aj+aL+ay.horizontalGutter}}if(aT){N(aT,aJ)}}function aC(){return -Y.position().left}function aA(){return -Y.position().top}function K(){var s=Z-v;return(s>20)&&(s-aA()<10)}function B(){var s=T-aj;return(s>20)&&(s-aC()<10)}function ag(){al.unbind(ac).bind(ac,function(aL,aM,aK,aI){var aJ=aa,s=I;Q.scrollBy(aK*ay.mouseWheelSpeed,-aI*ay.mouseWheelSpeed,false);return aJ==aa&&s==I})}function n(){al.unbind(ac)}function aB(){return false}function J(){Y.find(":input,a").unbind("focus.jsp").bind("focus.jsp",function(s){ab(s.target,false)})}function E(){Y.find(":input,a").unbind("focus.jsp")}function S(){var s,aI,aK=[];aE&&aK.push(am[0]);az&&aK.push(U[0]);Y.focus(function(){D.focus()});D.attr("tabindex",0).unbind("keydown.jsp keypress.jsp").bind("keydown.jsp",function(aN){if(aN.target!==this&&!(aK.length&&b(aN.target).closest(aK).length)){return}var aM=aa,aL=I;switch(aN.keyCode){case 40:case 38:case 34:case 32:case 33:case 39:case 37:s=aN.keyCode;aJ();break;case 35:M(Z-v);s=null;break;case 36:M(0);s=null;break}aI=aN.keyCode==s&&aM!=aa||aL!=I;return !aI}).bind("keypress.jsp",function(aL){if(aL.keyCode==s){aJ()}return !aI});if(ay.hideFocus){D.css("outline","none");if("hideFocus" in al[0]){D.attr("hideFocus",true)}}else{D.css("outline","");if("hideFocus" in al[0]){D.attr("hideFocus",false)}}function aJ(){var aM=aa,aL=I;switch(s){case 40:Q.scrollByY(ay.keyboardSpeed,false);break;case 38:Q.scrollByY(-ay.keyboardSpeed,false);break;case 34:case 32:Q.scrollByY(v*ay.scrollPagePercent,false);break;case 33:Q.scrollByY(-v*ay.scrollPagePercent,false);break;case 39:Q.scrollByX(ay.keyboardSpeed,false);break;case 37:Q.scrollByX(-ay.keyboardSpeed,false);break}aI=aM!=aa||aL!=I;return aI}}function R(){D.attr("tabindex","-1").removeAttr("tabindex").unbind("keydown.jsp keypress.jsp")}function C(){if(location.hash&&location.hash.length>1){var aK,aI,aJ=escape(location.hash.substr(1));try{aK=b("#"+aJ+', a[name="'+aJ+'"]')}catch(s){return}if(aK.length&&Y.find(aJ)){if(al.scrollTop()===0){aI=setInterval(function(){if(al.scrollTop()>0){ab(aK,true);b(document).scrollTop(al.position().top);clearInterval(aI)}},50)}else{ab(aK,true);b(document).scrollTop(al.position().top)}}}}function m(){if(b(document.body).data("jspHijack")){return}b(document.body).data("jspHijack",true);b(document.body).delegate("a[href*=#]","click",function(s){var aI=this.href.substr(0,this.href.indexOf("#")),aK=location.href,aO,aP,aJ,aM,aL,aN;if(location.href.indexOf("#")!==-1){aK=location.href.substr(0,location.href.indexOf("#"))}if(aI!==aK){return}aO=escape(this.href.substr(this.href.indexOf("#")+1));aP;try{aP=b("#"+aO+', a[name="'+aO+'"]')}catch(aQ){return}if(!aP.length){return}aJ=aP.closest(".jspScrollable");aM=aJ.data("jsp");aM.scrollToElement(aP,true);if(aJ[0].scrollIntoView){aL=b(a).scrollTop();aN=aP.offset().top;if(aN<aL||aN>aL+b(a).height()){aJ[0].scrollIntoView()}}s.preventDefault()
})}function an(){var aJ,aI,aL,aK,aM,s=false;al.unbind("touchstart.jsp touchmove.jsp touchend.jsp click.jsp-touchclick").bind("touchstart.jsp",function(aN){var aO=aN.originalEvent.touches[0];aJ=aC();aI=aA();aL=aO.pageX;aK=aO.pageY;aM=false;s=true}).bind("touchmove.jsp",function(aQ){if(!s){return}var aP=aQ.originalEvent.touches[0],aO=aa,aN=I;Q.scrollTo(aJ+aL-aP.pageX,aI+aK-aP.pageY);aM=aM||Math.abs(aL-aP.pageX)>5||Math.abs(aK-aP.pageY)>5;return aO==aa&&aN==I}).bind("touchend.jsp",function(aN){s=false}).bind("click.jsp-touchclick",function(aN){if(aM){aM=false;return false}})}function g(){var s=aA(),aI=aC();D.removeClass("jspScrollable").unbind(".jsp");D.replaceWith(ao.append(Y.children()));ao.scrollTop(s);ao.scrollLeft(aI);if(av){clearInterval(av)}}b.extend(Q,{reinitialise:function(aI){aI=b.extend({},ay,aI);ar(aI)},scrollToElement:function(aJ,aI,s){ab(aJ,aI,s)},scrollTo:function(aJ,s,aI){N(aJ,aI);M(s,aI)},scrollToX:function(aI,s){N(aI,s)},scrollToY:function(s,aI){M(s,aI)},scrollToPercentX:function(aI,s){N(aI*(T-aj),s)},scrollToPercentY:function(aI,s){M(aI*(Z-v),s)},scrollBy:function(aI,s,aJ){Q.scrollByX(aI,aJ);Q.scrollByY(s,aJ)},scrollByX:function(s,aJ){var aI=aC()+Math[s<0?"floor":"ceil"](s),aK=aI/(T-aj);W(aK*j,aJ)},scrollByY:function(s,aJ){var aI=aA()+Math[s<0?"floor":"ceil"](s),aK=aI/(Z-v);V(aK*i,aJ)},positionDragX:function(s,aI){W(s,aI)},positionDragY:function(aI,s){V(aI,s)},animate:function(aI,aL,s,aK){var aJ={};aJ[aL]=s;aI.animate(aJ,{duration:ay.animateDuration,easing:ay.animateEase,queue:false,step:aK})},getContentPositionX:function(){return aC()},getContentPositionY:function(){return aA()},getContentWidth:function(){return T},getContentHeight:function(){return Z},getPercentScrolledX:function(){return aC()/(T-aj)},getPercentScrolledY:function(){return aA()/(Z-v)},getIsScrollableH:function(){return aE},getIsScrollableV:function(){return az},getContentPane:function(){return Y},scrollToBottom:function(s){V(i,s)},hijackInternalLinks:b.noop,destroy:function(){g()}});ar(O)}e=b.extend({},b.fn.jScrollPane.defaults,e);b.each(["arrowButtonSpeed","trackClickSpeed","keyboardSpeed"],function(){e[this]=e[this]||e.speed});return this.each(function(){var f=b(this),g=f.data("jsp");if(g){g.reinitialise(e)}else{b("script",f).filter('[type="text/javascript"],:not([type])').remove();g=new d(f,e);f.data("jsp",g)}})};b.fn.jScrollPane.defaults={showArrows:false,maintainPosition:true,stickToBottom:false,stickToRight:false,clickOnTrack:true,autoReinitialise:false,autoReinitialiseDelay:500,verticalDragMinHeight:0,verticalDragMaxHeight:99999,horizontalDragMinWidth:0,horizontalDragMaxWidth:99999,contentWidth:c,animateScroll:false,animateDuration:300,animateEase:"linear",hijackInternalLinks:false,verticalGutter:4,horizontalGutter:4,mouseWheelSpeed:3,arrowButtonSpeed:0,arrowRepeatFreq:50,arrowScrollOnHover:false,trackClickSpeed:0,trackClickRepeatFreq:70,verticalArrowPositions:"split",horizontalArrowPositions:"split",enableKeyboardNavigation:true,hideFocus:false,keyboardSpeed:0,initialDelay:300,speed:30,scrollPagePercent:0.8}})(jQuery,this);


/*! Copyright (c) 2013 Brandon Aaron (http://brandonaaron.net)
 * Licensed under the MIT License (LICENSE.txt).
 *
 * Thanks to: http://adomas.org/javascript-mouse-wheel/ for some pointers.
 * Thanks to: Mathias Bank(http://www.mathias-bank.de) for a scope bug fix.
 * Thanks to: Seamus Leahy for adding deltaX and deltaY
 *
 * Version: 3.1.3
 *
 * Requires: 1.2.2+
 */

(function (factory) {
    if ( typeof define === 'function' && define.amd ) {
        // AMD. Register as an anonymous module.
        define(['jquery'], factory);
    } else if (typeof exports === 'object') {
        // Node/CommonJS style for Browserify
        module.exports = factory;
    } else {
        // Browser globals
        factory(jQuery);
    }
}(function ($) {

    var toFix = ['wheel', 'mousewheel', 'DOMMouseScroll', 'MozMousePixelScroll'];
    var toBind = 'onwheel' in document || document.documentMode >= 9 ? ['wheel'] : ['mousewheel', 'DomMouseScroll', 'MozMousePixelScroll'];
    var lowestDelta, lowestDeltaXY;

    if ( $.event.fixHooks ) {
        for ( var i = toFix.length; i; ) {
            $.event.fixHooks[ toFix[--i] ] = $.event.mouseHooks;
        }
    }

    $.event.special.mousewheel = {
        setup: function() {
            if ( this.addEventListener ) {
                for ( var i = toBind.length; i; ) {
                    this.addEventListener( toBind[--i], handler, false );
                }
            } else {
                this.onmousewheel = handler;
            }
        },

        teardown: function() {
            if ( this.removeEventListener ) {
                for ( var i = toBind.length; i; ) {
                    this.removeEventListener( toBind[--i], handler, false );
                }
            } else {
                this.onmousewheel = null;
            }
        }
    };

    $.fn.extend({
        mousewheel: function(fn) {
            return fn ? this.bind("mousewheel", fn) : this.trigger("mousewheel");
        },

        unmousewheel: function(fn) {
            return this.unbind("mousewheel", fn);
        }
    });


    function handler(event) {
        var orgEvent = event || window.event,
            args = [].slice.call(arguments, 1),
            delta = 0,
            deltaX = 0,
            deltaY = 0,
            absDelta = 0,
            absDeltaXY = 0,
            fn;
        event = $.event.fix(orgEvent);
        event.type = "mousewheel";

        // Old school scrollwheel delta
        if ( orgEvent.wheelDelta ) { delta = orgEvent.wheelDelta; }
        if ( orgEvent.detail )     { delta = orgEvent.detail * -1; }

        // New school wheel delta (wheel event)
        if ( orgEvent.deltaY ) {
            deltaY = orgEvent.deltaY * -1;
            delta  = deltaY;
        }
        if ( orgEvent.deltaX ) {
            deltaX = orgEvent.deltaX;
            delta  = deltaX * -1;
        }

        // Webkit
        if ( orgEvent.wheelDeltaY !== undefined ) { deltaY = orgEvent.wheelDeltaY; }
        if ( orgEvent.wheelDeltaX !== undefined ) { deltaX = orgEvent.wheelDeltaX * -1; }

        // Look for lowest delta to normalize the delta values
        absDelta = Math.abs(delta);
        if ( !lowestDelta || absDelta < lowestDelta ) { lowestDelta = absDelta; }
        absDeltaXY = Math.max(Math.abs(deltaY), Math.abs(deltaX));
        if ( !lowestDeltaXY || absDeltaXY < lowestDeltaXY ) { lowestDeltaXY = absDeltaXY; }

        // Get a whole value for the deltas
        fn = delta > 0 ? 'floor' : 'ceil';
        delta  = Math[fn](delta / lowestDelta);
        deltaX = Math[fn](deltaX / lowestDeltaXY);
        deltaY = Math[fn](deltaY / lowestDeltaXY);

        // Add event and delta to the front of the arguments
        args.unshift(event, delta, deltaX, deltaY);

        return ($.event.dispatch || $.event.handle).apply(this, args);
    }

}));


/******************************
	-	THE PREVIEW JS FUNCTIONS	-
********************************/

jQuery(document).ready(function() {

	/******************************
		-	SUB MENU SCROLLER	-
	********************************/
	jQuery('a.menubutton').each(function() {
	    var but = jQuery(this);

	    but.click(function() {
   		    var but = jQuery(this);
			var sto = but.data('where');

			if (sto && sto!=undefined && jQuery(sto)) {
				var pos = jQuery(sto).offset().top - 40;
				jQuery('body, html').animate({scrollTop:pos},{queue:false,duration:400});
				return false;
			}
		});
	})


	// HIDE JS IF WE ARE ON MOBILE
	if (!is_mobile()) {
		jQuery('.transition-selectbox').jScrollPane({});
	    var naviapi = jQuery('.transition-selectbox').data('jsp');
    } else {
      jQuery('.transition-selectbox').css({'overflow':'scroll'});
    }

    // THE TRANSITION SELECTOR

	jQuery('#transitselector').click(function() {
		var ts = jQuery('.transition-selectbox-holder');
		if (!ts.hasClass("opened")) {
			punchgs.TweenLite.fromTo(ts,0.2,{opacity:0,transformOrigin:"center bottom", transformPerspective:400, y:-50,rotationX:0,z:0},{opacity:1,y:0,rotationX:0,ease:punchgs.Power3.easeOut})
			ts.css({display:'block'});
			setTimeout(function() {
				naviapi.reinitialise();
			},100)
			ts.addClass("opened");
		}
	})

	jQuery('body').on('mouseleave','.transition-selectbox-holder.opened',function() {

				var ts = jQuery('.transition-selectbox-holder');
				punchgs.TweenLite.to(ts,0.2,{opacity:0,transformOrigin:"center bottom", transformPerspective:400, y:-50,rotationX:0,z:0,ease:punchgs.Power3.easeOut});
				ts.removeClass("opened");
		});


	jQuery('.transition-selectbox li').each(function() {
		var li = jQuery(this);
		li.click(function() {
			var li = jQuery(this);
			var anim = li.data('anim');

			jQuery('#mranim').text(li.text());
			jQuery('#mranim').data('val',anim);

			callChanger();

		})
	})

	/******************************
		-	TIMER CHANGER	-
	********************************/

	jQuery('#dectime').click(function() {
		var mrtime = jQuery('#mrtime');
		var curtime = mrtime.data('val');
		curtime=curtime-100;
		if (curtime<300) curtime=300;
		mrtime.data('val',curtime);
		mrtime.text('Time: '+curtime/1000+"s");

		callChanger();
	})

	jQuery('#inctime').click(function() {
		var mrtime = jQuery('#mrtime');
		var curtime = mrtime.data('val');
		curtime=curtime+100;
		mrtime.data('val',curtime);
		mrtime.text('Time: '+curtime/1000+"s");

		callChanger();
	})


	/******************************
		-	SLOT CHANGER	-
	********************************/
	jQuery('#decslot').click(function() {
		var mrslot = jQuery('#mrslot');
		var slot = mrslot.data('val');
		slot=slot-1;
		if (slot<1) slot=1;
		mrslot.data('val',slot);
		mrslot.text('Slots: '+slot);

		callChanger();
	})

	jQuery('#incslot').click(function() {
		var mrslot = jQuery('#mrslot');
		var slot = mrslot.data('val');
		slot=slot+1;
		if (slot>20) slot=20;
		mrslot.data('val',slot);
		mrslot.text('Slots: '+slot);

		callChanger();
	})

	var timeoutv;
	function callChanger() {
			clearTimeout(timeoutv);
			timeoutv = setTimeout(function() {

					var anim = jQuery('#mranim').data('val');
					var timer = jQuery('#mrtime').data('val');
					var slot = jQuery('#mrslot').data('val');

					jQuery('.spectaculus ul li').each(function() {
						jQuery(this).data("transition",anim);
						jQuery(this).data("slotamount",slot);
						jQuery(this).data("masterspeed",timer);
					})

					jQuery('#resultanim').text(anim);
					jQuery('#resultslot').text(slot);
					jQuery('#resultspeed').text(timer);
					revapi.revnext();
			},300);

	}

	/***************************************
		-	START THE ANIMATION CREATOR	-
	***************************************/

	prepareAnimateCreator();

	jQuery('#set-random-animation').click(function(){
			jQuery('input[name="movex"]').val((Math.floor(Math.random() * 101) - 50) * 10);
			jQuery('input[name="movey"]').val((Math.floor(Math.random() * 101) - 50) * 10);
			jQuery('input[name="movez"]').val((Math.floor(Math.random() * 11) - 5) * 10);

			jQuery('input[name="rotationx"]').val((Math.floor(Math.random() * 101) - 50) * 10);
			jQuery('input[name="rotationy"]').val((Math.floor(Math.random() * 101) - 50) * 10);
			jQuery('input[name="rotationz"]').val((Math.floor(Math.random() * 101) - 50) * 10);

			jQuery('input[name="scalex"]').val((Math.floor(Math.random() * 31)) * 10);
			jQuery('input[name="scaley"]').val((Math.floor(Math.random() * 31)) * 10);

			jQuery('input[name="skewx"]').val(Math.floor(Math.random() * 61));
			jQuery('input[name="skewy"]').val(Math.floor(Math.random() * 61));

			jQuery('input[name="captionopacity"]').val(0);
			jQuery('input[name="captionperspective"]').val(600);

			jQuery('input[name="originx"]').val((Math.floor(Math.random() * 41) - 20) * 10);
			jQuery('input[name="originy"]').val((Math.floor(Math.random() * 41) - 20) * 10);


			jQuery('input[name="captionspeed"]').val((Math.floor(Math.random() * 11) + 5) * 100);

			transition = jQuery('#caption-easing-demo option');
			var random = Math.floor(transition.length * (Math.random() % 1));

			transition.attr('selected',false).eq(random).attr('selected',true);


			jQuery('input[name="movexout"]').val((Math.floor(Math.random() * 101) - 50) * 10);
			jQuery('input[name="moveyout"]').val((Math.floor(Math.random() * 101) - 50) * 10);
			jQuery('input[name="movezout"]').val((Math.floor(Math.random() * 11) - 5) * 10);

			jQuery('input[name="rotationxout"]').val((Math.floor(Math.random() * 101) - 50) * 10);
			jQuery('input[name="rotationyout"]').val((Math.floor(Math.random() * 101) - 50) * 10);
			jQuery('input[name="rotationzout"]').val((Math.floor(Math.random() * 101) - 50) * 10);

			jQuery('input[name="scalexout"]').val((Math.floor(Math.random() * 31)) * 10);
			jQuery('input[name="scaleyout"]').val((Math.floor(Math.random() * 31)) * 10);

			jQuery('input[name="skewxout"]').val(Math.floor(Math.random() * 61));
			jQuery('input[name="skewyout"]').val(Math.floor(Math.random() * 61));

			jQuery('input[name="captionopacityout"]').val(0);
			jQuery('input[name="captionperspectiveout"]').val(600);

			jQuery('input[name="originxout"]').val((Math.floor(Math.random() * 41) - 20) * 10);
			jQuery('input[name="originyout"]').val((Math.floor(Math.random() * 41) - 20) * 10);


			jQuery('input[name="captionspeedout"]').val((Math.floor(Math.random() * 11) + 5) * 100);

			transition = jQuery('#caption-easing-demoout option');
			var random = Math.floor(transition.length * (Math.random() % 1));

			transition.attr('selected',false).eq(random).attr('selected',true);

		});





});



		//////////////////
		// IS MOBILE ?? //
		//////////////////
		function is_mobile() {
		    var agents = ['android', 'webos', 'iphone', 'ipad', 'blackberry','Android', 'webos', ,'iPod', 'iPhone', 'iPad', 'Blackberry', 'BlackBerry'];
			var ismobile=false;
		    for(i in agents) {

			    if (navigator.userAgent.split(agents[i]).length>1) {
		            ismobile = true;

		          }
		    }
		    return ismobile;
		}


/******************************
		-	ANIMATION CREATOR	-
	********************************/

	function prepareAnimateCreator() {
		var cic = jQuery('#caption-inout-controll');
		cic.data('direction',0);

		var osw = jQuery('#outanimation .settings_wrapper');
		osw.slideUp(200).addClass("collapsed");
		var isw = jQuery('#inanimation .settings_wrapper');

		jQuery('#outanimation .caption-demo-controll').click(function() {

			if (osw.hasClass("collapsed")) {
				osw.removeClass("collapsed").slideDown(200);
				isw.slideUp(200).addClass("collapsed");
			} else {
				osw.slideUp(200).addClass("collapsed");
				isw.removeClass("collapsed").slideDown(200);
			}
		});

		jQuery('#inanimation .caption-demo-controll').click(function() {

			if (isw.hasClass("collapsed")) {
				isw.removeClass("collapsed").slideDown(200);
				osw.slideUp(200).addClass("collapsed");
			} else {
				isw.slideUp(200).addClass("collapsed");
				osw.removeClass("collapsed").slideDown(200);
			}
		});




		jQuery('#caption-inout-controll').click(function() {
			if (cic.data('direction')==0) {
				cic.data('direction',1);
				jQuery('#revshowmetheinanim').removeClass("reviconinaction");
				jQuery('#revshowmetheoutanim').addClass("reviconinaction");
			} else

			if (cic.data('direction')==1) {
				cic.data('direction',2);
				jQuery('#revshowmetheinanim').addClass("reviconinaction");
				jQuery('#revshowmetheoutanim').addClass("reviconinaction");

			} else

			if (cic.data('direction')==2) {
				cic.data('direction',0);
				jQuery('#revshowmetheinanim').addClass("reviconinaction");
				jQuery('#revshowmetheoutanim').removeClass("reviconinaction");

			}
		});
		startAnimationInCreator();

	}

	function startAnimationInCreator() {
		stopAnimationInPreview();
		animateCreatorIn();
	}

	function killAnimationInCreator() {
		  var nextcaption = jQuery('#caption_custon_anim_preview');
		 punchgs.TweenLite.killTweensOf(nextcaption,false);
	}

	function animateCreatorIn() {


					  var nextcaption = jQuery('#caption_custon_anim_preview');
					  var cic = jQuery('#caption-inout-controll');

					  var transx = jQuery('input[name="movex"]').val();
					  var transy = jQuery('input[name="movey"]').val();
					  var transz = jQuery('input[name="movez"]').val();

					  var rotatex = jQuery('input[name="rotationx"]').val();
					  var rotatey = jQuery('input[name="rotationy"]').val();
					  var rotatez = jQuery('input[name="rotationz"]').val();

					  var scalex = jQuery('input[name="scalex"]').val()/100;
					  var scaley = jQuery('input[name="scaley"]').val()/100;

					  var skewx = jQuery('input[name="skewx"]').val();
					  var skewy = jQuery('input[name="skewy"]').val();
					  var opac = jQuery('input[name="captionopacity"]').val()/100;

					  var tper = jQuery('input[name="captionperspective"]').val();
					  //var tper = 600;

					  var originx = jQuery('input[name="originx"]').val()+"%";
					  var originy = jQuery('input[name="originy"]').val()+"%";
					  var origin = originx+" "+originy;

					  var speed = parseInt(jQuery('input[name="captionspeed"]').val(),0);
					  if (speed<100) speed=100;

					  var easedata = jQuery('#caption-easing-demo').val();



					  jQuery('#custominresult').text("x:"+transx+";y:"+transy+";z:"+transz+";"+
					  								 "rotationX:"+rotatex+";rotationY:"+rotatey+";rotationZ:"+rotatez+";"+
					  								 "scaleX:"+scalex+";scaleY:"+scaley+";"+
					  								 "skewX:"+skewx+";skewY:"+skewy+";"+
					  								 "opacity:"+opac+";"+
					  								 "transformPerspective:"+tper+";transformOrigin:"+origin+";");

					  jQuery('#custominspeed').text(speed);
					  jQuery('#custominease').text(easedata);
					  jQuery('#presplitin').text(jQuery('#caption-splitin-demo').val());
					  jQuery('#presplitout').text(jQuery('#caption-splitout-demo').val());
					  jQuery('#predelayin').text(jQuery('input[name="splitspeedin"]').val()/1000);
					  jQuery('#predleayout').text(jQuery('input[name="splitspeedout"]').val()/1000);



 					  speed=speed/1000;
					  var xx = jQuery('.tp-present-wrapper').width()/2  - 72;
					  var yy=jQuery('.tp-present-wrapper').height()/2 - 17;

					  if (nextcaption.data('mySplitText') != undefined)
						  nextcaption.data('mySplitText').revert();

					  nextcaption.data('splitin',jQuery('#caption-splitin-demo').val());
					  var animobject = nextcaption;
					  var splitspeed = 0;
					  var delayer = 1;

						if (nextcaption.data('splitin') == "chars" || nextcaption.data('splitin') == "words" || nextcaption.data('splitin') == "lines") {
							if (nextcaption.find('a').length>0)
								nextcaption.data('mySplitText',new SplitText(nextcaption.find('a'),{type:"lines,words,chars",charsClass:"tp-splitted",wordsClass:"tp-splitted",linesClass:"tp-splitted"}));
							 else
								nextcaption.data('mySplitText',new SplitText(nextcaption,{type:"lines,words,chars",charsClass:"tp-splitted",wordsClass:"tp-splitted",linesClass:"tp-splitted"}));

							nextcaption.addClass("splitted");
							splitspeed = jQuery('input[name="splitspeedin"]').val()/1000;
						}

						if (nextcaption.data('splitin') == "chars") {

							animobject = nextcaption.data('mySplitText').chars;
							delayer = animobject.length;
						}


						if (nextcaption.data('splitin') == "words") {
							animobject = nextcaption.data('mySplitText').words;
							delayer = animobject.length;
						}


						if (nextcaption.data('splitin') == "lines") {
							animobject = nextcaption.data('mySplitText').lines;
							delayer = animobject.length;
						}


						  punchgs.TweenLite.fromTo(nextcaption,speed,{top:yy, left:xx, opacity:opac},{top:yy, left:xx, opacity:1});

					  var newtl = new punchgs.TimelineLite();

					  punchgs.TweenLite.killTweensOf(animobject,false);
					  newtl.staggerFromTo(animobject,speed,
										{ scaleX:scalex,
										  scaleY:scaley,
										  rotationX:rotatex,
										  rotationY:rotatey,
										  rotationZ:rotatez,
										  x:transx,
										  y:transy,
										  z:transz+1,
										  skewX:skewx,
										  skewY:skewy,


										  transformPerspective:tper,
										  transformOrigin:origin,
										  visibility:'hidden'},

										{

										  x:0,
										  y:0,
										  scaleX:1,
										  scaleY:1,
										  rotationX:0,
										  rotationY:0,
										  rotationZ:0,
										  skewX:0,
										  skewY:0,
										  z:1,
										  visibility:'visible',
										  opacity:1,
										  ease:easedata,
										  overwrite:"all"
										},splitspeed);



						setTimeout(function() {animateCreatorOut()},(splitspeed/1000 * delayer) + (speed*1000)+500);
   }


   function animateCreatorOut() {


					  var nextcaption = jQuery('#caption_custon_anim_preview');
					  var cic = jQuery('#caption-inout-controll');

					  var transx = jQuery('input[name="movexout"]').val();
					  var transy = jQuery('input[name="moveyout"]').val();
					  var transz = jQuery('input[name="movezout"]').val();

					  var rotatex = jQuery('input[name="rotationxout"]').val();
					  var rotatey = jQuery('input[name="rotationyout"]').val();
					  var rotatez = jQuery('input[name="rotationzout"]').val();

					  var scalex = jQuery('input[name="scalexout"]').val()/100;
					  var scaley = jQuery('input[name="scaleyout"]').val()/100;

					  var skewx = jQuery('input[name="skewxout"]').val();
					  var skewy = jQuery('input[name="skewyout"]').val();
					  var opac = jQuery('input[name="captionopacityout"]').val()/100;

					  var tper = jQuery('input[name="captionperspectiveout"]').val();
					  //var tper = 600;

					  var originx = jQuery('input[name="originxout"]').val()+"%";
					  var originy = jQuery('input[name="originyout"]').val()+"%";
					  var origin = originx+" "+originy;

					  var speed = parseInt(jQuery('input[name="captionspeedout"]').val(),0);
					  if (speed<100) speed=100;

					  var easedata = jQuery('#caption-easing-demoout').val();



					   jQuery('#customoutresult').text("x:"+transx+";y:"+transy+";z:"+transz+";"+
					  								 "rotationX:"+rotatex+";rotationY:"+rotatey+";rotationZ:"+rotatez+";"+
					  								 "scaleX:"+scalex+";scaleY:"+scaley+";"+
					  								 "skewX:"+skewx+";skewY:"+skewy+";"+
					  								 "opacity:"+opac+";"+
					  								 "transformPerspective:"+tper+";transformOrigin:"+origin+";");

					  jQuery('#customoutspeed').text(speed);
					  jQuery('#customoutease').text(easedata);

					  jQuery('#presplitin').text(jQuery('#caption-splitin-demo').val());
					  jQuery('#presplitout').text(jQuery('#caption-splitout-demo').val());
					  jQuery('#predelayin').text(jQuery('input[name="splitspeedin"]').val()/1000);
					  jQuery('#predleayout').text(jQuery('input[name="splitspeedout"]').val()/1000);

					  speed=speed/1000;
					  var xx = jQuery('.tp-present-wrapper').width()/2  - 72;
					  var yy=jQuery('.tp-present-wrapper').height()/2 - 17;

					  if (nextcaption.data('mySplitText') != undefined)
						  nextcaption.data('mySplitText').revert();

					  nextcaption.data('splitout',jQuery('#caption-splitout-demo').val());


					  var animobject = nextcaption;
					  var splitspeed = 0;
					  var delayer = 1;

						if (nextcaption.data('splitout') == "chars" || nextcaption.data('splitout') == "words" || nextcaption.data('splitout') == "lines") {
							if (nextcaption.find('a').length>0)
								nextcaption.data('mySplitText',new SplitText(nextcaption.find('a'),{type:"lines,words,chars",charsClass:"tp-splitted",wordsClass:"tp-splitted",linesClass:"tp-splitted"}));
							 else
								nextcaption.data('mySplitText',new SplitText(nextcaption,{type:"lines,words,chars",charsClass:"tp-splitted",wordsClass:"tp-splitted",linesClass:"tp-splitted"}));

							nextcaption.addClass("splitted");
							splitspeed = jQuery('input[name="splitspeedout"]').val()/1000;
						}

						if (nextcaption.data('splitout') == "chars") {


							animobject = nextcaption.data('mySplitText').chars;
							delayer = animobject.length;
						}


						if (nextcaption.data('splitout') == "words") {
							animobject = nextcaption.data('mySplitText').words;
							delayer = animobject.length;
						}


						if (nextcaption.data('splitout') == "lines") {
							animobject = nextcaption.data('mySplitText').lines;
							delayer = animobject.length;
						}

					  if (nextcaption == animobject)
						  punchgs.TweenLite.fromTo(nextcaption,speed,{top:yy, left:xx, opacity:1},{top:yy, left:xx, opacity:opac});
					  else {
					 	 punchgs.TweenLite.set(nextcaption,{top:yy, left:xx, opacity:1});
					 	 setTimeout(function() {punchgs.TweenLite.fromTo(nextcaption,0.3,{top:yy, left:xx, opacity:1},{top:yy, left:xx, opacity:opac});},(splitspeed*1000 * delayer) + (speed*1000));
					  }

					  var newtl = new punchgs.TimelineLite();

					  punchgs.TweenLite.killTweensOf(animobject,false);
					  newtl.staggerFromTo(animobject,speed,
										{
										  x:0,
										  y:0,
										  scaleX:1,
										  scaleY:1,
										  rotationX:0,
										  rotationY:0,
										  rotationZ:0,
										  skewX:0,
										  skewY:0,
										  z:1,
										  visibility:'visible',
										  opacity:1
										 },

										{

										  scaleX:scalex,
										  scaleY:scaley,
										  rotationX:rotatex,
										  rotationY:rotatey,
										  rotationZ:rotatez,
										  x:transx,
										  y:transy,
										  z:transz+1,
										  skewX:skewx,
										  skewY:skewy,

										  transformPerspective:tper,
										  transformOrigin:origin,
										  ease:easedata,
										  overwrite:"all",
										  delay:0.3,
										  opacity:opac
										},splitspeed);

						setTimeout(function() {animateCreatorIn()},(splitspeed*1000 * delayer) + (speed*1000)+500);

   }


/******************************
		-	PLAY IN ANIMATION	-
	********************************/

	function stopAnimationInPreview() {
		var nextcaption = jQuery('#preview_caption_animateme');
		punchgs.TweenLite.killTweensOf(nextcaption,false);
		if (nextcaption.data("timer")) clearTimeout(nextcaption.data('timer'));
		if (nextcaption.data("timera")) clearTimeout(nextcaption.data('timera'));
	}

