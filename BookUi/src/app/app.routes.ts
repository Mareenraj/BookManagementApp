import { Routes } from '@angular/router';

export const routes: Routes = [
  {
    path: '',
    redirectTo: 'books',
    pathMatch: 'full'
  },

  {
    path: 'books',
    loadComponent: () =>
      import('./books/book-list/book-list.component')
        .then(m => m.BooksListComponent)
  },

  {
    path: 'books/new',
    loadComponent: () =>
      import('./books/book-form/book-form.component')
        .then(m => m.BookFormComponent)
  },

  {
    path: 'books/:id/edit',
    loadComponent: () =>
      import('./books/book-form/book-form.component')
        .then(m => m.BookFormComponent)
  }
];
