import { Component, OnInit } from '@angular/core';
import { ApiService } from '../api';
import { Book } from '../book.model';

@Component({
  selector: 'app-book-list',
  templateUrl: './book-list.component.html',
  styleUrls: ['./book-list.component.css']
})
export class BookListComponent implements OnInit {
  books: Book[] = [];
  selectedBook: Book | null = null;
  newBook: Book = { id: 0, title: '', author: '', yearPublished: 0, genre: '', description: '', isAvailable: true };

  constructor(private apiService: ApiService) { }

  ngOnInit() {
    this.getBooks();
  }

  // Hämta alla böcker
  getBooks(): void {
    this.apiService.getAllBooks().subscribe({
      next: (data: any) => {
        console.log('Fetched books:', data);
        if (data.isSuccess && Array.isArray(data.result)) {
          this.books = data.result; 
        } else {
          console.error('No books found or isSuccess is false');
          this.books = []; 
        }
      },
      error: (error) => {
        console.error('Error fetching books:', error);
        this.books = []; 
      }
    });
  }

  
  selectBook(book: Book) {
    this.selectedBook = { ...book }; 
  }

  // Ta bort en bok
  deleteBook(id: number) {
    this.apiService.deleteBook(id).subscribe(
      () => {
        this.books = this.books.filter(book => book.id !== id); 
      },
      error => {
        console.error('Error deleting book', error);
      }
    );
  }

  // Lägg till en ny bok
  addBook(bookData: Book) {
    this.apiService.addBook(bookData).subscribe(
      (newBook: Book) => {
        this.books.push(newBook); 
        this.newBook = { id: 0, title: '', author: '', yearPublished: 0, genre: '', description: '', isAvailable: true };

        this.getBooks(); 
      },
      error => {
        console.error('Error adding book', error);
      }
    );
  }


  // Uppdatera en befintlig bok
  updateBook(book: Book) {
    this.apiService.updateBook(book.id, book).subscribe(
      (updatedBook: Book) => {
        const index = this.books.findIndex(b => b.id === updatedBook.id);
        if (index !== -1) {
          this.books[index] = updatedBook; 
        }
        this.clearSelection();
        this.getBooks(); 
      },
      error => {
        console.error('Error updating book', error);
      }

    );
  }

  
  clearSelection() {
    this.selectedBook = null;
  }
}
