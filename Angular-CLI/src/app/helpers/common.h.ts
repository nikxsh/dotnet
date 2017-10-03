import * as Global from '../global'   
import { DatePipe } from '@angular/common';
import { KeyValuePair } from "../Models/common.m";

export function getPager(totalItems: number, currentPage: number = 1, pageSize: number = Global.PAGE_SIZE) {
    let totalPages = Math.ceil(totalItems / pageSize);
    let startPage: number, endPage: number;

    if (totalPages <= Global.PAGE_LENGTH) {
        startPage = 1;
        endPage = totalPages;
    }
    else {
        let balanceFactor = Math.floor(Global.PAGE_LENGTH / 2);
        if (currentPage == totalPages) {
            startPage = currentPage - (balanceFactor * 2);
            endPage = totalPages;
        }
        else if ((currentPage + balanceFactor) > Global.PAGE_LENGTH) {
            startPage = currentPage - balanceFactor;
            endPage = currentPage + balanceFactor;
        }
        else {
            startPage = 1;
            endPage = Global.PAGE_LENGTH;
        }

        if (startPage < 1) startPage = 1;
        if (endPage > totalPages) {
            startPage = totalPages - (balanceFactor * 2);
            endPage = totalPages;
        }
    }

    let startIndex = (currentPage - 1) * pageSize;
    let endIndex = Math.min(startIndex + pageSize - 1, totalItems - 1);

    let pages: Number[] = [];

    for (var _i = startPage; _i <= endPage; _i++) {
        pages.push(_i);
    }

    return {
        startIndex: startIndex,
        endIndex: endIndex,
        startPage: startPage,
        endPage: endPage,
        pages: pages,
        currentPage: currentPage,
        pageSize: pageSize,
        totalPages: totalPages
    }
}

export function ToTitleCase(input: string) {
    return input.replace(/\w\S*/g, (txt => txt[0].toUpperCase() + txt.substr(1).toLowerCase()));
}


export function getLogoURL(path) {
    return Global.Domain + 'TenantLogos/' + path + '.jpeg';
}

export function getFormattedDate(sourceDate) {     
    var datePipe = new DatePipe("en-IN");
    return datePipe.transform(sourceDate, 'yyyy-MM-dd');
}

export function getFormattedDateTime(sourceDate) {
    var datePipe = new DatePipe("en-IN");
    return datePipe.transform(sourceDate, 'MMM d, y h:mm:ss a');
}

export function getProductTypes() {
    return [
        new KeyValuePair(0, "Goods"),
        new KeyValuePair(1, "Services")
    ];
}

export function getTaxSlabs() {
    return [
        new KeyValuePair(1, "3"),
        new KeyValuePair(2, "5"),
        new KeyValuePair(3, "12"),
        new KeyValuePair(4, "18"),
        new KeyValuePair(5, "28")
    ];
}

export function getUoms() {
    return [
        new KeyValuePair(0, "BAG - Bag"),
        new KeyValuePair(0, "BGS- Bags"),
        new KeyValuePair(0, "BKL- Buckles"),
        new KeyValuePair(0, "BOU- Bou"),
        new KeyValuePair(0, "BOX- Box"),
        new KeyValuePair(0, "BTL- Bottles"),
        new KeyValuePair(0, "BUN- Bunches"),
        new KeyValuePair(0, "CBM- Cubic Meter"),
        new KeyValuePair(0, "CCM- Cubic Centimeter"),
        new KeyValuePair(0, "CIN- Cubic Inches"),
        new KeyValuePair(0, "CMS- Centimeter"),
        new KeyValuePair(0, "CQM- Cubic Meters"),
        new KeyValuePair(0, "CTN- Carton"),
        new KeyValuePair(0, "CRT- Carat"),
        new KeyValuePair(0, "DOZ- Dozen"),
        new KeyValuePair(0, "DRM- Drum"),
        new KeyValuePair(0, "FTS- Feet"),
        new KeyValuePair(0, "GGR- Great Gross"),
        new KeyValuePair(0, "GMS- Grams"),
        new KeyValuePair(0, "GRS- Gross"),
        new KeyValuePair(0, "GYD- Gross Yards"),
        new KeyValuePair(0, "HKS- Hanks"),
        new KeyValuePair(0, "INC- Inches"),
        new KeyValuePair(0, "KGS- Kilograms"),
        new KeyValuePair(0, "KLR- Kiloliter"),
        new KeyValuePair(0, "KME- Kilometers"),
        new KeyValuePair(0, "LBS- Pounds"),
        new KeyValuePair(0, "LOT- Lots"),
        new KeyValuePair(0, "LTR- Liters"),
        new KeyValuePair(0, "MGS- Milli Grams"),
        new KeyValuePair(0, "MLT- Milli Liter"),
        new KeyValuePair(0, "MTR- Meter"),
        new KeyValuePair(0, "MTS- Metric Ton"),
        new KeyValuePair(0, "NOS- Numbers"),
        new KeyValuePair(0, "ODD- Odds"),
        new KeyValuePair(0, "PAC- Packs"),
        new KeyValuePair(0, "PCS- Pieces"),
        new KeyValuePair(0, "PRS- Pairs"),
        new KeyValuePair(0, "QTL- Quintal"),
        new KeyValuePair(0, "ROL- Rolls"),
        new KeyValuePair(0, "SDM- Decameter Square"),
        new KeyValuePair(0, "SET- Sets"),
        new KeyValuePair(0, "SHT- Sheets"),
        new KeyValuePair(0, "SQF- Square Feet"),
        new KeyValuePair(0, "SQI- Square Inches"),
        new KeyValuePair(0, "SQM- Square Meter"),
        new KeyValuePair(0, "SQY- Square Yards"),
        new KeyValuePair(0, "TBS- Tablets"),
        new KeyValuePair(0, "THD- Thousands"),
        new KeyValuePair(0, "TGM- Ten Grams"),
        new KeyValuePair(0, "TOL- Tola"),
        new KeyValuePair(0, "TUB- Tubes"),
        new KeyValuePair(0, "UGS- US Gallons"),
        new KeyValuePair(0, "UNT- Units"),
        new KeyValuePair(0, "VLS- Vials"),
        new KeyValuePair(0, "YDS- Yards"),
        new KeyValuePair(1, "Hr"),
        new KeyValuePair(1, "Day"),
        new KeyValuePair(1, "Year")
    ];
}