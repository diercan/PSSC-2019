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
/******/ 	return __webpack_require__(__webpack_require__.s = 145);
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

/***/ 145:
/***/ (function(module, exports, __webpack_require__) {

Object.defineProperty(exports, "__esModule", { value: true });
__webpack_require__(146);
var FullCalendar = __webpack_require__(1);
/* Hungarian initialisation for the jQuery UI date picker plugin. */
FullCalendar.datepickerLocale('hu', 'hu', {
    closeText: "bezár",
    prevText: "vissza",
    nextText: "előre",
    currentText: "ma",
    monthNames: ["Január", "Február", "Március", "Április", "Május", "Június",
        "Július", "Augusztus", "Szeptember", "Október", "November", "December"],
    monthNamesShort: ["Jan", "Feb", "Már", "Ápr", "Máj", "Jún",
        "Júl", "Aug", "Szep", "Okt", "Nov", "Dec"],
    dayNames: ["Vasárnap", "Hétfő", "Kedd", "Szerda", "Csütörtök", "Péntek", "Szombat"],
    dayNamesShort: ["Vas", "Hét", "Ked", "Sze", "Csü", "Pén", "Szo"],
    dayNamesMin: ["V", "H", "K", "Sze", "Cs", "P", "Szo"],
    weekHeader: "Hét",
    dateFormat: "yy.mm.dd.",
    firstDay: 1,
    isRTL: false,
    showMonthAfterYear: true,
    yearSuffix: ""
});
FullCalendar.locale("hu", {
    buttonText: {
        month: "Hónap",
        week: "Hét",
        day: "Nap",
        list: "Napló"
    },
    allDayText: "Egész nap",
    eventLimitText: "további",
    noEventsMessage: "Nincs megjeleníthető esemény"
});


/***/ }),

/***/ 146:
/***/ (function(module, exports, __webpack_require__) {

//! moment.js locale configuration
;
(function (global, factory) {
     true ? factory(__webpack_require__(0)) :
        typeof define === 'function' && define.amd ? define(['../moment'], factory) :
            factory(global.moment);
}(this, (function (moment) {
    
    var weekEndings = 'vasárnap hétfőn kedden szerdán csütörtökön pénteken szombaton'.split(' ');
    function translate(number, withoutSuffix, key, isFuture) {
        var num = number;
        switch (key) {
            case 's':
                return (isFuture || withoutSuffix) ? 'néhány másodperc' : 'néhány másodperce';
            case 'ss':
                return num + (isFuture || withoutSuffix) ? ' másodperc' : ' másodperce';
            case 'm':
                return 'egy' + (isFuture || withoutSuffix ? ' perc' : ' perce');
            case 'mm':
                return num + (isFuture || withoutSuffix ? ' perc' : ' perce');
            case 'h':
                return 'egy' + (isFuture || withoutSuffix ? ' óra' : ' órája');
            case 'hh':
                return num + (isFuture || withoutSuffix ? ' óra' : ' órája');
            case 'd':
                return 'egy' + (isFuture || withoutSuffix ? ' nap' : ' napja');
            case 'dd':
                return num + (isFuture || withoutSuffix ? ' nap' : ' napja');
            case 'M':
                return 'egy' + (isFuture || withoutSuffix ? ' hónap' : ' hónapja');
            case 'MM':
                return num + (isFuture || withoutSuffix ? ' hónap' : ' hónapja');
            case 'y':
                return 'egy' + (isFuture || withoutSuffix ? ' év' : ' éve');
            case 'yy':
                return num + (isFuture || withoutSuffix ? ' év' : ' éve');
        }
        return '';
    }
    function week(isFuture) {
        return (isFuture ? '' : '[múlt] ') + '[' + weekEndings[this.day()] + '] LT[-kor]';
    }
    var hu = moment.defineLocale('hu', {
        months: 'január_február_március_április_május_június_július_augusztus_szeptember_október_november_december'.split('_'),
        monthsShort: 'jan_feb_márc_ápr_máj_jún_júl_aug_szept_okt_nov_dec'.split('_'),
        weekdays: 'vasárnap_hétfő_kedd_szerda_csütörtök_péntek_szombat'.split('_'),
        weekdaysShort: 'vas_hét_kedd_sze_csüt_pén_szo'.split('_'),
        weekdaysMin: 'v_h_k_sze_cs_p_szo'.split('_'),
        longDateFormat: {
            LT: 'H:mm',
            LTS: 'H:mm:ss',
            L: 'YYYY.MM.DD.',
            LL: 'YYYY. MMMM D.',
            LLL: 'YYYY. MMMM D. H:mm',
            LLLL: 'YYYY. MMMM D., dddd H:mm'
        },
        meridiemParse: /de|du/i,
        isPM: function (input) {
            return input.charAt(1).toLowerCase() === 'u';
        },
        meridiem: function (hours, minutes, isLower) {
            if (hours < 12) {
                return isLower === true ? 'de' : 'DE';
            }
            else {
                return isLower === true ? 'du' : 'DU';
            }
        },
        calendar: {
            sameDay: '[ma] LT[-kor]',
            nextDay: '[holnap] LT[-kor]',
            nextWeek: function () {
                return week.call(this, true);
            },
            lastDay: '[tegnap] LT[-kor]',
            lastWeek: function () {
                return week.call(this, false);
            },
            sameElse: 'L'
        },
        relativeTime: {
            future: '%s múlva',
            past: '%s',
            s: translate,
            ss: translate,
            m: translate,
            mm: translate,
            h: translate,
            hh: translate,
            d: translate,
            dd: translate,
            M: translate,
            MM: translate,
            y: translate,
            yy: translate
        },
        dayOfMonthOrdinalParse: /\d{1,2}\./,
        ordinal: '%d.',
        week: {
            dow: 1,
            doy: 4 // The week that contains Jan 4th is the first week of the year.
        }
    });
    return hu;
})));


/***/ })

/******/ });
});
//# sourceMappingURL=hu.js.map