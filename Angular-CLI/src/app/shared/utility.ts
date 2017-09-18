import { DatePipe } from "@angular/common";

export namespace Utilty {
    export function FormatDate(source: string): string {
        var formatter =  new DatePipe('en-US');
        return formatter.transform(source,'short');
    }
}