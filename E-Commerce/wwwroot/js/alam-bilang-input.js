;(function ( $ ) {
    $.fn.alamBiLangInput = function( options ) {

        var elems = this;
        var pDiv = null; 
        var ajxReq=null;


        var opt = $.extend({ 
            tl:'ar',
            parentDivClass:'.form-group'
        }, options );
        
        elems.each(function () {
            var elem = $(this);
            var langAttr = elem.attr('data-lang'); 
            if(langAttr){
                opt.tl = langAttr;
            }
            pDiv = elem.parents(opt.parentDivClass);
            elem.addClass( "txt-alam-lang-input" ); 
            pDiv.attr('data-alam-bilang-enable',1);
            pDiv.attr('data-alam-bilang-lang',opt.tl);
            elem.before('<span class="btn-alam-lang-input" title="click to disable">'+opt.tl+'</span>');
            
            

        });
         
        //toggle enable disable
        $("body")
            .off("click", ".btn-alam-lang-input")
            .on("click", ".btn-alam-lang-input", function (e) {
            pDiv = $(this).parents(opt.parentDivClass);
            var enableVal = pDiv.attr('data-alam-bilang-enable')==1?0:1; 
            pDiv.attr('data-alam-bilang-enable',enableVal);
            pDiv.find('.txt-alam-lang-input').focus();
            $(this).attr('title',enableVal==1?'click to disable':'click to enable');
        });
 
        var keys = {
            "`": "ذ",
            // "0": "۰",
            // "1": "۱",
            // "2": "۲",
            // "3": "۳",
            // "4": "٤",
            // "5": "۵",
            // "6": "٦",
            // "7": "۷",
            // "8": "۸",
            // "9": "۹",
            // "0": "۰",
            "q": "ض",
            "w": "ص",
            "e": "ث",
            "r": "ق",
            "t": "ف",
            "y": "غ",
            "u": "ع",
            "i": "ه",
            "o": "خ",
            "p": "ح",
            "[": "ج",
            "]": "د",
            "a": "ش",
            "s": "س",
            "d": "ي",
            "f": "ب",
            "g": "ل",
            "h": "ا",
            "j": "ت",
            "k": "ن",
            "l": "م",
            "\\": "ك",
            "\'": "ط",
            "z": "ئ",
            "x": "ء",
            "c": "ؤ",
            "v": "ر",
            "b": "لا",
            "n": "ى",
            "m": "ة",
            ",": "و",
            ".": "ز",
            "\/": "ظ",
            "~": " ّ",
            "Q": "َ",
            "W": "ً",
            "E": "ُ",
            "R": "ٌ",
            "T": "لإ",
            "Y": "إ",
            "U": "‘",
            "I": "÷",
            "O": "×",
            "P": "؛",
            "A": "ِ",
            "S": "ٍ",
            "G": "لأ",
            "H": "أ",
            "J": "ـ",
            "K": "،",
            "L": "/",
            "Z": "~",
            "X": "ْ",
            "B": "لآ",
            "N": "آ",
            "M": "’",
            "?": "؟",
            };
        var rKeys = swap(keys); 

        //keyup event of arabic inputs
        $('[data-alam-bilang-lang="ar"] .txt-alam-lang-input').keyup(function(e){
            pDiv = $(this).parents(opt.parentDivClass);
            if(pDiv.attr('data-alam-bilang-enable')==1){
                var src = $(this).val(); 
                var tStr = '';
                for (var i = 0; i < src.length; i++) {
                    var sChr = src.charAt(i);   
                    var tChr = keys.hasOwnProperty(sChr)?keys[sChr]:sChr;
                    tStr += tChr; 
                }
                $(this).val(tStr);
            }
        });
 
        //keyup event of english inputs
        $('[data-alam-bilang-lang="en"] .txt-alam-lang-input').keyup(function(e){
            pDiv = $(this).parents(opt.parentDivClass);
            if(pDiv.attr('data-alam-bilang-enable')==1){
                var src = $(this).val(); 
                var tStr = '';
                for (var i = 0; i < src.length; i++) {
                    var sChr = src.charAt(i);   
                    var tChr = rKeys.hasOwnProperty(sChr)?rKeys[sChr]:sChr;
                    tStr += tChr; 
                }
                $(this).val(tStr);
            }
        });

        //swap object indexes with values
        function swap(json){
            var ret = {};
            for(var key in json){
                ret[json[key]] = key;
            }
            return ret;
        }
 
    };
}( jQuery ));