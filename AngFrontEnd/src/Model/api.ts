import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Book } from './book.model'; 

@Injectable({
  providedIn: 'root'
})
export class ApiService {

  private baseUrl = 'https://localhost:7108/api'; 

  constructor(private http: HttpClient) { }

  // GET: Hämta alla böcker
  getAllBooks(): Observable<Book[]> {
    return this.http.get<Book[]>(`${this.baseUrl}/books`); 
  }

  // GET: Hämta bok via ID
  getBookById(id: number): Observable<Book> {
    return this.http.get<Book>(`${this.baseUrl}/book/${id}`); 
  }

  // GET: Hämta böcker via namn
  getBooksByName(name: string): Observable<Book[]> {
    return this.http.get<Book[]>(`${this.baseUrl}/book/${name}`); 
  }

  // POST: Lägg till en ny bok
  addBook(book: Book): Observable<Book> {
    return this.http.post<Book>(`${this.baseUrl}/book`, book); 
  }

  // PUT: Uppdatera en bok
  updateBook(id: number, book: Book): Observable<Book> {
    return this.http.put<Book>(`${this.baseUrl}/book/${id}`, book); 
  }

  // DELETE: Ta bort en bok
  deleteBook(id: number): Observable<void> {
    return this.http.delete<void>(`${this.baseUrl}/book/${id}`); 
  }
}
