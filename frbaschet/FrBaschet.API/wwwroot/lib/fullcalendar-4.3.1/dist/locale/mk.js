/*!
 * FullCalendar v0.0.2
 * Docs & License: https://fullcalendar.io/
 * (c) 2018 Adam Shaw
 */
(function webpackUniversalModuleDefinition(root, factory) {
	if(typeof exports === 'object' && typeof module === 'object')
		module.exports = factory(require("moment"), require("fullcalendar"));
	else if(typeof define === 'function' && define.amd)
		define(["moment", "fullcalendar"], factory);
	else if(typeof exports === 'object')
		factory(require("moment"), require("fullcalendar"));
	else
		factory(root["moment"], root["FullCalendar"]);
})(typeof self !== 'undefined' ? self : this, function(__WEBPACK_EXTERNAL_MODULE_0__, __WEBPACK_EXTERNAL_MODULE_1__) {
return /******/ (function(modules) { // webpackBootstrap
/******/ 	// The module cache
/******/ 	var installedModules = {};
/******/
/******/ 	// The require function
/******/ 	function __webpack_require__(moduleId) {
/******/
/******/ 		// Check if module is in cache
/******/ 		if(installedModules[moduleId]) {
/******/ 			return installedModules[moduleId].exports;
/******/ 		}
/******/ 		// Create a new module (and put it into the cache)
/******/ 		var module = installedModules[moduleId] = {
/******/ 			i: moduleId,
/******/ 			l: false,
/******/ 			exports: {}
/******/ 		};
/******/
/******/ 		// Execute the module function
/******/ 		modules[moduleId].call(module.exports, module, module.exports, __webpack_require__);
/******/
/******/ 		// Flag the module as loaded
/******/ 		module.l = true;
/******/
/******/ 		// Return the exports of the module
/******/ 		return module.exports;
/******/ 	}
/******/
/******/
/******/ 	// expose the modules object (__webpack_modules__)
/******/ 	__webpack_require__.m = modules;
/******/
/******/ 	// expose the module cache
/******/ 	__webpack_require__.c = installedModules;
/******/
/******/ 	// define getter function for harmony exports
/******/ 	__webpack_require__.d = function(exports, name, getter) {
/******/ 		if(!__webpack_require__.o(exports, name)) {
/******/ 			Object.defineProperty(exports, name, {
/******/ 				configurable: false,
/******/ 				enumerable: true,
/******/ 				get: getter
/******/ 			});
/******/ 		}
/******/ 	};
/******/
/******/ 	// getDefaultExport function for compatibility with non-harmony modules
/******/ 	__webpack_require__.n = function(module) {
/******/ 		var getter = module && module.__esModule ?
/******/ 			function getDefault() { return module['default']; } :
/******/ 			function getModuleExports() { return module; };
/******/ 		__webpack_require__.d(getter, 'a', getter);
/******/ 		return getter;
/******/ 	};
/******/
/******/ 	// Object.prototype.hasOwnProperty.call
/******/ 	__webpack_require__.o = function(object, property) { return Object.prototype.hasOwnProperty.call(object, property); };
/******/
/******/ 	// __webpack_public_path__
/******/ 	__webpack_require__.p = "";
/******/
/******/ 	// Load entry module and return exports
/******/ 	return __webpack_require__(__webpack_require__.s = 167);
/******/ })
/************************************************************************/
/******/ ({

/***/ 0:
/***/ (function(module, exports) {

module.exports = __WEBPACK_EXTERNAL_MODULE_0__;

/***/ }),

/***/ 1:
/***/ (function(module, exports) {

module.exports = __WEBPACK_EXTERNAL_MODULE_1__;

/***/ }),

/***/ 167:
/***/ (function(module, exports, __webpack_require__) {

Object.defineProperty(exports, "__esModule", { value: true });
__webpack_require__(168);
var FullCalendar = __webpack_require__(1);
/* Macedonian i18n for the jQuery UI date picker plugin. */
/* Written by Stojce Slavkovski. */
FullCalendar.datepickerLocale('mk', 'mk', {
    closeText: "Затвори",
    prevText: "&#x3C;",
    nextText: "&#x3E;",
    currentText: "Денес",
    monthNames: ["Јануари", "Февруари", "Март", "Април", "Мај", "Јуни",
        "Јули", "Август", "Септември", "Октомври", "Ноември", "Декември"],
    monthNamesShort: ["Јан", "Фев", "Мар", "Апр", "Мај", "Јун",
        "Јул", "Авг", "Сеп", "Окт", "Ное", "Дек"],
    dayNames: ["Недела", "Понеделник", "Вторник", "Среда", "Четврток", "Петок", "Сабота"],
    dayNamesShort: ["Нед", "Пон", "Вто", "Сре", "Чет", "Пет", "Саб"],
    dayNamesMin: ["Не", "По", "Вт", "Ср", "Че", "Пе", "Са"],
    weekHeader: "Сед",
    dateFormat: "dd.mm.yy",
    firstDay: 1,
    isRTL: false,
    showMonthAfterYear: false,
    yearSuffix: ""
});
FullCalendar.locale("mk", {
    buttonText: {
        month: "Месец",
        week: "Недела",
        day: "Ден",
        list: "График"
    },
    allDayText: "Цел ден",
    eventLimitText: function (n) {
        return "+повеќе " + n;
    },
    noEventsMessage: "Нема настани за прикажување"
});


/***/ }),

/***/ 168:
/***/ (function(module, exports, __webpack_require__) {

//! moment.js locale configuration
;
(function (global, factory) {
     true ? factory(__webpack_require__(0)) :
        typeof define === 'function' && define.amd ? define(['../moment'], factory) :
            factory(global.moment);
}(this, (function (moment) {
    
    var mk = moment.defineLocale('mk', {
        months: 'јануари_февруари_март_април_мај_јуни_јули_август_септември_октомври_ноември_декември'.split('_'),
        monthsShort: 'јан_фев_мар_апр_мај_јун_јул_авг_сеп_окт_ное_дек'.split('_'),
        weekdays: 'недела_понеделник_вторник_среда_четврток_петок_сабота'.split('_'),
        weekdaysShort: 'нед_пон_вто_сре_чет_пет_саб'.split('_'),
        weekdaysMin: 'нe_пo_вт_ср_че_пе_сa'.split('_'),
        longDateFormat: {
            LT: 'H:mm',
            LTS: 'H:mm:ss',
            L: 'D.MM.YYYY',
            LL: 'D MMMM YYYY',
            LLL: 'D MMMM YYYY H:mm',
            LLLL: 'dddd, D MMMM YYYY H:mm'
        },
        calendar: {
            sameDay: '[Денес во] LT',
            nextDay: '[Утре во] LT',
            nextWeek: '[Во] dddd [во] LT',
            lastDay: '[Вчера во] LT',
            lastWeek: function () {
                switch (this.day()) {
                    case 0:
                    case 3:
                    case 6:
                        return '[Изминатата] dddd [во] LT';
                    case 1:
                    case 2:
                    case 4:
                    case 5:
                        return '[Изминатиот] dddd [во] LT';
                }
            },
            sameElse: 'L'
        },
        relativeTime: {
            future: 'после %s',
            past: 'пред %s',
            s: 'неколку секунди',
            ss: '%d секунди',
            m: 'минута',
            mm: '%d минути',
            h: 'час',
            hh: '%d часа',
            d: 'ден',
            dd: '%d дена',
            M: 'месец',
            MM: '%d месеци',
            y: 'година',
            yy: '%d години'
        },
        dayOfMonthOrdinalParse: /\d{1,2}-(ев|ен|ти|ви|ри|ми)/,
        ordinal: function (number) {
            var lastDigit = number % 10, last2Digits = number % 100;
            if (number === 0) {
                return number + '-ев';
            }
            else if (last2Digits === 0) {
                return number + '-ен';
            }
            else if (last2Digits > 10 && last2Digits < 20) {
                return number + '-ти';
            }
            else if (lastDigit === 1) {
                return number + '-ви';
            }
            else if (lastDigit === 2) {
                return number + '-ри';
            }
            else if (lastDigit === 7 || lastDigit === 8) {
                return number + '-ми';
            }
            else {
                return number + '-ти';
            }
        },
        week: {
            dow: 1,
            doy: 7 // The week that contains Jan 7th is the first week of the year.
        }
    });
    return mk;
})));


/***/ })

/******/ });
});
//# sourceMappingURL=mk.js.map