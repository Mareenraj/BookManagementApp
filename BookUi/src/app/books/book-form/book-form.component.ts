import { Component, inject, OnInit, ChangeDetectorRef } from '@angular/core';
import { ActivatedRoute, Router, RouterModule } from '@angular/router';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { BookApiService } from '../book-api.service';
import { CreateBookDto } from '../book.model';

@Component({
  standalone: true,
  selector: 'app-book-form',
  imports: [CommonModule, FormsModule, RouterModule],
  templateUrl: './book-form.component.html',
})
export class BookFormComponent implements OnInit {
  private route = inject(ActivatedRoute);
  private router = inject(Router);
  private api = inject(BookApiService);
  private cdr = inject(ChangeDetectorRef);

  id = this.route.snapshot.paramMap.get('id');

  formData: CreateBookDto = {
    title: '',
    author: '',
    isbn: '',
    publicationDate: '',
  };

  loading = false;
  submitting = false;
  serverError: string | null = null;
  touched: Record<string, boolean> = {};

  get isEditMode(): boolean {
    return !!this.id;
  }

  ngOnInit(): void {
    if (this.isEditMode) {
      this.loading = true;
      this.api.getBookById(Number(this.id)).subscribe({
        next: (book) => {
          this.formData = {
            title: book.title,
            author: book.author,
            isbn: book.isbn,
            publicationDate: book.publicationDate,

          };
          this.loading = false;
          this.cdr.markForCheck();
        },
        error: () => {
          this.serverError = 'Failed to load book details.';
          this.loading = false;
          this.cdr.markForCheck();
        },
      });
    }
  }

  markTouched(field: string): void {
    this.touched[field] = true;
  }

  get titleError(): string | null {
    if (!this.touched['title']) return null;
    if (!this.formData.title.trim()) return 'Title is required.';
    return null;
  }

  get authorError(): string | null {
    if (!this.touched['author']) return null;
    if (!this.formData.author.trim()) return 'Author is required.';
    if (this.formData.author.trim().length < 2) return 'Author must be at least 2 characters.';
    return null;
  }

  get isbnError(): string | null {
    if (!this.touched['isbn']) return null;
    if (!this.formData.isbn.trim()) return 'ISBN is required.';
    if (!/^\d{13}$/.test(this.formData.isbn.trim())) return 'ISBN must be exactly 13 digits.';
    return null;
  }

  get dateError(): string | null {
    if (!this.touched['publicationDate']) return null;
    if (!this.formData.publicationDate) return 'Publication date is required.';
    return null;
  }

  get isValid(): boolean {
    return (
      !!this.formData.title.trim() &&
      this.formData.author.trim().length >= 2 &&
      /^\d{13}$/.test(this.formData.isbn.trim()) &&
      !!this.formData.publicationDate
    );
  }

  onSubmit(): void {
    this.touched = { title: true, author: true, isbn: true, publicationDate: true };
    if (!this.isValid) return;

    this.submitting = true;
    this.serverError = null;

    const payload: CreateBookDto = {
      ...this.formData,
      title: this.formData.title.trim(),
      author: this.formData.author.trim(),
      isbn: this.formData.isbn.trim(),
    };

    const onSuccess = () => {
      this.router.navigate(['/books']);
    };

    const onError = (err: { error?: { message?: string; errors?: Record<string, string[]> } }) => {
      this.submitting = false;
      if (err.error?.message) {
        this.serverError = err.error.message;
      } else if (err.error?.errors) {
        const messages = Object.values(err.error.errors).flat();
        this.serverError = messages.join(' ');
      } else {
        this.serverError = 'Something went wrong. Please try again.';
      }
      this.cdr.markForCheck();
    };

    if (this.isEditMode) {
      this.api.updateBook(Number(this.id), payload).subscribe({ next: onSuccess, error: onError });
    } else {
      this.api.createBook(payload).subscribe({ next: onSuccess, error: onError });
    }
  }
}
