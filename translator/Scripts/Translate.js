var vowels = "(A((O(M)?)|(o))?)|(En)|(H)|(I)|(M)|(TR)|(U)|(\\:)|(\\|(\\|)?)|(a((A)|(a)|(i)|(u))?)|(e(e)?)|(i)|(o(o)?)|(tR)|(u)"
var consonants = "(B(h)?)|(Ch)|(D((dD)|(h))?)|(G)|(L(lL)?)|(N(nN)?)|(R(rR)?)|(Sh)|(T(h)?)|(Y)|(b(h)?)|(ch)|(d(h)?)|(f)|(g((G)|(h))?)|(h)|(j(h)?)|(k(h)?)|(l)|(m)|(n(Y)?)|(p(h)?)|(q(h)?)|(r)|(s(h)?)|(t(h)?)|(v)|(y)|(z)"
var letter_codes = {
    "~a": "&#2309;",
    "~aa": "&#2310;",
    "~A": "&#2310;",
    "~i": "&#2311;",
    "~e": "&#2319;",
    "~ee": "&#2312;",
    "~I": "&#2312;",
    "~u": "&#2313;",
    "~oo": "&#2314;",
    "~U": "&#2314;",
    "~tR": "&#2315;",
    "~En": "&#2317;",
    "~o": "&#2323;",
    "~ai": "&#2320;",
    "~Ao": "&#2321;",
    "~au": "&#2324;",
    "~TR": "&#2400;",
    "a": "",
    "aa": "&#2366;",
    "A": "&#2366;",
    "A": "&#2366;",
    "i": "&#2367;",
    "e": "&#2375;",
    "ee": "&#2368;",
    "I": "&#2368;",
    "u": "&#2369;",
    "oo": "&#2370;",
    "U": "&#2370;",
    "tR": "&#2371;",
    "En": "&#2373;",
    "o": "&#2379;",
    "ai": "&#2376;",
    "Ao": "&#2377;",
    "au": "&#2380;",
    "TR": "&#2372;",
    "k": "&#2325;",
    "kh": "&#2326;",
    "g": "&#2327;",
    "gh": "&#2328;",
    "G": "&#2329;",
    "ch": "&#2330;",
    "Ch": "&#2331;",
    "j": "&#2332;",
    "jh": "&#2333;",
    "nY": "&#2334;",
    "t": "&#2335;",
    "T": "&#2336;",
    "d": "&#2337;",
    "D": "&#2338;",
    "N": "&#2339;",
    "th": "&#2340;",
    "Th": "&#2341;",
    "dh": "&#2342;",
    "Dh": "&#2343;",
    "n": "&#2344;",
    "NnN": "&#2345;",
    "p": "&#2346;",
    "ph": "&#2347;",
    "b": "&#2348;",
    "bh": "&#2349;",
    "B": "&#2349;",
    "Bh": "&#2349;",
    "m": "&#2350;",
    "y": "&#2351;",
    "r": "&#2352;",
    "R": "&#2353;",
    "l": "&#2354;",
    "L": "&#2355;",
    "LlL": "&#2356;",
    "v": "&#2357;",
    "sh": "&#2358;",
    "Sh": "&#2359;",
    "s": "&#2360;",
    "h": "&#2361;",
    "q": "&#2392;",
    "qh": "&#2393;",
    "gG": "&#2394;",
    "z": "&#2395;",
    "DdD": "&#2396;",
    "RrR": "&#2397;",
    "f": "&#2398;",
    "Y": "&#2399;",
    "AO": "&#2305;",
    "M": "&#2306;",
    "H": "&#2307;",
    ":": "&#2307;",
    "aA": "&#2365;",
    "|": "&#2404;",
    "||": "&#2405;",
    "AOM": "&#2384;",
    "~AO": "&#2305;",
    "~M": "&#2306;",
    "~H": "&#2307;",
    "~:": "&#2307;",
    "~aA": "&#2365;",
    "~|": "&#2404;",
    "~||": "&#2405;",
    "~AOM": "&#2384;",
    "*": "&#2381;"
}

function split_word(word) {
    var syllables = new Array(0);
    var vowel_start_p = true;
    while (word.length) {
        re = new RegExp(vowels);
        var index = word.search(vowels);
        if (index == 0) {  //the vowel's at the start of word
            var matched = re.exec(word)[0]; //what is it?
            if (vowel_start_p) {
                syllables.push(("~" + matched)); //one more to the syllables
            } else {
                syllables.push(matched);
            }
            vowel_start_p = true;
            word = word.substring(matched.length);
        } else {
            re = new RegExp(consonants);
            var index = word.search(consonants);
            if (index == 0) {
                var matched = re.exec(word)[0];
                syllables.push(matched);
                vowel_start_p = false;
                word = word.substring(matched.length);

                //look ahead for virama setting
                var next = word.search(vowels);
                if (next != 0 || word.length == 0)
                    syllables.push('*');
            } else {
                syllables.push(word.charAt(0));
                word = word.substring(1);
            }
        }
    }
    return syllables;
}

function match_code(syllable_mcc) {
    var matched = letter_codes[syllable_mcc];

    if (matched != null) return matched;
    return syllable_mcc;
}

function one_word(word_ow) {
    if (!word_ow) return "";
    var syllables_ow = split_word(word_ow);
    var letters_ow = new Array(0);

    for (var i_ow = 0; i_ow < syllables_ow.length; i_ow++) {
        letters_ow.push(match_code(syllables_ow[i_ow]));
    }
    return letters_ow.join("");
}

function many_words(sentence) {
    var regex = "((" + vowels + ")|(" + consonants + "))+";
    var words = new Array(0);
    while (sentence.length >= 1) {
        re = new RegExp("^``" + regex);
        var match = re.exec(sentence);
        if (match != null) {
            match = match[0];
            words.push("`");
            words.push(one_word(match.substring(2)));
            sentence = sentence.substring(match.length);
        } else {
            re = new RegExp("^`" + regex);
            match = re.exec(sentence);
            if (match != null) {
                match = match[0];
                words.push(match.substring(1));
                sentence = sentence.substring(match.length);
            } else {
                re = new RegExp("^" + regex);
                match = re.exec(sentence);
                if (match != null) {
                    match = match[0];
                    words.push(one_word(match));
                    sentence = sentence.substring(match.length);
                } else {
                    words.push(sentence.charAt(0));
                    sentence = sentence.substring(1);
                }
            }
        }
    }
    return words.join("");
}

function print_many_words(index_pmw) {
    var text_pmw = many_words(document.getElementsByName('txtAreaFrom')[0].value);

    var ans = "";
    while (text_pmw.length) {
        var unicode_chars = /&#[0-9]+;/;
        re = unicode_chars;
        var matche = re.exec(text_pmw);
        if (matche != null) {
            matche = matche[0];
            search = text_pmw.search(unicode_chars);
            ans += text_pmw.substring(0, search);
            ans += String.fromCharCode(matche.match(/[0-9]+/));
            text_pmw = text_pmw.substring(search + matche.length);
        } else {
            ans += text_pmw.substring(0);
            text_pmw = "";
        }
    }

    document.getElementsByName('txtAreaTo')[0].value = ans;

    var html_txt = "";
    for (i = 0; i < ans.length; i++) {
        var unicode_character = ans.charCodeAt(i);
        switch (unicode_character) {
            case 32:
                html_txt += " ";
                break;
            case 10:
            case 13:
                html_txt += "<br/>\n";
                break;
            default:
                html_txt += "&#" + unicode_character + ";";
        }
    }

    document.getElementsByName('txtAreaUnicode')[0].value = html_txt;
}
