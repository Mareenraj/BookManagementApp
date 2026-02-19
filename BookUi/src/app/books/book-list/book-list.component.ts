import { Component, inject, OnInit, ChangeDetectorRef } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';
import { FormsModule } from '@angular/forms';
import { BookApiService } from '../book-api.service';
import { Book } from '../book.model';

@Component({
  standalone: true,
  selector: 'app-books-list',
  imports: [CommonModule, RouterModule, FormsModule],
  templateUrl: './book-list.component.html',
})
export class BooksListComponent implements OnInit {
  private api = inject(BookApiService);
  private cdr = inject(ChangeDetectorRef);

  books: Book[] = [];
  loading = true;
  error: string | null = null;
  searchQuery = '';

  // Delete modal
  showDeleteModal = false;
  bookToDelete: Book | null = null;
  deleting = false;

  // Toast
  toast: { message: string; type: 'success' | 'error' } | null = null;
  private toastTimer: ReturnType<typeof setTimeout> | null = null;

  get filteredBooks(): Book[] {
    if (!this.searchQuery.trim()) return this.books;
    const q = this.searchQuery.toLowerCase().trim();
    return this.books.filter(
      b => b.title.toLowerCase().includes(q) ||
        b.author.toLowerCase().includes(q) ||
        b.isbn.includes(q)
    );
  }

  ngOnInit(): void {
    this.loadBooks();
  }

  loadBooks(): void {
    this.loading = true;
    this.error = null;
    this.api.getAll().subscribe({
      next: (data) => {
        this.books = data;
        this.loading = false;
        this.cdr.markForCheck();
      },
      error: () => {
        this.error = 'Could not load books. Make sure the backend is running on http://localhost:5282';
        this.loading = false;
        this.cdr.markForCheck();
      },
    });
  }

  // Delete
  confirmDelete(book: Book): void {
    this.bookToDelete = book;
    this.showDeleteModal = true;
  }

  cancelDelete(): void {
    this.showDeleteModal = false;
    this.bookToDelete = null;
  }

  executeDelete(): void {
    if (!this.bookToDelete) return;
    this.deleting = true;
    this.api.deleteBookById(this.bookToDelete.id).subscribe({
      next: () => {
        this.showToast(`"${this.bookToDelete!.title}" deleted`, 'success');
        this.showDeleteModal = false;
        this.bookToDelete = null;
        this.deleting = false;
        this.cdr.markForCheck();
        this.loadBooks();
      },
      error: () => {
        this.showToast('Delete failed. Try again.', 'error');
        this.deleting = false;
        this.cdr.markForCheck();
      },
    });
  }

  showToast(message: string, type: 'success' | 'error'): void {
    if (this.toastTimer) clearTimeout(this.toastTimer);
    this.toast = { message, type };
    this.toastTimer = setTimeout(() => {
      this.toast = null;
      this.cdr.markForCheck();
    }, 3000);
  }
}
