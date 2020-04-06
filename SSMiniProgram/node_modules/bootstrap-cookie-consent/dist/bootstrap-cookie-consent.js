(function (global, factory) {
	typeof exports === 'object' && typeof module !== 'undefined' ? module.exports = factory() :
	typeof define === 'function' && define.amd ? define(factory) :
	(global.BootstrapCookieConsent = factory());
}(this, (function () { 'use strict';

	function createCommonjsModule(fn, module) {
		return module = { exports: {} }, fn(module, module.exports), module.exports;
	}

	var js_cookie = createCommonjsModule(function (module, exports) {
	(function (factory) {
		var registeredInModuleLoader = false;
		{
			module.exports = factory();
			registeredInModuleLoader = true;
		}
		if (!registeredInModuleLoader) {
			var OldCookies = window.Cookies;
			var api = window.Cookies = factory();
			api.noConflict = function () {
				window.Cookies = OldCookies;
				return api;
			};
		}
	}(function () {
		function extend () {
			var i = 0;
			var result = {};
			for (; i < arguments.length; i++) {
				var attributes = arguments[ i ];
				for (var key in attributes) {
					result[key] = attributes[key];
				}
			}
			return result;
		}

		function init (converter) {
			function api (key, value, attributes) {
				var result;
				if (typeof document === 'undefined') {
					return;
				}

				// Write

				if (arguments.length > 1) {
					attributes = extend({
						path: '/'
					}, api.defaults, attributes);

					if (typeof attributes.expires === 'number') {
						var expires = new Date();
						expires.setMilliseconds(expires.getMilliseconds() + attributes.expires * 864e+5);
						attributes.expires = expires;
					}

					// We're using "expires" because "max-age" is not supported by IE
					attributes.expires = attributes.expires ? attributes.expires.toUTCString() : '';

					try {
						result = JSON.stringify(value);
						if (/^[\{\[]/.test(result)) {
							value = result;
						}
					} catch (e) {}

					if (!converter.write) {
						value = encodeURIComponent(String(value))
							.replace(/%(23|24|26|2B|3A|3C|3E|3D|2F|3F|40|5B|5D|5E|60|7B|7D|7C)/g, decodeURIComponent);
					} else {
						value = converter.write(value, key);
					}

					key = encodeURIComponent(String(key));
					key = key.replace(/%(23|24|26|2B|5E|60|7C)/g, decodeURIComponent);
					key = key.replace(/[\(\)]/g, escape);

					var stringifiedAttributes = '';

					for (var attributeName in attributes) {
						if (!attributes[attributeName]) {
							continue;
						}
						stringifiedAttributes += '; ' + attributeName;
						if (attributes[attributeName] === true) {
							continue;
						}
						stringifiedAttributes += '=' + attributes[attributeName];
					}
					return (document.cookie = key + '=' + value + stringifiedAttributes);
				}

				// Read

				if (!key) {
					result = {};
				}

				// To prevent the for loop in the first place assign an empty array
				// in case there are no cookies at all. Also prevents odd result when
				// calling "get()"
				var cookies = document.cookie ? document.cookie.split('; ') : [];
				var rdecode = /(%[0-9A-Z]{2})+/g;
				var i = 0;

				for (; i < cookies.length; i++) {
					var parts = cookies[i].split('=');
					var cookie = parts.slice(1).join('=');

					if (!this.json && cookie.charAt(0) === '"') {
						cookie = cookie.slice(1, -1);
					}

					try {
						var name = parts[0].replace(rdecode, decodeURIComponent);
						cookie = converter.read ?
							converter.read(cookie, name) : converter(cookie, name) ||
							cookie.replace(rdecode, decodeURIComponent);

						if (this.json) {
							try {
								cookie = JSON.parse(cookie);
							} catch (e) {}
						}

						if (key === name) {
							result = cookie;
							break;
						}

						if (!key) {
							result[name] = cookie;
						}
					} catch (e) {}
				}

				return result;
			}

			api.set = api;
			api.get = function (key) {
				return api.call(api, key);
			};
			api.getJSON = function () {
				return api.apply({
					json: true
				}, [].slice.call(arguments));
			};
			api.defaults = {};

			api.remove = function (key, attributes) {
				api(key, '', extend(attributes, {
					expires: -1
				}));
			};

			api.withConverter = init;

			return api;
		}

		return init(function () {});
	}));
	});

	// Copyright Joyent, Inc. and other Node contributors.

	var R = typeof Reflect === 'object' ? Reflect : null;
	var ReflectApply = R && typeof R.apply === 'function'
	  ? R.apply
	  : function ReflectApply(target, receiver, args) {
	    return Function.prototype.apply.call(target, receiver, args);
	  };

	var ReflectOwnKeys;
	if (R && typeof R.ownKeys === 'function') {
	  ReflectOwnKeys = R.ownKeys;
	} else if (Object.getOwnPropertySymbols) {
	  ReflectOwnKeys = function ReflectOwnKeys(target) {
	    return Object.getOwnPropertyNames(target)
	      .concat(Object.getOwnPropertySymbols(target));
	  };
	} else {
	  ReflectOwnKeys = function ReflectOwnKeys(target) {
	    return Object.getOwnPropertyNames(target);
	  };
	}

	function ProcessEmitWarning(warning) {
	  if (console && console.warn) console.warn(warning);
	}

	var NumberIsNaN = Number.isNaN || function NumberIsNaN(value) {
	  return value !== value;
	};

	function EventEmitter() {
	  EventEmitter.init.call(this);
	}
	var events = EventEmitter;

	// Backwards-compat with node 0.10.x
	EventEmitter.EventEmitter = EventEmitter;

	EventEmitter.prototype._events = undefined;
	EventEmitter.prototype._eventsCount = 0;
	EventEmitter.prototype._maxListeners = undefined;

	// By default EventEmitters will print a warning if more than 10 listeners are
	// added to it. This is a useful default which helps finding memory leaks.
	var defaultMaxListeners = 10;

	Object.defineProperty(EventEmitter, 'defaultMaxListeners', {
	  enumerable: true,
	  get: function() {
	    return defaultMaxListeners;
	  },
	  set: function(arg) {
	    if (typeof arg !== 'number' || arg < 0 || NumberIsNaN(arg)) {
	      throw new RangeError('The value of "defaultMaxListeners" is out of range. It must be a non-negative number. Received ' + arg + '.');
	    }
	    defaultMaxListeners = arg;
	  }
	});

	EventEmitter.init = function() {

	  if (this._events === undefined ||
	      this._events === Object.getPrototypeOf(this)._events) {
	    this._events = Object.create(null);
	    this._eventsCount = 0;
	  }

	  this._maxListeners = this._maxListeners || undefined;
	};

	// Obviously not all Emitters should be limited to 10. This function allows
	// that to be increased. Set to zero for unlimited.
	EventEmitter.prototype.setMaxListeners = function setMaxListeners(n) {
	  if (typeof n !== 'number' || n < 0 || NumberIsNaN(n)) {
	    throw new RangeError('The value of "n" is out of range. It must be a non-negative number. Received ' + n + '.');
	  }
	  this._maxListeners = n;
	  return this;
	};

	function $getMaxListeners(that) {
	  if (that._maxListeners === undefined)
	    return EventEmitter.defaultMaxListeners;
	  return that._maxListeners;
	}

	EventEmitter.prototype.getMaxListeners = function getMaxListeners() {
	  return $getMaxListeners(this);
	};

	EventEmitter.prototype.emit = function emit(type) {
	  var args = [];
	  for (var i = 1; i < arguments.length; i++) args.push(arguments[i]);
	  var doError = (type === 'error');

	  var events = this._events;
	  if (events !== undefined)
	    doError = (doError && events.error === undefined);
	  else if (!doError)
	    return false;

	  // If there is no 'error' event listener then throw.
	  if (doError) {
	    var er;
	    if (args.length > 0)
	      er = args[0];
	    if (er instanceof Error) {
	      // Note: The comments on the `throw` lines are intentional, they show
	      // up in Node's output if this results in an unhandled exception.
	      throw er; // Unhandled 'error' event
	    }
	    // At least give some kind of context to the user
	    var err = new Error('Unhandled error.' + (er ? ' (' + er.message + ')' : ''));
	    err.context = er;
	    throw err; // Unhandled 'error' event
	  }

	  var handler = events[type];

	  if (handler === undefined)
	    return false;

	  if (typeof handler === 'function') {
	    ReflectApply(handler, this, args);
	  } else {
	    var len = handler.length;
	    var listeners = arrayClone(handler, len);
	    for (var i = 0; i < len; ++i)
	      ReflectApply(listeners[i], this, args);
	  }

	  return true;
	};

	function _addListener(target, type, listener, prepend) {
	  var m;
	  var events;
	  var existing;

	  if (typeof listener !== 'function') {
	    throw new TypeError('The "listener" argument must be of type Function. Received type ' + typeof listener);
	  }

	  events = target._events;
	  if (events === undefined) {
	    events = target._events = Object.create(null);
	    target._eventsCount = 0;
	  } else {
	    // To avoid recursion in the case that type === "newListener"! Before
	    // adding it to the listeners, first emit "newListener".
	    if (events.newListener !== undefined) {
	      target.emit('newListener', type,
	                  listener.listener ? listener.listener : listener);

	      // Re-assign `events` because a newListener handler could have caused the
	      // this._events to be assigned to a new object
	      events = target._events;
	    }
	    existing = events[type];
	  }

	  if (existing === undefined) {
	    // Optimize the case of one listener. Don't need the extra array object.
	    existing = events[type] = listener;
	    ++target._eventsCount;
	  } else {
	    if (typeof existing === 'function') {
	      // Adding the second element, need to change to array.
	      existing = events[type] =
	        prepend ? [listener, existing] : [existing, listener];
	      // If we've already got an array, just append.
	    } else if (prepend) {
	      existing.unshift(listener);
	    } else {
	      existing.push(listener);
	    }

	    // Check for listener leak
	    m = $getMaxListeners(target);
	    if (m > 0 && existing.length > m && !existing.warned) {
	      existing.warned = true;
	      // No error code for this since it is a Warning
	      // eslint-disable-next-line no-restricted-syntax
	      var w = new Error('Possible EventEmitter memory leak detected. ' +
	                          existing.length + ' ' + String(type) + ' listeners ' +
	                          'added. Use emitter.setMaxListeners() to ' +
	                          'increase limit');
	      w.name = 'MaxListenersExceededWarning';
	      w.emitter = target;
	      w.type = type;
	      w.count = existing.length;
	      ProcessEmitWarning(w);
	    }
	  }

	  return target;
	}

	EventEmitter.prototype.addListener = function addListener(type, listener) {
	  return _addListener(this, type, listener, false);
	};

	EventEmitter.prototype.on = EventEmitter.prototype.addListener;

	EventEmitter.prototype.prependListener =
	    function prependListener(type, listener) {
	      return _addListener(this, type, listener, true);
	    };

	function onceWrapper() {
	  var args = [];
	  for (var i = 0; i < arguments.length; i++) args.push(arguments[i]);
	  if (!this.fired) {
	    this.target.removeListener(this.type, this.wrapFn);
	    this.fired = true;
	    ReflectApply(this.listener, this.target, args);
	  }
	}

	function _onceWrap(target, type, listener) {
	  var state = { fired: false, wrapFn: undefined, target: target, type: type, listener: listener };
	  var wrapped = onceWrapper.bind(state);
	  wrapped.listener = listener;
	  state.wrapFn = wrapped;
	  return wrapped;
	}

	EventEmitter.prototype.once = function once(type, listener) {
	  if (typeof listener !== 'function') {
	    throw new TypeError('The "listener" argument must be of type Function. Received type ' + typeof listener);
	  }
	  this.on(type, _onceWrap(this, type, listener));
	  return this;
	};

	EventEmitter.prototype.prependOnceListener =
	    function prependOnceListener(type, listener) {
	      if (typeof listener !== 'function') {
	        throw new TypeError('The "listener" argument must be of type Function. Received type ' + typeof listener);
	      }
	      this.prependListener(type, _onceWrap(this, type, listener));
	      return this;
	    };

	// Emits a 'removeListener' event if and only if the listener was removed.
	EventEmitter.prototype.removeListener =
	    function removeListener(type, listener) {
	      var list, events, position, i, originalListener;

	      if (typeof listener !== 'function') {
	        throw new TypeError('The "listener" argument must be of type Function. Received type ' + typeof listener);
	      }

	      events = this._events;
	      if (events === undefined)
	        return this;

	      list = events[type];
	      if (list === undefined)
	        return this;

	      if (list === listener || list.listener === listener) {
	        if (--this._eventsCount === 0)
	          this._events = Object.create(null);
	        else {
	          delete events[type];
	          if (events.removeListener)
	            this.emit('removeListener', type, list.listener || listener);
	        }
	      } else if (typeof list !== 'function') {
	        position = -1;

	        for (i = list.length - 1; i >= 0; i--) {
	          if (list[i] === listener || list[i].listener === listener) {
	            originalListener = list[i].listener;
	            position = i;
	            break;
	          }
	        }

	        if (position < 0)
	          return this;

	        if (position === 0)
	          list.shift();
	        else {
	          spliceOne(list, position);
	        }

	        if (list.length === 1)
	          events[type] = list[0];

	        if (events.removeListener !== undefined)
	          this.emit('removeListener', type, originalListener || listener);
	      }

	      return this;
	    };

	EventEmitter.prototype.off = EventEmitter.prototype.removeListener;

	EventEmitter.prototype.removeAllListeners =
	    function removeAllListeners(type) {
	      var listeners, events, i;

	      events = this._events;
	      if (events === undefined)
	        return this;

	      // not listening for removeListener, no need to emit
	      if (events.removeListener === undefined) {
	        if (arguments.length === 0) {
	          this._events = Object.create(null);
	          this._eventsCount = 0;
	        } else if (events[type] !== undefined) {
	          if (--this._eventsCount === 0)
	            this._events = Object.create(null);
	          else
	            delete events[type];
	        }
	        return this;
	      }

	      // emit removeListener for all listeners on all events
	      if (arguments.length === 0) {
	        var keys = Object.keys(events);
	        var key;
	        for (i = 0; i < keys.length; ++i) {
	          key = keys[i];
	          if (key === 'removeListener') continue;
	          this.removeAllListeners(key);
	        }
	        this.removeAllListeners('removeListener');
	        this._events = Object.create(null);
	        this._eventsCount = 0;
	        return this;
	      }

	      listeners = events[type];

	      if (typeof listeners === 'function') {
	        this.removeListener(type, listeners);
	      } else if (listeners !== undefined) {
	        // LIFO order
	        for (i = listeners.length - 1; i >= 0; i--) {
	          this.removeListener(type, listeners[i]);
	        }
	      }

	      return this;
	    };

	function _listeners(target, type, unwrap) {
	  var events = target._events;

	  if (events === undefined)
	    return [];

	  var evlistener = events[type];
	  if (evlistener === undefined)
	    return [];

	  if (typeof evlistener === 'function')
	    return unwrap ? [evlistener.listener || evlistener] : [evlistener];

	  return unwrap ?
	    unwrapListeners(evlistener) : arrayClone(evlistener, evlistener.length);
	}

	EventEmitter.prototype.listeners = function listeners(type) {
	  return _listeners(this, type, true);
	};

	EventEmitter.prototype.rawListeners = function rawListeners(type) {
	  return _listeners(this, type, false);
	};

	EventEmitter.listenerCount = function(emitter, type) {
	  if (typeof emitter.listenerCount === 'function') {
	    return emitter.listenerCount(type);
	  } else {
	    return listenerCount.call(emitter, type);
	  }
	};

	EventEmitter.prototype.listenerCount = listenerCount;
	function listenerCount(type) {
	  var events = this._events;

	  if (events !== undefined) {
	    var evlistener = events[type];

	    if (typeof evlistener === 'function') {
	      return 1;
	    } else if (evlistener !== undefined) {
	      return evlistener.length;
	    }
	  }

	  return 0;
	}

	EventEmitter.prototype.eventNames = function eventNames() {
	  return this._eventsCount > 0 ? ReflectOwnKeys(this._events) : [];
	};

	function arrayClone(arr, n) {
	  var copy = new Array(n);
	  for (var i = 0; i < n; ++i)
	    copy[i] = arr[i];
	  return copy;
	}

	function spliceOne(list, index) {
	  for (; index + 1 < list.length; index++)
	    list[index] = list[index + 1];
	  list.pop();
	}

	function unwrapListeners(arr) {
	  var ret = new Array(arr.length);
	  for (var i = 0; i < ret.length; ++i) {
	    ret[i] = arr[i].listener || arr[i];
	  }
	  return ret;
	}

	class CookieConsentApi extends events
	{
	    constructor(conf = {})
	    {
	        super();

	        const defaultConf = {
	            cookieName: 'cookie_consent_settings',
	            cookieDuration: 365,
	            cookieDomain: null,
	            services: []
	        };

	        // Merge default config with user config
	        this._conf = Object.assign({}, defaultConf, conf);

	        // Console log errors if conf is invalid
	        this._validateConf();

	        // Replace dom elements based on cookie consent value
	        this._updateView();
	    }

	    reset()
	    {
	        js_cookie.remove(this._conf.cookieName, {domain: this._conf.cookieDomain});
	        this._updateView();
	        this.emit('clear');
	    }

	    acceptAll()
	    {
	        let cookieServices = {};
	        this._conf.services.forEach(service => cookieServices[service] = true);

	        this._setCookieServices(cookieServices);
	        this._updateView();
	        this.emit('allConfigured');
	    }

	    accept(service)
	    {
	        let cookieServices = this._getCookieServices();
	        cookieServices[service] = true;

	        this._setCookieServices(cookieServices);
	        this._updateView();
	        this.emit('accept', service);

	        if (this.isAllConfigured()) this.emit('allConfigured');
	    }

	    refuse(service)
	    {
	        let cookieServices = this._getCookieServices();
	        cookieServices[service] = false;

	        this._setCookieServices(cookieServices);
	        this._updateView();
	        this.emit('refuse', service);

	        if (this.isAllConfigured()) this.emit('allConfigured');
	    }

	    isAllConfigured()
	    {
	        const cookieServices = this._getCookieServices();
	        let isAllConfigured = true;

	        this._conf.services.forEach(service => {
	            if (cookieServices[service] === undefined) isAllConfigured = false;
	        });

	        return isAllConfigured;
	    }

	    isConfigured(service)
	    {
	        const cookieServices = this._getCookieServices();
	        return cookieServices[service] !== undefined;
	    }
	    
	    isAccepted(service)
	    {
	        const cookieServices = this._getCookieServices();
	        return cookieServices[service] !== undefined && cookieServices[service] === true;
	    }

	    isRefused(service)
	    {
	        const cookieServices = this._getCookieServices();
	        return cookieServices[service] !== undefined && cookieServices[service] === false;
	    }

	    getServices()
	    {
	        return this._conf.services;
	    }

	    _getCookieServices()
	    {
	        return js_cookie.getJSON(this._conf.cookieName) || {};
	    }

	    _setCookieServices(cookieServices)
	    {
	        js_cookie.set(this._conf.cookieName, cookieServices, {
	            duration: this._conf.cookieDuration,
	            domain: this._conf.cookieDomain
	        });
	    }

	    _validateConf()
	    {
	        // Services
	        if (!Array.isArray(this._conf.services)) {
	            console.error('CCM: Services is not an array');
	        }
	        else {
	            this._conf.services.forEach(service => {
	                if (/^[a-zA-Z0-9]+$/.test(service) === false) {
	                    console.error('CCM: "' + service + '" is not a valid service name, only alphanumeric allowed');
	                }
	            });
	        }
	    }

	    _updateView()
	    {
	        const cookieServices = this._getCookieServices();

	        this._conf.services.forEach(service => {

	            const elems = document.querySelectorAll('[data-cookie-consent="' + service + '"]');

	            // Service is accepted in cookie
	            if (cookieServices[service] === true) {
	                elems.forEach(elem => {
	                    if (!elem.getAttribute('data-ccm-fallback')) {
	                        elem.setAttribute('data-ccm-fallback', elem.innerHTML);
	                    }
	                    var match = elem.innerHTML.match(new RegExp('\<\!--if-consent(.*?)endif--\>', 's'));
	                    if (match && match.length == 2) {
	                        elem.innerHTML = match[1];
	                        this._executeScripts(elem);
	                    }
	                });
	            }

	            // Service is refused in cookie
	            else {
	                elems.forEach(elem => {
	                    let fallbackContent = elem.getAttribute('data-ccm-fallback');
	                    if (fallbackContent) {
	                        elem.innerHTML = fallbackContent;
	                        this._executeScripts(elem);
	                    }
	                });
	            }

	        });
	    }

	    _executeScripts(elem)
	    {
	        const scriptsDom = elem.querySelectorAll('script');
	        scriptsDom.forEach(function(scriptDom) {
	            let script = document.createElement('script');
	            script.innerHTML = scriptDom.innerHTML;
	            scriptDom.remove();
	            elem.append(script);
	        });
	    }
	}

	/**
	 * @version 0.0.1
	 * @author Robin D https://www.robin-d.fr/
	 * @license The MIT License (MIT)
	 */

	var bsn = require("bootstrap.native");

	class BootstrapCookieConsent {
	  constructor(conf = {}) {
	    const defaultConf = {
	      'show_selector': '.cc',
	      'accept_id': 'accept-cookie',
	      'banner_text': 'Ce site utilise des services tiers susceptible de vous déposer un cookie. Pour une navigation optimale, acceptez-vous de les utiliser sur ce site ?',
	      'button_text': 'J\'accepte',
	      'banner_id': 'cookies-banner',
	      'link_more_info': '#',
	      'more_info_label': 'En savoir plus',
	      'details_title': 'Vie Privée',
	      'details_text': 'Vous pouvez accepter ou refuster l\'utilisation sur ce site de certains services.',
	      'checkbox_class': 'switch-sm',
	      'method': 1,
	      // 0: native boostrap, 1:jquery bootstrap or (string) 'bsn'
	      services: [],
	      services_descr: {}
	    };
	    this._conf = Object.assign({}, defaultConf, conf);
	    this.cookieConsent = new CookieConsentApi(this._conf);

	    if (this.cookieConsent.isAllConfigured() == false) {
	      this._showBanner();
	    }

	    this.cookieConsent.on('allConfigured', () => {
	      this._hideBanner();
	    });
	    document.querySelectorAll(this._conf.show_selector).forEach(item => {
	      item.addEventListener('click', () => {
	        if (document.getElementById('cookie-modal') === null) {
	          this._createDetails();
	        }

	        if (this._conf.method === 1) {
	          $('#cookie-modal').modal('toggle');
	        } else {
	          let modal = document.getElementById('cookie-modal');
	          let iModal = typeof this._conf.method == 'string' ? new eval(this._conf.method + '.Modal(modal)') : new Modal(modal);
	          iModal.show();
	        }
	      });
	    });
	  }

	  _hideBanner() {
	    let bannerElement = document.getElementById(this._conf.banner_id);

	    if (bannerElement !== null) {
	      bannerElement.parentNode.removeChild(bannerElement);
	    }
	  }

	  _showBanner() {
	    let parser = new DOMParser();
	    let banner = document.createElement('div');
	    banner.setAttribute('id', this._conf.banner_id);
	    banner.setAttribute('class', 'alert alert-warning text-center');
	    banner.innerHTML = this._conf.banner_text + ' <button class="btn btn-success btn-gradient btn-sm" id="' + this._conf.accept_id + '">' + this._conf.button_text + '</button> <a href="' + this._conf.link_more_info + '">' + this._conf.more_info_label + '</a>';
	    document.body.insertBefore(banner, document.body.firstChild);
	    document.getElementById(this._conf.accept_id).addEventListener('click', () => {
	      this.cookieConsent.acceptAll();

	      this._hideBanner();
	    });
	  }

	  _createDetails() {
	    let modal = '';
	    modal += '<div class="modal-dialog" role="document"><div class="modal-content"><div class="modal-header">';
	    modal += '<h5 class="modal-title">' + this._conf.details_title + '</h5>';
	    modal += '<button type="button" class="close" data-dismiss="modal" aria-label="Fermer"><span aria-hidden="true">&times;</span></button></div>';
	    modal += '<div class="modal-body">';
	    modal += '<p>' + this._conf.details_text + ' <a href="' + this._conf.link_more_info + '">' + this._conf.more_info_label + '</a></p>';
	    modal += '<table class="table"><thead class="thead-light"><tr><th scope="col" class="col-10">Service</th><th scope="col" class="col-2">Accepter</th></tr></thead>';
	    modal += '<tbody>';

	    this._conf.services.forEach(elem => {
	      modal += '<tr><td>' + elem + (elem in this._conf.services_descr ? '<br><small>' + this._conf.services_descr[elem] + '</small>' : '') + '</td><td class="text-center">';
	      modal += '<span class="switch"><input type="checkbox" class="' + this._conf.checkbox_class + '" id="switch-' + elem + '"';
	      modal += this.cookieConsent.isAccepted(elem) ? ' checked' : '';
	      modal += '><label for="switch-' + elem + '"></label></span>';
	      modal += '</td></tr>';
	    });

	    modal += '</tbody></table></div></div></div6';
	    let modalElement = document.createElement('div');
	    modalElement.setAttribute('id', 'cookie-modal');
	    modalElement.setAttribute('class', 'modal fade');
	    modalElement.innerHTML = modal;
	    document.body.appendChild(modalElement);

	    this._conf.services.forEach(elem => {
	      document.getElementById('switch-' + elem).addEventListener('change', () => {
	        if (document.getElementById('switch-' + elem).checked) {
	          this.cookieConsent.accept(elem);
	        } else {
	          this.cookieConsent.refuse(elem);
	        }
	      });
	    });
	  }

	}

	return BootstrapCookieConsent;

})));
