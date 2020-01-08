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
/******/ 	return __webpack_require__(__webpack_require__.s = 143);
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

/***/ 143:
/***/ (function(module, exports, __webpack_require__) {

Object.defineProperty(exports, "__esModule", { value: true });
__webpack_require__(144);
var FullCalendar = __webpack_require__(1);
/* Croatian i18n for the jQuery UI date picker plugin. */
/* Written by Vjekoslav Nesek. */
FullCalendar.datepickerLocale('hr', 'hr', {
    closeText: "Zatvori",
    prevText: "&#x3C;",
    nextText: "&#x3E;",
    currentText: "Danas",
    monthNames: ["Siječanj", "Veljača", "Ožujak", "Travanj", "Svibanj", "Lipanj",
        "Srpanj", "Kolovoz", "Rujan", "Listopad", "Studeni", "Prosinac"],
    monthNamesShort: ["Sij", "Velj", "Ožu", "Tra", "Svi", "Lip",
        "Srp", "Kol", "Ruj", "Lis", "Stu", "Pro"],
    dayNames: ["Nedjelja", "Ponedjeljak", "Utorak", "Srijeda", "Četvrtak", "Petak", "Subota"],
    dayNamesShort: ["Ned", "Pon", "Uto", "Sri", "Čet", "Pet", "Sub"],
    dayNamesMin: ["Ne", "Po", "Ut", "Sr", "Če", "Pe", "Su"],
    weekHeader: "Tje",
    dateFormat: "dd.mm.yy.",
    firstDay: 1,
    isRTL: false,
    showMonthAfterYear: false,
    yearSuffix: ""
});
FullCalendar.locale("hr", {
    buttonText: {
        prev: "Prijašnji",
        next: "Sljedeći",
        month: "Mjesec",
        week: "Tjedan",
        day: "Dan",
        list: "Raspored"
    },
    allDayText: "Cijeli dan",
    eventLimitText: function (n) {
        return "+ još " + n;
    },
    noEventsMessage: "Nema događaja za prikaz"
});


/***/ }),

/***/ 144:
/***/ (function(module, exports, __webpack_require__) {

//! moment.js locale configuration
;
(function (global, factory) {
     true ? factory(__webpack_require__(0)) :
        typeof define === 'function' && define.amd ? define(['../moment'], factory) :
            factory(global.moment);
}(this, (function (moment) {
    
    function translate(number, withoutSuffix, key) {
        var result = number + ' ';
        switch (key) {
            case 'ss':
                if (number === 1) {
                    result += 'sekunda';
                }
                else if (number === 2 || number === 3 || number === 4) {
                    result += 'sekunde';
                }
                else {
                    result += 'sekundi';
                }
                return result;
            case 'm':
                return withoutSuffix ? 'jedna minuta' : 'jedne minute';
            case 'mm':
                if (number === 1) {
                    result += 'minuta';
                }
                else if (number === 2 || number === 3 || number === 4) {
                    result += 'minute';
                }
                else {
                    result += 'minuta';
                }
                return result;
            case 'h':
                return withoutSuffix ? 'jedan sat' : 'jednog sata';
            case 'hh':
                if (number === 1) {
                    result += 'sat';
                }
                else if (number === 2 || number === 3 || number === 4) {
                    result += 'sata';
                }
                else {
                    result += 'sati';
                }
                return result;
            case 'dd':
                if (number === 1) {
                    result += 'dan';
                }
                else {
                    result += 'dana';
                }
                return result;
            case 'MM':
                if (number === 1) {
                    result += 'mjesec';
                }
                else if (number === 2 || number === 3 || number === 4) {
                    result += 'mjeseca';
                }
                else {
                    result += 'mjeseci';
                }
                return result;
            case 'yy':
                if (number === 1) {
                    result += 'godina';
                }
                else if (number === 2 || number === 3 || number === 4) {
                    result += 'godine';
                }
                else {
                    result += 'godina';
                }
                return result;
        }
    }
    var hr = moment.defineLocale('hr', {
        months: {
            format: 'siječnja_veljače_ožujka_travnja_svibnja_lipnja_srpnja_kolovoza_rujna_listopada_studenoga_prosinca'.split('_'),
            standalone: 'siječanj_veljača_ožujak_travanj_svibanj_lipanj_srpanj_kolovoz_rujan_listopad_studeni_prosinac'.split('_')
        },
        monthsShort: 'sij._velj._ožu._tra._svi._lip._srp._kol._ruj._lis._stu._pro.'.split('_'),
        monthsParseExact: true,
        weekdays: 'nedjelja_ponedjeljak_utorak_srijeda_četvrtak_petak_subota'.split('_'),
        weekdaysShort: 'ned._pon._uto._sri._čet._pet._sub.'.split('_'),
        weekdaysMin: 'ne_po_ut_sr_če_pe_su'.split('_'),
        weekdaysParseExact: true,
        longDateFormat: {
            LT: 'H:mm',
            LTS: 'H:mm:ss',
            L: 'DD.MM.YYYY',
            LL: 'D. MMMM YYYY',
            LLL: 'D. MMMM YYYY H:mm',
            LLLL: 'dddd, D. MMMM YYYY H:mm'
        },
        calendar: {
            sameDay: '[danas u] LT',
            nextDay: '[sutra u] LT',
            nextWeek: function () {
                switch (this.day()) {
                    case 0:
                        return '[u] [nedjelju] [u] LT';
                    case 3:
                        return '[u] [srijedu] [u] LT';
                    case 6:
                        return '[u] [subotu] [u] LT';
                    case 1:
                    case 2:
                    case 4:
                    case 5:
                        return '[u] dddd [u] LT';
                }
            },
            lastDay: '[jučer u] LT',
            lastWeek: function () {
                switch (this.day()) {
                    case 0:
                    case 3:
                        return '[prošlu] dddd [u] LT';
                    case 6:
                        return '[prošle] [subote] [u] LT';
                    case 1:
                    case 2:
                    case 4:
                    case 5:
                        return '[prošli] dddd [u] LT';
                }
            },
            sameElse: 'L'
        },
        relativeTime: {
            future: 'za %s',
            past: 'prije %s',
            s: 'par sekundi',
            ss: translate,
            m: translate,
            mm: translate,
            h: translate,
            hh: translate,
            d: 'dan',
            dd: translate,
            M: 'mjesec',
            MM: translate,
            y: 'godinu',
            yy: translate
        },
        dayOfMonthOrdinalParse: /\d{1,2}\./,
        ordinal: '%d.',
        week: {
            dow: 1,
            doy: 7 // The week that contains Jan 7th is the first week of the year.
        }
    });
    return hr;
})));


/***/ })

/******/ });
});
//# sourceMappingURL=hr.js.map