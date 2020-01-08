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
/******/ 	return __webpack_require__(__webpack_require__.s = 105);
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

/***/ 105:
/***/ (function(module, exports, __webpack_require__) {

Object.defineProperty(exports, "__esModule", { value: true });
__webpack_require__(106);
var FullCalendar = __webpack_require__(1);
/* Greek (el) initialisation for the jQuery UI date picker plugin. */
/* Written by Alex Cicovic (http://www.alexcicovic.com) */
FullCalendar.datepickerLocale('el', 'el', {
    closeText: "Κλείσιμο",
    prevText: "Προηγούμενος",
    nextText: "Επόμενος",
    currentText: "Σήμερα",
    monthNames: ["Ιανουάριος", "Φεβρουάριος", "Μάρτιος", "Απρίλιος", "Μάιος", "Ιούνιος",
        "Ιούλιος", "Αύγουστος", "Σεπτέμβριος", "Οκτώβριος", "Νοέμβριος", "Δεκέμβριος"],
    monthNamesShort: ["Ιαν", "Φεβ", "Μαρ", "Απρ", "Μαι", "Ιουν",
        "Ιουλ", "Αυγ", "Σεπ", "Οκτ", "Νοε", "Δεκ"],
    dayNames: ["Κυριακή", "Δευτέρα", "Τρίτη", "Τετάρτη", "Πέμπτη", "Παρασκευή", "Σάββατο"],
    dayNamesShort: ["Κυρ", "Δευ", "Τρι", "Τετ", "Πεμ", "Παρ", "Σαβ"],
    dayNamesMin: ["Κυ", "Δε", "Τρ", "Τε", "Πε", "Πα", "Σα"],
    weekHeader: "Εβδ",
    dateFormat: "dd/mm/yy",
    firstDay: 1,
    isRTL: false,
    showMonthAfterYear: false,
    yearSuffix: ""
});
FullCalendar.locale("el", {
    buttonText: {
        month: "Μήνας",
        week: "Εβδομάδα",
        day: "Ημέρα",
        list: "Ατζέντα"
    },
    allDayText: "Ολοήμερο",
    eventLimitText: "περισσότερα",
    noEventsMessage: "Δεν υπάρχουν γεγονότα για να εμφανιστεί"
});


/***/ }),

/***/ 106:
/***/ (function(module, exports, __webpack_require__) {

//! moment.js locale configuration
;
(function (global, factory) {
     true ? factory(__webpack_require__(0)) :
        typeof define === 'function' && define.amd ? define(['../moment'], factory) :
            factory(global.moment);
}(this, (function (moment) {
    
    function isFunction(input) {
        return input instanceof Function || Object.prototype.toString.call(input) === '[object Function]';
    }
    var el = moment.defineLocale('el', {
        monthsNominativeEl: 'Ιανουάριος_Φεβρουάριος_Μάρτιος_Απρίλιος_Μάιος_Ιούνιος_Ιούλιος_Αύγουστος_Σεπτέμβριος_Οκτώβριος_Νοέμβριος_Δεκέμβριος'.split('_'),
        monthsGenitiveEl: 'Ιανουαρίου_Φεβρουαρίου_Μαρτίου_Απριλίου_Μαΐου_Ιουνίου_Ιουλίου_Αυγούστου_Σεπτεμβρίου_Οκτωβρίου_Νοεμβρίου_Δεκεμβρίου'.split('_'),
        months: function (momentToFormat, format) {
            if (!momentToFormat) {
                return this._monthsNominativeEl;
            }
            else if (typeof format === 'string' && /D/.test(format.substring(0, format.indexOf('MMMM')))) { // if there is a day number before 'MMMM'
                return this._monthsGenitiveEl[momentToFormat.month()];
            }
            else {
                return this._monthsNominativeEl[momentToFormat.month()];
            }
        },
        monthsShort: 'Ιαν_Φεβ_Μαρ_Απρ_Μαϊ_Ιουν_Ιουλ_Αυγ_Σεπ_Οκτ_Νοε_Δεκ'.split('_'),
        weekdays: 'Κυριακή_Δευτέρα_Τρίτη_Τετάρτη_Πέμπτη_Παρασκευή_Σάββατο'.split('_'),
        weekdaysShort: 'Κυρ_Δευ_Τρι_Τετ_Πεμ_Παρ_Σαβ'.split('_'),
        weekdaysMin: 'Κυ_Δε_Τρ_Τε_Πε_Πα_Σα'.split('_'),
        meridiem: function (hours, minutes, isLower) {
            if (hours > 11) {
                return isLower ? 'μμ' : 'ΜΜ';
            }
            else {
                return isLower ? 'πμ' : 'ΠΜ';
            }
        },
        isPM: function (input) {
            return ((input + '').toLowerCase()[0] === 'μ');
        },
        meridiemParse: /[ΠΜ]\.?Μ?\.?/i,
        longDateFormat: {
            LT: 'h:mm A',
            LTS: 'h:mm:ss A',
            L: 'DD/MM/YYYY',
            LL: 'D MMMM YYYY',
            LLL: 'D MMMM YYYY h:mm A',
            LLLL: 'dddd, D MMMM YYYY h:mm A'
        },
        calendarEl: {
            sameDay: '[Σήμερα {}] LT',
            nextDay: '[Αύριο {}] LT',
            nextWeek: 'dddd [{}] LT',
            lastDay: '[Χθες {}] LT',
            lastWeek: function () {
                switch (this.day()) {
                    case 6:
                        return '[το προηγούμενο] dddd [{}] LT';
                    default:
                        return '[την προηγούμενη] dddd [{}] LT';
                }
            },
            sameElse: 'L'
        },
        calendar: function (key, mom) {
            var output = this._calendarEl[key], hours = mom && mom.hours();
            if (isFunction(output)) {
                output = output.apply(mom);
            }
            return output.replace('{}', (hours % 12 === 1 ? 'στη' : 'στις'));
        },
        relativeTime: {
            future: 'σε %s',
            past: '%s πριν',
            s: 'λίγα δευτερόλεπτα',
            ss: '%d δευτερόλεπτα',
            m: 'ένα λεπτό',
            mm: '%d λεπτά',
            h: 'μία ώρα',
            hh: '%d ώρες',
            d: 'μία μέρα',
            dd: '%d μέρες',
            M: 'ένας μήνας',
            MM: '%d μήνες',
            y: 'ένας χρόνος',
            yy: '%d χρόνια'
        },
        dayOfMonthOrdinalParse: /\d{1,2}η/,
        ordinal: '%dη',
        week: {
            dow: 1,
            doy: 4 // The week that contains Jan 4st is the first week of the year.
        }
    });
    return el;
})));


/***/ })

/******/ });
});
//# sourceMappingURL=el.js.map