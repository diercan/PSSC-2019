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
/******/ 	return __webpack_require__(__webpack_require__.s = 157);
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

/***/ 157:
/***/ (function(module, exports, __webpack_require__) {

Object.defineProperty(exports, "__esModule", { value: true });
__webpack_require__(158);
var FullCalendar = __webpack_require__(1);
/* Kazakh (UTF-8) initialisation for the jQuery UI date picker plugin. */
/* Written by Dmitriy Karasyov (dmitriy.karasyov@gmail.com). */
FullCalendar.datepickerLocale('kk', 'kk', {
    closeText: "Жабу",
    prevText: "&#x3C;Алдыңғы",
    nextText: "Келесі&#x3E;",
    currentText: "Бүгін",
    monthNames: ["Қаңтар", "Ақпан", "Наурыз", "Сәуір", "Мамыр", "Маусым",
        "Шілде", "Тамыз", "Қыркүйек", "Қазан", "Қараша", "Желтоқсан"],
    monthNamesShort: ["Қаң", "Ақп", "Нау", "Сәу", "Мам", "Мау",
        "Шіл", "Там", "Қыр", "Қаз", "Қар", "Жел"],
    dayNames: ["Жексенбі", "Дүйсенбі", "Сейсенбі", "Сәрсенбі", "Бейсенбі", "Жұма", "Сенбі"],
    dayNamesShort: ["жкс", "дсн", "ссн", "срс", "бсн", "жма", "снб"],
    dayNamesMin: ["Жк", "Дс", "Сс", "Ср", "Бс", "Жм", "Сн"],
    weekHeader: "Не",
    dateFormat: "dd.mm.yy",
    firstDay: 1,
    isRTL: false,
    showMonthAfterYear: false,
    yearSuffix: ""
});
FullCalendar.locale("kk", {
    buttonText: {
        month: "Ай",
        week: "Апта",
        day: "Күн",
        list: "Күн тәртібі"
    },
    allDayText: "Күні бойы",
    eventLimitText: function (n) {
        return "+ тағы " + n;
    },
    noEventsMessage: "Көрсету үшін оқиғалар жоқ"
});


/***/ }),

/***/ 158:
/***/ (function(module, exports, __webpack_require__) {

//! moment.js locale configuration
;
(function (global, factory) {
     true ? factory(__webpack_require__(0)) :
        typeof define === 'function' && define.amd ? define(['../moment'], factory) :
            factory(global.moment);
}(this, (function (moment) {
    
    var suffixes = {
        0: '-ші',
        1: '-ші',
        2: '-ші',
        3: '-ші',
        4: '-ші',
        5: '-ші',
        6: '-шы',
        7: '-ші',
        8: '-ші',
        9: '-шы',
        10: '-шы',
        20: '-шы',
        30: '-шы',
        40: '-шы',
        50: '-ші',
        60: '-шы',
        70: '-ші',
        80: '-ші',
        90: '-шы',
        100: '-ші'
    };
    var kk = moment.defineLocale('kk', {
        months: 'қаңтар_ақпан_наурыз_сәуір_мамыр_маусым_шілде_тамыз_қыркүйек_қазан_қараша_желтоқсан'.split('_'),
        monthsShort: 'қаң_ақп_нау_сәу_мам_мау_шіл_там_қыр_қаз_қар_жел'.split('_'),
        weekdays: 'жексенбі_дүйсенбі_сейсенбі_сәрсенбі_бейсенбі_жұма_сенбі'.split('_'),
        weekdaysShort: 'жек_дүй_сей_сәр_бей_жұм_сен'.split('_'),
        weekdaysMin: 'жк_дй_сй_ср_бй_жм_сн'.split('_'),
        longDateFormat: {
            LT: 'HH:mm',
            LTS: 'HH:mm:ss',
            L: 'DD.MM.YYYY',
            LL: 'D MMMM YYYY',
            LLL: 'D MMMM YYYY HH:mm',
            LLLL: 'dddd, D MMMM YYYY HH:mm'
        },
        calendar: {
            sameDay: '[Бүгін сағат] LT',
            nextDay: '[Ертең сағат] LT',
            nextWeek: 'dddd [сағат] LT',
            lastDay: '[Кеше сағат] LT',
            lastWeek: '[Өткен аптаның] dddd [сағат] LT',
            sameElse: 'L'
        },
        relativeTime: {
            future: '%s ішінде',
            past: '%s бұрын',
            s: 'бірнеше секунд',
            ss: '%d секунд',
            m: 'бір минут',
            mm: '%d минут',
            h: 'бір сағат',
            hh: '%d сағат',
            d: 'бір күн',
            dd: '%d күн',
            M: 'бір ай',
            MM: '%d ай',
            y: 'бір жыл',
            yy: '%d жыл'
        },
        dayOfMonthOrdinalParse: /\d{1,2}-(ші|шы)/,
        ordinal: function (number) {
            var a = number % 10, b = number >= 100 ? 100 : null;
            return number + (suffixes[number] || suffixes[a] || suffixes[b]);
        },
        week: {
            dow: 1,
            doy: 7 // The week that contains Jan 7th is the first week of the year.
        }
    });
    return kk;
})));


/***/ })

/******/ });
});
//# sourceMappingURL=kk.js.map