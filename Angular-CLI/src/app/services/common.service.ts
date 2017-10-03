import { Injectable } from '@angular/core'
import { Http } from '@angular/http'
import { Observable } from 'rxjs/Observable';
import 'rxjs/add/observable/of';
import 'rxjs/add/operator/map';

import * as Global from '../global'
import { ValueObjectPair } from '../models/common.model';

@Injectable()
export class CommonService {
    private baseUrl = './assets/Json/';
    constructor(private httpRef: Http) {
    }

    public _getStates(): Observable<ValueObjectPair[]>{
        
        let url = this.baseUrl + 'IndianStates.json';
        
        return this.httpRef.get(url)
            .map(response => response.json() as ValueObjectPair[]);
    }

    public _getCountries(): Observable<ValueObjectPair[]> {

        let url = this.baseUrl + 'Selectedcountries.json';

        return this.httpRef.get(url)
            .map(response => response.json() as ValueObjectPair[]);
    }

    public _getIndustryTypes(): Observable<ValueObjectPair[]> {

        let url = this.baseUrl + 'IndustryTypes.json';

        return this.httpRef.get(url)
            .map(response => response.json() as ValueObjectPair[]);
    }   
 }


