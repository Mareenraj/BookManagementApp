import { Injectable } from "@angular/core";
import { HttpClient } from "@angular/common/http";
import {Book, CreateBookDto, UpdateBookDto} from "./book.model";
import { Observable } from "rxjs/internal/Observable";

@Injectable({providedIn: 'root'})
export class BookApiService{

  private readonly baseUrl = 'http://localhost:5282/api/Book';

  constructor(private http: HttpClient) {

  }

  getAll(): Observable<Book[]>{
  return this.http.get<Book[]>(`${this.baseUrl}`);
}

  getBookById(id: number){
    return this.http.get<Book>(`${this.baseUrl}/${id}`);
  }

  deleteBookById(id: number){
    return this.http.delete<void>(`${this.baseUrl}/${id}`);
  }

  createBook(payload: CreateBookDto){
    return this.http.post<Book>(this.baseUrl, payload);
  }

  updateBook(id: number, payload: UpdateBookDto){
    return this.http.put<void>(`${this.baseUrl}/${id}`, payload);
  }
}
