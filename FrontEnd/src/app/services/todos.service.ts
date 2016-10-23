import { Injectable } from '@angular/core';
import { Http, Headers } from '@angular/Http';
import { GenericResult, GenericSimpleResult } from '../models/genericResult';
import { Todo } from '../models/todo';
import 'rxjs/add/operator/map';
import { Observable } from 'rxjs/Observable';

@Injectable()
export class TodoService {
    private baseUrl: string = "http://localhost:25001/api/todo";

    constructor(private http: Http) {

    }

    public getById(id: number): Observable<GenericResult<Todo>> {
        return this.http.get(this.baseUrl + '/' + id)
            .map(res => res.json());
    }

    public list(): Observable<GenericResult<Todo[]>> {
        return this.http.get(this.baseUrl)
            .map(res => res.json());
    }

    public post(todo: Todo): Observable<GenericResult<Todo>> {
        let headers = new Headers();
        headers.append('Content-Type', 'application/json');
        return this.http.post(this.baseUrl, JSON.stringify(todo), { headers: headers })
            .map(res => res.json());
    }

    public put(id: number, todo: Todo): Observable<GenericSimpleResult> {
        let headers = new Headers();
        headers.append('Content-Type', 'application/json');
        return this.http.put(this.baseUrl + '/' + id, JSON.stringify(todo), { headers: headers })
            .map(res => res.json());
    }

    public delete(id: number): Observable<GenericSimpleResult> {
        return this.http.delete(this.baseUrl + '/' + id)
            .map(res => res.json());
    }
}