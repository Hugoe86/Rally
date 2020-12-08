(function () {
    var triggerBttn_empleado = document.getElementById('trigger-overlay-empleado'),
		overlay = document.querySelector('div.overlay'),
		closeBttn_empleado = overlay.querySelector('button.overlay-close');
    var triggerBttn_aval = document.getElementById('trigger-overlay-aval'),
		overlay_aval = document.querySelector('div.overlay.aval'),
		closeBttn_aval = overlay_aval.querySelector('button.overlay-close.aval');

    transEndEventNames = {
        'WebkitTransition': 'webkitTransitionEnd',
        'MozTransition': 'transitionend',
        'OTransition': 'oTransitionEnd',
        'msTransition': 'MSTransitionEnd',
        'transition': 'transitionend'
    },
    transEndEventName = transEndEventNames[Modernizr.prefixed('transition')],
    support = { transitions: Modernizr.csstransitions };

    function toggleOverlay() {
        if (classie.has(overlay, 'open')) {
            classie.remove(overlay, 'open');
            classie.add(overlay, 'close');
            var onEndTransitionFn = function (ev) {
                if (support.transitions) {
                    if (ev.propertyName !== 'visibility') return;
                    this.removeEventListener(transEndEventName, onEndTransitionFn);
                }
                classie.remove(overlay, 'close');
            };
            if (support.transitions) {
                overlay.addEventListener(transEndEventName, onEndTransitionFn);
            }
            else {
                onEndTransitionFn();
            }
        }
        else if (!classie.has(overlay, 'close')) {
            classie.add(overlay, 'open');
            $('.input-search-jahn').focus();
        }
    }

    function toggleOverlay_aval() {
        if (classie.has(overlay_aval, 'open')) {
            classie.remove(overlay_aval, 'open');
            classie.add(overlay_aval, 'close');
            var onEndTransitionFn = function (ev) {
                if (support.transitions) {
                    if (ev.propertyName !== 'visibility') return;
                    this.removeEventListener(transEndEventName, onEndTransitionFn);
                }
                classie.remove(overlay_aval, 'close');
            };
            if (support.transitions) {
                overlay_aval.addEventListener(transEndEventName, onEndTransitionFn);
            }
            else {
                onEndTransitionFn();
            }
        }
        else if (!classie.has(overlay_aval, 'close')) {
            classie.add(overlay_aval, 'open');
            $('.input-search-jahn').focus();
        }
    }

    triggerBttn_empleado.addEventListener('click', toggleOverlay);
    triggerBttn_aval.addEventListener('click', toggleOverlay_aval);
    closeBttn_aval.addEventListener('click', toggleOverlay_aval);
    closeBttn_empleado.addEventListener('click', toggleOverlay);
})();